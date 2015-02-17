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
    public partial class Frm_PenaltyReceived : Form
    {
        public Frm_PenaltyReceived()
        {
            InitializeComponent();
        }

        ClsDataAccess ObjData = new ClsDataAccess();
        public int Type = 1;
        int CashId = 0;
        float PenaltyRate = 0;
        double PreviousPenalty = 0;
        int PCriteria = 0;

        private void Frm_CashBank_Load(object sender, EventArgs e)
        {
            try
            {
                string SSQl = "Select P_Name + '-' + Convert(Varchar,L_FormNo) As LoanNo,L_Id As Id From LoanSanction Inner Join PartyMaster On P_Id = L_PartyId Order By L_FormNo";
                DataTable Dtdata = ObjData.GetDataTable(SSQl);
                cboSanctionNo.DisplayMember = "LoanNo";
                cboSanctionNo.ValueMember = "Id";
                cboSanctionNo.DataSource = Dtdata;
                if (cboSanctionNo.DataSource != null)
                {
                    cboSanctionNo.SelectedIndex = 0;
                }
                ClearAll();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void Frm_CashBank_KeyDown(object sender, KeyEventArgs e)
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

        public void DisplayGrid()
        {
            try
            {
                if (dg_Late.DataSource != null)
                {
                    dg_Late.Columns["C"].Visible = false;
                    dg_Late.Columns["CDate"].Visible = false;
                    dg_Late.Columns["Id"].Visible = false;
                    dg_Late.Columns["APenalty"].Visible = false;
                    dg_Late.Columns["No"].Width = 50;
                    dg_Late.Columns["Due Date"].Width = 80;
                    dg_Late.Columns["Penalty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dg_Late.Columns["Amount"].Width = 80;
                    dg_Late.Columns["Days Late"].Width = 80;

                    dg_Late.Columns["No"].ReadOnly = true;
                    dg_Late.Columns["Due Date"].ReadOnly = true;
                    dg_Late.Columns["Penalty"].ReadOnly = true;
                    dg_Late.Columns["Days Late"].ReadOnly = true;

                    dg_Late.Columns["Penalty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dg_Late.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    if (dg_Late.Rows.Count > 0)
                    {
                        PCriteria = int.Parse(dg_Late.Rows[0].Cells["C"].EditedFormattedValue.ToString().StringEmpty());
                    }

                }

                if (dg_Balance.DataSource != null)
                {
                    dg_Balance.Columns["CDate"].Visible = false;
                    dg_Balance.Columns["Id"].Visible = false;
                    dg_Balance.Columns["APenalty"].Visible = false;
                    dg_Balance.Columns["No"].Width = 50;
                    dg_Balance.Columns["Due Date"].Width = 80;
                    dg_Balance.Columns["Penalty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dg_Balance.Columns["Amount"].Width = 80;
                    dg_Balance.Columns["Days Late"].Width = 80;

                    dg_Balance.Columns["No"].ReadOnly = true;
                    dg_Balance.Columns["Due Date"].ReadOnly = true;
                    dg_Balance.Columns["Penalty"].ReadOnly = true;
                    dg_Balance.Columns["Days Late"].ReadOnly = true;

                    dg_Balance.Columns["Penalty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dg_Balance.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public void ClearAll()
        {
            try
            {
                PCriteria = 0;
                PenaltyRate = 0;
                CashId = 0;
                string SS = "Sp_GetPenaltyBalance 0";
                DataSet Ds = ObjData.GetDataSet(SS);
                dg_Late.DataSource = Ds.Tables[0];
                if (Ds.Tables.Count > 1)
                {
                    dg_Balance.DataSource = Ds.Tables[1];
                }
                DisplayGrid();
                txtAccountbalance.Text = "0.00";
                txtAddress.Clear();
                txtBankName.Clear();
                txtChequeDate.Clear();
                txtChequeno.Clear();
                txtContactNo.Clear();
                txtParty.Clear();
                txtRemarks.Clear();
                cboSanctionNo.Focus();
                PreviousPenalty = 0;
                dtpdate.Value = DateTime.Now;
                lblLate.Text = "Total : 0.00";
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                int ApplicationId = cboSanctionNo.ValueGet("Please Select Loan Sanction No");
                if (ApplicationId == 0)
                {
                    cboSanctionNo.Focus();
                    return;
                }
                string SS = "Select  ";
                SS = SS + " P_Name As Name,P_Address1 + ', ' + P_Address2 + ', ' + P_City + ', ' + P_Pin As Address, ";
                SS = SS + " P_Phone1 + ' ' + P_Phone2 + ' ' + P_Mobile1 + ' ' + P_Mobile2 As ContactNo, ";
                SS = SS + " L_PenaltyRate As PenaltyRate ";
                SS = SS + " From  LoanSanction  ";
                SS = SS + " Inner Join PartyMaster On P_Id =  L_PartyId  ";
                SS = SS + " Where L_Id = " + ApplicationId + " ";
                DataTable DtData = ObjData.GetDataTable(SS);
                if (DtData.Rows.Count > 0)
                {
                    txtAccountbalance.Text = ObjData.GetLoanbalanceStr(ApplicationId);
                    txtParty.Text = DtData.Rows[0]["Name"].ToString();
                    txtContactNo.Text = DtData.Rows[0]["ContactNo"].ToString();
                    txtAddress.Text = DtData.Rows[0]["Address"].ToString();
                    txtPenaltyRate.Text = DtData.Rows[0]["PenaltyRate"].ToString().StringEmpty();

                    

                    SS = "Sp_GetPenaltyBalance " + ApplicationId;
                    DataSet Ds = ObjData.GetDataSet(SS);
                    dg_Late.DataSource = Ds.Tables[0];
                    if (Ds.Tables.Count > 1)
                    {
                        dg_Balance.DataSource = Ds.Tables[1];
                    }
                    DisplayGrid();
                    dtpdate_ValueChanged(new object(), new EventArgs());
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void chkDocuments_ItemCheck(object sender, ItemCheckEventArgs e)
        {


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
                int ApplicationId = cboSanctionNo.ValueGet("Please Select Loan Sanction No");
                if (ApplicationId == 0)
                {
                    cboSanctionNo.Focus();
                    return;
                }

                dg_Balance.ClearSelection();
                dg_Late.ClearSelection();

                for (int i = 0; i < dg_Late.Rows.Count; i++)
                {
                    double Penalty = dg_Late.Rows[i].Cells["Penalty"].EditedFormattedValue.ToString().DoubleParse();
                    double Amount = dg_Late.Rows[i].Cells["Amount"].EditedFormattedValue.ToString().DoubleParse();
                    if (Amount > Penalty)
                    {
                        clsGeneral.ShowMessage("Amount Received is More Than Penalty");
                        dg_Late.CurrentCell = dg_Late[dg_Late.Columns["Amount"].Index, i];
                        dg_Late.Focus();
                        return;
                    }
                    if (Amount != Penalty)
                    {
                        clsGeneral.ShowMessage("You Cannot Paid Partially Penalty, Please Settle Full");
                        dg_Late.CurrentCell = dg_Late[dg_Late.Columns["Amount"].Index, i];
                        dg_Late.Focus();
                        return;
                    }
                }

                for (int i = 0; i < dg_Balance.Rows.Count; i++)
                {
                    double Penalty = dg_Balance.Rows[i].Cells["Penalty"].EditedFormattedValue.ToString().DoubleParse();
                    double Amount = dg_Balance.Rows[i].Cells["Amount"].EditedFormattedValue.ToString().DoubleParse();
                    if (Amount > Penalty)
                    {
                        clsGeneral.ShowMessage("Amount Received is More Than Penalty");
                        dg_Balance.CurrentCell = dg_Balance[dg_Balance.Columns["Amount"].Index, i];
                        dg_Balance.Focus();
                        return;
                    }
                    if (Amount != Penalty)
                    {
                        clsGeneral.ShowMessage("You Cannot Paid Partially Penalty, Please Settle Full");
                        dg_Balance.CurrentCell = dg_Balance[dg_Balance.Columns["Amount"].Index, i];
                        dg_Balance.Focus();
                        return;
                    }
                }


                DialogResult DrM = MessageBox.Show("Are Sure Want To Save Because Once Save Cannot Be Edited", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (DrM == DialogResult.No)
                {
                    return;
                }

                for (int i = 0; i < dg_Late.Rows.Count; i++)
                {
                    double Penalty = dg_Late.Rows[i].Cells["Penalty"].EditedFormattedValue.ToString().DoubleParse();
                    double Amount = dg_Late.Rows[i].Cells["Amount"].EditedFormattedValue.ToString().DoubleParse();
                    double EMINO = dg_Late.Rows[i].Cells["No"].EditedFormattedValue.ToString().DoubleParse();
                    double Id = dg_Late.Rows[i].Cells["Id"].EditedFormattedValue.ToString().DoubleParse();
                    
                    if (Amount > 0 && EMINO > 0 && Id > 0)
                    {
                        string SS = "Insert Into EMIPenaltyReceived(P_SId,P_Id,P_No,P_Date,P_Amount)";
                        SS = SS + "Values(" + ApplicationId + "," + Id + "," + EMINO + ",'" + dtpdate.DateFormat() + "'," + Penalty + ")";
                        ObjData.ExecuteQuery(SS);
                    }
                }

                for (int i = 0; i < dg_Balance.Rows.Count; i++)
                {
                    double Penalty = dg_Balance.Rows[i].Cells["Penalty"].EditedFormattedValue.ToString().DoubleParse();
                    double Amount = dg_Balance.Rows[i].Cells["Amount"].EditedFormattedValue.ToString().DoubleParse();
                    double EMINO = dg_Balance.Rows[i].Cells["No"].EditedFormattedValue.ToString().DoubleParse();
                    double Id = dg_Balance.Rows[i].Cells["Id"].EditedFormattedValue.ToString().DoubleParse();
                   
                    if (Amount > 0 && EMINO > 0 && Id > 0)
                    {
                        string SS = "Insert Into EMIBPenaltyReceived(P_SId,P_Id,P_No,P_Date,P_Amount)";
                        SS = SS + "Values(" + ApplicationId + "," + Id + "," + EMINO + ",'" + dtpdate.DateFormat() + "'," + Penalty + ")";
                        ObjData.ExecuteQuery(SS);
                    }
                }

                ClearAll();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnLedger_Click(object sender, EventArgs e)
        {
            int ApplicationId = cboSanctionNo.ValueGet("Please Select Loan Sanction No");
            if (ApplicationId == 0)
            {
                cboSanctionNo.Focus();
                return;
            }
            Frm_LoanLedger F = new Frm_LoanLedger();
            F.LoanId = ApplicationId;
            F.Show();
        }

        private void dtpdate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                double Total = 0;
                if (dg_Late.DataSource != null && PCriteria == 1)
                {
                    double PRate = txtPenaltyRate.Text.DoubleParse();
                    PRate = (PRate * 12) / 100;
                    for (int i = 0; i < dg_Late.Rows.Count; i++)
                    {
                        System.DateTime Dt = DateTime.Parse(dg_Late.Rows[i].Cells["CDate"].EditedFormattedValue.ToString());
                        double P = dg_Late.Rows[i].Cells["APenalty"].EditedFormattedValue.ToString().DoubleParse();
                        TimeSpan Ts = dtpdate.Value.Subtract(Dt);
                        int days = 0;
                        if (Ts.Days > 0)
                        {
                            dg_Late.Rows[i].Cells["Days Late"].Value = Ts.Days;
                            days = Ts.Days;
                        }
                        else
                        {
                            dg_Late.Rows[i].Cells["Days Late"].Value = "0";
                            days = 0;
                        }
                        double A = P * Math.Pow((1 + PRate / 365), days);
                        dg_Late.Rows[i].Cells["Penalty"].Value = clsGeneral.CurFormat(A);
                        Total = Total + A;
                    }
                }
                lblLate.Text = "Total : " + clsGeneral.CurFormat(Total) + "";

                Total = 0;
                if (dg_Balance.DataSource != null)
                {
                    double PRate = txtPenaltyRate.Text.DoubleParse();
                    PRate = (PRate * 12) / 100;
                    for (int i = 0; i < dg_Balance.Rows.Count; i++)
                    {
                        System.DateTime Dt = DateTime.Parse(dg_Balance.Rows[i].Cells["CDate"].EditedFormattedValue.ToString());
                        double P = dg_Balance.Rows[i].Cells["APenalty"].EditedFormattedValue.ToString().DoubleParse();
                        TimeSpan Ts = dtpdate.Value.Subtract(Dt);
                        int days = 0;
                        if (Ts.Days > 0)
                        {
                            dg_Balance.Rows[i].Cells["Days Late"].Value = Ts.Days;
                            days = Ts.Days;
                        }
                        else
                        {
                            dg_Balance.Rows[i].Cells["Days Late"].Value = "0";
                            days = 0;
                        }
                        double A = P * Math.Pow((1 + PRate / 365), days);
                        dg_Balance.Rows[i].Cells["Penalty"].Value = clsGeneral.CurFormat(A);
                        Total = Total + A;
                    }
                }
                lblbalance.Text = "Total : " + clsGeneral.CurFormat(Total) + "";
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }
        TextBox Tb_Numeric;
        private void dg_head_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                int Amount = dg_Late.Columns["Amount"].Index;
                if ( dg_Late.CurrentCell.ColumnIndex == Amount )
                {
                    e.CellStyle.BackColor = Color.White;
                    Tb_Numeric = (TextBox)e.Control;
                    Tb_Numeric.KeyPress += new KeyPressEventHandler(Tb_Numeric_KeyPress);
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }

        void Tb_Numeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int Amount = dg_Late.Columns["Amount"].Index;
                if ( dg_Late.CurrentCell.ColumnIndex == Amount )
                {
                    int q = dg_Late[dg_Late.CurrentCell.ColumnIndex, dg_Late.CurrentCell.RowIndex].EditedFormattedValue.ToString().IndexOf(".");
                    if (q == -1)
                    {
                        if (char.IsDigit(e.KeyChar) || e.KeyChar == 46 || char.IsControl(e.KeyChar))
                        {
                            e.Handled = false;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                        {
                            e.Handled = false;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }


        TextBox Tb_Numeric1;
        private void dg_Balance_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                int Amount = dg_Balance.Columns["Amount"].Index;
                if (dg_Balance.CurrentCell.ColumnIndex == Amount)
                {
                    e.CellStyle.BackColor = Color.White;
                    Tb_Numeric1 = (TextBox)e.Control;
                    Tb_Numeric1.KeyPress += new KeyPressEventHandler(Tb_Numeric1_KeyPress);
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }

        void Tb_Numeric1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int Amount = dg_Balance.Columns["Amount"].Index;
                if (dg_Balance.CurrentCell.ColumnIndex == Amount)
                {
                    int q = dg_Balance[dg_Balance.CurrentCell.ColumnIndex, dg_Balance.CurrentCell.RowIndex].EditedFormattedValue.ToString().IndexOf(".");
                    if (q == -1)
                    {
                        if (char.IsDigit(e.KeyChar) || e.KeyChar == 46 || char.IsControl(e.KeyChar))
                        {
                            e.Handled = false;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
                        {
                            e.Handled = false;
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                }
            }
            catch (Exception ex) { clsGeneral.ShowErrMsg(ex.Message); }
        }

        private void brnDelete_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Delete.ToString());
            if (Rights == false)
            {
                return;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            bool Rights = ClsModuleName.ChechRights(ClsLogin.LoginId, this.Tag, ClsModuleName.Rights.U_Print.ToString());
            if (Rights == false)
            {
                return;
            }
        }
    }
}

