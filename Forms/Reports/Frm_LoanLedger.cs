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
    public partial class Frm_LoanLedger : Form
    {
        public Frm_LoanLedger()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();
        public int LoanId = 0;
        public string TableName = "LoanSanction";

        public void GetChart()
        {
            try
            {
                double Total = 0;
                string SS = "Sp_Calculation " + LoanId;
                DataTable Dt = ObjData.GetDataTable(SS);
                Dt.DefaultView.RowFilter = "Iden <> 1 And Amount <> 0";
                Dt.DefaultView.Sort = "[Date Received] Asc,Iden Asc, [EMI No] Asc";
                DataTable D = Dt.DefaultView.ToTable();

                SS = "Select F_Date From AccountForeClosure Where F_SId = " + LoanId + "";
                DataTable DD = ObjData.GetDataTable(SS);
                if (DD.Rows.Count > 0)
                {
                    DateTime Dtp = DateTime.Parse(DD.Rows[0]["F_Date"].ToString());
                    D = D.Delete("[Date Received] > '" + Dtp.ToShortDateString() + "'");
                    D.AcceptChanges();
                }

                dg_head.Rows.Clear();
                for (int i = 0; i < D.Rows.Count; i++)
                {
                    int Iden = D.Rows[i]["Iden"].ToString().IntParse();
                    string Particular = D.Rows[i]["Particular"].ToString();
                    string Date = D.Rows[i]["Date"].ToString();
                    string EMI = D.Rows[i]["EMI No"].ToString();
                    double Amount = D.Rows[i]["Amount"].ToString().DoubleParse();
                    dg_head.Rows.Add();
                    dg_head.Rows[i].Cells["ColDate"].Value = Date;
                    dg_head.Rows[i].Cells["ColEMI"].Value = EMI;
                    dg_head.Rows[i].Cells["ColParticular"].Value = Particular;
                    if (Iden == -2 || Iden == 3 || Iden == 5 || Iden == 10 || Iden == 11)
                    {
                        dg_head.Rows[i].Cells["ColDrAmount"].Value = clsGeneral.CurFormat(Amount);
                        Total = Total + Amount;
                    }
                    else
                    {
                        dg_head.Rows[i].Cells["ColCrAmount"].Value = clsGeneral.CurFormat(Amount);
                        Total = Total - Amount;
                    }

                    if (Total > 0)
                    {
                        dg_head.Rows[i].Cells["ColBalance"].Value = clsGeneral.CurFormat(Math.Abs(Total)) + " Dr";
                        dg_head.Rows[i].Cells["ColBalance"].Style.ForeColor = Color.Green;
                    }
                    else if (Total < 0)
                    {
                        dg_head.Rows[i].Cells["ColBalance"].Value = clsGeneral.CurFormat(Math.Abs(Total)) + " Cr";
                        dg_head.Rows[i].Cells["ColBalance"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dg_head.Rows[i].Cells["ColBalance"].Value = "0.00";
                    }
                }

                clsGeneral.DatagridSort(dg_head);
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void Frm_LoanReturnChart_Load(object sender, EventArgs e)
        {
            //string SSQl = "Select P_Name + '-' + Convert(Varchar,L_FormNo) As LoanNo,L_Id As Id From LoanSanction Inner Join PartyMaster On P_Id = L_PartyId Order By L_FormNo";

            string SS = "Sanction";
            string SSQl = "Select P_Name + ' - " + SS + " No ' + Convert(varchar,L_FormNo) As LoanNo, L_Id As Id From " + TableName + " Inner Join PartyMaster On L_PartyId = P_Id Order By (P_Name + '-' + Convert(Varchar,L_FormNo))";
            DataTable Dtdata = ObjData.GetDataTable(SSQl);
            CboNo.DisplayMember = "LoanNo";
            CboNo.ValueMember = "Id";
            CboNo.DataSource = Dtdata;
            CboNo.NextControl = btnShow;
            CboNo.BindData();
            if (CboNo.DataSource != null)
            {
                CboNo.SelectedValue = LoanId;
            }
            GetChart();
        }

        public void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
                if (Rights == false)
                {
                    return;
                }
                if (CboNo.SelectedValue != null)
                {
                    LoanId = int.Parse(CboNo.SelectedValue.ToString());
                    GetChart();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LoanId = 0;
            GetChart();
        }

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
            clsGeneral.ExcelExport(dg_head, "Loan Ledger of " + CboNo.SelectedText + "");
        }

        private void btnCashReceipt_Click(object sender, EventArgs e)
        {
            Frm_PenaltyReceipt F = new Frm_PenaltyReceipt();
            F.Show();
        }
    }
}
