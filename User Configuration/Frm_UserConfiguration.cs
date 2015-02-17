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
    public partial class Frm_UserConfiguration : Form
    {
        public Frm_UserConfiguration()
        {
            InitializeComponent();
        }

        ClsDataAccess ObjData = new ClsDataAccess();
        int UserId = 0;

        public void LoadData(DataTable Dt)
        {
            try
            {
                bool MasterAdd = false;
                bool TransactionAdd = false;
                bool ReportsAdd = false;
                DgData.Rows.Clear();
                Array A = Enum.GetValues(typeof(ClsModuleName.ModuleName));
                for (int i = 0; i < A.Length; i++)
                {
                    bool CSave = false; bool CDelete = false; bool CPrint = false; bool CView = false;
                    string Name = A.GetValue(i).ToString();
                    int Id = Convert.ToInt32((ClsModuleName.ModuleName)Enum.Parse(typeof(ClsModuleName.ModuleName), Name));
                    if (Id > 100 && Id < 199)
                    {
                        if (MasterAdd == false)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColFId"].Value = 100;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColModuleName"].Value = "Master";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColSNo"].Value = "1";
                            DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                            DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                           
                             CSave = false;  CDelete = false;  CPrint = false;  CView = false;

                            if (Dt != null)
                            {
                                DataRow[] Dr = Dt.Select("U_MId = 100");
                                if (Dr.Length > 0)
                                {
                                    CSave = bool.Parse(Dr[0]["U_Save"].ToString());
                                    CDelete = bool.Parse(Dr[0]["U_Delete"].ToString());
                                    CPrint = bool.Parse(Dr[0]["U_Print"].ToString());
                                    CView = bool.Parse(Dr[0]["U_View"].ToString());
                                }
                            }

                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColSave"].Value = CSave;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColDelete"].Value = CDelete;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColPrint"].Value = CPrint;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColView"].Value = CView;

                            MasterAdd = true;
                        }
                    }
                    if (Id > 200 && Id < 299)
                    {
                        if (TransactionAdd == false)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColFId"].Value = 200;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColModuleName"].Value = "Transaction";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColSNo"].Value = "2";
                            DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                            DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            
                            CSave = false; CDelete = false; CPrint = false; CView = false;
                            
                            if (Dt != null)
                            {
                                DataRow[] Dr = Dt.Select("U_MId = 200");
                                if (Dr.Length > 0)
                                {
                                    CSave = bool.Parse(Dr[0]["U_Save"].ToString());
                                    CDelete = bool.Parse(Dr[0]["U_Delete"].ToString());
                                    CPrint = bool.Parse(Dr[0]["U_Print"].ToString());
                                    CView = bool.Parse(Dr[0]["U_View"].ToString());
                                }
                            }

                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColSave"].Value = CSave;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColDelete"].Value = CDelete;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColPrint"].Value = CPrint;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColView"].Value = CView;

                            TransactionAdd = true;
                        }
                    }
                    if (Id > 300 && Id < 399)
                    {
                        if (ReportsAdd == false)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColFId"].Value = 300;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColModuleName"].Value = "Reports";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColSNo"].Value = "3";
                            DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                            DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                            CSave = false; CDelete = false; CPrint = false; CView = false;
                            if (Dt != null)
                            {
                                DataRow[] Dr = Dt.Select("U_MId = 300");
                                if (Dr.Length > 0)
                                {
                                    CSave = bool.Parse(Dr[0]["U_Save"].ToString());
                                    CDelete = bool.Parse(Dr[0]["U_Delete"].ToString());
                                    CPrint = bool.Parse(Dr[0]["U_Print"].ToString());
                                    CView = bool.Parse(Dr[0]["U_View"].ToString());
                                }
                            }

                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColSave"].Value = CSave;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColDelete"].Value = CDelete;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColPrint"].Value = CPrint;
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColView"].Value = CView;

                            ReportsAdd = true;
                        }
                    }
                    DgData.Rows.Add();
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColFId"].Value = Id;
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColModuleName"].Value = Name;
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColModuleName"].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColModuleName"].Style.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    CSave = false;  CDelete = false;  CPrint = false;  CView = false;

                    if (Dt != null)
                    {
                        DataRow[] Dr = Dt.Select("U_MId = "+Id+"");
                        if (Dr.Length > 0)
                        {
                            CSave = bool.Parse(Dr[0]["U_Save"].ToString());
                            CDelete = bool.Parse(Dr[0]["U_Delete"].ToString());
                            CPrint = bool.Parse(Dr[0]["U_Print"].ToString());
                            CView = bool.Parse(Dr[0]["U_View"].ToString());
                        }
                    }

                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColSave"].Value = CSave;
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColDelete"].Value = CDelete;
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColPrint"].Value = CPrint;
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColView"].Value = CView;
                }
                DgData.ClearSelection();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void ShowData(int Id)
        {
            try
            {
                string SS = "SELECT  UserMaster.UId, ";
                SS = SS + " UserMaster.UName, ";
                SS = SS + " UserMaster.UPassword, ";
                SS = SS + " UserMaster.ULoginName, ";
                SS = SS + " UserConfiguration.U_MId, ";
                SS = SS + " UserConfiguration.U_Save, ";
                SS = SS + " UserConfiguration.U_Delete, ";
                SS = SS + " UserConfiguration.U_Print, ";
                SS = SS + " UserConfiguration.U_View ";
                SS = SS + " FROM UserMaster ";
                SS = SS + " Left JOIN UserConfiguration ON UserMaster.UId = UserConfiguration.U_UId Where UserMaster.UId = " + Id + "";
                DataTable Dt = ObjData.GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    UserId = Dt.Rows[0]["UId"].ToString().IntParse();
                    txtLoginName.Text = Dt.Rows[0]["ULoginName"].ToString();
                    txtName.Text = Dt.Rows[0]["UName"].ToString();
                    txtPassword.Text = Dt.Rows[0]["UPassword"].ToString();
                    LoadData(Dt);
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void Frm_UserConfiguration_Load(object sender, EventArgs e)
        {
            UserId = 0;
            txtLoginName.Clear();
            txtPassword.Clear();
            txtName.Clear();
            txtName.Focus();
            DataTable Dt = null;
            LoadData(Dt);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Frm_UserConfiguration_Load(new object(), new EventArgs());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Trim().Length == 0)
                {
                    clsGeneral.ShowMessage("Please Enter Name");
                    txtName.Focus();
                    return;
                }
                if (txtLoginName.Text.Trim().Length == 0)
                {
                    clsGeneral.ShowMessage("Please Enter Login Name");
                    txtLoginName.Focus();
                    return;
                }
                if (txtPassword.Text.Trim().Length == 0)
                {
                    clsGeneral.ShowMessage("Please Enter Password");
                    txtPassword.Focus();
                    return;
                }

                string sSQL = "Select * From UserMaster Where UId <> " + UserId + " and UName = '" + txtName.Text.Trim().SqlEncode() + "'";
                DataTable data = ObjData.GetDataTable(sSQL);
                if (data.Rows.Count > 0)
                {
                    clsGeneral.ShowMessage("This Name Already Exists");
                    txtName.SelectAll();
                    txtName.Focus();
                    return;
                }

                sSQL = "Select * From UserMaster Where UId <> " + UserId + " and ULoginName = '" + txtLoginName.Text.Trim().SqlEncode() + "'";
                data = ObjData.GetDataTable(sSQL);
                if (data.Rows.Count > 0)
                {
                    clsGeneral.ShowMessage("This Login Name Already Exists");
                    txtLoginName.SelectAll();
                    txtLoginName.Focus();
                    return;
                }

                string SS = "Delete UserConfiguration Where U_UId = " + UserId + "";
                ObjData.ExecuteQuery(SS);

                if (UserId > 0)
                {
                    SS = "Update UserMaster Set UName = '" + txtName.SqlEncode() + "',ULoginName = '" + txtLoginName.SqlEncode() + "',UPassword = '" + txtPassword.SqlEncode() + "' Where UId = " + UserId + "";
                    ObjData.ExecuteQuery(SS);
                }
                else
                {
                    SS = "Insert Into UserMaster(UName,ULoginName,UPassword)";
                    SS = SS + "Values('" + txtName.SqlEncode() + "','" + txtLoginName.SqlEncode() + "','" + txtPassword.SqlEncode() + "')";
                    UserId = ObjData.ExecuteQueryIdentity(SS);
                }

                for (int i = 0;i< DgData.Rows.Count; i++)
                {
                    int FId = DgData.Rows[i].Cells["ColFId"].EditedFormattedValue.ToString().IntParse();
                    bool Save = bool.Parse(DgData.Rows[i].Cells["ColSave"].EditedFormattedValue.ToString());
                    bool Delete = bool.Parse(DgData.Rows[i].Cells["ColDelete"].EditedFormattedValue.ToString());
                    bool Print = bool.Parse(DgData.Rows[i].Cells["ColPrint"].EditedFormattedValue.ToString());
                    bool View = bool.Parse(DgData.Rows[i].Cells["ColView"].EditedFormattedValue.ToString());

                    SS = "Insert Into UserConfiguration(U_UId,U_MId,U_Save,U_Delete,U_Print,U_View)";
                    SS = SS + "Values(" + UserId + "," + FId + ",'" + Save + "','" + Delete + "','" + Print + "','" + View + "')";
                    ObjData.ExecuteQuery(SS);
                }
                clsGeneral.ShowMessage("Save Successfully");
                btnClear_Click(new object(), new EventArgs());
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            Frm_Find F = new Frm_Find();
            F.StatementPass = "SELECT '' As SNo,UId As Id, UName As Name,  ULoginName As LoginName FROM UserMaster  Order By UName ";
            F.ShowDialog();
            if (F.Id > 0)
            {
                ShowData(F.Id);
            }
        }

        private void DgData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == ColSave.Index || e.ColumnIndex == ColDelete.Index ||
                    e.ColumnIndex == ColPrint.Index || e.ColumnIndex == ColView.Index)
                {
                    int Id = DgData.Rows[e.RowIndex].Cells["ColFId"].EditedFormattedValue.ToString().IntParse();
                    bool Val = bool.Parse(DgData.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString());
                    if (Id % 100 == 0)
                    {
                        for (int i = e.RowIndex+1;i< DgData.Rows.Count; i++)
                        {
                           int SId = DgData.Rows[i].Cells["ColFId"].EditedFormattedValue.ToString().IntParse();
                            if (SId > Id && SId < Id+100)
                            {
                                DgData.Rows[i].Cells[e.ColumnIndex].Value = Val;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }
    }
}
