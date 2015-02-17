using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using System.IO;

namespace Finance_Management_System
{
    public partial class Frm_ForeClosure : Form
    {
        public Frm_ForeClosure()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();
        public int CashId = 0;
        DataTable DtFinal = new DataTable();
        clsNumberToWord ObjNumber = new clsNumberToWord();

        #region Code Of Frm_CashReceipt(Load)
        private void Frm_CashReceipt_Load(object sender, EventArgs e)
        {
            string SS = "Select * From PartyMaster Where P_Group = 2 Order By P_Name";
            DataTable DtBank = ObjData.GetDataTable(SS);
            cboBank.DisplayMember = "P_Name";
            cboBank.ValueMember = "P_Id";
            cboBank.DataSource = DtBank;


            string SSQl = "Select P_Name + '-' + Convert(Varchar,L_FormNo) As LoanNo,L_Id As Id From LoanSanction Inner Join PartyMaster On P_Id = L_PartyId Order By (P_Name + '-' + Convert(Varchar,L_FormNo))";
            //string SSdata = "Select * From PartyFill Where  GroupCode in (32,44,31,43) Or GroupCode2 in (32,44,31,43) Order By NAME";
            DataTable Dt = ObjData.GetDataTable(SSQl);
            txtName.DataSource = Dt;
            txtName.DisplayMember = "LoanNo";
            txtName.ValueMember = "Id";
            txtName.NextControl = txtPrincipal;
            txtName.BindData();
            ClearAll();
        }
        #endregion

