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
    public partial class Frm_Gurantors : Form
    {
        public Frm_Gurantors()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();
        int GuarantorsId = 0;

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ClearAll()
        {
            GuarantorsId = 0;
            TextBox t;
            for(int i=0;i<this.Controls.Count;i++)
            {
                if(this.Controls[i] is TextBox)
                {
                    t=(TextBox) this.Controls[i];
                    t.Text = "";
                }
            }
            pictureBox1.Image = null;
            txtFirmName.Focus();
            string SS = "Select G_Code as max from GuarantorsMaster Order By G_Id Desc ";
            DataTable DtCount = ObjData.GetDataTable(SS);
            txtCode.Text = "01";
            if (DtCount.Rows.Count > 0)
            {
                string code = DtCount.Rows[0]["max"].ToString();
                txtCode.Text = clsGeneral.MaxAlfaNumeric(code.ToString());
            }
            lblTotalRec.Text = "Total Records : " + DtCount.Rows.Count.ToString();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void txtPin_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                clsGeneral.IsNumberic(e,txtPin);
               
            }
            catch(Exception ex)
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
                clsGeneral.IsNumberic(e, txtPhone1);
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

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            try
            {
                if(clsGeneral.EmailValidation(txtEmail.Text)==false)
                {
                    if(txtEmail.Text=="")
                    {
                        txtGuarantorsBank.Focus();
                    }
                    else
                    {
                        clsGeneral.ShowMessage("Invalid Email Address.");
                        txtEmail.Focus();
                    }
                }
            }
            catch(Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void Frm_Gurantors_KeyDown(object sender, KeyEventArgs e)
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Delete.ToString());
                if (Rights == false)
                {
                    return;
                }
                if (GuarantorsId > 0)
                {
                    //DialogResult dr = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    //if (dr == DialogResult.Yes)
                    //{
                    //    string s = "Delete from GuarantorsMaster where G_Id=" + GuarantorsId + "";
                    //    ObjData.ExecuteQuery(s);
                    //    ClearAll();
                    //}
                }
            }
            catch(Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void Frm_Gurantors_Load(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
            }
            catch(Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = null;
            }
            catch(Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofDlg = new OpenFileDialog();
                ofDlg.Filter = "JPG Image(*.jpg)|*.jpg|GIF Image(*.gif)|*.gif|PNG Image(*.png)|*.png|BMP Image(*.bmp)|*.bmp|JPEG Image(*.jpeg)|*.jpeg";
                if (DialogResult.OK == ofDlg.ShowDialog())
                {
                    pictureBox1.Image = Image.FromFile(ofDlg.FileName);
                }
            }
            catch(Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        public bool Validation()
        {
            try
            {
                string s = "Select * from GuarantorsMaster where G_Code='" + txtCode.Text + "' and G_Id<>" + GuarantorsId + "";
                DataTable dt = ObjData.GetDataTable(s);
                if (dt.Rows.Count > 0)
                {
                    clsGeneral.ShowMessage("The Code Already Exists.");
                    txtCode.Focus();
                    return false;
                }
                if(txtName.Text.Trim()=="")
                {
                    clsGeneral.ShowMessage("Fill the Name First.");
                    txtName.Focus();
                    return false;
                }
            }
            catch(Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
            return true;
        }

        public void SaveData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ObjData.GetConnectionObj();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_GuarantorsMaster";
                cmd.Parameters.AddWithValue("@G_Id",GuarantorsId);
                cmd.Parameters.AddWithValue("@G_Code",txtCode.Text);
                cmd.Parameters.AddWithValue("@G_FirmName",txtFirmName.Text);
                cmd.Parameters.AddWithValue("@G_Name",txtName.Text);
                cmd.Parameters.AddWithValue("@G_FatherName",txtFatherName.Text);
                cmd.Parameters.AddWithValue("@G_Address1",txtAddress1.Text);
                cmd.Parameters.AddWithValue("@G_Address2",txtAddress2.Text);
                cmd.Parameters.AddWithValue("@G_City",txtCity.Text);
                cmd.Parameters.AddWithValue("@G_Pin",txtPin.Text);
                cmd.Parameters.AddWithValue("@G_Phone1",txtPhone1.Text);
                cmd.Parameters.AddWithValue("@G_Phone2",txtPhone2.Text);
                cmd.Parameters.AddWithValue("@G_Mobile1",txtMobile1.Text);
                cmd.Parameters.AddWithValue("@G_Mobile2",txtMobile2.Text);
                cmd.Parameters.AddWithValue("@G_Email",txtEmail.Text);
                cmd.Parameters.AddWithValue("@G_GuarantorsBank",txtGuarantorsBank.Text);
                cmd.Parameters.AddWithValue("@G_BankAddress",txtBankAddress.Text);
                cmd.Parameters.AddWithValue("@G_ChequeNo",txtChequeNo.Text);
                cmd.Parameters.AddWithValue("@G_Remarks",txtRemarks.Text);
                cmd.Parameters.AddWithValue("@G_Picture",ObjData.ImageToBase64(pictureBox1.Image));
                int a = cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }

        private void ShowData(int Id)
        {
            try
            {
                string s = "Select * from GuarantorsMaster where G_Id="+Id+"";
                DataTable dt = ObjData.GetDataTable(s);
                if(dt.Rows.Count>0)
                {
                    GuarantorsId = int.Parse(dt.Rows[0]["G_Id"].ToString());
                    txtCode.Text = dt.Rows[0]["G_Code"].ToString();
                    txtFirmName.Text = dt.Rows[0]["G_FirmName"].ToString();
                    txtName.Text = dt.Rows[0]["G_Name"].ToString();
                    txtFatherName.Text = dt.Rows[0]["G_FatherName"].ToString();
                    txtAddress1.Text = dt.Rows[0]["G_Address1"].ToString();
                    txtAddress2.Text = dt.Rows[0]["G_Address2"].ToString();
                    txtCity.Text = dt.Rows[0]["G_City"].ToString();
                    txtPin.Text = dt.Rows[0]["G_Pin"].ToString();
                    txtPhone1.Text = dt.Rows[0]["G_Phone1"].ToString();
                    txtPhone2.Text = dt.Rows[0]["G_Phone2"].ToString();
                    txtMobile1.Text = dt.Rows[0]["G_Mobile1"].ToString();
                    txtMobile2.Text = dt.Rows[0]["G_Mobile2"].ToString();
                    txtEmail.Text = dt.Rows[0]["G_Email"].ToString();
                    txtGuarantorsBank.Text = dt.Rows[0]["G_GuarantorsBank"].ToString();
                    txtBankAddress.Text = dt.Rows[0]["G_BankAddress"].ToString();
                    txtChequeNo.Text = dt.Rows[0]["G_ChequeNo"].ToString();
                    txtRemarks.Text = dt.Rows[0]["G_Remarks"].ToString();
                    pictureBox1.Image = ObjData.Base64ToImage(dt.Rows[0]["G_Picture"].ToString());
                }
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
                if(Validation()==true)
                {
                    SaveData();
                    ClearAll();
                }
            }
            catch(Exception ex)
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
                string s = "Select Isnull(max(G_Id),0) as G_Id from GuarantorsMaster";
                if (GuarantorsId > 0)
                {
                    s = "Select * from GuarantorsMaster where G_Id =(Select top 1 G_Id from GuarantorsMaster where G_Id <> " + GuarantorsId + " and G_Id < " + GuarantorsId + " order by G_Id desc)";
                }
                DataTable dt = ObjData.GetDataTable(s);
                if (dt.Rows.Count > 0)
                {
                    int Id = int.Parse(dt.Rows[0]["G_Id"].ToString());
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
                string s = "Select * from GuarantorsMaster where G_Id =(Select top 1 G_Id from GuarantorsMaster where G_Id <> " + GuarantorsId + " and G_Id > " + GuarantorsId + " order by G_Id asc)";
                DataTable dt = ObjData.GetDataTable(s);
                if (dt.Rows.Count > 0)
                {
                    int Id = int.Parse(dt.Rows[0]["G_Id"].ToString());
                    ShowData(Id);
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
                Frm_Find f = new Frm_Find();
                f.heading = "Guarantors Master";
                f.StatementPass = "";
                f.StatementPass = "Select G_Id As Id,G_Code As Code,G_FirmName As [Firm Name],G_Name As [Person Name],G_City As City,G_Pin As Pin,G_Mobile1 + ' ' + G_Mobile2 As Mobile,G_Address1 + ' ' + G_Address2 + ' ' + G_City + ' ' + G_Pin As Address From GuarantorsMaster Order By G_Code";
                f.ShowDialog();
                ShowData(f.Id);
            }
            catch(Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }
       

    }
}
