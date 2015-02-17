using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Finance_Management_System
{
    public partial class Frm_InterestReport : Form
    {
        public Frm_InterestReport()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();
        DataTable DtFull;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Print.ToString());
            if (Rights == false)
            {
                return;
            }
            clsGeneral.ExcelExport(dg_head, "Interest Report From " + dtpStart.Value.ToString("dd/MM/yyyy") + " To " + dtpEnd.Value.ToString("dd/MM/yyyy") + "");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
            if (Rights == false)
            {
                return;
            }
            DtFull = null;

            string SS = "SP_InterestReport ";
            DataTable DtData = ObjData.GetDataTable(SS);

            DataTable DtId = DtData.DefaultView.ToTable(true, "LoanId");

            for (int i = 0; i < DtId.Rows.Count; i++)
            {
                int Id = DtId.Rows[i]["LoanId"].ToString().IntParse();

                SS = "Select F_Date From AccountForeClosure Where F_SId = " + Id + "";
                DataTable DD = ObjData.GetDataTable(SS);
                if (DD.Rows.Count > 0)
                {
                    DateTime Dtp = DateTime.Parse(DD.Rows[0]["F_Date"].ToString());
                    DtData = DtData.Delete("[Date Received] > '" + Dtp.ToShortDateString() + "' And LoanId = " + Id + "");
                    DtData.AcceptChanges();
                }
            }
            DtData.Delete("[Date Received] < '" + dtpStart.Value.ToShortDateString() + "'");
            DtData.Delete("[Date Received] > '" + dtpEnd.Value.ToShortDateString() + "'");
            DtData.AcceptChanges();
            LoadData(DtData);
        }

        public void LoadData(DataTable Dt)
        {
            try
            {
                dg_head.DataSource = Dt.DefaultView;
                dg_head.Columns["LoanId"].Visible = false;
                dg_head.Columns["Date Received"].Visible = false;
                dg_head.Columns["Particular"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dg_head.Columns["Interest"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg_head.Columns["Particular"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                object ObjTotal = Dt.DefaultView.ToTable().Compute("Sum([Interest])", "");
                lblTotal.Text = "Rs. " + ObjTotal.ToString().CurrFormat();

            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dg_head.DataSource = null;
        }

        private void dg_head_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (dg_head.SelectedRows.Count == 1)
                {
                    int Id = int.Parse(dg_head.SelectedRows[0].Cells["LoanId"].EditedFormattedValue.ToString());
                    Frm_LoanSanction F = new Frm_LoanSanction();
                    F.Show();
                    F.FillData(Id);
                }
            }
        }

    }
}
