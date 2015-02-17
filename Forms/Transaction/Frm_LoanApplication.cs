using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Finance_Management_System
{
    public partial class Frm_LoanApplication : Form
    {
        public Frm_LoanApplication()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();
        public int LoanApplicationId = 0;

        public void ClearAll()
        {
            try
            {
                LoanApplicationId = 0;
                if (cboAgent.DataSource != null)
                {
                    cboAgent.SelectedIndex = 0;
                }
                if (cboOffice.DataSource != null)
                {
                    cboOffice.SelectedIndex = 0;
                }
                txtCode.SelectedValue = null;
                txtName.SelectedValue = null;
                txtGuaranter.SelectedValue = null;
                TextBox t;
                for (int i = 0; i < pnlParty.Controls.Count; i++)
                {
                    if (pnlParty.Controls[i] is TextBox)
                    {
                        t = (TextBox)pnlParty.Controls[i];
                        t.Text = "";
                    }
                }
                for (int i = 0; i < pnlLoan.Controls.Count; i++)
                {
                    if (pnlLoan.Controls[i] is TextBox)
                    {
                        t = (TextBox)pnlLoan.Controls[i];
                        t.Text = "";
                    }
                }
                cboInterType.SelectedIndex = 0;
                cboLoanType.SelectedIndex = 0;
                cboPenaltyType.SelectedIndex = 0;
                cboThrough.SelectedIndex = 0;
                pictureBox1.Image = null;
                txtLoanAmount.Text = "0.00";
                txtMonthly.Text = "0.00";
                txtPeriod.Text = "0";
                txtNetAmount.Text = "0.00";
                txtEMI.Text = "0.00";
                txtInterestRate.Text = "0.00";
                txtPenalRate.Text = "0.00";
                chkCompounded.Checked = false;
                GetMaxFormNo();
                dtpChDate.Value = System.DateTime.Now;
                dtpdate.Value = System.DateTime.Now;
                for (int i = 0; i < chkDocuments.Items.Count; i++)
                {
                    chkDocuments.SetItemChecked(i, false);
                    chkDocuments.ClearSelected();
                }
                cboAgent.Focus();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void GetMaxFormNo()
        {
            string SS = "Select L_FormNo as max from LoanApplication Order By L_Id Desc ";
            DataTable DtCount = ObjData.GetDataTable(SS);
            txtFormNo.Text = "1";
            if (DtCount.Rows.Count > 0)
            {
                int code = DtCount.Rows[0]["max"].ToString().IntParse() + 1;
                txtFormNo.Text = code.ToString();
            }
            lbltotalRecords.Text = "Total Records : " + DtCount.Rows.Count.ToString();
        }

        public void FillDocuments()
        {
            try
            {
                string SS = "Select * From DocumentMaster Order By DocumentName";
                DataTable DtData = ObjData.GetDataTable(SS);
                chkDocuments.DataSource = DtData;
                chkDocuments.DisplayMember = "DocumentName";
                chkDocuments.ValueMember = "DocumentId";
                
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void FillAgent()
        {
            try
            {
                string SS = "Select AgentId,AgentName From AgentMaster Order By AgentName";
                DataTable DtData = ObjData.GetDataTable(SS);
                cboAgent.DisplayMember = "AgentName";
                cboAgent.ValueMember = "AgentId";
                cboAgent.DataSource = DtData;
                if (cboAgent.DataSource != null)
                {
                    cboAgent.SelectedIndex = 0;
                }
                if (cboOffice.DataSource != null)
                {
                    cboOffice.SelectedIndex = 0;
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void FillOffice()
        {
            try
            {
                string SS = "Select OfficeId,OfficeName From OfficeMaster Order By OfficeName";
                DataTable DtData = ObjData.GetDataTable(SS);
                cboOffice.DisplayMember = "OfficeName";
                cboOffice.ValueMember = "OfficeId";
                cboOffice.DataSource = DtData;
                if (cboOffice.DataSource != null)
                {
                    cboOffice.SelectedIndex = 0;
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void FillParty()
        {
            try
            {
                string SS = "Select * From PartyMaster Where P_Group = 31 Order By P_Name";
                DataTable DtCode = ObjData.GetDataTable(SS);
                txtCode.DisplayMember = "P_Code";
                txtCode.ValueMember = "P_Id";
                txtCode.DataSource = DtCode;
                txtCode.NextControl = chkDocuments;
                txtCode.BindData();
                txtCode.SelectedValue = null;

                DataTable DtName = DtCode.Copy();
                txtName.DisplayMember = "P_Name";
                txtName.ValueMember = "P_Id";
                txtName.DataSource = DtName;
                txtName.NextControl = chkDocuments;
                txtName.BindData();
                txtName.SelectedValue = null;
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void FillGuarantor()
        {
            try
            {
                string SS = "Select * From GuarantorsMaster Order by G_Name";
                DataTable DtCode = ObjData.GetDataTable(SS);
                txtGuaranter.DisplayMember = "G_Name";
                txtGuaranter.ValueMember = "G_Id";
                txtGuaranter.DataSource = DtCode;
                txtGuaranter.NextControl = txtRelation;
                txtGuaranter.BindData();
                txtGuaranter.SelectedValue = null;
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void Calculate()
        {
            try
            {
                string LoanAmount = txtLoanAmount.Text;
                string Period = txtPeriod.Text;
                string NetAmount = txtNetAmount.Text;
                string EMI = txtEMI.Text;
                string Interest = txtInterestRate.Text;

                double LA = 0;
                double P = 0;
                double NA = 0;
                double E = 0;
                double I = 0;

                double.TryParse(LoanAmount, out LA);
                double.TryParse(Period, out P);
                double.TryParse(NetAmount, out NA);
                double.TryParse(EMI, out E);
                double.TryParse(Interest, out I);

                if (cboLoanType.SelectedIndex == 0 || cboLoanType.SelectedIndex == 1)
                {
                    NA = E * P;
                    txtNetAmount.Text = clsGeneral.CurFormat(NA);
                    if (P > 0 && NA > 0)
                    {
                        I = NA - LA;
                        I = (I / LA) * 100;
                        if (cboLoanType.SelectedIndex == 0)
                        {
                          
                        }
                        else
                        {
                            //double D = P * 7;
                            //I = I / D;
                        }
                        txtInterestRate.Text = clsGeneral.CurFormat(I);
                    }
                }

                if (cboLoanType.SelectedIndex == 2)
                {
                   
                    double Net = 0;
                    if (cboInterType.SelectedIndex == 0)
                    {
                        Net = LA * (I / 100) * P;
                        Net = LA + Net;
                    }
                    else
                    {
                        Net = LA + I;
                    }
                    NA = Net;
                     //txtEMI.Text = "0.00";
                    txtNetAmount.Text = clsGeneral.CurFormat(NA);
                    if (P > 0 && NA > 0)
                    {
                        E = NA / P;
                        txtEMI.Text = clsGeneral.CurFormat(E);
                    }
                }

                
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

        private void Frm_LoanApplication_Load(object sender, EventArgs e)
        {
            FillDocuments();
            FillAgent();
            FillOffice();
            FillParty();
            FillGuarantor();
            ClearAll();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void btnAddAgent_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            Frm_AgentMaster F = new Frm_AgentMaster();
            F.ShowDialog();
            //this.Visible = true;
            FillAgent();
            cboAgent.Focus();
        }

        private void btnAddOffice_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            Frm_OfficeMaster F = new Frm_OfficeMaster();
            F.ShowDialog();
            //this.Visible = true;
            FillOffice();
            cboOffice.Focus();
        }

        private void Frm_LoanApplication_KeyDown(object sender, KeyEventArgs e)
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

        private void btnAddParty_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            Frm_Party F = new Frm_Party();
            F.ShowDialog();
            //this.Visible = true;
            FillParty();
            txtName.Focus();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            //this.Visible = false;
            Frm_Gurantors F = new Frm_Gurantors();
            F.ShowDialog();
            //this.Visible = true;
            FillGuarantor();
            txtGuaranter.Focus();
        }

        private void txtLoanAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsGeneral.IsNumberic(e, txtLoanAmount);
        }

        private void txtPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsGeneral.IsNumbericWithOutDecimal(e, txtPeriod);
        }

        private void txtNetAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsGeneral.IsNumberic(e, txtNetAmount);
        }

        private void txtEMI_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsGeneral.IsNumberic(e, txtEMI);
        }

        private void txtInterestRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsGeneral.IsNumberic(e, txtInterestRate);
        }

        private void txtPenalRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsGeneral.IsNumberic(e, txtPenalRate);
        }

        private void txtEMI_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void txtLoanAmount_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void txtPeriod_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void txtNetAmount_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void txtInterestRate_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void cboInterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void txtCode_ValueChanged(object Value)
        {
            try
            {
                if (Value != null)
                {
                    DataTable D = (DataTable)txtCode.DataSource;
                    DataRow[] Dr = D.Select("P_Id = " + Value.ToString() + "");
                    if (Dr.Length > 0)
                    {
                        txtCode.SelectedValue = Value.ToString();
                        txtName.SelectedValue = Value.ToString();
                        txtFathers.Text = Dr[0]["P_FatherName"].ToString();
                        txtAdd1.Text = Dr[0]["P_Address1"].ToString();
                        txtAdd2.Text = Dr[0]["P_Address2"].ToString();
                        txtCity.Text = Dr[0]["P_City"].ToString();
                        txtPin.Text = Dr[0]["P_Pin"].ToString();
                        txtPh1.Text = Dr[0]["P_Phone1"].ToString();
                        txtPh2.Text = Dr[0]["P_Phone2"].ToString();
                        txtMob1.Text = Dr[0]["P_Mobile1"].ToString();
                        txtMob2.Text = Dr[0]["P_Mobile2"].ToString();
                        txtEMail.Text = Dr[0]["P_Email"].ToString();

                        txtInterestRate.Text = Dr[0]["P_IntRate"].ToString();
                        cboInterType.SelectedIndex = 0;
                        txtPenalRate.Text = Dr[0]["P_Penalty"].ToString();
                        cboPenaltyType.SelectedIndex = int.Parse(Dr[0]["P_PenaltyCombo"].ToString());
                        pictureBox1.Image = null;
                        pictureBox1.Image = ObjData.Base64ToImage(Dr[0]["P_Picture"].ToString());
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void txtLoanAmount_Leave(object sender, EventArgs e)
        {
            txtLoanAmount.Text = clsGeneral.CurFormat(txtLoanAmount.Text);
        }

        private void txtGuaranter_ValueChanged(object Value)
        {
            try
            {
                if (Value != null)
                {
                    DataTable D = (DataTable)txtGuaranter.DataSource;
                    DataRow[] Dr = D.Select("G_Id = " + Value.ToString() + "");
                    if (Dr.Length > 0)
                    {
                        txtGAdd1.Text = Dr[0]["G_Address1"].ToString();
                        txtGAdd2.Text = Dr[0]["G_Address2"].ToString();
                        txtGPh.Text = Dr[0]["G_Phone1"].ToString();
                        txtGMob1.Text = Dr[0]["G_Mobile1"].ToString();
                        txtGMob2.Text = Dr[0]["G_Mobile2"].ToString();
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void cboLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPeriod.Text = "Period (" + cboLoanType.SelectedItem.ToString().Substring(0, 1) + ")";
            lblEMI.Text = "EMI (" + cboLoanType.SelectedItem.ToString().Substring(0, 1) + ")";
            txtEMI.ReadOnly = true;
            cboInterType.Enabled = true;
            if (cboLoanType.SelectedIndex == 0 || cboLoanType.SelectedIndex == 1)
            {
                cboInterType.Enabled = false;
                cboInterType.SelectedIndex = 0;
                txtEMI.ReadOnly = false;
            }
            Calculate();
        }

        private void label30_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Frm_DocumentsMaster F = new Frm_DocumentsMaster();
            F.ShowDialog();
            this.Visible = true;
            FillDocuments();
        }

        public bool IsValidate()
        {
            bool Isvalid = true;
            try
            {
                int Agent = cboAgent.ValueGet("Please Select Agent");
                if (Agent == 0)
                {
                    cboAgent.Focus();
                    return Isvalid = false;
                }

                int Office = cboOffice.ValueGet("Please Select Office");
                if (Office == 0)
                {
                    cboOffice.Focus();
                    return Isvalid = false; 
                }

                int Customer = txtName.ValueGet("Please Select Customer");
                if (Customer == 0)
                {
                    txtName.Focus();
                    return Isvalid = false; 
                }

                //int Guaranter = txtGuaranter.ValueGet("Please Select Guaranter");
                //if (Guaranter == 0)
                //{
                //    txtGuaranter.Focus();
                //    return Isvalid = false; 
                //}

                //if (txtRelation.Text.Trim().Length == 0)
                //{
                //    clsGeneral.ShowMessage("Please Enter Guaranter Relation");
                //    txtRelation.Focus();
                //    return Isvalid = false; 
                //}

                if (txtLoanAmount.DoubleParse() == 0)
                {
                    clsGeneral.ShowMessage("Please Enter Loan Amount");
                    txtLoanAmount.Focus();
                    return Isvalid = false; 
                }

                if (txtPeriod.DoubleParse() == 0)
                {
                    clsGeneral.ShowMessage("Please Enter Loan Period");
                    txtPeriod.Focus();
                    return Isvalid = false;
                }

                if (txtEMI.DoubleParse() == 0)
                {
                    clsGeneral.ShowMessage("Please Enter Loan EMI");
                    txtEMI.Focus();
                    return Isvalid = false;
                }

                if (txtNetAmount.DoubleParse() == 0)
                {
                    clsGeneral.ShowMessage("Please Enter Loan Net Amount");
                    txtNetAmount.Focus();
                    return Isvalid = false;
                }

                if (txtInterestRate.DoubleParse() == 0)
                {
                    clsGeneral.ShowMessage("Please Enter Loan Interest");
                    txtInterestRate.Focus();
                    return Isvalid = false;
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
                Isvalid = false;
            }
            return Isvalid;
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
                if (IsValidate() == true)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = ObjData.GetConnectionObj();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_LoanApplication";
                    cmd.Parameters.AddWithValue("@L_Id", LoanApplicationId);
                    cmd.Parameters.AddWithValue("@L_AgentId", cboAgent.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@L_OfficeId", cboOffice.SelectedValue.ToString());
                    if (LoanApplicationId == 0)
                    {
                        GetMaxFormNo();
                    }
                    cmd.Parameters.AddWithValue("@L_FormNo", txtFormNo.Text.Trim()); ;
                    cmd.Parameters.AddWithValue("@L_FormDate", dtpdate.DateFormat());
                    cmd.Parameters.AddWithValue("@L_PartyId", txtName.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@L_LoanAmount", txtLoanAmount.DoubleParse());
                    cmd.Parameters.AddWithValue("@L_LoanThrough", cboThrough.SelectedIndex);
                    cmd.Parameters.AddWithValue("@L_ChequeNo", txtChequeNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@L_ChequeDate", dtpChDate.DateFormat());
                    cmd.Parameters.AddWithValue("@L_ChequeBank", txtBankName.Text.Trim());
                    cmd.Parameters.AddWithValue("@L_LoanType", cboLoanType.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@L_Period", txtPeriod.DoubleParse());
                    cmd.Parameters.AddWithValue("@L_NetAmount", txtNetAmount.DoubleParse());
                    cmd.Parameters.AddWithValue("@L_EMI", txtEMI.DoubleParse());
                    cmd.Parameters.AddWithValue("@L_Guarantor", txtGuaranter.ValueGet("").ToString());
                    cmd.Parameters.AddWithValue("@L_Relation", txtRelation.Text);
                    cmd.Parameters.AddWithValue("@L_InterestType", cboInterType.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@L_InterestRate", txtInterestRate.DoubleParse());
                    cmd.Parameters.AddWithValue("@L_PenaltyType", cboPenaltyType.SelectedIndex.ToString());
                    cmd.Parameters.AddWithValue("@L_PenaltyRate", txtPenalRate.DoubleParse());
                    cmd.Parameters.AddWithValue("@L_PartyBank", txtPartyBank.Text.Trim());
                    cmd.Parameters.AddWithValue("@L_PartyBankAdd", txtPartyBAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@L_PartyBankCh", txtPartyBChequeNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@L_Compounded", chkCompounded.Checked);
                    if (chkCompounded.Checked == false)
                    {
                        txtMonthly.Text = "0.00";
                    }
                    cmd.Parameters.AddWithValue("@L_Monthly", txtMonthly.DoubleParse());
                    int a = cmd.ExecuteNonQuery();
                    ClearAll();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void chkDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void FillData(int Id)
        {
            try
            {
                string SS = "Select * From LoanApplication Where L_Id = " + Id + "";
                DataTable Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    LoanApplicationId = int.Parse(Dt.Rows[0]["L_Id"].ToString());
                    cboAgent.SelectedValue = Dt.Rows[0]["L_AgentId"].ToString();
                    cboOffice.SelectedValue = Dt.Rows[0]["L_OfficeId"].ToString();
                    txtFormNo.Text = Dt.Rows[0]["L_FormNo"].ToString();
                    dtpdate.Value = System.DateTime.Parse(Dt.Rows[0]["L_FormDate"].ToString());
                    txtName.SelectedValue = Dt.Rows[0]["L_PartyId"].ToString();
                    txtCode_ValueChanged(txtName.SelectedValue);
                    txtLoanAmount.Text = Dt.Rows[0]["L_LoanAmount"].ToString();
                    cboThrough.SelectedIndex = int.Parse(Dt.Rows[0]["L_LoanThrough"].ToString());
                    txtChequeNo.Text = Dt.Rows[0]["L_ChequeNo"].ToString();
                    dtpChDate.Value = System.DateTime.Parse(Dt.Rows[0]["L_ChequeDate"].ToString());
                    txtBankName.Text = Dt.Rows[0]["L_ChequeBank"].ToString();
                    cboLoanType.SelectedIndex = int.Parse(Dt.Rows[0]["L_LoanType"].ToString());
                    txtPeriod.Text = Dt.Rows[0]["L_Period"].ToString();
                    txtNetAmount.Text = Dt.Rows[0]["L_NetAmount"].ToString();
                    txtEMI.Text = Dt.Rows[0]["L_EMI"].ToString();
                    txtGuaranter.SelectedValue = Dt.Rows[0]["L_Guarantor"].ToString();
                    txtGuaranter_ValueChanged(txtGuaranter.SelectedValue);
                    txtRelation.Text = Dt.Rows[0]["L_Relation"].ToString();
                    cboInterType.SelectedIndex = int.Parse(Dt.Rows[0]["L_InterestType"].ToString());
                    txtInterestRate.Text = Dt.Rows[0]["L_InterestRate"].ToString();
                    cboPenaltyType.SelectedIndex = int.Parse(Dt.Rows[0]["L_PenaltyType"].ToString());
                    txtPenalRate.Text = Dt.Rows[0]["L_PenaltyRate"].ToString();
                    txtPartyBank.Text = Dt.Rows[0]["L_PartyBank"].ToString();
                    txtPartyBAddress.Text = Dt.Rows[0]["L_PartyBankAdd"].ToString();
                    txtPartyBChequeNo.Text = Dt.Rows[0]["L_PartyBankCh"].ToString();

                    txtMonthly.Text = Dt.Rows[0]["L_Monthly"].ToString();

                    chkCompounded.Checked = bool.Parse(Dt.Rows[0]["L_Compounded"].ToString());
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
                if (Rights == false)
                {
                    return;
                }
                //this.Visible = false;
                Frm_Find f = new Frm_Find();
                f.heading = "Guarantors Master";
                f.StatementPass = "";
                f.StatementPass = "Select L_Id As Id,Convert(Varchar,L_FormNo) As [Application No], AgentName As [Agent Name],OfficeName As Office,P_Name As [Customer Name],Convert(varchar,L_LoanAmount) As [Loan Amount],Convert(varchar,L_EMI) As [Loan EMI] From LoanApplication Inner Join AgentMaster On AgentId = L_AgentId Inner Join OfficeMaster On OfficeId = L_OfficeId Inner Join PartyMaster On P_Id = L_PartyId Order By L_FormNo";
                f.ShowDialog();
                //this.Visible = true;
                FillData(f.Id);
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
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
                string SS = "Select * From vw_LoanApplication Where Id = " + LoanApplicationId + "";
                DataTable Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    clsNumberToWord ObjNumber = new clsNumberToWord();
                    Forms.Printing.LoanApplicationWithoutLetter Rpt = new Finance_Management_System.Forms.Printing.LoanApplicationWithoutLetter();
                    string EMI = Dt.Rows[0]["EMI"].ToString();
                    string EMIWords = ObjNumber.changeCurrencyToWords(EMI);
                    string PLoanAmount = Dt.Rows[0]["PLoanAmount"].ToString();
                    string PLoanAmountWords = ObjNumber.changeCurrencyToWords(PLoanAmount);
                    Dt.Rows[0]["EMIWords"] = "Rs. " + EMIWords;
                    Dt.Rows[0]["PLoanAmountWords"] = "Rs. " + PLoanAmountWords;
                    Rpt.SetDataSource(Dt);
                    //Rpt.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
                    Rpt.PrintToPrinter(1, true, 0, 0);
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Frm_LoanReturnChart F = new Frm_LoanReturnChart();
            F.LoanId = LoanApplicationId;
            F.Show();
        }

        private void txtMonthly_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsGeneral.IsNumberic(e, txtMonthly);
        }

        private void chkCompounded_CheckedChanged(object sender, EventArgs e)
        {
            txtMonthly.Enabled = chkCompounded.Checked;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Delete.ToString());
            if (Rights == false)
            {
                return;
            }
        }
    }
}
