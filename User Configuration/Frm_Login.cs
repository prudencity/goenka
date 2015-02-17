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
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        ClsDataAccess ObjData = new ClsDataAccess();

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string SS = "Select * From UserMaster Where ULoginName = '" + txtLoginName.SqlEncode() + "' And UPassword = '" + txtPassword.SqlEncode() + "'";
                DataTable Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    ClsLogin.LoginId = Dt.Rows[0]["UId"].ToString().IntParse();
                    ClsLogin.LoginName = Dt.Rows[0]["UName"].ToString();
                    this.Close();
                }
                else { clsGeneral.ShowMessage("Invalid User Name or Password"); }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }
    }
}
