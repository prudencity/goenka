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
    public partial class Frm_Find : Form
    {
        public Frm_Find()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();
        public int Id = 0;
        DataTable DtFull = new DataTable();
        public string StatementPass = "";
        public string heading = "";
        public bool IsDialog = true;
        public Form F ;

        public void FillData(string RowFilter)
        {
            try
            {
                DtFull.DefaultView.RowFilter = RowFilter;
                dgv.DataSource = DtFull.DefaultView;
                dgv.Columns["Id"].Visible = false;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.ClearSelection();
                lbl_totalrec.Text = "Total Records : " + dgv.Rows.Count.ToString();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void Frm_Find_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable Dt = ObjData.GetDataTable(StatementPass);
                DtFull = Dt;
                for (int i = 0; i < Dt.Columns.Count; i++)
                {
                    
                    if (Dt.Columns[i].ToString().ToUpperInvariant() != "ID")
                    {
                        cbSearchOn.Items.Add(Dt.Columns[i].ToString());
                        cbSearchOn.SelectedIndex = 0;
                    }
                }
                FillData("");
                lblHeading.Text = heading;
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }   
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.SelectedRows.Count == 1)
                {
                    Id = int.Parse(dgv.SelectedRows[0].Cells["Id"].EditedFormattedValue.ToString());
                }
                if (IsDialog == true)
                {
                    this.Close();
                }
               
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Filter = "";
                if (txtSearch.Text.Trim().Length == 0)
                {
                    FillData(Filter);
                }
                else
                {
                    Filter = " [" + cbSearchOn.SelectedItem.ToString() + "] Like '" +clsGeneral.SQLEncode (txtSearch.Text) + "%' ";
                    FillData(Filter);
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void dgv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                BtnShow_Click(new object(), new EventArgs());
            }
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            BtnShow_Click(new object(), new EventArgs());
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

    }
}
