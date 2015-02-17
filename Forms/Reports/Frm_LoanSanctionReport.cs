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
    public partial class Frm_LoanSanctionReport : Form
    {
        public Frm_LoanSanctionReport()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();

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
            clsGeneral.ExcelExport(dg_head, "Loan Sanction Report From " + dtpStart.Value.ToString("dd/MM/yyyy") + " To " + dtpEnd.Value.ToString("dd/MM/yyyy") + "");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
            if (Rights == false)
            {
                return;
            }
            string SS = "Select ROW_NUMBER() OVER(ORDER BY FormNo Asc) AS [Sr No],Id,Agent,Office,FormNo As [Sanction No],Convert(Varchar,FormDate,106) As Date, ";
            SS = SS + " PartyName As [Customer Name],PLoanAmount As [Loan Amount], ";
            SS = SS + " CashCheque As [Cash Cheque],LoanType As [Loan Type],LoanPeriod As [No of EMI],EMI From vw_LoanSanction ";
            SS = SS + " Where FormDate >= Convert(DateTime,'" + dtpStart.DateFormat() + "') And FormDate <= Convert(DateTime,'" + dtpEnd.DateFormat() + "') ";
            SS = SS + " Order By FormNo";
            DataTable Dt = ObjData.GetDataTable(SS);
            dg_head.DataSource = Dt;

            dg_head.Columns["Id"].Visible = false;
            dg_head.Columns["Sr No"].Width = 60;

            dg_head.Columns["Sr No"].DefaultCellStyle.ForeColor = Color.White;
            dg_head.Columns["Sr No"].DefaultCellStyle.SelectionForeColor = Color.White;
            dg_head.Columns["Sr No"].DefaultCellStyle.BackColor = Color.Teal;
            dg_head.Columns["Sr No"].DefaultCellStyle.SelectionBackColor = Color.Teal;
            
            dg_head.Columns["Agent"].Width = 80;
            dg_head.Columns["Office"].Width = 80;
            dg_head.Columns["Sanction No"].Width = 80;
            dg_head.Columns["Loan Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg_head.Columns["EMI"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg_head.Columns["Customer Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
                    int Id = int.Parse(dg_head.SelectedRows[0].Cells["Id"].EditedFormattedValue.ToString());
                    Frm_LoanSanction F = new Frm_LoanSanction();
                    F.Show();
                    F.FillData(Id);
                }
            }
        }
    }
}
