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
    public partial class Frm_AgentHistory : Form
    {
        public Frm_AgentHistory()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();
       

        private void Frm_LoanReturnChart_Load(object sender, EventArgs e)
        {
            string SS = "Select * From AgentMaster Order By AgentName";
            DataTable DtCode = ObjData.GetDataTable(SS);
            txtName.DisplayMember = "AgentName";
            txtName.ValueMember = "AgentId";
            txtName.DataSource = DtCode;
            txtName.NextControl = btnShow;
            txtName.SelectedValue = null; 
            txtName.BindData();
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
                int PartyId = txtName.ValueGet("Please Select Agent");
                if (PartyId > 0)
                {
                    string SS = "SELECT L_Id As Id,Row_Number()Over(Order By L_FormNo) As [S.No.], ";
                    SS = SS + " 'Loan Sanction No : ' + Convert(Varchar,L_FormNo) As [Loan No], ";
                    SS = SS + " Convert(Varchar,L_FormDate,106) As [Loan Date], ";
                    SS = SS + " P_Name As Customer FROM LoanSanction  ";
                    SS = SS + " Inner Join PartyMaster On L_PartyId = P_Id ";
                    SS = SS + " Where L_AgentId = "+PartyId+" Order By L_FormNo  ";
                    DataTable Dt = ObjData.GetDataTable(SS);
                    dg_head.DataSource = Dt;
                    dg_head.Columns["Id"].Visible = false;
                    dg_head.Columns["S.No."].Width = 80;
                    dg_head.Columns["Customer"].Width = 150;
                    dg_head.Columns["Loan No"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dg_head.Columns["S.No."].DefaultCellStyle.BackColor = Color.Teal;
                    dg_head.Columns["S.No."].DefaultCellStyle.SelectionBackColor = Color.Teal;
                    dg_head.Columns["S.No."].DefaultCellStyle.ForeColor = Color.White;
                    dg_head.Columns["S.No."].DefaultCellStyle.SelectionForeColor = Color.White;
                    dg_head.Columns["S.No."].DisplayIndex = 1;
                    dg_head.Columns["Loan No"].DisplayIndex = 2;
                    dg_head.Columns["Loan Date"].DisplayIndex = 3;
                    dg_head.Columns["Customer"].DisplayIndex = 3;
                    ColDrAmount.DisplayIndex = 5;
                    ColCrAmount.DisplayIndex = 6;
                    ColBalance.DisplayIndex = 7;

                    double TotalBalance = 0;

                    for (int i = 0; i < dg_head.Rows.Count; i++)
                    {
                        string Balance = "";
                        int Id = dg_head.Rows[i].Cells["Id"].EditedFormattedValue.ToString().IntParse();
                        double OpBalance = ObjData.GetLoanbalance(Id);
                        TotalBalance = TotalBalance + OpBalance;
                        if (OpBalance > 0)
                        {
                            Balance = clsGeneral.CurFormat(Math.Abs(OpBalance)) + " Dr";
                            dg_head.Rows[i].Cells["ColDrAmount"].Value = Balance;
                        }
                        else if (OpBalance < 0)
                        {
                            Balance = clsGeneral.CurFormat(Math.Abs(OpBalance)) + " Cr";
                            dg_head.Rows[i].Cells["ColCrAmount"].Value = Balance;
                        }
                        else
                        {
                            Balance = "0.00";
                        }

                        if (TotalBalance > 0)
                        {
                            Balance = clsGeneral.CurFormat(Math.Abs(TotalBalance)) + " Dr";
                            dg_head.Rows[i].Cells["ColBalance"].Value = Balance;
                        }
                        else if (TotalBalance < 0)
                        {
                            Balance = clsGeneral.CurFormat(Math.Abs(TotalBalance)) + " Cr";
                            dg_head.Rows[i].Cells["ColBalance"].Value = Balance;
                        }
                        else
                        {
                            Balance = "0.00";
                        }

                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.SelectedValue = null;
            dg_head.DataSource = null;
            txtName.Focus();
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
            clsGeneral.ExcelExport(dg_head, "Customer History of " + txtName.SelectedText);
        }

        private void dg_head_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter && dg_head.SelectedRows.Count == 1)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    int Id = dg_head.SelectedRows[0].Cells["Id"].EditedFormattedValue.ToString().IntParse();
                    Frm_LoanLedger F = new Frm_LoanLedger();
                    F.Show();
                    F.CboNo.SelectedValue = Id;
                    F.LoanId = Id;
                    F.GetChart();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }
    }
}
