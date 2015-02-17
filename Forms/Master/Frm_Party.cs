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
    public partial class Frm_Party : Form
    {
        public Frm_Party()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();
        int PartyId = 0;

        private void btnRemove_Click(object sender, EventArgs e)
        {
            PicParty.Image = null;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofDlg = new OpenFileDialog();
                ofDlg.Filter = "JPG Image(*.jpg)|*.jpg|GIF Image(*.gif)|*.gif|PNG Image(*.png)|*.png|BMP Image(*.bmp)|*.bmp|JPEG Image(*.jpeg)|*.jpeg";
                if (DialogResult.OK == ofDlg.ShowDialog())
                {
                    PicParty.Image = Image.FromFile(ofDlg.FileName);
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }

        private void ClearAll()
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
                    PicParty.Image = null;
                    txtFirmName.Focus();
                }
                string SS = "Select P_Code as max from PartyMaster Order By P_Id Desc ";
                DataTable DtCount = ObjData.GetDataTable(SS);
                txtCode.Text = "01";
                if (DtCount.Rows.Count > 0)
                {
                    string code = DtCount.Rows[0]["max"].ToString();
                    txtCode.Text = clsGeneral.MaxAlfaNumeric(code.ToString());
                }
                lbltotalRecords.Text = "Total Records : " + DtCount.Rows.Count.ToString();
                txtPercent.Text = "%";
                cbPenalty.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void Frm_Party_KeyDown(object sender, KeyEventArgs e)
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        
        public bool ValidData()
        {
            try
            {
                string s = "Select * from PartyMaster where P_Code='" + txtCode.Text + "' and P_Id<>" + PartyId + "";
                DataTable dt = ObjData.GetDataTable(s);
                if (dt.Rows.Count > 0)
                {
                    clsGeneral.ShowMessage("The Code Already Exists.");
                    txtCode.Focus();
                    return false;
                }

                if (txtFirmName.Text.Trim() == "")
                {
                    clsGeneral.ShowMessage("Fill the Name first");

                    txtFirmName.Focus();
                    return false;
                }

                if (CboHead.ValueGet("Please Select Group Name") == 0)
                {
                    CboHead.Focus();
                    return false;
                }

                //if (txtIntRate.Text.Trim() == "")
                //{
                //    clsGeneral.ShowMessage("Fill the Interest Rate");
                //    txtIntRate.Focus();
                //    return false;
                //}

            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
            return true;
        }

        public void savedata()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ObjData.GetConnectionObj();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_PartyMaster";
                cmd.Parameters.AddWithValue("@P_Id", PartyId);
                cmd.Parameters.AddWithValue("@P_Code", txtCode.Text.Trim());
                cmd.Parameters.AddWithValue("@P_FirmName", txtFirmName.Text.Trim());
                cmd.Parameters.AddWithValue("@P_Name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@P_FatherName", txtFatherName.Text.Trim());
                cmd.Parameters.AddWithValue("@P_Address1", txtAddress1.Text.Trim());
                cmd.Parameters.AddWithValue("@P_Address2", txtAddress2.Text.Trim());
                cmd.Parameters.AddWithValue("@P_City", txtCity.Text.Trim());
                cmd.Parameters.AddWithValue("@P_Pin", txtPin.Text.Trim());
                cmd.Parameters.AddWithValue("@P_Phone1", txtPhone1.Text.Trim());
                cmd.Parameters.AddWithValue("@P_Phone2", txtPhone2.Text.Trim());
                cmd.Parameters.AddWithValue("@P_Mobile1", txtMobile1.Text.Trim());
                cmd.Parameters.AddWithValue("@P_Mobile2", txtMobile2.Text.Trim());

                string BDay = "";
                if (txtbday.Text.Trim().Length > 0)
                {
                    BDay = txtbday.Text;
                    cmd.Parameters.AddWithValue("@P_Birthday", clsGeneral.FormatDate(DateTime.Parse(BDay)));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@P_Birthday", "");
                }

                string ADay = "";
                if (txtAnn.Text.Trim().Length > 0)
                {
                    ADay = txtAnn.Text;
                    cmd.Parameters.AddWithValue("@P_Anniversary", clsGeneral.FormatDate(DateTime.Parse(ADay)));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@P_Anniversary", "");
                }
                
                cmd.Parameters.AddWithValue("@P_ReferredBy", txtReferredBy.Text);
                cmd.Parameters.AddWithValue("@P_MobileRB", txtMobileRB.Text);
                cmd.Parameters.AddWithValue("@P_PhoneRB", txtPhoneRB.Text);
                cmd.Parameters.AddWithValue("@P_Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@P_IntRate", txtIntRate.Text.DoubleParse());
                cmd.Parameters.AddWithValue("@P_Penalty", txtPenalty.Text.DoubleParse());
                cmd.Parameters.AddWithValue("@P_PenaltyCombo", cbPenalty.SelectedIndex.ToString());
                cmd.Parameters.AddWithValue("@P_Remarks", txtRemarks.Text.Trim());
                cmd.Parameters.AddWithValue("@P_Picture", ObjData.ImageToBase64(PicParty.Image));
                cmd.Parameters.AddWithValue("@P_Group", CboHead.ValueGet(""));

                int a = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void txtIntRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                clsGeneral.IsNumberic(e, txtIntRate);
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                clsGeneral.IsNumberic(e, txtPin);
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void txtPhone1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                clsGeneral.IsNumberic(e, txtPhone1);
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void txtPhone2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                clsGeneral.IsNumberic(e, txtPhone2);
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void txtMobile1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                clsGeneral.IsNumberic(e, txtMobile1);

                if (Convert.ToInt32(e.KeyChar) == 8)
                {
                    return;
                }
                string s = txtMobile1.Text;
                int len = txtMobile1.Text.Trim().Length;
                int res = txtMobile1.Text.IndexOf("0");
                if (res == 0)
                {
                    if (len == 1)
                    {
                        s = s.Insert(1, "-");
                    }
                    if (len == 7)
                    {
                        s = s.Insert(7, "-");
                    }
                }
                else
                {
                    if (len == 5)
                    {
                        s = s.Insert(5, "-");
                    }
                }
                txtMobile1.Text = s;
                txtMobile1.SelectionStart = txtMobile1.Text.Length;
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void txtMobile2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                clsGeneral.IsNumberic(e, txtMobile2);

                if (Convert.ToInt32(e.KeyChar) == 8)
                {
                    return;
                }
                string s = txtMobile2.Text;
                int len = txtMobile2.Text.Trim().Length;
                int res = txtMobile2.Text.IndexOf("0");
                if (res == 0)
                {
                    if (len == 1)
                    {
                        s = s.Insert(1, "-");
                    }
                    if (len == 7)
                    {
                        s = s.Insert(7, "-");
                    }
                }
                else
                {
                    if (len == 5)
                    {
                        s = s.Insert(5, "-");
                    }
                }
                txtMobile2.Text = s;
                txtMobile2.SelectionStart = txtMobile2.Text.Length;
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void txtMobileRB_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                clsGeneral.IsNumberic(e, txtMobileRB);

                if (Convert.ToInt32(e.KeyChar) == 8)
                {
                    return;
                }
                string s = txtMobileRB.Text;
                int len = txtMobileRB.Text.Trim().Length;
                int res = txtMobileRB.Text.IndexOf("0");
                if (res == 0)
                {
                    if (len == 1)
                    {
                        s = s.Insert(1, "-");
                    }
                    if (len == 7)
                    {
                        s = s.Insert(7, "-");
                    }
                }
                else
                {
                    if (len == 5)
                    {
                        s = s.Insert(5, "-");
                    }
                }
                txtMobileRB.Text = s;
                txtMobileRB.SelectionStart = txtMobileRB.Text.Length;
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void txtPhoneRB_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                clsGeneral.IsNumberic(e, txtPhoneRB);
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
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
                if (ValidData() == true)
                {
                    savedata();
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Delete.ToString());
                if (Rights == false)
                {
                    return;
                }
                //if (PartyId > 0)
                //{
                //    DialogResult dr = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //    if (dr == DialogResult.Yes)
                //    {
                //        string s = "Delete from PartyMaster where P_Id=" + PartyId + "";
                //        ObjData.ExecuteQuery(s);
                //        ClearAll();
                //    }
                //}
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
                if (Rights == false)
                {
                    return;
                }
                string s = "Select Isnull(max(P_Id),0) as P_Id from PartyMaster";
                if (PartyId > 0)
                {
                    s = "Select * from PartyMaster where P_Id =(Select top 1 P_Id from PartyMaster where P_Id <> " + PartyId + " and P_Id < " + PartyId + " order by P_Id desc)";
                }
                DataTable dt = ObjData.GetDataTable(s);
                if (dt.Rows.Count > 0)
                {
                    int Id = int.Parse(dt.Rows[0]["P_Id"].ToString());
                    ShowData(Id);
                }


            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
                if (Rights == false)
                {
                    return;
                }
                string s = "Select * from PartyMaster where P_Id =(Select top 1 P_Id from PartyMaster where P_Id <> " + PartyId + " and P_Id > " + PartyId + " order by P_Id asc)";
                DataTable dt = ObjData.GetDataTable(s);
                if (dt.Rows.Count > 0)
                {
                    int Id = int.Parse(dt.Rows[0]["P_Id"].ToString());
                    ShowData(Id);
                }
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void Frm_Party_Load(object sender, EventArgs e)
        {
            try
            {
                string SS = "Select ag_code As Id,ag_name As Name From AccountGroups Order By ag_name";
                DataTable Dt = ObjData.GetDataTable(SS);
                CboHead.DisplayMember = "Name";
                CboHead.ValueMember = "Id";
                CboHead.DataSource = Dt;
                CboHead.SelectedIndex = 0;
                ClearAll();
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void ShowData(int Id)
        {
            try
            {
                String s = "Select * from PartyMaster where P_Id=" + Id + "";
                DataTable dt = ObjData.GetDataTable(s);
                if (dt.Rows.Count > 0)
                {
                    PartyId = int.Parse(dt.Rows[0]["P_Id"].ToString());
                    txtCode.Text = dt.Rows[0]["P_Code"].ToString();
                    txtFirmName.Text = dt.Rows[0]["P_FirmName"].ToString();
                    txtName.Text = dt.Rows[0]["P_Name"].ToString();
                    txtFatherName.Text = dt.Rows[0]["P_FatherName"].ToString();
                    txtAddress1.Text = dt.Rows[0]["P_Address1"].ToString();
                    txtAddress2.Text = dt.Rows[0]["P_Address2"].ToString();
                    txtCity.Text = dt.Rows[0]["P_City"].ToString();
                    txtPin.Text = dt.Rows[0]["P_Pin"].ToString();
                    txtPhone1.Text = dt.Rows[0]["P_Phone1"].ToString();
                    txtPhone2.Text = dt.Rows[0]["P_Phone2"].ToString();

                    CboHead.SelectedValue = dt.Rows[0]["P_Group"].ToString();

                    txtMobile1.Text = dt.Rows[0]["P_Mobile1"].ToString();
                    txtMobile2.Text = dt.Rows[0]["P_Mobile2"].ToString();
                    txtbday.Text = clsGeneral.DateFormat(dt.Rows[0]["P_Birthday"].ToString());
                    txtAnn.Text = clsGeneral.DateFormat(dt.Rows[0]["P_Anniversary"].ToString());
                    txtReferredBy.Text = dt.Rows[0]["P_ReferredBy"].ToString();
                    txtMobileRB.Text = dt.Rows[0]["P_MobileRB"].ToString();
                    txtPhoneRB.Text = dt.Rows[0]["P_PhoneRB"].ToString();
                    txtEmail.Text = dt.Rows[0]["P_Email"].ToString();
                    txtIntRate.Text = dt.Rows[0]["P_IntRate"].ToString();
                    txtPenalty.Text = dt.Rows[0]["P_Penalty"].ToString();
                    cbPenalty.SelectedIndex = int.Parse(dt.Rows[0]["P_PenaltyCombo"].ToString());
                    txtRemarks.Text = dt.Rows[0]["P_Remarks"].ToString();
                    PicParty.Image = ObjData.Base64ToImage(dt.Rows[0]["P_Picture"].ToString());

                }
            }
            catch (Exception ex)
            {
                clsGeneral.ShowMessage(ex.Message);
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            try
            {
                if (clsGeneral.EmailValidation(txtEmail.Text) == false)
                {
                    if (txtEmail.Text == "")
                    {
                        txtReferredBy.Focus();
                    }
                    else
                    {
                        clsGeneral.ShowMessage("Invalid EMail Address.");
                        txtEmail.Focus();
                    }
                }


            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
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
                Frm_Find F = new Frm_Find();
                F.heading = "Customer Master";
                F.StatementPass = "Select P_Id As Id,P_Code As Code,P_FirmName As [Firm Name],P_Name As [Person Name],P_City As City,P_Pin As Pin,P_Mobile1 + ' ' + P_Mobile2 As Mobile,P_Address1 + ' ' + P_Address2 + ' ' + P_City + ' ' + P_Pin As Address From PartyMaster Order By P_FirmName";
                F.ShowDialog();
                ShowData(F.Id);
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void txtbday_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtbday.Text.Trim().Length > 0)
                {
                    DateTime.Parse(txtbday.Text);
                }
            }
            catch (Exception ex)
            {
                clsGeneral.ShowMessage("Improper Date Format");
                txtbday.Clear();
                txtbday.Focus();
            }
        }

        private void txtAnn_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtAnn.Text.Trim().Length > 0)
                {
                    DateTime.Parse(txtAnn.Text);
                }
            }
            catch (Exception ex)
            {
                clsGeneral.ShowMessage("Improper Date Format");
                txtAnn.Clear();
                txtAnn.Focus();
            }
        }

        private void CboHead_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }       
}

