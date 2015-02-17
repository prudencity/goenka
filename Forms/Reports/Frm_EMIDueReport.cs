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
    public partial class Frm_EMIDueReport : Form
    {
        public Frm_EMIDueReport()
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
            clsGeneral.ExcelExport(dg_head, "EMI Due Report From " + dtpStart.Value.ToString("dd/MM/yyyy") + " To " + dtpEnd.Value.ToString("dd/MM/yyyy") + "");
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
            if (Rights == false)
            {
                return;
            }
            DtFull = null;
            string SS = "Select L_Id As Id From LoanSanction";
            DataTable DtId = ObjData.GetDataTable(SS);
            for (int i = 0; i < DtId.Rows.Count; i++)
            {
                int Id = DtId.Rows[i]["Id"].ToString().IntParse();
                SS = "Sp_Calculation " + Id;
                DataTable DtData = ObjData.GetDataTable(SS);

                SS = "Select F_Date From AccountForeClosure Where F_SId = " + Id + "";
                DataTable DD = ObjData.GetDataTable(SS);
                if (DD.Rows.Count > 0)
                {
                    DateTime Dtp = DateTime.Parse(DD.Rows[0]["F_Date"].ToString());
                    DtData = DtData.Delete("[Date Received] > '" + Dtp.ToShortDateString() + "'");
                    DtData.AcceptChanges();
                }


                if (DtFull == null)
                {
                    DtFull = DtData.Clone();
                }
                DtFull.Merge(DtData);
            }

            DtFull.DefaultView.RowFilter = "Iden = 1 And [Date Received] >= '" + dtpStart.Value.ToShortDateString() + "' And [Date Received] <= '" + dtpEnd.Value.ToShortDateString() + "'";
            DtFull.DefaultView.Sort = "[Date Received] Asc , [EMI No] Asc";
            DtFull.Columns.Add("Cheque No");
            DtFull.Columns.Add("Cheque Amount");
            LoadData(DtFull);
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
                dg_head.Columns["SID"].Visible = false;
                dg_head.Columns["Iden"].Visible = false;
                dg_head.Columns["Date Received"].Visible = false;
                dg_head.Columns["EMI No"].Width = 60;
                dg_head.Columns["Date"].Width = 80;
                dg_head.Columns["Loan No"].Width = 80;

                dg_head.Columns["Party Name"].Width = 150;
                dg_head.Columns["ContactDetails"].Width = 150;

                for (int i = 0; i < dg_head.Rows.Count; i++)
                {
                    int SId = dg_head.Rows[i].Cells["SID"].EditedFormattedValue.ToString().IntParse();
                    int EMINO = dg_head.Rows[i].Cells["EMI No"].EditedFormattedValue.ToString().IntParse();
                    if (SId > 0 && EMINO > 0)
                    {
                        string SS = "Select [S_ChequeDate],[S_ChequeNo] From [ChequeDetails] Where [S_Id] = " + SId + " And [S_SNo] = " + EMINO + "";
                        DataTable DtDetails = ObjData.GetDataTable(SS);
                        if (DtDetails.Rows.Count > 0)
                        {
                            dg_head.Rows[i].Cells["Cheque No"].Value = DtDetails.Rows[0]["S_ChequeNo"].ToString();
                            dg_head.Rows[i].Cells["Cheque Amount"].Value = DtDetails.Rows[0]["S_ChequeDate"].ToString();
                        }
                    }
                }

                dg_head.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dg_head.Columns["Party Name"].Width = 200;
                dg_head.Columns["ContactDetails"].Width = 200;
                dg_head.Columns["Particular"].Width = 100;
                dg_head.Columns["Agent Name"].Width = 100;
                dg_head.Columns["Address"].Width = 100;

                object ObjTotal = Dt.DefaultView.ToTable().Compute("Sum([Amount])", "");
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
                    int Id = int.Parse(dg_head.SelectedRows[0].Cells["SID"].EditedFormattedValue.ToString());
                    Frm_LoanSanction F = new Frm_LoanSanction();
                    F.Show();
                    F.FillData(Id);
                }
            }
        }

        private void Frm_MonthlyLedger_Load(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DtFull != null)
                {
                    DtFull.DefaultView.RowFilter = "";
                    DtFull.DefaultView.RowFilter = "Iden = 1 And [Date Received] >= '" + dtpStart.Value.ToShortDateString() + "' And [Date Received] <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    DtFull.DefaultView.Sort = "[Date Received] Asc , [EMI No] Asc";

                    if (txtSearch.Text.Trim().Length == 0)
                    {
                        LoadData(DtFull);
                    }
                    else
                    {
                        DtFull.DefaultView.RowFilter = DtFull.DefaultView.RowFilter + " And [Party Name]Like '" + clsGeneral.SQLEncode(txtSearch.Text) + "%'";
                        LoadData(DtFull);
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnSMS_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dg_head.SelectedRows.Count; i++)
                {
                    double Amount = dg_head.SelectedRows[i].Cells["Amount"].EditedFormattedValue.ToString().DoubleParse();
                    string Date = dg_head.SelectedRows[i].Cells["Date"].EditedFormattedValue.ToString();
                    int EMI = dg_head.SelectedRows[i].Cells["EMI No"].EditedFormattedValue.ToString().IntParse();
                    string Contact = dg_head.SelectedRows[i].Cells["ContactDetails"].EditedFormattedValue.ToString();

                    string[] ContactNo = Contact.Split(' ', ',');
                    for (int j = 0; j < ContactNo.Length; j++)
                    {
                        string CNo = ContactNo[j].ToString();
                        if (CNo.Length > 5 )
                        {
                            string Body = "Dear Customer";
                            Body = Body + Environment.NewLine;
                            Body = Body + "      Your " + EMI.FormatOrdinalNumber() + " EMI Rs " + clsGeneral.CurFormat(Amount) + "/- is Due on ";
                            Body = Body + "" + Date + ".Please pay in time to avoid penalty.";
                            Body = Body + Environment.NewLine;
                            Body = Body + "Thanks";
                            Body = Body + Environment.NewLine;
                            Body = Body + clsGeneral.CompanyName + "(" + clsGeneral.ContactNo + ")";
                            clsGeneral.SendSMSMobile(CNo, Body);
                        }
                    }
                }
            }
            catch (Exception Er)
            {
                clsGeneral.ShowErrMsg(Er.Message);
            }
        }

        private void txtAgentName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (DtFull != null)
                {
                    DtFull.DefaultView.RowFilter = "";
                    DtFull.DefaultView.RowFilter = "Iden = 1 And [Date Received] >= '" + dtpStart.Value.ToShortDateString() + "' And [Date Received] <= '" + dtpEnd.Value.ToShortDateString() + "'";
                    DtFull.DefaultView.Sort = "[Date Received] Asc , [EMI No] Asc";

                    if (txtAgentName.Text.Trim().Length == 0)
                    {
                        LoadData(DtFull);
                    }
                    else
                    {
                        DtFull.DefaultView.RowFilter = DtFull.DefaultView.RowFilter + " And [Agent Name]Like '" + clsGeneral.SQLEncode(txtAgentName.Text) + "%'";
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
