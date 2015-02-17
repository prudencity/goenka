using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace Finance_Management_System
{
    public partial class Frm_SqlSettings : Form
    {
        ClsDataAccess ObjDataAccess = new ClsDataAccess();
        public bool ConnectionChange = false;
        public Frm_SqlSettings()
        {
            
            InitializeComponent();
        }
       
        private void frm_SqlSettings_Load(object sender, EventArgs e)
        {
            try
            {
                DoStartUpWork();
            }
            catch (Exception ex)
            { clsGeneral.ShowErrMsg(ex.Message); }         
        }

        public void DoStartUpWork()
        {
            cmb_Auth_Mode.SelectedIndex = 0;
            txt_password.Enabled = false;
            txt_userName.Enabled = false;
           
            bool chk_file = false;
            chk_file = System.IO.File.Exists("Finance.stl");

            if (chk_file == false)
            {
                StreamWriter SW;
                SW = File.CreateText("Finance.stl");
                SW.WriteLine(Program.connectionString);
                SW.Close();
            }
          



            if (chk_file == true)
            {
                StreamReader objReader = new StreamReader("Finance.stl");
                string connString = objReader.ReadLine();
                objReader.Close();
                if (connString.ToString() != "")
                {
                    string[] connSplit = connString.Split(';');

                    if (connSplit.Length == 6)
                    {
                        string [] DataSourceName = connSplit[0].Split('=');
                        string[] DataBaseName = connSplit[1].Split('=');
                        txt_servername.Text = DataSourceName[1].ToString();
                        cbodatabase.Text = DataBaseName[1].ToString();
                        cmb_Auth_Mode.SelectedIndex = 0;
                        ConnectionChange = true;
                    }

                    else if (connSplit.Length > 6)
                    {
                        string[] DataSourceName = connSplit[0].Split('=');
                        string[] DataBaseName = connSplit[1].Split('=');
                        string[] UserName = connSplit[2].Split('=');
                        string[] PassWord = connSplit[3].Split('=');
                        txt_servername.Text = DataSourceName[1].ToString();
                        cmb_Auth_Mode.SelectedIndex = 1;
                        txt_userName.Text = UserName[1].ToString();
                        txt_password.Text = PassWord[1].ToString();
                        cbodatabase.Text = DataBaseName[1].ToString();
                        ConnectionChange = true;
                    }
                   
                }
            }
        }

        private void frm_SqlSettings_KeyDown(object sender, KeyEventArgs e)
            {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    DialogResult dr = MessageBox.Show("Exit From This From", "Information", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            { clsGeneral.ShowErrMsg(ex.Message); }
        }

        private void frm_SqlSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                ObjDataAccess.Dispose();
            }
            catch (Exception ex)
            { clsGeneral.ShowErrMsg(ex.Message); }
        }

        
        private void cmb_Auth_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_Auth_Mode.SelectedIndex == 1)
            {
                txt_userName.Enabled = true;
                txt_password.Enabled = true;
            }
            else
            {
                txt_userName.Enabled = false;
                txt_password.Enabled = false;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_servername.Text == "")
                {
                    clsGeneral.ShowErrMsg("Please Fill Server Name");
                    return;
                }

                if (cbodatabase.SelectedValue.ToString().Trim().Length == 0)
                {
                    clsGeneral.ShowErrMsg("Please Select Database Name");
                    return;
                }

                if (cmb_Auth_Mode.SelectedIndex == 1)
                {
                    if (txt_password.Text == "")
                    {
                        clsGeneral.ShowErrMsg("Please Fill Password");
                        return;
                    }
                    if (txt_userName.Text == "")
                    {
                        clsGeneral.ShowErrMsg("Please Fill User Name");
                        return;
                    }
                    Program.connectionString = @"Data Source=" + txt_servername.Text + ";Initial Catalog=" + Program.DataBaseName + "" + cbodatabase.SelectedValue.ToString() + ";User id=" + txt_userName.Text + ";password=" + txt_password.Text + ";pooling=false; MultipleActiveResultSets = True; ";
                }

                else
                {
                    Program.connectionString = @"Data Source=" + txt_servername.Text + ";Initial Catalog=" + Program.DataBaseName + "" + cbodatabase.SelectedValue.ToString() + ";Trusted_Connection=Yes;pooling=false;MultipleActiveResultSets = True;";
                }

                



                ObjDataAccess.CreateConnection();
                clsGeneral.ShowMessage("Successfully Connected");

                StreamWriter SW = new StreamWriter("Finance.stl");
                SW.Write(Program.connectionString);
                SW.Close();
                SW.Dispose();

              
                this.Close();
                this.DialogResult = DialogResult.Yes;
            }
            catch (Exception ex)
            {
                clsGeneral.ShowErrMsg(ex.Message.ToString()); ;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbldatabase_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmb_Auth_Mode.SelectedIndex == 1)
                {
                    if (txt_password.Text == "")
                    {
                        clsGeneral.ShowErrMsg("Please Fill Password");
                        return;
                    }
                    if (txt_userName.Text == "")
                    {
                        clsGeneral.ShowErrMsg("Please Fill User Name");
                        return;
                    }
                    Program.connectionString = @"Data Source=" + txt_servername.Text + ";Initial Catalog=master;User id=" + txt_userName.Text + ";password=" + txt_password.Text + ";pooling=false; MultipleActiveResultSets = True; ";
                }
                else
                {
                    Program.connectionString = @"Data Source=" + txt_servername.Text + ";Initial Catalog=master;Trusted_Connection=Yes;pooling=false;MultipleActiveResultSets = True;";
                }
                ObjDataAccess.CloseConnection();
                cbodatabase.DataSource = null;
                ObjDataAccess.CreateConnection();
                string SSData = "SELECT Name FROM master..sysdatabases Order By Name";
                DataTable DtData = ObjDataAccess.GetDataTable(SSData);
                cbodatabase.DataSource = DtData;
                cbodatabase.DisplayMember = "Name";
                cbodatabase.ValueMember = "Name";
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

       
    }
}