        #region Code Of Frm_CashReceipt(KeyDown)
        private void Frm_CashReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                
                if (e.KeyData == Keys.Enter)
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                }
                if (e.KeyData == Keys.Escape)
                {
                    if (pnlFind.Visible == true)
                    {
                        pnlFind.Visible = false;
                        return;
                    }
                    DialogResult result = MessageBox.Show("Are You Sure You Want To Exit", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }
        #endregion

        #region Code Of btnClear(Click)
        public void ClearAll()
        {
            try
            {
                cboBank.SelectedIndex = -1;
                CashId = 0;
                txtPrincipal.Text = "0.00"; txtPreviousAmount.Text = "0.00";
                txtName.SelectedValue = null;
                txtRemarks.Clear();
                dtpDate.Value = System.DateTime.Now.Date;
                txtName.Focus();
                lblBalance.Text = "0.00"; txtInterest.Text = "0.00";
                lblPayable.Text = "Net Payable : 0.00 + 0.00 + 0.00";
                txtInterest_TextChanged(new object(), new EventArgs());
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
        #endregion

        #region Code Of btnExit(Click)
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Code Of btnDelete(Click)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Delete.ToString());
                if (Rights == false)
                {
                    return;
                }
                if (CashId > 0)
                {
                    string Data = "Delete from AccountForeClosure where F_Id =" + CashId;
                    ObjData.ExecuteQuery(Data);
                    ClearAll();
                }
                else
                {
                    clsGeneral.ShowMessage("Please Select Record To Delete");
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        #endregion


        #region Code Of btnSave(Click)
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Save.ToString());
                if (Rights == false)
                {
                    return;
                }
                DialogResult Dr = MessageBox.Show("Are You Sure Want To Save ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (Dr == DialogResult.No)
                {
                    return;
                }

                int PartyId = 0;
                if (txtName.SelectedValue == null)
                {
                    clsGeneral.ShowMessage("Please Select Party");
                    txtName.Focus();
                    return;
                }
                if (txtName.SelectedValue != null)
                {
                    int.TryParse(txtName.SelectedValue.ToString(), out PartyId);
                }
                if (PartyId == 0)
                {
                    clsGeneral.ShowMessage("Please Select Party");
                    txtName.Focus();
                    return;
                }
                if (txtNet.Text.DoubleParse() == 0)
                {
                    clsGeneral.ShowMessage("Please Enter Amount");
                    txtPrincipal.Focus();
                    return;
                }

                int BankId = cboBank.ValueGet("");

                double Previous = txtPreviousAmount.Text.DoubleParse();
                double Principal = txtPrincipal.Text.DoubleParse();
                double IntRate = txtInterest.Text.DoubleParse();
                double IntAmount = txtPayableAmount.Text.DoubleParse() - Principal;

                string Data = "Delete from AccountForeClosure where F_SId =" + PartyId;
                ObjData.ExecuteQuery(Data);

                Data = "Delete from AccountForeClosure where F_Id =" + CashId;
                ObjData.ExecuteQuery(Data);

                string Insdata = "INSERT INTO AccountForeClosure(F_SId,F_Date,F_Principal,F_InterestRate,F_InterestAmount ";
                Insdata = Insdata + " ,F_Previous,F_Remarks,F_BankId) VALUES";
                Insdata = Insdata + " (" + PartyId + ",'" + dtpDate.DateFormat() + "'," + Principal + "," + IntRate + "," + IntAmount + "," + Previous + ",'" + txtRemarks.SqlEncode() + "',"+BankId+")";
                int IdGet = ObjData.ExecuteQueryIdentity(Insdata);

                Dr = MessageBox.Show("Are You Sure Want To Print No-Due ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (Dr == DialogResult.Yes)
                {
                    CashId = IdGet;
                    btnPrint_Click(new object(), new EventArgs());
                }

                ClearAll();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }
        #endregion

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            clsGeneral.CheckTextBoxIsEmpty(txtPrincipal);
            double Amount  = double.Parse(txtPrincipal.Text);
            int PartyId = 0;
            if (txtName.SelectedValue == null)
            {
               //
            }
            if (txtName.SelectedValue != null)
            {
                int.TryParse(txtName.SelectedValue.ToString(), out PartyId);
            }
            if (PartyId == 0)
            {
                //
            }
           
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsGeneral.IsNumberic(e, txtPrincipal);
        }

        #region Code Of btnPrint(Click)
        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Print.ToString());
            if (Rights == false)
            {
                return;
            }
            if (CashId > 0)
            {
                string SS = "Select * From vw_LoanSanction Where Id = " + txtName.ValueGet("") + "";
                DataTable Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    if (ObjData.GetLoanbalance(txtName.ValueGet("")) > 0)
                    {
                        clsGeneral.ShowMessage("Loan balance is not Clearded");
                        return;
                    }

                    Forms.Printing.NOC Rpt = new Finance_Management_System.Forms.Printing.NOC();
                    Rpt.SetDataSource(Dt);
                    Rpt.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
                    // Rpt.PrintToPrinter(1, true, 0, 0);
                    Frm_Preview F = new Frm_Preview(Rpt, Dt);
                    F.Show();
                }
            }
        }
        #endregion

        private void btnFind_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
            if (Rights == false)
            {
                return;
            }
            pnlFind.Height = 345;
            pnlFind.Visible = true;
            //dtpStart.Value = Program.FromDate;

            string SSdata = "SELECT '' As SrNo ,AccountForeClosure.F_Id AS Id,P_Name + '-' + Convert(Varchar,L_FormNo) As Name,  ";
            SSdata = SSdata + " Convert(Varchar, AccountForeClosure.F_Date,103) As ReceiptDate, ";
            SSdata = SSdata + " CONVERT(VARCHAR, CONVERT(MONEY, AccountForeClosure.F_Principal), 1) As Amount ";
            SSdata = SSdata + " FROM AccountForeClosure ";
            SSdata = SSdata + " INNER JOIN LoanSanction ON AccountForeClosure.F_SId = LoanSanction.L_Id ";
            SSdata = SSdata + " INNER JOIN PartyMaster ON LoanSanction.L_PartyId = PartyMaster.P_Id Where ";
            SSdata = SSdata + " F_Date >= " + clsGeneral.ComapreDateFormat(dtpStart.Value) + " And F_Date <= " + clsGeneral.ComapreDateFormat(dtpEnd.Value) + " Order By F_Date";

            DataTable Dt = ObjData.GetDataTable(SSdata);

            Dt = ObjData.GetDataTable(SSdata);
            DtFinal = Dt;
            LoadData(Dt);
        }

        public void FillData(int Id)
        {
            try
            {
                string SSddata = "Select * From AccountForeClosure Where F_Id = " + Id + "";
                DataTable DtData = ObjData.GetDataTable(SSddata);
                if (DtData.Rows.Count > 0)
                {
                    CashId = Id;
                    dtpDate.Value = System.DateTime.Parse(DtData.Rows[0]["F_Date"].ToString());
                    txtName.SelectedValue = DtData.Rows[0]["F_SId"].ToString();
                    txtName_ValueChanged(txtName.SelectedValue);
                    txtPreviousAmount.Text = clsGeneral.CurFormat(DtData.Rows[0]["F_Previous"].ToString());
                    txtPrincipal.Text = clsGeneral.CurFormat(DtData.Rows[0]["F_Principal"].ToString());
                    txtInterest.Text = DtData.Rows[0]["F_InterestRate"].ToString();
                    txtRemarks.Text = DtData.Rows[0]["F_Remarks"].ToString();
                    cboBank.SelectedValue = DtData.Rows[0]["F_BankId"].ToString().IntParse();
                    pnlFind.Visible = false;
                    txtInterest_TextChanged(new object(), new EventArgs());
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void LoadData(DataTable Dt)
        {
            try
            {
                dataGridView1.DataSource = null;
                if (Dt.Rows.Count > 0)
                {
                    dataGridView1.DataSource = Dt;
                    dataGridView1.Columns["SrNo"].HeaderText = "S.N.";
                    dataGridView1.Columns["SrNo"].DefaultCellStyle.BackColor = Color.Teal;
                    dataGridView1.Columns["SrNo"].DefaultCellStyle.ForeColor = Color.White;
                    dataGridView1.Columns["SrNo"].DefaultCellStyle.SelectionBackColor = Color.Teal;
                    dataGridView1.Columns["SrNo"].DefaultCellStyle.SelectionForeColor = Color.White;
                    dataGridView1.Columns["SrNo"].Width = 50;
                    dataGridView1.Columns["Amount"].Width = 80;
                    dataGridView1.Columns["ReceiptDate"].Width = 80;
                    dataGridView1.Columns["ReceiptDate"].HeaderText = "Date";
                    dataGridView1.Columns["Id"].Visible = false;
                    dataGridView1.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dataGridView1.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        dataGridView1.Rows[i].Cells["SrNo"].Value = (i + 1).ToString();
                        dataGridView1.CurrentRow.Selected = false;
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        #region Code Of btnShow(Click)
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                btnFind_Click(sender, e);
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        #endregion

        private void txtPartyName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] Dr = null;
                if (txtPartyName.Text.Trim().Length == 0)
                {
                    LoadData(DtFinal);
                }
                else
                {
                    DataTable DtTemp = DtFinal.Clone();
                    Dr = DtFinal.Select("Name Like '" + clsGeneral.SQLEncode(txtPartyName.Text) + "%'");
                    for (int i = 0; i < Dr.Length; i++)
                    {
                        DtTemp.ImportRow(Dr[i]);
                    }
                    LoadData(DtTemp);
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter && dataGridView1.SelectedRows.Count > 0)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    int Id = Int32.Parse(dataGridView1.SelectedRows[0].Cells["Id"].Value.ToString());
                    FillData(Id);
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void txtName_ValueChanged(object Value)
        {
            try
            {
                if (Value != null)
                {
                    DataTable Dt = (DataTable)txtName.DataSource;
                    DataRow[] Dr = null;
                    Dr = Dt.Select("Id = " + Value.ToString() + "");
                    if (Dr.Length > 0)
                    {
                        double Amount = 0;
                        DataTable Dtdata = null;
                        ObjData.GetLoanbalance(Value.ToString().IntParse(), out Amount, out Dtdata);

                        double DrBalance = 0;
                        double CrBalance = 0;
                        string Balance = "";

                        Dtdata.DefaultView.RowFilter = "[Date Received] <= '" + dtpDate.Value.ToString("dd/MM/yyyy") + "'";

                        object DrTotal = Dtdata.DefaultView.ToTable().Compute("Sum(Amount)", "Iden In (-2,3,5) ");
                        object CrTotal = Dtdata.DefaultView.ToTable().Compute("Sum(Amount)", "Iden In (2,4) ");

                        DrBalance = DrTotal.ToString().DoubleParse();
                        CrBalance = CrTotal.ToString().DoubleParse();

                        double OpBalance = DrBalance - CrBalance;
                        if (OpBalance > 0)
                        {
                            Balance = clsGeneral.CurFormat(Math.Abs(OpBalance)) + " Dr";
                        }
                        else if (OpBalance < 0)
                        {
                            Balance = clsGeneral.CurFormat(Math.Abs(OpBalance)) + " Cr";
                        }
                        else
                        {
                            Balance = "0.00";
                        }

                        txtPreviousAmount.Text = OpBalance.ToString().CurrFormat();

                        lblBalance.Text = Balance;

                        //string SS = "Select ROUND(((((Select IsNull(Sum(C_AmountRec),0) From EMIReceived Where C_SId = L_Id)/(L_EMI)) ";
                        //SS = SS + " *((L_NetAmount - L_LoanAmount)/L_Period)) ";
                        //SS = SS + " -(Select IsNull(Sum(C_AmountRec),0) From EMIReceived Where C_SId = L_Id) ";
                        //SS = SS + " + L_LoanAmount),0) As [Balance Amount] From LoanSanction Where L_Id = " + Value.ToString() + " And L_Id Not In (Select F_SId From AccountForeClosure)";

                        string SS = "Select ROUND(Case 	When ((L_Compounded = 0) Or (L_Compounded = 1 And L_Monthly > 0)) Then ";
                        SS = SS + " ((((Select IsNull(Sum(C_AmountRec),0) From EMIReceived Where C_SId = L_Id)/(L_EMI)) ";
                        SS = SS + " *((L_NetAmount - L_LoanAmount)/L_Period))-(Select IsNull(Sum(C_AmountRec),0) From ";
                        SS = SS + " EMIReceived Where C_SId = L_Id)+ L_LoanAmount) Else Case ";
                        SS = SS + " When (Select IsNull(Sum(C_AmountRec),0) From EMIReceived ";
                        SS = SS + " Where C_SId = L_Id) >= L_LoanAmount Then 0 Else L_LoanAmount ";
                        SS = SS + " End End,0) As [Balance Amount] From LoanSanction ";
                        SS = SS + " Inner Join partymaster On P_Id = L_PartyId ";
                        SS = SS + " Where L_Id Not In (Select F_SId From AccountForeClosure) And ";
                        SS = SS + " L_Id = " + Value.ToString() + " ";
                        SS = SS + " Order By L_FormDate ";

                        //string SS = "Select ROUND(((((Select Max(C_EMINo) From EMIReceived Where C_SId = L_Id) * ";
                        //SS = SS + " Convert(Numeric(18,2),((L_NetAmount - L_LoanAmount)/L_Period))) ";
                        //SS = SS + " - (Select IsNull(Sum(C_AmountRec),0) From EMIReceived Where C_SId = L_Id)) + L_LoanAmount) ";
                        //SS = SS + " ,0) As [Balance Amount] From LoanSanction Where L_Id = "+Value.ToString()+" ";
                        double Rbalance = 0;
                        if (ObjData.GetDataTable(SS).Rows.Count > 0)
                        {
                            Rbalance = ObjData.GetDataTable(SS).Rows[0]["Balance Amount"].ToString().DoubleParse();
                        }
                        txtPrincipal.Text = Rbalance.ToString().CurrFormat();

                        txtInterest_TextChanged(new object(), new EventArgs());
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void txtName_IndexChanged(object Value)
        {
            try
            {
                if (Value != null)
                {
                    //lblBalance.Text = ObjData.GetLoanbalanceStr(Value.ToString().IntParse());
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnledger_Click(object sender, EventArgs e)
        {
            int ApplicationId = txtName.ValueGet("Please Select Loan Sanction No");
            if (ApplicationId == 0)
            {
                txtName.Focus();
                return;
            }
            Frm_LoanLedger F = new Frm_LoanLedger();
            F.LoanId = ApplicationId;
            F.Show();
        }

        private void txtInterest_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsGeneral.IsNumberic(e, txtInterest);
        }

        private void txtInterest_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double Payable = 0;
                double Interest = txtInterest.Text.DoubleParse();
                double Amount = txtPrincipal.Text.DoubleParse();
                Payable = ((100 + Interest) * Amount) / 100;
                txtPayableAmount.Text = Payable.ToString().CurrFormat();
                lblPayable.Text = "Net Payable : " + txtPrincipal.Text + " + " + (Payable - Amount).ToString().CurrFormat() + " + " + txtPreviousAmount.Text.CurrFormat();
                txtNet.Text = (txtPrincipal.Text.DoubleParse() + (Payable - Amount) + txtPreviousAmount.Text.DoubleParse()).ToString().CurrFormat();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void txtPreviousAmount_TextChanged(object sender, EventArgs e)
        {
            txtInterest_TextChanged(sender, e);
        }
    }
}
