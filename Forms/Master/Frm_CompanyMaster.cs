using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Finance_Management_System
{
    public partial class Frm_CompanyMaster : Form
    {
        public Frm_CompanyMaster()
        {
            InitializeComponent();
        }

        #region Member Variable
        ClsDataAccess ObjData = new ClsDataAccess();
        public static int PartyId = 0;

        int Cnt = 0;
        #endregion

        #region Functions

        #region ClearAll
        public void ClearAll()
        { 
            try
            {
                PartyId = 0;
                TextBox t;
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (this.Controls[i] is TextBox)
                    {
                        t = (TextBox)this.Controls[i];
                        t.Text = "";
                    }
                }
                cboPurchaseType.SelectedIndex = 1;
                cboSaleType.SelectedIndex = 1;
                txtCode.Focus();
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region FillData
        public void FillData(string OledbId,string OledbName,string OledbTable,vControls.vDropDown vDropDown,TextBox NextControl,int Height)
        { 
            try
            {
                string Data = "Select " + OledbId + "," + OledbName + " from " + OledbTable + " order by " + OledbName;
                DataTable Dt = ObjData.GetDataTable(Data);
                vDropDown.DataSource = Dt;
                vDropDown.DisplayMember = OledbName;
                vDropDown.ValueMember = OledbName;
                vDropDown.BindData();
                vDropDown.AutoMoveToNextControl = true;
                vDropDown.NextControl = NextControl;
                vDropDown.SelectedValue = null;
                vDropDown.ListControlHeight = Height;
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); } 
        }
        #endregion

        #region MobileCheck
        public void MobileCheck(KeyPressEventArgs e, TextBox tb)
        {
            try
            {
                int CheckZero = 0;
                clsGeneral.IsNumberic(e, tb);
                if (tb.Text.Trim() != "")
                {
                    if (tb.Text.Substring(0, 1) == "0")
                    {
                        CheckZero = 6;
                    }
                    else
                    {
                        CheckZero = 5;
                    }
                    if (char.IsControl(e.KeyChar) == false)
                    {
                        if (tb.Text.Length == CheckZero)
                        {
                            tb.Text = tb.Text + "-";
                            tb.SelectionStart = tb.Text.Length;
                        }
                    }
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region SaveData
        public void SaveData()
        { 
            try
            {
                string Data = "";
                Data = Data + "Insert into CompanyProfile(SubTitle,Name,Contact,Add1,Add2,City,Pincode,";
                Data = Data + "Phone,Mobile,Tin,Pan,Email,Category,TaxNo,Remarks,Website)";
                Data = Data + "values('" + clsGeneral.SQLEncode(txtCode.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtName.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtContactPerson.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtAddress.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtAddress2.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtCity.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtPin.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtPhone.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtMobile.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtTin.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtPan.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtEmail.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtServiceCategory.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtServiceTaxNo.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtRemarks.Text) + "','";
                Data = Data + clsGeneral.SQLEncode(txtWebsite.Text) + "')";
                ObjData.ExecuteQuery(Data);

                Data = "";
                Data = "Insert Into Slogan(Slogan1,Slogan2,Slogan3,Slogan4,Slogan5,POSlogan1,POSlogan2,POSlogan3,POSlogan4,POSlogan5)";
                Data = Data + " Values('"+clsGeneral.SQLEncode(txtSlogan1.Text.Trim())+"','"+clsGeneral.SQLEncode(txtSlogan2.Text.Trim())+"',";
                Data = Data + " '" + clsGeneral.SQLEncode(txtSlogan3.Text.Trim()) + "','" + clsGeneral.SQLEncode(txtSlogan4.Text.Trim()) + "',";
                Data = Data + " '" + clsGeneral.SQLEncode(txtSlogan5.Text.Trim()) + "','" + clsGeneral.SQLEncode(txtPO1.Text.Trim()) + "','" + clsGeneral.SQLEncode(txtPO2.Text.Trim()) + "',";
                Data = Data + " '" + clsGeneral.SQLEncode(txtPO3.Text.Trim()) + "','" + clsGeneral.SQLEncode(txtPO4.Text.Trim()) + "',";
                Data = Data + " '" + clsGeneral.SQLEncode(txtPO5.Text.Trim()) + "')";
                ObjData.ExecuteQuery(Data);

            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region Validations
        public bool Validations()
        { 
            try
            {
                //int Id = 0;
                //int.TryParse(PartyId.ToString(), out Id);
                //if (ObjData.DuplicateChacking("PartyMaster", "P_Name", txtName.Text, "P_Id", Id) == true)
                //{
                //    clsGeneral.ShowMessage("Party Name Already Exsists");
                //    txtName.Focus();
                //    return false;
                //}

                if (txtName.Text.Trim() == "")
                {
                    clsGeneral.ShowMessage("Please Fill Party");
                    txtName.Focus();
                    return false;
                }

                if (txtCity.Text.Trim() == null)
                {
                    clsGeneral.ShowMessage("Please Fill City");
                    txtCity.Focus();
                    return false;
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
            return true;
        }
        #endregion

        #region Update
        public void UpdateData()
        {
            try
            {
                string Data = "";
                Data = Data + "Update CompanyMaster set P_Code='" + clsGeneral.SQLEncode(txtCode.Text);
                Data = Data + "',P_Name='" + clsGeneral.SQLEncode(txtName.Text);
                Data = Data + "',P_Contact='" + clsGeneral.SQLEncode(txtContactPerson.Text);
                Data = Data + "',P_Add1='" + clsGeneral.SQLEncode(txtAddress.Text);
                Data = Data + "',P_Add2='" + clsGeneral.SQLEncode(txtAddress2.Text);
                Data = Data + "',P_City='" + clsGeneral.SQLEncode(txtCity.Text);
                Data = Data + "',P_Pin='" + clsGeneral.SQLEncode(txtPin.Text);
                Data = Data + "' where P_Id=" + PartyId;
                ObjData.ExecuteQuery(Data);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #endregion

        #region btnExit (Click)
        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region btnClear (Click)
        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();    
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region btnSave (Click)
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Save.ToString());
                if (Rights == false)
                {
                    return;
                }
                if (Validations() == true)
                {

                    string Data = "Delete from CompanyProfile";// where P_Id=" + PartyId;
                    ObjData.ExecuteQuery(Data);

                    Data = "";
                    Data = "Delete from Slogan";// where P_Id=" + PartyId;
                    ObjData.ExecuteQuery(Data);

                    SaveData();

                    clsGeneral.ShowMessage("Application Will Be Restarted To Make Changes");
                    Application.Restart();
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region Frm_PartyMaster (Load)
        private void Frm_PartyMaster_Load(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
                if (Rights == false)
                {
                    return;
                }
                ClearAll();
                PnlFind.Height = 400;
                FillData("Id", "Name", "CompanyProfile", txtFind, txtCode, 300);
                DataTable Dt = (DataTable)txtFind.DataSource;
                if (Dt.Rows.Count > 0)
                {
                    txtFind_ValueChanged(Dt.Rows[0]["Id"]);
                }
                string SSdat = "Select * From Slogan";
                DataTable DtData = ObjData.GetDataTable(SSdat);
                if (DtData.Rows.Count > 0)
                {
                    txtSlogan1.Text = DtData.Rows[0]["Slogan1"].ToString();
                    txtSlogan2.Text = DtData.Rows[0]["Slogan2"].ToString();
                    txtSlogan3.Text = DtData.Rows[0]["Slogan3"].ToString();
                    txtSlogan4.Text = DtData.Rows[0]["Slogan4"].ToString();
                    txtSlogan5.Text = DtData.Rows[0]["Slogan5"].ToString();

                    txtPO1.Text = DtData.Rows[0]["POSlogan1"].ToString();
                    txtPO2.Text = DtData.Rows[0]["POSlogan2"].ToString();
                    txtPO3.Text = DtData.Rows[0]["POSlogan3"].ToString();
                    txtPO4.Text = DtData.Rows[0]["POSlogan4"].ToString();
                    txtPO5.Text = DtData.Rows[0]["POSlogan5"].ToString();
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region btnDelete (Click)
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (PartyId > 0)
                {
                    DialogResult result = MessageBox.Show("Are You Sure You Want To Delete This Record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (result == DialogResult.Yes)
                    {
                        string Data = "Delete from PartyMaster where P_Id=" + PartyId;
                        ObjData.ExecuteQuery(Data);
                        ClearAll();
                    }
                }
                else
                {
                    clsGeneral.ShowMessage("Please Edit A Record Before Deleting");
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion


        #region btnFind (Click)
        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (PnlFind.Visible == false)
                {
                    PnlFind.BringToFront();
                    PnlFind.Visible = true;
                    FillData("P_Id", "P_Name", "CompanyMaster", txtFind, txtCode, 300);
                    txtFind.Focus();
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtFind (ValueChanged)
        private void txtFind_ValueChanged(object Value)
        {
            try
            {
                string Data = "Select * from CompanyProfile where Id=" + Value;
                DataTable Dt = ObjData.GetDataTable(Data);
                if (Dt.Rows.Count > 0)
                {
                    PartyId = Int32.Parse(Dt.Rows[0]["Id"].ToString());
                   // txtCode.Text = Dt.Rows[0]["P_Code"].ToString();
                    txtName.Text = Dt.Rows[0]["Name"].ToString();
                    txtContactPerson.Text = Dt.Rows[0]["Contact"].ToString();
                    txtAddress.Text = Dt.Rows[0]["Add1"].ToString();
                    txtAddress2.Text = Dt.Rows[0]["Add2"].ToString();
                    txtCity.Text = Dt.Rows[0]["City"].ToString();
                    txtPin.Text = Dt.Rows[0]["Pincode"].ToString();
                    txtPhone.Text = Dt.Rows[0]["Phone"].ToString();
                    txtMobile.Text = Dt.Rows[0]["Mobile"].ToString();
                    txtTin.Text = Dt.Rows[0]["Tin"].ToString();
                    txtWebsite.Text = Dt.Rows[0]["Website"].ToString();
                    txtPan.Text = Dt.Rows[0]["Pan"].ToString();
                    txtEmail.Text = Dt.Rows[0]["Email"].ToString();
                    //cboPurchaseType.SelectedIndex = Int32.Parse(Dt.Rows[0]["P_PurchaseType"].ToString());
                    //cboSaleType.SelectedIndex = Int32.Parse(Dt.Rows[0]["P_SaleType"].ToString());
                    txtRemarks.Text = Dt.Rows[0]["Remarks"].ToString();
                    txtServiceCategory.Text = Dt.Rows[0]["Category"].ToString();
                    txtServiceTaxNo.Text = Dt.Rows[0]["TaxNo"].ToString();
                    txtCode.Focus();
                    txtCode.SelectionStart = txtCode.Text.Length;
                    PnlFind.Visible = false;
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region Frm_PartyMaster (KeyDown)
        private void Frm_PartyMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (this.ActiveControl != txtRemarks)
                    {
                        this.SelectNextControl(this.ActiveControl, true, true, true, true);
                    }
                }

                if (e.KeyData == Keys.Escape)
                {
                    if (PnlFind.Visible == true)
                    {
                        PnlFind.Visible = false;
                        txtCode.Focus();
                        return;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Are You Sure You Want To Exit", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            this.Close();
                        }
                    }
                }

                if (e.KeyData == Keys.F3)
                {
                    btnFind_Click(sender, e);
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtName (Leave)
        private void txtName_Leave(object sender, EventArgs e)
        {
            try
            {
                txtName.Text = clsGeneral.CapitalCaseString(txtName.Text);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtContactPerson (Leave)
        private void txtContactPerson_Leave(object sender, EventArgs e)
        {
            try
            {
                txtContactPerson.Text = clsGeneral.CapitalCaseString(txtContactPerson.Text);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtAddress (Leave)
        private void txtAddress_Leave(object sender, EventArgs e)
        {
            try
            {
                txtAddress.Text = clsGeneral.CapitalCaseString(txtAddress.Text);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtAddress2 (Leave)
        private void txtAddress2_Leave(object sender, EventArgs e)
        {
            try
            {
                txtAddress2.Text = clsGeneral.CapitalCaseString(txtAddress2.Text);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtPin (KeyPress)
        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
               // clsGeneral.IsNumberic(e, txtPin);    
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtPhone (KeyPress)
        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
               // clsGeneral.IsNumberic(e, txtPhone);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtTin (KeyPress)
        private void txtTin_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
               // clsGeneral.IsNumberic(e, txtTin);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtPan (KeyPress)
        private void txtPan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
               // clsGeneral.IsNumberic(e, txtPan);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtMobile (KeyPress)
        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
               // MobileCheck(e, txtMobile);    
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtEmail (Leave)
        private void txtEmail_Leave(object sender, EventArgs e)
        {
            try
            {
              //  clsGeneral.EmailValidation(txtEmail.Text);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtRemarks (KeyDown)
        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Cnt == 2)
                {
                    Cnt = 0;
                    btnSave.Focus();
                    return;
                }
                if (e.KeyData == Keys.Enter)
                {
                    Cnt++;
                }
                else
                {
                    Cnt = 0;
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion
    }
}