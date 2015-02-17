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
    public partial class Frm_CashBook : Form
    {
        public Frm_CashBook()
        {
            InitializeComponent();
        }

        ClsDataAccess ObjData = new ClsDataAccess();

        private void btnClear_Click(object sender, EventArgs e)
        {
            DgData.Rows.Clear();
        }

        private void Frm_NewCashBook_Load(object sender, EventArgs e)
        {
            lblCoName.Text = clsGeneral.CompanyName;
            //DtpStart.MinDate = Program.FromDate.Date;
            //DtpStart.MaxDate = Program.FromDate.AddYears(1).AddDays(-1).Date;
            //lblYear.Text = Program.FromDate.Year.ToString() + " - " + Program.ToDate.Year.ToString();
            //DtpStart.Value = DtpEnd.SetDate();
            //DtpEnd.Value = DtpEnd.SetDate();
            btnClear_Click(new object(), new EventArgs());
            Frm_NewCashBook_Resize(new object(), new EventArgs());
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                bool IsRemarks = chkNarration.Checked;
                string SS = "Select Cash, AId, [Voucher No], ";
                SS = SS + " CDate,Date, ";
                SS = SS + " Particular, ";
                SS = SS + " [Amount Cr],[Amount Dr],Remarks From Vw_Voucher ";
                SS = SS + " Where Vr_Date   <= Convert(DateTime,'" + DtpEnd.DateFormat() + "') ";
                SS = SS + " Order By Vr_Date,Vr_No,ACNAME";
                DataTable DtData = ObjData.GetDataTable(SS);

                if (radioAll.Checked == false)
                {
                    double CheckDr = 0;
                    double CheckCr = 0;
                    if (radioCash.Checked == true)
                    {
                        DtData.Delete("Cash = 1");
                    }
                    if (radioCredit.Checked == true)
                    {
                        DtData.Delete("Cash = 0");
                    }
                }

                double OpBalance = 0;
                if (chkWithBalance.Checked == true)
                {
                    double OpCr = DtData.Compute("Sum([Amount Cr])", "CDate < " + DtpStart.DateFormat() + "").ToString().DoubleParse();
                    double OpDr = DtData.Compute("Sum([Amount Dr])", "CDate < " + DtpStart.DateFormat() + "").ToString().DoubleParse();
                    OpBalance = OpDr - OpCr;
                }

                DtData.Delete("CDate < " + DtpStart.DateFormat() + "");
                
                DtData.AcceptChanges();

                SS = "Select AcId,P_Name From GroupCashBook Inner Join PartyMaster On P_Id = AcId";
                DataTable DtGroup = ObjData.GetDataTable(SS);

                int DrIndex = 0;
                int CrIndex = 0;
                DgData.Rows.Clear();
                int DaysDiff = DtpEnd.Value.Subtract(DtpStart.Value).Days.ToString().IntParse();
                for (int i = 0; i <= DaysDiff; i++)
                {
                    System.DateTime Dtp = DtpStart.Value.AddDays(i);
                    string DayName = Dtp.ToString("dd/MM/yyyy   ddddd");

                    #region Days
                    DgData.Rows.Add();
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColCrParticular"].Value = DayName;
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColDrParticular"].Value = DayName;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Teal;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.Teal;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.White;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    #endregion

                    double TotalDr = 0;
                    double TotalCr = 0;
                    double DiffAmount = 0;
                    double TotalAmount = 0;
                    int Index = 0;
                    int ParticularIndex = 0;
                    int RemarksIndex = 0;
                    int AmountIndex = 0;
                    int VoucherIndex = 0;
                    double Amount = 0;

                    #region Opbalance
                    if (OpBalance > 0)
                    {
                        Amount = Math.Abs(OpBalance);
                        ParticularIndex = ColCrParticular.Index;
                        AmountIndex = ColCrAmount.Index;
                        VoucherIndex = ColCrVNo.Index;
                        TotalCr = Amount;
                    }
                    else
                    {
                        Amount = Math.Abs(OpBalance);
                        ParticularIndex = ColDrParticular.Index;
                        AmountIndex = ColDrAmount.Index;
                        VoucherIndex = ColDrVNo.Index;
                        TotalDr = Amount;
                    }

                    DgData.Rows.Add();
                    DgData.Rows[DgData.Rows.Count - 1].Cells[ParticularIndex].Value = "Balance Amount B/D";
                    DgData.Rows[DgData.Rows.Count - 1].Cells[AmountIndex].Value = Math.Abs(OpBalance).ToString("N2");//.CurrFormat();
                    DgData.Rows[DgData.Rows.Count - 1].Cells[VoucherIndex].Value = "";
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.Red;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    #endregion

                    DrIndex = DgData.Rows.Count;
                    CrIndex = DgData.Rows.Count;

                    #region Group
                    DataTable DtGroupTotal = new DataTable();
                    DtGroupTotal.Columns.Add("AId", typeof(int));
                    DtGroupTotal.Columns.Add("Particular");
                    DtGroupTotal.Columns.Add("Amount", typeof(double));

                    for (int a = 0; a < DtGroup.Rows.Count; a++)
                    {
                        DtGroupTotal.Rows.Add(DtGroup.Rows[a]["AcId"].ToString(), DtGroup.Rows[a]["P_name"].ToString(), 0);
                    }

                    for (int k = 0; k < DtGroupTotal.Rows.Count; k++)
                    {
                        int Acid = DtGroupTotal.Rows[k]["AId"].ToString().IntParse();
                        string AcName = DtGroupTotal.Rows[k]["Particular"].ToString();
                        double AmtDr = DtData.Compute("Sum([Amount Cr])", "AId = " + Acid + " And CDate = '" + Dtp.ToString("yyyyMMdd") + "'").ToString().DoubleParse();
                        double AmtCr = DtData.Compute("Sum([Amount Dr])", "AId = " + Acid + " And CDate = '" + Dtp.ToString("yyyyMMdd") + "'").ToString().DoubleParse();

                        double Amt = 0;
                        if (AmtDr > 0)
                        {
                            Amt = AmtDr;
                            TotalDr = TotalDr + Math.Abs(Amt);
                            Index = DrIndex;
                            Amount = Math.Abs(Amt);
                            ParticularIndex = ColDrParticular.Index;
                            AmountIndex = ColDrAmount.Index;
                            VoucherIndex = ColDrVNo.Index;
                            if (Index >= DgData.Rows.Count)
                            {
                                DgData.Rows.Add();
                            }
                            DgData.Rows[Index].Cells[ParticularIndex].Value = "***" + AcName;
                            DgData.Rows[Index].Cells[AmountIndex].Value = Amount.ToString("N2");//.CurrFormat();
                            DgData.Rows[Index].Cells[ParticularIndex].Style.ForeColor = Color.Black;
                            DgData.Rows[Index].Cells[ParticularIndex].Style.SelectionForeColor = Color.Black;
                            DgData.Rows[Index].Cells[ParticularIndex].Style.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            DgData.Rows[Index].Cells[AmountIndex].Style.ForeColor = Color.Black;
                            DgData.Rows[Index].Cells[AmountIndex].Style.SelectionForeColor = Color.Black;
                            DgData.Rows[Index].Cells[AmountIndex].Style.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            DrIndex = DrIndex + 1;
                        }

                        if (AmtCr > 0)
                        {
                            Amt = AmtCr;
                            TotalCr = TotalCr + Math.Abs(Amt);
                            Index = CrIndex;
                            Amount = Math.Abs(Amt);
                            ParticularIndex = ColCrParticular.Index;
                            AmountIndex = ColCrAmount.Index;
                            VoucherIndex = ColCrVNo.Index;
                            if (Index >= DgData.Rows.Count)
                            {
                                DgData.Rows.Add();
                            }
                            DgData.Rows[Index].Cells[ParticularIndex].Value = "***" + AcName;
                            DgData.Rows[Index].Cells[AmountIndex].Value = Amount.ToString("N2");//.CurrFormat();
                            DgData.Rows[Index].Cells[ParticularIndex].Style.ForeColor = Color.Black;
                            DgData.Rows[Index].Cells[ParticularIndex].Style.SelectionForeColor = Color.Black;
                            DgData.Rows[Index].Cells[ParticularIndex].Style.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            DgData.Rows[Index].Cells[AmountIndex].Style.ForeColor = Color.Black;
                            DgData.Rows[Index].Cells[AmountIndex].Style.SelectionForeColor = Color.Black;
                            DgData.Rows[Index].Cells[AmountIndex].Style.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            CrIndex = CrIndex + 1;
                        }
                    }
                    #endregion

                    #region Details
                    DataRow[] Dr = DtData.Select("CDate = '" + Dtp.ToString("yyyyMMdd") + "'");
                    for (int j = 0; j < Dr.Length; j++)
                    {
                        int AcId = Dr[j]["AId"].ToString().IntParse();

                        DataRow[] DD = DtGroup.Select("AcId = " + AcId + "");
                        if (DD.Length == 0)
                        {
                            double CrAmount = Dr[j]["Amount Dr"].ToString().DoubleParse();
                            double DrAmount = Dr[j]["Amount Cr"].ToString().DoubleParse();
                            string Name = Dr[j]["Particular"].ToString();
                            string VNo = Dr[j]["Voucher No"].ToString();
                            string Remarks = Dr[j]["Remarks"].ToString();
                            Amount = 0;
                            Index = 0;
                            ParticularIndex = 0;
                            AmountIndex = 0;
                            VoucherIndex = 0;
                            if (VNo == "0")
                            {
                                VNo = "";
                            }

                            if (CrAmount > 0)
                            {
                                TotalCr = TotalCr + CrAmount;
                                Index = CrIndex;
                                Amount = CrAmount;
                                ParticularIndex = ColCrParticular.Index;
                                RemarksIndex = ColCrRemarks.Index;
                                AmountIndex = ColCrAmount.Index;
                                VoucherIndex = ColCrVNo.Index;
                            }
                            else
                            {
                                TotalDr = TotalDr + DrAmount;
                                Index = DrIndex;
                                Amount = DrAmount;
                                ParticularIndex = ColDrParticular.Index;
                                RemarksIndex = ColDrRemarks.Index;
                                AmountIndex = ColDrAmount.Index;
                                VoucherIndex = ColDrVNo.Index;
                            }

                            if (Index >= DgData.Rows.Count)
                            {
                                DgData.Rows.Add();
                            }
                            DgData.Rows[Index].Cells[ParticularIndex].Value = Name;
                            DgData.Rows[Index].Cells[AmountIndex].Value = Amount.ToString("N2");//.CurrFormat();
                            DgData.Rows[Index].Cells[VoucherIndex].Value = VNo;

                            if (VNo.Trim().Length > 0)
                            {
                                double CheckDr = 0; double CheckCr = 0;
                                CheckDr = DtData.Compute("Sum([Amount Dr])", " [Voucher No] = '" + VNo + "'").ToString().DoubleParse();
                                CheckCr = DtData.Compute("Sum([Amount Cr])", " [Voucher No] = '" + VNo + "'").ToString().DoubleParse();
                                if (CheckDr == CheckCr)
                                {
                                    DgData.Rows[Index].Cells[ParticularIndex].Style.ForeColor = pnlColor.BackColor;
                                    DgData.Rows[Index].Cells[AmountIndex].Style.ForeColor = pnlColor.BackColor;
                                    DgData.Rows[Index].Cells[VoucherIndex].Style.ForeColor = pnlColor.BackColor;

                                    DgData.Rows[Index].Cells[ParticularIndex].Style.SelectionForeColor = pnlColor.BackColor;
                                    DgData.Rows[Index].Cells[AmountIndex].Style.SelectionForeColor = pnlColor.BackColor;
                                    DgData.Rows[Index].Cells[VoucherIndex].Style.SelectionForeColor = pnlColor.BackColor;
                                }
                            }


                            if (IsRemarks == true)
                            {
                                DgData.Rows[Index].Cells[RemarksIndex].Value = Remarks;
                            }

                            if (CrAmount > 0)
                            {
                                CrIndex = CrIndex + 1;
                            }
                            else
                            {
                                DrIndex = DrIndex + 1;
                            }
                        }
                    }
                    #endregion

                    #region Closing
                    if (TotalDr > TotalCr)
                    {
                        TotalAmount = TotalDr;
                        DiffAmount = TotalCr - TotalDr;

                        ParticularIndex = ColCrParticular.Index;
                        AmountIndex = ColCrAmount.Index;
                        VoucherIndex = ColCrVNo.Index;

                    }
                    else if (TotalDr < TotalCr)
                    {
                        TotalAmount = TotalCr;
                        DiffAmount = TotalCr - TotalDr;

                        ParticularIndex = ColDrParticular.Index;
                        AmountIndex = ColDrAmount.Index;
                        VoucherIndex = ColDrVNo.Index;
                    }


                    if (DiffAmount == 0)
                    {
                        TotalAmount = TotalCr;
                        if (OpBalance > 0)
                        {
                            ParticularIndex = ColDrParticular.Index;
                            AmountIndex = ColDrAmount.Index;
                            VoucherIndex = ColDrVNo.Index;
                            TotalCr = Amount;
                        }
                        else
                        {
                            ParticularIndex = ColCrParticular.Index;
                            AmountIndex = ColCrAmount.Index;
                            VoucherIndex = ColCrVNo.Index;

                        }
                    }


                    DgData.Rows.Add();
                    Index = DgData.Rows.Count - 1;
                    DgData.Rows[Index].Cells[ParticularIndex].Value = "Balance Amount C/D";
                    DgData.Rows[Index].Cells[AmountIndex].Value = Math.Abs(DiffAmount).ToString("N2");//.CurrFormat();
                    DgData.Rows[Index].Cells[VoucherIndex].Value = "";
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.Red;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    DgData.Rows.Add();
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColCrParticular"].Value = "Total Amount";
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColDrParticular"].Value = "Total Amount";
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColCrAmount"].Value = TotalAmount.ToString("N2");//.CurrFormat(); ;
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColDrAmount"].Value = TotalAmount.ToString("N2");//.CurrFormat(); ;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Teal;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.Teal;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
                    DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.White;
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColCrParticular"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColDrParticular"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    OpBalance = DiffAmount;
                    #endregion
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void Frm_NewCashBook_Resize(object sender, EventArgs e)
        {
            lblreceipt.Size = new Size(ColCrAmount.Width + ColCrParticular.Width + ColCrVNo.Width, lblreceipt.Height);
            lblpayment.Size = new Size(ColDrAmount.Width + ColDrParticular.Width + ColDrVNo.Width, lblpayment.Height);

            lblpayment.Location = new Point(lblreceipt.Width, 0);
        }

        private void Frm_NewCashBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (pnlNarration.Visible == true)
                {
                    pnlNarration.Visible = false;
                    return;
                }
            }
            if (e.KeyCode == Keys.F2)
            {
                pnlNarration.Visible = true;
            }
        }

        private void DgData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string Remarks = "";
                if (e.ColumnIndex == ColCrAmount.Index || e.ColumnIndex == ColCrParticular.Index || e.ColumnIndex == ColCrVNo.Index)
                {
                    Remarks = DgData.Rows[e.RowIndex].Cells["ColCrRemarks"].EditedFormattedValue.ToString();
                }
                else
                {
                    Remarks = DgData.Rows[e.RowIndex].Cells["ColDrRemarks"].EditedFormattedValue.ToString();
                }
                txtnarration.Text = Remarks;
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void chkVoucher_CheckedChanged(object sender, EventArgs e)
        {
            ColCrVNo.Visible = chkVoucher.Checked;
            ColDrVNo.Visible = chkVoucher.Checked;
        }

        private void pnlColor_Click(object sender, EventArgs e)
        {
            ColorDialog opd = new ColorDialog();
            DialogResult Dr = opd.ShowDialog();
            if (Dr == DialogResult.OK)
            {
                pnlColor.BackColor = opd.Color;
                btnShow_Click(new object(), new EventArgs());
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Col1");//VNo
            Dt.Columns.Add("ParticularCr");//Part
            Dt.Columns.Add("Amount1Cr");//Amount
            Dt.Columns.Add("Amount2Cr");//Remarks

            Dt.Columns.Add("Col2"); //VNo
            Dt.Columns.Add("ParticularDr");//Part
            Dt.Columns.Add("Amount1Dr");//Amount
            Dt.Columns.Add("Amount2Dr");//Remarks

            for (int i = 0; i < DgData.Rows.Count; i++)
            {
                string CrVNo = DgData.Rows[i].Cells["ColCrVNo"].EditedFormattedValue.ToString();
                string CrPart = DgData.Rows[i].Cells["ColCrParticular"].EditedFormattedValue.ToString();
                string CrAmount = DgData.Rows[i].Cells["ColCrAmount"].EditedFormattedValue.ToString();
                string CrRem = DgData.Rows[i].Cells["ColCrRemarks"].EditedFormattedValue.ToString();

                string DrVNo = DgData.Rows[i].Cells["ColDrVNo"].EditedFormattedValue.ToString();
                string DrPart = DgData.Rows[i].Cells["ColDrParticular"].EditedFormattedValue.ToString();
                string DrAmount = DgData.Rows[i].Cells["ColDrAmount"].EditedFormattedValue.ToString();
                string DrRem = DgData.Rows[i].Cells["ColDrRemarks"].EditedFormattedValue.ToString();

                if (chkVoucher.Checked == false)
                {
                    CrVNo = ""; DrVNo = "";
                }

                if (chkNarration.Checked == false)
                {
                    DrRem = ""; CrRem = "";
                }

                Dt.Rows.Add(CrVNo, CrPart, CrAmount, CrRem, DrVNo, DrPart, DrAmount, DrRem);
            }

            //FORM.NewAccounting.NewCashBook S = new Finance_Management_System.FORM.NewAccounting.NewCashBook();
            //#region Code Of Company

            //CrystalDecisions.CrystalReports.Engine.TextObject ObjHeading;
            //ObjHeading = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section1.ReportObjects["txtHeading"];
            //ObjHeading.Text = "Cash Book From " + DtpStart.Value.ToString("dd/MM/yyyy") + " To " + DtpEnd.Value.ToString("dd/MM/yyyy");

            //CrystalDecisions.CrystalReports.Engine.TextObject ObjCompany;
            //ObjCompany = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section1.ReportObjects["txtCompany"];
            //ObjCompany.Text = clsGeneral.CompanyName;
            //ObjCompany.ApplyFont(Program.FontStyle);

            //CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd1;
            //ObjAdd1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section1.ReportObjects["txtAddress1"];
            //ObjAdd1.Text = clsGeneral.Address;

            //CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd2;
            //ObjAdd2 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section1.ReportObjects["txtAddress2"];
            //ObjAdd2.Text = clsGeneral.Address2;

            //CrystalDecisions.CrystalReports.Engine.TextObject ObjCity;
            //ObjCity = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section1.ReportObjects["txtCity"];
            //ObjCity.Text = clsGeneral.City;

            //CrystalDecisions.CrystalReports.Engine.TextObject ObjContact;
            //ObjContact = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section1.ReportObjects["txtContact"];
            //ObjContact.Text = clsGeneral.ContactNo;

            //CrystalDecisions.CrystalReports.Engine.TextObject ObjEmail;
            //ObjEmail = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section1.ReportObjects["txtEmail"];
            //ObjEmail.Text = clsGeneral.Email;

            //CrystalDecisions.CrystalReports.Engine.TextObject ObjTin;
            //ObjTin = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section1.ReportObjects["txtTin"];
            //ObjTin.Text = "Tin No : " + clsGeneral.TinNo;

            //#endregion
            //Frm_Preview F = new Frm_Preview(S, Dt);
            //F.Show();

        }

        private void lblGroup_Click(object sender, EventArgs e)
        {
            Frm_GroupCashAccount F = new Frm_GroupCashAccount();
            F.ShowDialog();
            btnShow_Click(new object(), new EventArgs());
        }
    }
}
