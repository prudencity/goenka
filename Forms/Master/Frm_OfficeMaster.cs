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
    public partial class Frm_OfficeMaster : Form
    {
        public Frm_OfficeMaster()
        {
            InitializeComponent();
        }

        #region Member Variable
        ClsDataAccess ObjData = new ClsDataAccess();
        public int OfficeId = 0;
        DataTable Dt = new DataTable();
        #endregion

        #region Fuctions

        #region ClearAll
        public void ClearAll()
        {
            try
            {
                OfficeId = 0;
                txtName.Text = "";
                txtAddress.Text = "";
                txtContact.Text = "";
                txtName.Focus();
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message);
            }
        }
        #endregion

        #region LoadData
        public void LoadData()
        {
            try
            {
                string GetData = "Select OfficeName,OfficeId from OfficeMaster  order by OfficeName";
                Dt = ObjData.GetDataTable(GetData);
                txtCitysearch.DataSource = Dt;
                txtCitysearch.DisplayMember = "OfficeName";
                txtCitysearch.ValueMember = "OfficeId";
                txtCitysearch.BindData();
                txtCitysearch.NextControl = txtName;
                if (Dt.Rows.Count <= 0)
                {
                    PnlSearch.Visible = false;
                    txtName.Focus();
                    clsGeneral.ShowMessage("No Record Found");
                }
                else
                {
                    PnlSearch.Visible = true;
                    txtCitysearch.Focus();
                }
                txtCitysearch.SelectedValue = null;
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region SaveData
        public void SaveData()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = ObjData.GetConnectionObj();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_OfficeMaster";
                cmd.Parameters.AddWithValue("@OfficeId", OfficeId);
                cmd.Parameters.AddWithValue("@OfficeName", clsGeneral.SQLEncode(txtName.Text.Trim()));
                cmd.Parameters.AddWithValue("@OfficeAddress", clsGeneral.SQLEncode(txtAddress.Text.Trim()));
                cmd.Parameters.AddWithValue("@OfficeContactNo", clsGeneral.SQLEncode(txtContact.Text.Trim()));
                int a = cmd.ExecuteNonQuery();
            }
            catch (Exception Erp) { clsGeneral.ShowErrMsg(Erp.Message); }
        }
        #endregion

        #region DeleteRecord
        public void DeleteRecord(int Id)
        {
            try
            {
                DialogResult result = MessageBox.Show("Are You Sure You Want To Delete This Record", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    string Delete = "Delete from OfficeMaster where OfficeId=" + OfficeId+"";
                    ObjData.ExecuteQuery(Delete);
                    ClearAll();
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

      
        #endregion

        #region BtnClear (Click)
        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region BtnExit (Click)
        private void BtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region Validation (Function)
        public bool Validation()
        {
            try
            {
                if (txtName.Text.Trim() == "")
                {
                    clsGeneral.ShowMessage("Please Enter The Agent Name");
                    txtName.Focus();
                    return false;
                }
                string s = "Select * from OfficeMaster where OfficeName='" + txtName.Text + "' and OfficeId !=" + OfficeId + "";
                DataTable dt = ObjData.GetDataTable(s);
                if(dt.Rows.Count>0)
                {
                    clsGeneral.ShowMessage("Office Name already exists");
                    txtName.Focus();
                    return false;
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
            return true;
        }
        #endregion

        #region BtnSave (Click)
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Save.ToString());
                if (Rights == false)
                {
                    return;
                }
                if (Validation() == true)
                {
                    SaveData();
                    ClearAll();
                    //AutoFill();
                    //LastCode("");
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region Frm_CityMaster (KeyDown)
        private void Frm_CityMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Escape)
                {
                    if (PnlSearch.Visible == true)
                    {
                        txtName.Focus();
                        PnlSearch.Visible = false;
                        return;
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Are You Sure You Want To Exit", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                        {
                            this.Close();
                        }
                    }
                }
                if (e.KeyData == Keys.Enter)
                {
                    this.SelectNextControl(this.ActiveControl, true, true, true, true);
                    return;
                }
                if (e.KeyData == Keys.F3)
                {
                    if (PnlSearch.Visible == false)
                    {
                        BtnFind_Click(this, e);
                        return;
                    }
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region BtnFind (Click)
        private void BtnFind_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
                if (Rights == false)
                {
                    return;
                }
                if (PnlSearch.Visible == false)
                {
                    LoadData();
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region Btn Delete (Click)
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Delete.ToString());
                if (Rights == false)
                {
                    return;
                }
                //if (OfficeId > 0)
                //{
                //    DeleteRecord(OfficeId);
                //    ClearAll();
                    
                //}
                //else
                //{
                //    txtName.Focus();
                //    clsGeneral.ShowMessage("Please Edit A Record Before Deleting");
                //}
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtCity (Leave)
        private void txtCityLeave(object sender, EventArgs e)
        {
            try
            {
                txtName.Text = clsGeneral.CapitalCaseString(txtName.Text);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region Frm_CityMaster (Load)
        private void Frm_CityMaster_Load(object sender, EventArgs e)
        {
            try
            {
                PnlSearch.Height = 168;
                ClearAll();
            }
            catch(Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtCitysearch (ValueChanged)
        private void txtCitysearch_ValueChanged(object Value)
        {
            try
            {
                string Getdata = "select * from OfficeMaster where OfficeId=" + clsGeneral.SQLEncode(Value.ToString()) + "";
                ObjData.GetDataTable(Getdata);
                DataTable Dt = ObjData.GetDataTable(Getdata);
                if (Dt.Rows.Count > 0)
                {
                    //Lbl_Mode.Text = "Edit City";
                    PnlSearch.Visible = false;
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        OfficeId = Int32.Parse(Dt.Rows[i]["OfficeId"].ToString());
                        txtName.Text = Dt.Rows[i]["OfficeName"].ToString();
                        txtAddress.Text = Dt.Rows[i]["OfficeAddress"].ToString();
                        txtContact.Text = Dt.Rows[i]["OfficeContactNo"].ToString();
                        txtName.SelectionStart = txtName.Text.Length;
                    }
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtState (Leave)
        private void txtState_Leave(object sender, EventArgs e)
        {
            try
            {
                txtAddress.Text = clsGeneral.CapitalCaseString(txtAddress.Text);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion

        #region txtCountry (Leave)
        private void txtCountry_Leave(object sender, EventArgs e)
        {
            try
            {
                txtContact.Text = clsGeneral.CapitalCaseString(txtContact.Text);
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }
        #endregion
    }
}