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
    public partial class Frm_LoanSanction : Form
    {
        public Frm_LoanSanction()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();
        public int LoanSanctionId = 0;

        public void ClearAll()
        {
            try
            {
                cboBank.SelectedIndex = -1;
                cboBank.Enabled = false;
                LoanSanctionId = 0;
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
                txtComm.Text = "0.00";
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

                DgChequeDetails.Rows.Clear();
                string SSQl = "Select 'Application No ' + Convert(varchar,L_FormNo) As LoanNo, L_Id As Id From LoanApplication Order By L_FormNo";
                DataTable Dtdata = ObjData.GetDataTable(SSQl);
                cboApplicationNo.DisplayMember = "LoanNo";
                cboApplicationNo.ValueMember = "Id";
                cboApplicationNo.DataSource = Dtdata;
                if (cboApplicationNo.DataSource != null && Dtdata.Rows.Count > 0)
                {
                    cboApplicationNo.SelectedIndex = 0;
                }

                cboApplicationNo.Focus();

                string Ss = "Select Distinct(L_PartyBank) As Bank From  LoanSanction Where L_PartyBank <> ''  Order By L_PartyBank ";
                DataTable Dt = ObjData.GetDataTable(Ss);
                string[] bName = clsGeneral.DataTableToArray(Dt);
                txtPartyBank.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtPartyBank.AutoCompleteCustomSource.AddRange(bName);


                Ss = "Select Distinct(L_PartyBankAdd) As Bank From  LoanSanction Where L_PartyBankAdd <> ''  Order By L_PartyBankAdd ";
                Dt = ObjData.GetDataTable(Ss);
                string[] AName = clsGeneral.DataTableToArray(Dt);
                txtPartyBAddress.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtPartyBAddress.AutoCompleteCustomSource.AddRange(AName);
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void GetMaxFormNo()
        {
            string SS = "Select L_FormNo as max from LoanSanction Order By L_Id Desc ";
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
                //Where P_Group In (31,43)
                string SS = "Select * From PartyMaster Order By P_Name";
                DataTable DtFull = ObjData.GetDataTable(SS);

                DtFull.DefaultView.RowFilter = "P_Group = 2";
                DataTable DtBank = DtFull.DefaultView.ToTable();

                cboBank.DisplayMember = "P_Name";
                cboBank.ValueMember = "P_Id";
                cboBank.DataSource = DtBank;

                DtFull.DefaultView.RowFilter = "";
                DtFull.DefaultView.RowFilter = "P_Group = 31 Or P_Group = 43";
                DataTable DtCode = DtFull.DefaultView.ToTable();
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
            DgChequeDetails.Rows.Clear();
            int Period = txtPeriod.Text.IntParse();
            for (int i = 0; i < Period; i++)
            {
                DgChequeDetails.Rows.Add();
                DgChequeDetails.Rows[i].Cells["ColSNo"].Value = (i + 1).ToString();
            }
            DgChequeDetails.ClearSelection();
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
                    int GId = txtGuaranter.ValueGet("");
                    string SS = "Select L_Id,P_Name From LoanSanction Left Join PartyMaster on P_Id = L_PartyId Where L_Guarantor  =" + GId + " And L_Id <> " + LoanSanctionId + "";

                    DataTable DtData = ObjData.GetDataTable(SS);
                    for (int i = 0; i < DtData.Rows.Count; i++)
                    {
                        double Balance = ObjData.GetLoanbalance(DtData.Rows[i]["L_Id"].ToString().IntParse());
                        string PartyName = DtData.Rows[i]["P_Name"].ToString();
                        if (Balance > 0)
                        {
                            clsGeneral.ShowMessage("You Cannot Select This Guaranter Beacause Already Guarantee Of : " + PartyName + " Balance Due is Rs " + clsGeneral.CurFormat(Balance + ""));
                            txtGuaranter.SelectedValue = null;
                            return;
                        }
                    }

                    DataTable D = (DataTable)txtGuaranter.DataSource;
                    DataRow[] Dr = D.Select("G_Id = " + Value.ToString() + "");
                    if (Dr.Length > 0)
                    {
                        txtGAdd1.Text = Dr[0]["G_Address1"].ToString();
                        txtGAdd2.Text = Dr[0]["G_Address2"].ToString();

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

            //if (cboLoanType.SelectedIndex == 2)
            //{
            //    cboPenaltyType.Enabled = false;
            //    cboPenaltyType.SelectedIndex = 1;
            //}

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
                int ApplicationId = cboApplicationNo.ValueGet("Please Select Application No");
                if (ApplicationId == 0)
                {
                    cboApplicationNo.Focus();
                    return Isvalid = false;
                }

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
                if (LoanSanctionId > 0)
                {
                    //string SS = "Select * From LoanEMI Where R_SId = " + LoanSanctionId + " And R_Paid = 1";
                    //DataTable Dt = ObjData.GetDataTable(SS);
                    //if (Dt.Rows.Count > 0)
                    //{
                    //    clsGeneral.ShowMessage("You Cannot Make Changes Because EMI is Already Paid");
                    //    return Isvalid = false;
                    //}
                }
                else
                {
                    string SSQL = "Select * From LoanSanction Where L_ApplicationId = " + ApplicationId + "";
                    DataTable DtData = ObjData.GetDataTable(SSQL);
                    if (DtData.Rows.Count > 0)
                    {
                        clsGeneral.ShowMessage("Application is Already Sanction");
                        cboApplicationNo.Focus();
                        return Isvalid = false;
                    }
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
                    int BankId = cboBank.ValueGet("");
                    if (cboThrough.SelectedIndex > 0)
                    {
                        if (BankId == 0)
                        {
                            clsGeneral.ShowMessage("Please Select Loan Issuing Bank");
                            cboBank.Focus();
                            return;
                        }
                    }
                    DialogResult Dr = MessageBox.Show("Are You Sure Want To Save This Laon Sanction", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (Dr == DialogResult.No)
                    {
                        return;
                    }

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = ObjData.GetConnectionObj();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_LoanSanction";
                    cmd.Parameters.AddWithValue("@L_Id", LoanSanctionId);
                    cmd.Parameters.AddWithValue("@L_ApplicationId", cboApplicationNo.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@L_AgentId", cboAgent.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@L_OfficeId", cboOffice.SelectedValue.ToString());
                    if (LoanSanctionId == 0)
                    {
                        GetMaxFormNo();
                    }
                    cmd.Parameters.AddWithValue("@L_FormNo", txtFormNo.Text.Trim());

                    cmd.Parameters.AddWithValue("@L_FormDate", dtpdate.DateFormat());
                    cmd.Parameters.AddWithValue("@L_ApplicableDate", dtpInstallment.DateFormat());
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


                    if (LoanSanctionId > 0)
                    {
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        LoanSanctionId = int.Parse(cmd.ExecuteScalar().ToString());
                    }

                    string UpdateQry = "Update LoanSanction Set L_Known = '" + txtKnown.SqlEncode() + "',L_Comm = " + txtComm.Text.DoubleParse() + ",L_BankId = "+BankId+" Where L_Id = " + LoanSanctionId + "";
                    ObjData.ExecuteQuery(UpdateQry);

                    ObjData.ExecuteQuery("Delete LoanDocuments Where D_LId = " + LoanSanctionId + "");

                    ObjData.ExecuteQuery("Delete ChequeDetails Where S_Id = " + LoanSanctionId + "");

                    for (int i = 0; i < DgChequeDetails.Rows.Count; i++)
                    {
                        string D = DgChequeDetails.Rows[i].Cells["ColChequeDate"].EditedFormattedValue.ToString();
                        string No = DgChequeDetails.Rows[i].Cells["ColChequeNo"].EditedFormattedValue.ToString();

                        string SS = "Insert Into ChequeDetails(S_Id,S_SNo,S_ChequeDate,S_ChequeNo)";
                        SS = SS + "Values(" + LoanSanctionId + "," + (i + 1).ToString() + ",'" + D.SqlEncode() + "','" + No.SqlEncode() + "')";
                        ObjData.ExecuteQuery(SS);

                    }

                    //
                    UpdateQry = "Delete TmpDataCal Where Id = " + LoanSanctionId + "";
                    ObjData.ExecuteQuery(UpdateQry);

                    UpdateQry = "Sp_InsTmpData " + LoanSanctionId + "";
                    ObjData.ExecuteQuery(UpdateQry);

                    //

                    for (int i = 0; i < chkDocuments.Items.Count; i++)
                    {
                        if (chkDocuments.GetItemChecked(i) == true)
                        {
                            chkDocuments.SetSelected(i, true);
                            int Id = chkDocuments.SelectedValue.ToString().IntParse();
                            string InsQry = "Insert Into LoanDocuments(D_LId,D_DId)Values(" + LoanSanctionId + "," + Id + ")";
                            ObjData.ExecuteQuery(InsQry);
                        }
                    }

                    Dr = MessageBox.Show("Do You Want to Send SMS", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (Dr == DialogResult.Yes)
                    {
                        string CNo = txtMob1.Text + "," + txtMob2.Text;
                        string[] ContactNo = CNo.Split(',');
                        for (int j = 0; j < ContactNo.Length; j++)
                        {
                            string Body = "Dear Customer,";
                            Body = Body + Environment.NewLine + "      Your Loan is Sanction For Rs " + clsGeneral.CurFormat(txtLoanAmount.Text) + "/- Period " + txtPeriod.Text + " " + cboLoanType.Text + " on " + dtpdate.Value.ToString("dd/MM/yyyy") + "";
                            Body = Body + Environment.NewLine + "Thanks";
                            Body = Body + Environment.NewLine + clsGeneral.CompanyName + "(" + clsGeneral.ContactNo + ")";
                            CNo = ContactNo[j].ToString();
                            if (CNo.Trim().Length > 5)
                            {
                                clsGeneral.SendSMSMobile(CNo, Body);
                            }
                        }
                    }

                    ClearAll();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void FillData(int Id)
        {
            try
            {
                string SS = "Select * From LoanSanction Where L_Id = " + Id + "";
                DataTable Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    LoanSanctionId = int.Parse(Dt.Rows[0]["L_Id"].ToString());
                    cboApplicationNo.SelectedValue = Dt.Rows[0]["L_ApplicationId"].ToString();
                    cboAgent.SelectedValue = Dt.Rows[0]["L_AgentId"].ToString();
                    cboOffice.SelectedValue = Dt.Rows[0]["L_OfficeId"].ToString();
                    txtFormNo.Text = Dt.Rows[0]["L_FormNo"].ToString();
                    dtpdate.Value = System.DateTime.Parse(Dt.Rows[0]["L_FormDate"].ToString());
                    txtKnown.Text = Dt.Rows[0]["L_Known"].ToString();
                    dtpInstallment.Value = System.DateTime.Parse(Dt.Rows[0]["L_ApplicableDate"].ToString());
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
                    txtComm.Text = Dt.Rows[0]["L_Comm"].ToString().DoubleParse().ToString();
                    chkCompounded.Checked = bool.Parse(Dt.Rows[0]["L_Compounded"].ToString());
                    cboBank.SelectedValue = Dt.Rows[0]["L_BankId"].ToString().IntParse().ToString(); 

                    SS = "Select * From ChequeDetails Where S_Id = " + LoanSanctionId + " Order By S_SNo";
                    DataTable DtDetails = ObjData.GetDataTable(SS);
                    for (int i = 0; i < DtDetails.Rows.Count; i++)
                    {
                        if (i < DgChequeDetails.Rows.Count)
                        {
                            DgChequeDetails.Rows[i].Cells["ColChequeDate"].Value = DtDetails.Rows[i]["S_ChequeDate"].ToString();
                            DgChequeDetails.Rows[i].Cells["ColChequeNo"].Value = DtDetails.Rows[i]["S_ChequeNo"].ToString();
                        }
                    }

                    SS = "Select D_DId From LoanDocuments Where D_LId = " + Id + "";
                    DataTable DDocuments = ObjData.GetDataTable(SS);
                    for (int i = 0; i < chkDocuments.Items.Count; i++)
                    {
                        chkDocuments.SetSelected(i, true);
                        int DId = chkDocuments.SelectedValue.ToString().IntParse();
                        chkDocuments.SetSelected(i, false);
                        DataRow[] Dr = DDocuments.Select("D_DId = " + DId + "");
                        if (Dr.Length > 0)
                        {
                            chkDocuments.SetItemChecked(i, true);
                        }
                    }
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
                f.StatementPass = "Select L_Id As Id,Convert(Varchar,L_FormNo) As [Application No], AgentName As [Agent Name],OfficeName As Office,P_Name As [Customer Name],Convert(varchar,L_LoanAmount) As [Loan Amount],Convert(varchar,L_EMI) As [Loan EMI] From LoanSanction Inner Join AgentMaster On AgentId = L_AgentId Inner Join OfficeMaster On OfficeId = L_OfficeId Inner Join PartyMaster On P_Id = L_PartyId Order By L_FormNo";
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
                string SS = "Select * From vw_LoanSanction Where Id = " + LoanSanctionId + "";
                DataTable Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    clsNumberToWord ObjNumber = new clsNumberToWord();
                    DialogResult Dr = MessageBox.Show("Print on Letter Head", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (Dr == DialogResult.No)
                    {
                        #region With Out Letter
                        Forms.Printing.LoanApplicationWithoutLetter Rpt = new Finance_Management_System.Forms.Printing.LoanApplicationWithoutLetter();
                        string EMI = Dt.Rows[0]["EMI"].ToString();
                        string EMIWords = ObjNumber.changeCurrencyToWords(EMI);
                        string PLoanAmount = Dt.Rows[0]["PLoanAmount"].ToString();
                        string PLoanAmountWords = ObjNumber.changeCurrencyToWords(PLoanAmount);
                        Dt.Rows[0]["EMIWords"] = "Rs. " + EMIWords;
                        Dt.Rows[0]["PLoanAmountWords"] = "Rs. " + PLoanAmountWords;
                        Rpt.SetDataSource(Dt);
                        CrystalDecisions.CrystalReports.Engine.TextObject txtS1;
                        txtS1 = (CrystalDecisions.CrystalReports.Engine.TextObject)Rpt.Section3.ReportObjects["txt1"];
                        txtS1.Text = clsGeneral.Slogan1;
                        CrystalDecisions.CrystalReports.Engine.TextObject txtS2;
                        txtS2 = (CrystalDecisions.CrystalReports.Engine.TextObject)Rpt.Section3.ReportObjects["txt2"];
                        txtS2.Text = clsGeneral.Slogan2;
                        CrystalDecisions.CrystalReports.Engine.TextObject txtS3;
                        txtS3 = (CrystalDecisions.CrystalReports.Engine.TextObject)Rpt.Section3.ReportObjects["txt3"];
                        txtS3.Text = clsGeneral.Slogan3;
                        CrystalDecisions.CrystalReports.Engine.TextObject txtS4;
                        txtS4 = (CrystalDecisions.CrystalReports.Engine.TextObject)Rpt.Section3.ReportObjects["txt4"];
                        txtS4.Text = clsGeneral.Slogan4;
                        CrystalDecisions.CrystalReports.Engine.TextObject txtS5;
                        txtS5 = (CrystalDecisions.CrystalReports.Engine.TextObject)Rpt.Section3.ReportObjects["txt5"];
                        txtS5.Text = clsGeneral.Slogan5;
                        Rpt.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
                        //Rpt.PrintToPrinter(1, true, 0, 0);
                        Frm_Preview F = new Frm_Preview(Rpt, Dt);
                        F.Show();
                        #endregion
                    }
                    else
                    {
                        #region With Letter
                        Forms.Printing.LoanApplicationWithLetter Rpt = new Finance_Management_System.Forms.Printing.LoanApplicationWithLetter();
                        string EMI = Dt.Rows[0]["EMI"].ToString();
                        string EMIWords = ObjNumber.changeCurrencyToWords(EMI);
                        string PLoanAmount = Dt.Rows[0]["PLoanAmount"].ToString();
                        string PLoanAmountWords = ObjNumber.changeCurrencyToWords(PLoanAmount);
                        Dt.Rows[0]["EMIWords"] = "Rs. " + EMIWords;
                        Dt.Rows[0]["PLoanAmountWords"] = "Rs. " + PLoanAmountWords;
                        Rpt.SetDataSource(Dt);
                        CrystalDecisions.CrystalReports.Engine.TextObject txtS1;
                        txtS1 = (CrystalDecisions.CrystalReports.Engine.TextObject)Rpt.Section3.ReportObjects["txt1"];
                        txtS1.Text = clsGeneral.Slogan1;
                        CrystalDecisions.CrystalReports.Engine.TextObject txtS2;
                        txtS2 = (CrystalDecisions.CrystalReports.Engine.TextObject)Rpt.Section3.ReportObjects["txt2"];
                        txtS2.Text = clsGeneral.Slogan2;
                        CrystalDecisions.CrystalReports.Engine.TextObject txtS3;
                        txtS3 = (CrystalDecisions.CrystalReports.Engine.TextObject)Rpt.Section3.ReportObjects["txt3"];
                        txtS3.Text = clsGeneral.Slogan3;
                        CrystalDecisions.CrystalReports.Engine.TextObject txtS4;
                        txtS4 = (CrystalDecisions.CrystalReports.Engine.TextObject)Rpt.Section3.ReportObjects["txt4"];
                        txtS4.Text = clsGeneral.Slogan4;
                        CrystalDecisions.CrystalReports.Engine.TextObject txtS5;
                        txtS5 = (CrystalDecisions.CrystalReports.Engine.TextObject)Rpt.Section3.ReportObjects["txt5"];
                        txtS5.Text = clsGeneral.Slogan5;
                        Rpt.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
                        //Rpt.PrintToPrinter(1, true, 0, 0);
                        Frm_Preview F = new Frm_Preview(Rpt, Dt);
                        F.Show();
                        #endregion
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Print.ToString());
            if (Rights == false)
            {
                return;
            }
            Frm_LoanReturnChart F = new Frm_LoanReturnChart();
            F.LoanId = LoanSanctionId;
            F.SSQl = "Sp_LoanSanctionChart";
            F.TableName = "LoanSanction";
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                int ApplicationId = cboApplicationNo.ValueGet("Please Select Application No");
                if (ApplicationId == 0)
                {
                    cboApplicationNo.Focus();
                    return;
                }
                string SSQL = "Select * From LoanSanction Where L_ApplicationId = " + ApplicationId + "";
                DataTable DtData = ObjData.GetDataTable(SSQL);
                if (DtData.Rows.Count > 0)
                {
                    LoanSanctionId = int.Parse(DtData.Rows[0]["L_Id"].ToString());
                    FillData(LoanSanctionId);
                }
                else
                {
                    string SS = "Select * From LoanApplication Where L_Id = " + ApplicationId + "";
                    DataTable Dt = ObjData.GetDataTable(SS);
                    if (Dt.Rows.Count > 0)
                    {
                        GetMaxFormNo();
                        cboAgent.SelectedValue = Dt.Rows[0]["L_AgentId"].ToString();
                        cboOffice.SelectedValue = Dt.Rows[0]["L_OfficeId"].ToString();
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
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnPromissory_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Print.ToString());
                if (Rights == false)
                {
                    return;
                }
                string SS = "Select * From Vw_PromissoryNote Where Id = " + LoanSanctionId + "";
                DataTable Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    Forms.Printing.PromissoryNote Rpt = new Finance_Management_System.Forms.Printing.PromissoryNote();
                    Rpt.SetDataSource(Dt);
                    Rpt.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
                    // Rpt.PrintToPrinter(1, true, 0, 0);
                    Frm_Preview F = new Frm_Preview(Rpt, Dt);
                    F.Show();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnShapath_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Print.ToString());
                if (Rights == false)
                {
                    return;
                }
                string SS = "Select * From vw_LoanSanction Where Id = " + LoanSanctionId + "";
                DataTable Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    Forms.Printing.GuarantorhalafNama Rpt = new Finance_Management_System.Forms.Printing.GuarantorhalafNama();
                    Rpt.SetDataSource(Dt);
                    Rpt.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
                    // Rpt.PrintToPrinter(1, true, 0, 0);
                    Frm_Preview F = new Frm_Preview(Rpt, Dt);
                    F.Show();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Print.ToString());
                if (Rights == false)
                {
                    return;
                }
                string SS = "Select * From vw_LoanSanction Where Id = " + LoanSanctionId + "";
                DataTable Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    Forms.Printing.Shapatra Rpt = new Finance_Management_System.Forms.Printing.Shapatra();
                    Rpt.SetDataSource(Dt);
                    Rpt.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;
                    // Rpt.PrintToPrinter(1, true, 0, 0);
                    Frm_Preview F = new Frm_Preview(Rpt, Dt);
                    F.Show();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnNoDue_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Print.ToString());
                if (Rights == false)
                {
                    return;
                }
                string SS = "Select * From vw_LoanSanction Where Id = " + LoanSanctionId + "";
                DataTable Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    if (ObjData.GetLoanbalance(LoanSanctionId) > 0)
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
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Delete.ToString());
            if (Rights == false)
            {
                return;
            }
        }

        private void txtComm_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsGeneral.IsNumberic(e, txtComm);
        }

        private void cboThrough_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboThrough.SelectedIndex == 0)
            {
                cboBank.SelectedIndex = -1;
                cboBank.Enabled = false;
            }
            else
            {
                cboBank.Enabled = true;
            }
        }
    }
}
