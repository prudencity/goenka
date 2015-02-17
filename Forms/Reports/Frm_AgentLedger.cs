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
    public partial class Frm_AgentLedger : Form
    {
        public Frm_AgentLedger()
        {
            InitializeComponent();
        }
        ClsDataAccess ObjData = new ClsDataAccess();
        public int AgentId = 0;

        public void GetChart()
        {
            try
            {
                if (AgentId == 0)
                {
                    return;
                }
                double Total = 0;
                string SS = "[Sp_AgentInterestComission] '19000101','" + dtpEnd.DateFormat() + "'";
                DataTable Dt = ObjData.GetDataTable(SS);
                Dt.DefaultView.RowFilter = "AgentCode = " + AgentId + "";
                DataTable D =  Dt.DefaultView.ToTable();


                // D.Delete("CDate < '" + dtpStart.Value.ToShortDateString() + "' And Iden = 0");
                D.Delete("CDate > '" + dtpEnd.Value.ToShortDateString() + "' And Iden = 0");
                D.AcceptChanges();
                dg_head.Rows.Clear();
                for (int i = 0; i < D.Rows.Count; i++)
                {
                    int Iden = D.Rows[i]["Iden"].ToString().IntParse();
                    string Particular = D.Rows[i]["Particular"].ToString();
                    string Date = D.Rows[i]["Date"].ToString();
                    double Amount = D.Rows[i]["Amount"].ToString().DoubleParse();
                    dg_head.Rows.Add();
                    dg_head.Rows[i].Cells["ColDate"].Value = Date;
                    dg_head.Rows[i].Cells["ColParticular"].Value = Particular;
                    if (Iden == 0)
                    {
                        dg_head.Rows[i].Cells["ColDrAmount"].Value = clsGeneral.CurFormat(Amount);
                        Total = Total + Amount;
                    }
                    else
                    {
                        dg_head.Rows[i].Cells["ColCrAmount"].Value = clsGeneral.CurFormat(Amount);
                        Total = Total - Amount;
                    }

                    if (Total > 0)
                    {
                        dg_head.Rows[i].Cells["ColBalance"].Value = clsGeneral.CurFormat(Math.Abs(Total)) + " Dr";
                        dg_head.Rows[i].Cells["ColBalance"].Style.ForeColor = Color.Green;
                    }
                    else if (Total < 0)
                    {
                        dg_head.Rows[i].Cells["ColBalance"].Value = clsGeneral.CurFormat(Math.Abs(Total)) + " Cr";
                        dg_head.Rows[i].Cells["ColBalance"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dg_head.Rows[i].Cells["ColBalance"].Value = "0.00";
                    }
                }

                clsGeneral.DatagridSort(dg_head);
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void Frm_LoanReturnChart_Load(object sender, EventArgs e)
        {
            string SSQl = "Select AgentId As Id,AgentName As Name From AgentMaster Order by AgentName";
            DataTable Dtdata = ObjData.GetDataTable(SSQl);
            CboNo.DisplayMember = "Name";
            CboNo.ValueMember = "Id";
            CboNo.DataSource = Dtdata;
            CboNo.NextControl = btnShow;
            CboNo.BindData();
            if (CboNo.DataSource != null)
            {
                CboNo.SelectedValue = AgentId;
            }
            GetChart();
        }

        public void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_View.ToString());
                if (Rights == false)
                {
                    return;
                }
                if (CboNo.SelectedValue != null)
                {
                    AgentId = int.Parse(CboNo.SelectedValue.ToString());
                    GetChart();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CboNo.SelectedValue = null;
            CboNo.Focus();
            dg_head.Rows.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Print.ToString());
            if (Rights == false)
            {
                return;
            }
            clsGeneral.ExcelExport(dg_head, "Agent Ledger of " + CboNo.SelectedText + "");
        }
    }
}
