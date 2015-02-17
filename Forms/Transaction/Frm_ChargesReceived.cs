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
    public partial class Frm_ChargesReceived : Form
    {
        public Frm_ChargesReceived()
        {
            InitializeComponent();
        }

        ClsDataAccess ObjData = new ClsDataAccess();
        public int Type = 1;
        int CashId = 0;
        float PenaltyRate = 0;
        double PreviousPenalty = 0;

        private void Frm_CashBank_Load(object sender, EventArgs e)
        {
            try
            {

                string SSQl = "Select P_Name + '-' + Convert(Varchar,L_FormNo) As LoanNo,L_Id As Id From LoanSanction Inner Join PartyMaster On P_Id = L_PartyId Order By L_FormNo";
                DataTable Dtdata = ObjData.GetDataTable(SSQl);
                cboSanctionNo.DisplayMember = "LoanNo";
                cboSanctionNo.ValueMember = "Id";
                cboSanctionNo.DataSource = Dtdata;
                if (cboSanctionNo.DataSource != null)
                {
                    cboSanctionNo.SelectedIndex = 0;
                }
                ClearAll();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void Frm_CashBank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        public void DisplayGrid()
        {
            try
            {
                if (dg_head.DataSource != null)
                {
                    dg_head.Columns["CDate"].Visible = false;
                    dg_head.Columns["No"].Width = 50;
                    dg_head.Columns["Due Date"].Width = 80;
                    dg_head.Columns["EMI"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dg_head.Columns["Amount"].Width = 80;
                    dg_head.Columns["Days Late"].Visible = false;
                    dg_head.Columns["EMI"].Visible = false;
                    dg_head.Columns["No"].ReadOnly = true;
                    dg_head.Columns["Due Date"].ReadOnly = true;
                    dg_head.Columns["EMI"].ReadOnly = true;
                    dg_head.Columns["Days Late"].ReadOnly = true;
                    dg_head.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dg_head.Columns["EMI"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dg_head.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void ClearAll()
        {
            try
            {
                PenaltyRate = 0;
                CashId = 0;
                string SS = "Sp_GetDueEMI 0";
                dg_head.DataSource = ObjData.GetDataTable(SS);
                DisplayGrid();
                txtAccountbalance.Text = "0.00";
                txtAddress.Clear();
                txtBankName.Clear();
                txtChequeDate.Clear();
                txtChequeno.Clear();
                txtContactNo.Clear();
                txtParty.Clear();
                txtRemarks.Clear();
                cboSanctionNo.Focus();
                PreviousPenalty = 0;
                dtpdate.Value = DateTime.Now;
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                int ApplicationId = cboSanctionNo.ValueGet("Please Select Loan Sanction No");
                if (ApplicationId == 0)
                {
                    cboSanctionNo.Focus();
                    return;
                }
                string SS = "Select  ";
                SS = SS + " P_Name As Name,P_Address1 + ', ' + P_Address2 + ', ' + P_City + ', ' + P_Pin As Address, ";
                SS = SS + " P_Phone1 + ' ' + P_Phone2 + ' ' + P_Mobile1 + ' ' + P_Mobile2 As ContactNo, ";
                SS = SS + " L_PenaltyRate As PenaltyRate ";
                SS = SS + " From  LoanSanction  ";
                SS = SS + " Inner Join PartyMaster On P_Id =  L_PartyId  ";
                SS = SS + " Where L_Id = " + ApplicationId + " ";
                DataTable DtData = ObjData.GetDataTable(SS);
                if (DtData.Rows.Count > 0)
                {
                    txtAccountbalance.Text = ObjData.GetLoanbalanceStr(ApplicationId);
                    txtParty.Text = DtData.Rows[0]["Name"].ToString();
                    txtContactNo.Text = DtData.Rows[0]["ContactNo"].ToString();
                    txtAddress.Text = DtData.Rows[0]["Address"].ToString();
                    txtPenaltyRate.Text = DtData.Rows[0]["PenaltyRate"].ToString().StringEmpty();

                    SS = "Sp_GetDueEMI " + ApplicationId;
                    DtData = ObjData.GetDataTable(SS);
                    dg_head.DataSource = DtData;
                    DisplayGrid();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void chkDocuments_ItemCheck(object sender, ItemCheckEventArgs e)
        {


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Save.ToString());
                if (Rights == false)
                {
                    return;
                }
                int ApplicationId = cboSanctionNo.ValueGet("Please Select Loan Sanction No");
                if (ApplicationId == 0)
                {
                    cboSanctionNo.Focus();
                    return;
                }


                DialogResult DrM = MessageBox.Show("Are Sure Want To Save Because Once Save Cannot Be Edited", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DrM == DialogResult.No)
                {
                    return;
                }

                for (int i = 0; i < dg_head.Rows.Count; i++)
                {
                    //double EMI = dg_head.Rows[i].Cells["EMI"].EditedFormattedValue.ToString().DoubleParse();
                    double Amount = dg_head.Rows[i].Cells["Amount"].EditedFormattedValue.ToString().DoubleParse();
                    double EMINO = dg_head.Rows[i].Cells["No"].EditedFormattedValue.ToString().DoubleParse();

                    if (Amount > 0 && EMINO > 0)
                    {
                        string SS = "Insert Into ChargesRec(C_SId,C_EMINo,C_AmountRec,C_Date,C_Remarks)";
                        SS = SS + "Values(" + ApplicationId + "," + EMINO + "," + Amount + ",'" + dtpdate.DateFormat() + "','"+txtRemarks.SqlEncode()+"')";
                        ObjData.ExecuteQuery(SS);
                    }
                }
                ClearAll();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnLedger_Click(object sender, EventArgs e)
        {
            int ApplicationId = cboSanctionNo.ValueGet("Please Select Loan Sanction No");
            if (ApplicationId == 0)
            {
                cboSanctionNo.Focus();
                return;
            }
            Frm_LoanLedger F = new Frm_LoanLedger();
            F.LoanId = ApplicationId;
            F.Show();
        }
       
        TextBox Tb_Numeric;
        private void dg_head_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                int Amount = dg_head.Columns["Amount"].Index;
                if ( dg_head.CurrentCell.ColumnIndex == Amount )
                {
                    e.CellStyle.BackColor = Color.White;
                    Tb_Numeric = (TextBox)e.Control;
                    Tb_Numeric.KeyPress += new KeyPressEventHandler(Tb_Numeric_KeyPress);
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }

        void Tb_Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int Amount = dg_head.Columns["Amount"].Index;
                if ( dg_head.CurrentCell.ColumnIndex == Amount )
                {
                    int q = dg_head[dg_head.CurrentCell.ColumnIndex, dg_head.CurrentCell.RowIndex].EditedFormattedValue.ToString().IndexOf(".");
                    if (q == -1)
                    {
                        if (char.IsDigit(e.KeyChar) || e.KeyChar == 46 || char.IsControl(e.KeyChar))
                        {
                            e.Handled = false;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                        {
                            e.Handled = false;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }

        private void brnDelete_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Delete.ToString());
            if (Rights == false)
            {
                return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Print.ToString());
            if (Rights == false)
            {
                return;
            }
        }
    }
}

