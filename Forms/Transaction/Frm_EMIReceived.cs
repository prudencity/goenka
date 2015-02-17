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
    public partial class Frm_EMIReceived : Form
    {
        public Frm_EMIReceived()
        {
            InitializeComponent();
        }

        ClsDataAccess ObjData = new ClsDataAccess();
        //public int Type = 1;
        int PreviousData = 0;
        float PenaltyRate = 0;
        double PreviousPenalty = 0;

        private void Frm_CashBank_Load(object sender, EventArgs e)
        {
            try
            {
                string SS = "Select * From PartyMaster Where P_Group = 2 Order By P_Name";
                DataTable DtBank = ObjData.GetDataTable(SS);
                cboBank.DisplayMember = "P_Name";
                cboBank.ValueMember = "P_Id";
                cboBank.DataSource = DtBank;

                string SSQl = "Select P_Name + '-' + Convert(Varchar,L_FormNo) As LoanNo,L_Id As Id From LoanSanction Inner Join PartyMaster On P_Id = L_PartyId Order By (P_Name + '-' + Convert(Varchar,L_FormNo))";
                DataTable Dtdata = ObjData.GetDataTable(SSQl);
                cboSanctionNo.DisplayMember = "LoanNo";
                cboSanctionNo.ValueMember = "Id";
                cboSanctionNo.DataSource = Dtdata;
                cboSanctionNo.NextControl = btnShow;
                cboSanctionNo.SelectedValue = null;
                cboSanctionNo.BindData();

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

        public void DisplayGrid(DataTable D)
        {
            try
            {

                D.DefaultView.RowFilter = "Iden = 1";
                D.DefaultView.Sort = "[Date Received] Asc,[EMI No] Asc";


                dg_head.DataSource = D.DefaultView;


                dg_head.Columns["Loan No"].Visible = false;
                dg_head.Columns["Party Name"].Visible = false;
                dg_head.Columns["Address"].Visible = false;
                dg_head.Columns["ContactDetails"].Visible = false;

                dg_head.Columns["SID"].Visible = false;
                dg_head.Columns["Iden"].Visible = false;
                dg_head.Columns["Date Received"].Visible = false;
                dg_head.Columns["Particular"].Visible = false;

                dg_head.Columns["EMI No"].Width = 50;
                dg_head.Columns["Date"].Width = 80;
                dg_head.Columns["Amount"].Width = 80;
                //dg_head.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dg_head.Columns["Amount Rec"].Width = 80;
                dg_head.Columns["Days Late"].Width = 80;

                dg_head.Columns["EMI No"].ReadOnly = true;
                dg_head.Columns["Date"].ReadOnly = true;
                dg_head.Columns["Amount"].ReadOnly = true;
                dg_head.Columns["Days Late"].ReadOnly = true;

                dg_head.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg_head.Columns["Amount Rec"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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
                cboBank.SelectedIndex = -1;
                PenaltyRate = 0;
                PreviousData = 0;
                string SS = "Sp_Calculation 0";
                DataTable D = ObjData.GetDataTable(SS);
                D.Columns.Add("Amount Rec");
                D.Columns.Add("Days Late");
                txtPrincipal.Text = "0.00";
                DisplayGrid(D);
                txtAccountbalance.Text = "0.00";
                txtAddress.Clear();
                txtBankName.Clear();
                txtPenaltyRate.Text = "0.00";
                txtChequeDate.Clear();
                txtChequeno.Clear();
                txtContactNo.Clear();
                txtParty.Clear();
                txtRemarks.Clear();
                cboSanctionNo.SelectedValue = null;
                cboSanctionNo.Focus();
                PreviousPenalty = 0;
                dtpdate.Value = DateTime.Now;
                ColD.Visible = false;
                dg_head_CellLeave(new object(), new DataGridViewCellEventArgs(0, 0));
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

                string SS = "Select F_Date From AccountForeClosure Where F_SId = " + ApplicationId + "";
                DataTable DD = ObjData.GetDataTable(SS);
                if (DD.Rows.Count > 0)
                {
                    clsGeneral.ShowMessage("Account Already Fore Closure");
                    return;
                }


                SS = "Select ROUND(Case 	When ((L_Compounded = 0) Or (L_Compounded = 1 And L_Monthly > 0)) Then ";
                SS = SS + " ((((Select IsNull(Sum(C_AmountRec),0) From EMIReceived Where C_SId = L_Id)/(L_EMI)) ";
                SS = SS + " *((L_NetAmount - L_LoanAmount)/L_Period))-(Select IsNull(Sum(C_AmountRec),0) From ";
                SS = SS + " EMIReceived Where C_SId = L_Id)+ L_LoanAmount) Else Case ";
                SS = SS + " When (Select IsNull(Sum(C_AmountRec),0) From EMIReceived ";
                SS = SS + " Where C_SId = L_Id) >= L_LoanAmount Then 0 Else L_LoanAmount ";
                SS = SS + " End End,0) As [Balance Amount] From LoanSanction ";
                SS = SS + " Inner Join partymaster On P_Id = L_PartyId ";
                SS = SS + " Where L_Id Not In (Select F_SId From AccountForeClosure) And ";
                SS = SS + " L_Id =  " + ApplicationId.ToString() + " ";
                SS = SS + " Order By L_FormDate ";


                double Rbalance = 0;
                if (ObjData.GetDataTable(SS).Rows.Count > 0)
                {
                    Rbalance = ObjData.GetDataTable(SS).Rows[0]["Balance Amount"].ToString().DoubleParse();
                }
                txtPrincipal.Text = Rbalance.ToString().CurrFormat();


                PreviousData = 0;
                SS = "Select  ";
                SS = SS + " P_Name As Name,P_Address1 + ', ' + P_Address2 + ', ' + P_City + ', ' + P_Pin As Address, ";
                SS = SS + " P_Phone1 + ' ' + P_Phone2 + ' ' + P_Mobile1 + ' ' + P_Mobile2 As ContactNo, ";
                SS = SS + " L_PenaltyRate As PenaltyRate, L_ChequeBank As ChequeBank, ";
                SS = SS + " Convert(Varchar,L_ChequeDate,106) As ChequeDate,L_ChequeNo As ChequeNo ";
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

                    txtChequeDate.Text = DtData.Rows[0]["ChequeDate"].ToString();
                    txtBankName.Text = DtData.Rows[0]["ChequeBank"].ToString();
                    txtChequeno.Text = DtData.Rows[0]["ChequeNo"].ToString();

                    SS = "Sp_Calculation " + ApplicationId;
                    DtData = ObjData.GetDataTable(SS);

                    //SS = "Select F_Date From AccountForeClosure Where F_SId = " + ApplicationId + "";
                    //DD = ObjData.GetDataTable(SS);
                    if (DD.Rows.Count > 0)
                    {
                        DateTime Dtp = DateTime.Parse(DD.Rows[0]["F_Date"].ToString());
                        DtData = DtData.Delete("[Date Received] > '" + Dtp.ToShortDateString() + "'");
                    }


                    DtData.Columns.Add("Amount Rec");
                    DtData.Columns.Add("Days Late");
                    DisplayGrid(DtData);
                    dtpdate_ValueChanged(new object(), new EventArgs());
                }
                ColD.Visible = false;
                dg_head_CellLeave(new object(), new DataGridViewCellEventArgs(0, 0));
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

                for (int i = 0; i < dg_head.Rows.Count; i++)
                {
                    double EMI = dg_head.Rows[i].Cells["Amount"].EditedFormattedValue.ToString().DoubleParse();
                    double Amount = dg_head.Rows[i].Cells["Amount Rec"].EditedFormattedValue.ToString().DoubleParse();
                    if (Amount > EMI)
                    {
                        clsGeneral.ShowMessage("Amount Received is More Than EMI");
                        dg_head.CurrentCell = dg_head[dg_head.Columns["Amount"].Index, i];
                        dg_head.Focus();
                        return;
                    }
                }

                if (PreviousData > 0)
                {
                    string DelData = "Delete EMIReceived Where C_SId = " + ApplicationId + "";
                    ObjData.ExecuteQuery(DelData);
                }

                int BankId = cboBank.ValueGet("");
                for (int i = 0; i < dg_head.Rows.Count; i++)
                {
                    double EMI = dg_head.Rows[i].Cells["Amount"].EditedFormattedValue.ToString().DoubleParse();
                    double Amount = dg_head.Rows[i].Cells["Amount Rec"].EditedFormattedValue.ToString().DoubleParse();
                    int EMINO = dg_head.Rows[i].Cells["EMI No"].EditedFormattedValue.ToString().IntParse();

                    if (Amount > 0 && EMINO > 0)
                    {
                        string SS = "Insert Into EMIReceived(C_SId,C_EMINo,C_EMIAmount,C_AmountRec,C_Date,C_BankId)";
                        SS = SS + "Values(" + ApplicationId + "," + EMINO + "," + EMI + "," + Amount + ",'" + dtpdate.DateFormat() + "'," + BankId + ")";
                        ObjData.ExecuteQuery(SS);

                        DialogResult Dr = MessageBox.Show("Do You Want to Send SMS", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (Dr == DialogResult.Yes)
                        {
                            string[] ContactNo = txtContactNo.Text.Split(' ', ',');
                            for (int j = 0; j < ContactNo.Length; j++)
                            {
                                string Body = "Dear Customer,";
                                Body = Body + Environment.NewLine + "      Your " + EMINO.FormatOrdinalNumber() + " EMI Rs " + clsGeneral.CurFormat(Amount) + "/- is Received on " + dtpdate.Value.ToString("dd/MM/yyyy") + "";
                                Body = Body + Environment.NewLine + "Thanks";
                                Body = Body + Environment.NewLine + clsGeneral.CompanyName + "(" + clsGeneral.ContactNo + ")";
                                string CNo = ContactNo[j].ToString();
                                if (CNo.Trim().Length > 5)
                                {
                                    clsGeneral.SendSMSMobile(CNo, Body);
                                }
                            }
                        }
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

        private void dtpdate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dg_head.DataSource != null)
                {
                    for (int i = 0; i < dg_head.Rows.Count; i++)
                    {
                        System.DateTime Dt = DateTime.Parse(dg_head.Rows[i].Cells["Date Received"].EditedFormattedValue.ToString());
                        TimeSpan Ts = dtpdate.Value.Subtract(Dt);
                        //int days = 0;
                        if (Ts.Days > 0)
                        {
                            dg_head.Rows[i].Cells["Days Late"].Value = Ts.Days;
                        }
                        else
                        {
                            dg_head.Rows[i].Cells["Days Late"].Value = "0";
                        }
                    }
                }
                dg_head_CellLeave(new object(), new DataGridViewCellEventArgs(0, 0));
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }
        TextBox Tb_Numeric;
        private void dg_head_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                int Amount = dg_head.Columns["Amount Rec"].Index;
                if (dg_head.CurrentCell.ColumnIndex == Amount)
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
                int Amount = dg_head.Columns["Amount Rec"].Index;
                if (dg_head.CurrentCell.ColumnIndex == Amount)
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
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
            PreviousData = 1;
            string SS = "Select C_SId As SID,1 As Iden, ";
            SS = SS + " C_EMINo As [EMI No],C_Date As [Date Received], ";
            SS = SS + " Convert(varchar,C_Date,106) As Date, ";
            SS = SS + "  '' As Particular,C_AmountRec As [Amount Rec] ,C_EMIAmount As Amount,0 As [Days Late] ";
            SS = SS + " ,'' As [Loan No],'' As [Party Name],'' As Address, '' As ContactDetails From EMIRECEIVED Where C_SId = " + ApplicationId + "";
            DataTable Dt = ObjData.GetDataTable(SS);
            DisplayGrid(Dt);
            ColD.Visible = true;
            dg_head_CellLeave(new object(), new DataGridViewCellEventArgs(0, 0));
        }

        private void brnDelete_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Delete.ToString());
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
            if (PreviousData > 0)
            {
                string DelData = "Delete EMIReceived Where C_SId = " + ApplicationId + "";
                ObjData.ExecuteQuery(DelData);
            }
            ClearAll();
        }

        private void dg_head_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == ColD.Index)
                {
                    int EMINo = dg_head["EMI No", e.RowIndex].EditedFormattedValue.ToString().IntParse();
                    int Id = dg_head["SID", e.RowIndex].EditedFormattedValue.ToString().IntParse();

                    string SS = "Select F_Date From AccountForeClosure Where F_SId = " + Id + "";
                    DataTable DD = ObjData.GetDataTable(SS);
                    if (DD.Rows.Count > 0)
                    {
                        clsGeneral.ShowMessage("Account Already Fore Closure");
                        return;
                    }

                    DialogResult Dr = MessageBox.Show("Are You Sure Want To Delete", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (Dr == DialogResult.No)
                    {
                        return;
                    }


                    if (EMINo > 0 && Id > 0)
                    {
                        string DelData = "Delete EMIReceived Where C_SId = " + Id + " And C_EMINo = " + EMINo + "";
                        ObjData.ExecuteQuery(DelData);
                        btnFind_Click(new object(), new EventArgs());
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void dg_head_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                double Amt = 0;
                double Amtrec = 0;
                for (int i = 0; i < dg_head.Rows.Count; i++)
                {
                    Amt = Amt + dg_head.Rows[i].Cells["Amount"].EditedFormattedValue.ToString().DoubleParse();
                    Amtrec = Amtrec + dg_head.Rows[i].Cells["Amount Rec"].EditedFormattedValue.ToString().DoubleParse();
                }
                lbltotal.Text = "Amount Total : " + Amt.ToString().CurrFormat();
                lblTotalRec.Text = "Amount Received Total : " + Amtrec.ToString().CurrFormat();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
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

