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
    public partial class Frm_LoanReturnChart : Form
    {
        public Frm_LoanReturnChart()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();
        public int LoanId = 0;
        public string SSQl = "Sp_LoanApplicationChart";
        public string TableName = "LoanApplication";

        public void GetChart()
        {
            try
            {
                string SS = SSQl + " " + LoanId;
                DataTable Dt = ObjData.GetDataTable(SS);
                dg_head.DataSource = Dt;
                dg_head.Columns["EMI No"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dg_head.Columns["EMI No"].DefaultCellStyle.BackColor = Color.Teal;
                dg_head.Columns["EMI No"].DefaultCellStyle.ForeColor = Color.White;
                dg_head.Columns["EMI No"].DefaultCellStyle.SelectionBackColor = Color.Teal;
                dg_head.Columns["EMI No"].DefaultCellStyle.SelectionForeColor = Color.White;
                dg_head.Columns["EMI No"].Width = 80;
                dg_head.Columns["Loan Balance"].Visible = false;
                dg_head.Columns["CDate"].Visible = false;
                dg_head.Columns["EMI"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg_head.Columns["Principal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg_head.Columns["Interest"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg_head.Columns["Loan Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg_head.Columns["Balance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


                Object Obj = Dt.Compute("Sum(Interest)", "");

                lblEMI.Text = "  0.00";
                lblInterest.Text = "  0.00";
                lblLoanAmount.Text = "  0.00";


                SS = "Select * From " + TableName + " Where L_Id = " + LoanId + "";
                Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    lblEMI.Text = "  " + Dt.Rows[0]["L_Period"].ToString();
                    lblInterest.Text = "  " + Obj.ToString().CurrFormat();
                    lblLoanAmount.Text = "  " + Dt.Rows[0]["L_LoanAmount"].ToString();
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
            string SS = "Application";
            if (TableName == "LoanSanction")
            {
                SS = "Sanction";
            }
            string SSQl = "Select P_Name + ' - " + SS + " No ' + Convert(varchar,L_FormNo) As LoanNo, L_Id As Id From " + TableName + " Inner Join PartyMaster On L_PartyId = P_Id Order By L_FormNo";
            DataTable Dtdata = ObjData.GetDataTable(SSQl);
            CboNo.DisplayMember = "LoanNo";
            CboNo.ValueMember = "Id";
            CboNo.DataSource = Dtdata;
            if (CboNo.DataSource != null)
            {
                CboNo.SelectedValue = LoanId;
            }
            GetChart();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
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
            clsGeneral.ExcelExport(dg_head, "Loan Chart of " + CboNo.Text + "");
        }
    }
}
