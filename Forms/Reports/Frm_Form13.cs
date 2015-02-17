using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Finance_Management_System
{
    public partial class Frm_Form13 : Form
    {
        public Frm_Form13()
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
            clsGeneral.ExcelExport(dg_head, "Form 13 From " + dtpStart.Value.ToString("dd/MM/yyyy") + " To " + dtpEnd.Value.ToString("dd/MM/yyyy") + "");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
            if (Rights == false)
            {
                return;
            }
            string Path = "Finance_Management_System.SQL.Form13.sql";
            Assembly A = Assembly.GetExecutingAssembly();
            StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
            string SS = D.ReadToEnd();

            SS = SS.Replace("%%",dtpEnd.DateFormat());

            DataTable DtId = ObjData.GetDataTable(SS);
            DtId.Delete("[Balance Amount] = 0 ");
            DtId.AcceptChanges();
          
            LoadData(DtId);
        }

        public void LoadData(DataTable Dt)
        {
            try
            {
                //string Sort = "";
                //string order = "";
                //string Column = "";
                //order = (radioAscending.Checked == true) ? "Asc" : "Desc";
                //Column = (radioParty.Checked == true) ? "Party" : "CDate";
                //Sort = Column + " " + order;
                //Dt.DefaultView.Sort = Sort;

                dg_head.DataSource = Dt.DefaultView;

                dg_head.Columns["Id"].Visible = false;

                dg_head.Columns["Sanction Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg_head.Columns["Balance Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                
                dg_head.Columns["Name"].Width = 200;
                dg_head.Columns["Address"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                ////dg_head.Columns["Particular"].Width = 100;
                ////dg_head.Columns["Agent Name"].Width = 100;
                ////dg_head.Columns["Address"].Width = 100;

                object ObjTotal = Dt.DefaultView.ToTable().Compute("Sum([Balance Amount])", "");
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
                    int Id = int.Parse(dg_head.SelectedRows[0].Cells["ID"].EditedFormattedValue.ToString());
                    Frm_LoanSanction F = new Frm_LoanSanction();
                    F.Show();
                    F.FillData(Id);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Print.ToString());
                if (Rights == false)
                {
                    return;
                }
                DataTable Dt = new DataTable();
                Dt.Columns.Add("CompanyName");
                Dt.Columns.Add("Address1");
                Dt.Columns.Add("Address2");
                Dt.Columns.Add("Date");
                Dt.Columns.Add("ContactName");
                Dt.Columns.Add("LicenseNo");
                Dt.Columns.Add("SNo");
                Dt.Columns.Add("Debtor");
                Dt.Columns.Add("AddressDebtor");
                Dt.Columns.Add("LoanAmount");
                Dt.Columns.Add("DateLoan");
                Dt.Columns.Add("DateMaturity");
                Dt.Columns.Add("Balance");
                Dt.Columns.Add("Rate");
                Dt.Columns.Add("Security");
                Dt.Columns.Add("Documents");
                Dt.Columns.Add("Information");

                for (int i = 0; i < dg_head.Rows.Count; i++)
                {
                    string CompanyName = clsGeneral.CompanyName;
                    string Address1 = clsGeneral.Address;
                    string Address2 = "";
                    string Date = "From " +  dtpStart.Value.ToString("dd/MM/yyyy") + " To " + dtpEnd.Value.ToString("dd/MM/yyyy");
                    string ContactName = clsGeneral.ContactName;
                    string LicenseNo = clsGeneral.LicenseNo;
                    string SNo = (i + 1).ToString();
                    string Debtor = dg_head.Rows[i].Cells["Name"].EditedFormattedValue.ToString();
                    string AddressDebtor = dg_head.Rows[i].Cells["Address"].EditedFormattedValue.ToString();
                    string LoanAmount = dg_head.Rows[i].Cells["Sanction Amount"].EditedFormattedValue.ToString();
                    string DateLoan = dg_head.Rows[i].Cells["Loan Date"].EditedFormattedValue.ToString();
                    string DateMaturity = dg_head.Rows[i].Cells["Date Maturity"].EditedFormattedValue.ToString();
                    string Balance = dg_head.Rows[i].Cells["Balance Amount"].EditedFormattedValue.ToString();
                    string Rate = txtInterestRate.Text;
                    string Security = "Cheque";
                    string Documents = "";
                    string Information = "";
                    Dt.Rows.Add(CompanyName, Address1, Address2, Date, ContactName, LicenseNo, SNo, Debtor, AddressDebtor, LoanAmount, DateLoan, DateMaturity,
                                Balance, Rate, Security, Documents, Information);
                }
                Forms.Printing.Form13 Rpt = new Finance_Management_System.Forms.Printing.Form13();
                Rpt.SetDataSource(Dt);
                Rpt.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
                Frm_Preview F = new Frm_Preview(Rpt, Dt);
                F.Show();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }
    }
}
