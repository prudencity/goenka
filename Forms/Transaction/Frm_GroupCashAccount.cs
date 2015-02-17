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
    public partial class Frm_GroupCashAccount : Form
    {
        public Frm_GroupCashAccount()
        {
            InitializeComponent();
        }
        bool SmartSearch = true;
        ClsDataAccess ObjData = new ClsDataAccess();

        private void Frm_GroupCashAccount_Load(object sender, EventArgs e)
        {
            lblCoName.Text = clsGeneral.CompanyName;
            try
            {
                string SSdata = "Select P_Id As Id,P_Name As Name From PartyMaster Order By P_Name ";
                DataTable Dt = ObjData.GetDataTable(SSdata);
                LstItem.DataSource = Dt;
                LstItem.DisplayMember = "Name";
                LstItem.ValueMember = "Id";
                ColTName.ColumnIndex = ColTName.Index;
                ColTName.NextColumnIndex = ColTName.Index;
                ColTName.PrevColumnIndex = ColTName.Index;
                ColTName.ListControl = LstItem;
                ColTName.ShouldOpenLast = true;
                ColTName.SmartSearch = !SmartSearch;
                LstItem.BringToFront();

                DgvTransport.Rows.Clear();
                DgvTransport.Rows.Add();
                string SS = "Select AcId,P_Name From GroupCashBook Inner Join PartyMaster On P_Id = AcId";
                DataTable DtGroup = ObjData.GetDataTable(SS);
                for (int i = 0; i < DtGroup.Rows.Count; i++)
                {
                    DgvTransport.Rows.Add();
                    DgvTransport.Rows[i].Cells["ColTId"].Value = DtGroup.Rows[i]["AcId"].ToString();
                    DgvTransport.Rows[i].Cells["ColTName"].Value = DtGroup.Rows[i]["P_Name"].ToString();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void LstItem_ValueChanged(object Value)
        {
            try
            {
                if (Value != null)
                {
                    DataTable Dt = (DataTable)LstItem.DataSource;
                    DataRow[] Dr = null;
                    Dr = Dt.Select("Id = " + Value.ToString() + "");
                    if (Dr.Length > 0)
                    {
                        DgvTransport.CurrentRow.Cells["ColTId"].Value = Value.ToString();
                        DgvTransport.CurrentRow.Cells["ColTName"].Value = Dr[0]["Name"].ToString();
                        DgvTransport.Rows.Add();
                        DgvTransport.ClearSelection();
                        DgvTransport.CurrentCell = DgvTransport[ColTName.Index, DgvTransport.Rows.Count - 1];
                        DgvTransport.Focus();
                        DgvTransport.CurrentCell = DgvTransport[ColTName.Index, DgvTransport.Rows.Count - 1];
                        DgvTransport.Focus();
                    }

                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void DgvTransport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (DgvTransport.CurrentCell.ColumnIndex == ColTName.Index)
                {
                    for (int q = 0; q < DgvTransport.ColumnCount; q++)
                    {
                        DgvTransport.CurrentRow.Cells[q].Value = "";
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string SS = "Delete GroupCashBook";
            ObjData.ExecuteQuery(SS);

            for (int i = 0; i < DgvTransport.Rows.Count; i++)
            {
                int Id = DgvTransport.Rows[i].Cells["ColTId"].EditedFormattedValue.ToString().IntParse();
                if (Id != 0)
                {
                    SS = "Insert into GroupCashBook(AcId)Values(" + Id + ")";
                    ObjData.ExecuteQuery(SS);
                }
            }
            clsGeneral.ShowMessage("Save Successfully");
        }
    }
}
