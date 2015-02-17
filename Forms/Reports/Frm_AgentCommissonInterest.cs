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
    public partial class Frm_AgentCommissonInterest : Form
    {
        public Frm_AgentCommissonInterest()
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
            clsGeneral.ExcelExport(dg_head, this.Name + " From " + dtpStart.Value.ToString("dd/MM/yyyy") + " To " + dtpEnd.Value.ToString("dd/MM/yyyy") + "");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
            if (Rights == false)
            {
                return;
            }
            DtFull = null;

            string SS = "Sp_AgentInterestComission '" + dtpStart.DateFormat() + "','" + dtpEnd.DateFormat() + "'";
            DtFull = ObjData.GetDataTable(SS);
            DtFull.DefaultView.RowFilter = "Iden = 0";

            DtFull.Delete("CDate < '" + dtpStart.Value.ToShortDateString() + "'");
            DtFull.Delete("CDate > '" + dtpEnd.Value.ToShortDateString() + "'");
            DtFull.AcceptChanges();
            LoadData(DtFull);
        }

        public void LoadData(DataTable Dt)
        {
            try
            {
                dg_head.DataSource = Dt.DefaultView;
                dg_head.Columns["CDate"].Visible = false;
                dg_head.Columns["Iden"].Visible = false;
                dg_head.Columns["AgentCode"].Visible = false;
                dg_head.Columns["RecId"].Visible = false;
                dg_head.Columns["LoanId"].Visible = false;
                dg_head.Columns["CommssionRate"].Visible = false;
                dg_head.Columns["Date"].Width = 80;
                dg_head.Columns["AgentName"].Width = 80;
                dg_head.Columns["Amount"].HeaderText = "Commission";
                dg_head.Columns["Particular"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dg_head.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg_head.Columns["Interest"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                object ObjCommssion = Dt.DefaultView.ToTable().Compute("Sum([Amount])", "");
                object ObjInterest = Dt.DefaultView.ToTable().Compute("Sum([Interest])", "");
                lblComm.Text = ObjCommssion.ToString().DoubleParse().ToString().CurrFormat();
                lblInt.Text = ObjInterest.ToString().DoubleParse().ToString().CurrFormat();
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

        private void Frm_MonthlyLedger_Load(object sender, EventArgs e)
        {

        }
        private void txtAgentName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DtFull != null)
                {
                    DtFull.DefaultView.RowFilter = "";
                    DtFull.DefaultView.RowFilter = "Iden = 0";
                    if (txtAgentName.Text.Trim().Length == 0)
                    {
                        LoadData(DtFull);
                    }
                    else
                    {
                        DtFull.DefaultView.RowFilter = DtFull.DefaultView.RowFilter + " And [AgentName]Like '" + clsGeneral.SQLEncode(txtAgentName.Text) + "%'";
                        LoadData(DtFull);
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

      
    }
}
