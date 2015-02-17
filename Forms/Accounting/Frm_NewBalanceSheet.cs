using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Finance_Management_System
{
    public partial class Frm_NewBalanceSheet : Form
    {
        public Frm_NewBalanceSheet()
        {
            InitializeComponent();
            HStyle = new DataGridViewCellStyle();
            HStyle.ForeColor = Color.Black;
            HStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            GStyle = new DataGridViewCellStyle();
            GStyle.ForeColor = Color.Red;
            GStyle.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            CStyle = new DataGridViewCellStyle();
            CStyle.ForeColor = Color.Maroon;
            CStyle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }

        DataGridViewCellStyle HStyle;

        DataGridViewCellStyle GStyle;

        DataGridViewCellStyle CStyle;


        ClsDataAccess ObjData = new ClsDataAccess();
        DataSet DSData = null;
        DataTable DtBS;
        double NpProfit = 0;
        double ObjDr = 0;
        double ObjCr = 0;

        public void DrDetails(string Id)
        {
            try
            {
                DataRow[] Dr = null;

                string SS = "sp_GetDependentGroups " + Id + ""; ;
                DataTable D = ObjData.GetDataTable(SS);
                if (D.Rows.Count > 0)
                {
                    string[] Code = D.Rows[0][0].ToString().Split(',');
                    if (Code.Length > 0)
                    {
                        string Space = "     ";
                        for (int q = 0; q < Code.Length; q++)
                        {
                            Dr = DtBS.Select("Code = " + Code[q].ToString() + " And HCode = " + Id + "");

                            DataTable Dt = DtBS.Clone();
                            for (int zz = 0; zz < Dr.Length; zz++)
                            {
                                Dt.ImportRow(Dr[zz]);
                            }
                            DataTable DDis = Dt.DefaultView.ToTable(true, "TType", "Code", "GCode", "HCode");
                            for (int a = 0; a < DDis.Rows.Count; a++)
                            {
                                if (Dr[a]["Code"].ToString() != Id)
                                {
                                    DgDr.Rows.Add();
                                    DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAmount1"].Value = DtBS.Compute("SUM(Balance)", "HCode = " + Id + " And Code = " + Dr[a]["Code"].ToString() + "").ToString().DoubleParse().ToString().CurrFormat();
                                    DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Value = Space + "**" + Dr[a]["CodeName"].ToString();
                                    DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrGId"].Value = Dr[a]["Code"].ToString();
                                    DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColDrType"].Value = Dr[a]["TType"].ToString();
                                    DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle = GStyle;
                                }

                                DataRow[] DrCode = DtBS.Select("Code = " + Dr[a]["Code"].ToString() + " And HCode = " + Id + "");
                                for (int v = 0; v < DrCode.Length; v++)
                                {
                                    DgDr.Rows.Add();
                                    DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAmount1"].Value = DrCode[v]["Balance"].ToString().CurrFormat();
                                    DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Value = Space + "       " + DrCode[v]["AcName"].ToString();
                                    DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrGId"].Value = Dr[v]["Code"].ToString();
                                    DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAId"].Value = Dr[v]["Id"].ToString();
                                    DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColDrType"].Value = Dr[v]["TType"].ToString();

                                    DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle = CStyle;
                                }
                                Space = Space + "     ";
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

        public void CrDetails(string Id)
        {
            try
            {
                DataRow[] Dr = null;

                string SS = "sp_GetDependentGroups " + Id + ""; ;
                DataTable D = ObjData.GetDataTable(SS);
                if (D.Rows.Count > 0)
                {
                    string[] Code = D.Rows[0][0].ToString().Split(',');
                    if (Code.Length > 0)
                    {
                        string Space = "     ";
                        for (int q = 0; q < Code.Length; q++)
                        {
                            Dr = DtBS.Select("Code = " + Code[q].ToString() + " And HCode = " + Id + "");
                            DataTable Dt = DtBS.Clone();
                            for (int zz = 0; zz < Dr.Length; zz++)
                            {
                                Dt.ImportRow(Dr[zz]);
                            }
                            DataTable DDis = Dt.DefaultView.ToTable(true, "TType", "Code", "GCode", "HCode");
                            for (int a = 0; a < DDis.Rows.Count; a++)
                            {
                                if (Dr[a]["Code"].ToString() != Id)
                                {
                                    DgCr.Rows.Add();
                                    DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrAmount1"].Value = DtBS.Compute("SUM(Balance)", "HCode = " + Id + " And Code = " + Dr[a]["Code"].ToString() + "").ToString().DoubleParse().ToString().CurrFormat();
                                    DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCr"].Value = Space + "**" + Dr[a]["CodeName"].ToString();
                                    DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrGId"].Value = Dr[a]["Code"].ToString();
                                    DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColCrType"].Value = Dr[a]["TType"].ToString();
                                    DgCr.Rows[DgCr.Rows.Count - 1].DefaultCellStyle = GStyle;
                                }

                                DataRow[] DrCode = DtBS.Select("Code = " + Dr[a]["Code"].ToString() + " And HCode = " + Id + "");
                                for (int v = 0; v < DrCode.Length; v++)
                                {
                                    DgCr.Rows.Add();
                                    DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrAmount1"].Value = DrCode[v]["Balance"].ToString().CurrFormat();
                                    DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCr"].Value = Space + "       " + DrCode[v]["AcName"].ToString();
                                    DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrGId"].Value = Dr[v]["Code"].ToString();
                                    DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrAId"].Value = Dr[v]["Id"].ToString();
                                    DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColCrType"].Value = Dr[v]["TType"].ToString();
                                    DgCr.Rows[DgCr.Rows.Count - 1].DefaultCellStyle = CStyle;
                                }
                                Space = Space + "     ";
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

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Refresh();
                Application.DoEvents();

                string Upto = "(Upto " + dateTimePicker1.Value.ToString("dd/MM/yyyy") + ")";

                //if (Program.FromDate.AddYears(1).AddDays(-1).Date == dateTimePicker1.Value.Date)
                //{
                //    Upto = "";
                //}

                lblHead.Text = "Balance Sheet for the year ended " + dateTimePicker1.Value.ToString("dd/MM/yyyy");

                //ColParticularCrAmount1.Visible = chkDetailed.Checked;
                //ColParticularDrAmount1.Visible = chkDetailed.Checked;

                Application.DoEvents();

                progressBar1.Visible = true;

                #region BackGround Worker
                BackgroundWorker bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.RunWorkerAsync();
                bw.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args)
                {
                    bw.ReportProgress(1);

                    ////ClosingStock = clsGeneral.ClStock;

                    #region Profit Loss
                    string Gross = "Declare @Date Varchar(10) ";
                    Gross = Gross + " Set @Date = '" + dateTimePicker1.DateFormat() + "' ";
                    Gross = Gross + " SELECT * Into #Tmpdata FROM( ";
                    Gross = Gross + " Select A_Id As Id, ";
                    Gross = Gross + " 'Opening Stock' As AccountName, ";
                    Gross = Gross + " a_GCode As GCode, ";
                    Gross = Gross + " 'Opening Stock' As GName, ";
                    Gross = Gross + " a_opbal As Balance  ";
                    Gross = Gross + " From AccountMaster Left Join ";
                    Gross = Gross + " AccountGroups on ag_code = a_GCode Where A_Id = -6 ";
                    Gross = Gross + " UNION ALL ";
                    Gross = Gross + " Select a_id As Id, ";
                    Gross = Gross + " a_name As AccountName, ";
                    Gross = Gross + " a_GCode As GCode, ";
                    Gross = Gross + " ag_name As GName, ";
                    Gross = Gross + " dbo.udf_GetAccountBalance(a_id,@Date) As Balance ";
                    Gross = Gross + " From Accountmaster Left Join ";
                    Gross = Gross + " AccountGroups on ag_code = a_GCode ";
                    Gross = Gross + " Where a_GCode in (23,24,25,26,21,22) ";
                    Gross = Gross + " ) A  ";
                    Gross = Gross + " Update #Tmpdata Set Balance = Balance * -1 Where GCode In (24,26,22) ";
                    Gross = Gross + " Delete #Tmpdata Where Balance = 0 ";
                    Gross = Gross + " Select GName,GCode, ";
                    Gross = Gross + " IsNull(Sum(Balance),0) As Balance From #Tmpdata ";
                    Gross = Gross + " Group By GName,GCode ";
                    Gross = Gross + " Drop table #Tmpdata ";
                    DataTable DtGoss = ObjData.GetDataTable(Gross);
                    ObjDr = DtGoss.Compute("Sum(Balance)", "GCode In (6,23,25,21)").ToString().DoubleParse();
                    ObjCr = DtGoss.Compute("Sum(Balance)", "GCode In (24,26,22)").ToString().DoubleParse();
                    ObjCr = ObjCr;
                    NpProfit = ObjDr - ObjCr;
                    #endregion

                    #region BS
               string     SSClosing = "Select * Into #T From ( ";
                    SSClosing = SSClosing + " SELECT Vrd_AId As Id,ACNAME, ";
                    SSClosing = SSClosing + " AG_CODE,AG_GCODE,AG_HCODE, ";
                    SSClosing = SSClosing + " 0.00 As OBalance, ";
                    SSClosing = SSClosing + " (SUM(ISNULL(VRD_CRAMT,0)) - SUM(ISNULL(VRD_DRAMT,0)) ) As TBalance ";
                    SSClosing = SSClosing + " FROM VW_VOUCHER WHERE VR_ENTRYTYPE > 0 AND VR_DATE <= CAST('" + dateTimePicker1.DateFormat() + "' AS DATETIME) ";
                    SSClosing = SSClosing + " And AG_HCODE in (1,2,3,4,5,6,7,11,12,13,14) ";
                    SSClosing = SSClosing + " Group By Vrd_AId,ACNAME,AG_CODE,AG_GCODE,AG_HCODE ";
                    SSClosing = SSClosing + " UNION ALL ";
                    SSClosing = SSClosing + " SELECT Vrd_AId As Id,ACNAME, ";
                    SSClosing = SSClosing + " AG_CODE,AG_GCODE,AG_HCODE, ";
                    SSClosing = SSClosing + " (SUM(ISNULL(VRD_DRAMT,0)) - SUM(ISNULL(VRD_CRAMT,0)) ) As OBalance, ";
                    SSClosing = SSClosing + " 0.00 As TBalance ";
                    SSClosing = SSClosing + " FROM VW_VOUCHER WHERE VR_ENTRYTYPE = 0  ";
                    SSClosing = SSClosing + " And AG_HCODE in (1,2,3,4,5,6,7,11,12,13,14)  ";
                    SSClosing = SSClosing + " Group By Vrd_AId,ACNAME,AG_CODE,AG_GCODE,AG_HCODE ";
                    SSClosing = SSClosing + " )A Order By AG_CODE,AG_GCODE,AG_HCODE ";
                    SSClosing = SSClosing + " Select Case ";
                    SSClosing = SSClosing + " When AG_CODE = AG_GCODE And AG_GCODE = AG_HCODE Then 1 ";
                    SSClosing = SSClosing + " When AG_HCODE = AG_GCODE Then 2 ";
                    SSClosing = SSClosing + " Else 3 End As TType, ";
                    SSClosing = SSClosing + " Id,AG_CODE As Code,AG_GCODE As GCode, ";
                    SSClosing = SSClosing + " AG_HCODE As HCode,ACNAME, ";
                    SSClosing = SSClosing + " (Select Top 1 ag_type From  AccountGroups Where AG_HCODE = #T.AG_HCODE) As [Type], ";
                    SSClosing = SSClosing + " (Select ag_name From  AccountGroups Where ag_code = #T.AG_CODE) As CodeName, ";
                    SSClosing = SSClosing + " (Select ag_name From  AccountGroups Where ag_code = #T.AG_GCODE) As GroupName, ";
                    SSClosing = SSClosing + " (Select ag_name From  AccountGroups Where ag_code = #T.AG_HCODE) As HeadGroupName, ";
                    SSClosing = SSClosing + " Case 	When AG_HCODE = 1 Or AG_HCODE = 2 Or AG_HCODE = 3 Or AG_HCODE = 4 Or AG_HCODE = 5 Or AG_HCODE = 6 Or AG_HCODE = 7 Then ";
                    SSClosing = SSClosing + " IsNull(Sum(OBalance),0) + IsNull(Sum(TBalance),0)  ";
                    SSClosing = SSClosing + " Else (IsNull(Sum(OBalance),0) + IsNull(Sum(TBalance),0)) * - 1 ";
                    SSClosing = SSClosing + " End As Balance From #T ";
                    SSClosing = SSClosing + " Group By Id,ACNAME,AG_CODE,AG_GCODE,AG_HCODE ";
                    SSClosing = SSClosing + " Having (IsNull(Sum(OBalance),0) + IsNull(Sum(TBalance),0)) <> 0 ";
                    SSClosing = SSClosing + " Order By AG_HCODE,AG_GCODE,AG_CODE,ACNAME ";
                    SSClosing = SSClosing + " Drop Table #T ";
                    DtBS = ObjData.GetDataTable(SSClosing);
                    #endregion
                });

                
                bw.ProgressChanged += new ProgressChangedEventHandler(
                delegate(object o, ProgressChangedEventArgs args)
                {
                 
                });

                 
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object o, RunWorkerCompletedEventArgs args)
                {
                try
                {
                    DgCr.Rows.Clear();
                    DgDr.Rows.Clear();

                    ColParticularCrAmount1.DefaultCellStyle.Format = "N2";
                    ColParticularCrAmount2.DefaultCellStyle.Format = "N2";
                    ColParticularDrAmount1.DefaultCellStyle.Format = "N2";
                    ColParticularDrAmount2.DefaultCellStyle.Format = "N2";
                    ColParticularCrAmount1.Visible = chkDetailed.Checked;
                    ColParticularDrAmount1.Visible = chkDetailed.Checked;

                    if (DtBS != null)
                    {
                        if (NpProfit > 0)
                        {
                            DgDr.Rows.Add();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = Math.Abs(NpProfit).ToString().CurrFormat();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Value = "Net Loss";
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColDrType"].Value = "1";
                        }
                        else if (NpProfit < 0)
                        {
                            DgCr.Rows.Add();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrAmount2"].Value = Math.Abs(NpProfit).ToString().CurrFormat();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCr"].Value = "Net Profit";
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColCrType"].Value = "1";
                        }

                        double CashBalance = new ClsBalance().GetCashBalance(dateTimePicker1.Value);
                        double BankAccounts = DtBS.Compute("SUM(Balance)", "HCode = 2").ToString().DoubleParse();
                        double FixedAssets = DtBS.Compute("SUM(Balance)", "HCode = 3").ToString().DoubleParse();
                        double CurrentAssets = DtBS.Compute("SUM(Balance)", "HCode = 4").ToString().DoubleParse();
                        double Investments = DtBS.Compute("SUM(Balance)", "HCode = 5").ToString().DoubleParse();
                        double Stockinhand = 0;
                        double MiscExpenses = DtBS.Compute("SUM(Balance)", "HCode = 7").ToString().DoubleParse();
                        double Capital = DtBS.Compute("SUM(Balance)", "HCode = 11").ToString().DoubleParse();
                        double CurrentLiability = DtBS.Compute("SUM(Balance)", "HCode = 12").ToString().DoubleParse();
                        double Loansliabilities = DtBS.Compute("SUM(Balance)", "HCode = 13").ToString().DoubleParse();
                        double Suspense = DtBS.Compute("SUM(Balance)", "HCode = 14").ToString().DoubleParse();


                        if (CashBalance != 0)
                        {
                            DgDr.Rows.Add();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = CashBalance.ToString().CurrFormat();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Value = "Cash";// DtBS.Select("HCode = 1")[0]["HeadGroupName"].ToString();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrGId"].Value = "1";
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColDrType"].Value = "1";
                            DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle = HStyle;

                            if (chkDetailed.Checked == true)
                            {
                            }
                        }

                        if (BankAccounts != 0)
                        {
                            DgDr.Rows.Add();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = BankAccounts.ToString().CurrFormat();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Value = DtBS.Select("HCode = 2")[0]["HeadGroupName"].ToString();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrGId"].Value = "2";
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColDrType"].Value = "1";
                            DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle = HStyle;

                            if (chkDetailed.Checked == true)
                            {
                                DrDetails("2");
                            }
                        }

                        if (FixedAssets != 0)
                        {
                            DgDr.Rows.Add();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = FixedAssets.ToString().CurrFormat();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Value = DtBS.Select("HCode = 3")[0]["HeadGroupName"].ToString();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrGId"].Value = "3";
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColDrType"].Value = "1";
                            DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle = HStyle;

                            if (chkDetailed.Checked == true)
                            {
                                DrDetails("3");
                            }
                        }

                        if (CurrentAssets != 0)
                        {
                            DgDr.Rows.Add();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = CurrentAssets.ToString().CurrFormat();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Value = DtBS.Select("HCode = 4")[0]["HeadGroupName"].ToString();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrGId"].Value = "4";
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColDrType"].Value = "1";
                            DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle = HStyle;

                            if (chkDetailed.Checked == true)
                            {
                                DrDetails("4");
                            }
                        }

                        if (Investments != 0)
                        {
                            DgDr.Rows.Add();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = Investments.ToString().CurrFormat();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Value = DtBS.Select("HCode = 5")[0]["HeadGroupName"].ToString();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrGId"].Value = "5";
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColDrType"].Value = "1";
                            DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle = HStyle;

                            if (chkDetailed.Checked == true)
                            {
                                DrDetails("5");
                            }
                        }

                        if (Stockinhand != 0)
                        {
                            DgDr.Rows.Add();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = Stockinhand.ToString().CurrFormat();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Value = "Stock In Hand";
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrGId"].Value = "6";
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColDrType"].Value = "1";
                            DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle = HStyle;

                            if (chkDetailed.Checked == true)
                            {

                            }
                        }

                        if (MiscExpenses != 0)
                        {
                            DgDr.Rows.Add();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = MiscExpenses.ToString().CurrFormat();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Value = DtBS.Select("HCode = 7")[0]["HeadGroupName"].ToString();
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrGId"].Value = "7";
                            DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColDrType"].Value = "1";
                            DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle = HStyle;

                            if (chkDetailed.Checked == true)
                            {
                                DrDetails("7");
                            }
                        }

                        if (Capital != 0)
                        {
                            DgCr.Rows.Add();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrAmount2"].Value = Capital.ToString().CurrFormat();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCr"].Value = DtBS.Select("HCode = 11")[0]["HeadGroupName"].ToString();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrGId"].Value = "11";
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColCrType"].Value = "1";
                            DgCr.Rows[DgCr.Rows.Count - 1].DefaultCellStyle = HStyle;

                            if (chkDetailed.Checked == true)
                            {
                                CrDetails("11");
                            }
                        }

                        if (CurrentLiability != 0)
                        {
                            DgCr.Rows.Add();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrAmount2"].Value = CurrentLiability.ToString().CurrFormat();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCr"].Value = DtBS.Select("HCode = 12")[0]["HeadGroupName"].ToString();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrGId"].Value = "12";
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColCrType"].Value = "1";
                            DgCr.Rows[DgCr.Rows.Count - 1].DefaultCellStyle = HStyle;

                            if (chkDetailed.Checked == true)
                            {
                                CrDetails("12");
                            }
                        }

                        if (Loansliabilities != 0)
                        {
                            DgCr.Rows.Add();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrAmount2"].Value = Loansliabilities.ToString().CurrFormat();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCr"].Value = DtBS.Select("HCode = 13")[0]["HeadGroupName"].ToString();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrGId"].Value = "13";
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColCrType"].Value = "1";
                            DgCr.Rows[DgCr.Rows.Count - 1].DefaultCellStyle = HStyle;

                            if (chkDetailed.Checked == true)
                            {
                                CrDetails("13");
                            }
                        }

                        if (Suspense != 0)
                        {
                            DgCr.Rows.Add();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrAmount2"].Value = Suspense.ToString().CurrFormat();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCr"].Value = DtBS.Select("HCode = 14")[0]["HeadGroupName"].ToString();
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrGId"].Value = "14";
                            DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColCrType"].Value = "1";
                            DgCr.Rows[DgCr.Rows.Count - 1].DefaultCellStyle = HStyle;

                            if (chkDetailed.Checked == true)
                            {
                                CrDetails("14");
                            }
                        }

                        int DrCount = DgDr.Rows.Count + 1;
                        int CrCount = DgCr.Rows.Count + 1;
                        int Add = 0;

                        if (DrCount > CrCount)
                        {
                            Add = DrCount - CrCount;
                            for (int i = 0; i < Add; i++)
                            {
                                DgCr.Rows.Add();
                            }
                        }
                        else if (DrCount < CrCount)
                        {
                            Add = CrCount - DrCount;
                            for (int i = 0; i < Add; i++)
                            {
                                DgDr.Rows.Add();
                            }
                        }

                        double CrTotal = 0;
                        double DrTotal = 0;

                        for (int i = 0; i < DgCr.Rows.Count; i++)
                        {
                            double CrAmount2 = DgCr.Rows[i].Cells["ColParticularCrAmount2"].EditedFormattedValue.ToString().DoubleParse();
                            CrTotal = CrTotal + CrAmount2;
                        }

                        for (int i = 0; i < DgDr.Rows.Count; i++)
                        {
                            double DrAmount2 = DgDr.Rows[i].Cells["ColParticularDrAmount2"].EditedFormattedValue.ToString().DoubleParse();
                            DrTotal = DrTotal + DrAmount2;
                        }

                        DgDr.Rows.Add();
                        DgCr.Rows.Add();

                        DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Teal;
                        DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
                        DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.Teal;
                        DgDr.Rows[DgDr.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.White;

                        DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCrAmount2"].Value = CrTotal.ToString().CurrFormat();
                        DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCr"].Value = "Total";
                        DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColParticularCr"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DgCr.Rows[DgCr.Rows.Count - 1].Cells["ColCrType"].Value = "10";

                        DgCr.Rows[DgCr.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Teal;
                        DgCr.Rows[DgCr.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
                        DgCr.Rows[DgCr.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.Teal;
                        DgCr.Rows[DgCr.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.White;

                        DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = DrTotal.ToString().CurrFormat();
                        DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Value = "Total";
                        DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColDrType"].Value = "10";

                        DgDr.Rows[DgDr.Rows.Count - 1].Cells["ColParticularDr"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        DgDr.ClearSelection();
                        DgCr.ClearSelection();
                        progressBar1.Visible = false;
                    }
                }
                catch (Exception Erp)
                {
                    clsGeneral.ShowErrMsg(Erp.Message);
                }
                });

                #endregion
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (DgCr.Rows.Count == 0 && DgDr.Rows.Count == 0)
            {
                return;
            }

            DataTable DtPrintCr = new DataTable();
            DataTable DtPrintDr = new DataTable();

            DtPrintCr.Columns.Add("Col1");
            DtPrintCr.Columns.Add("ParticularCr");
            DtPrintCr.Columns.Add("Amount1Cr");
            DtPrintCr.Columns.Add("Amount2Cr");

            DtPrintDr.Columns.Add("Col2");
            DtPrintDr.Columns.Add("ParticularDr");
            DtPrintDr.Columns.Add("Amount1Dr");
            DtPrintDr.Columns.Add("Amount2Dr");



            for (int i = 0; i < DgDr.Rows.Count; i++)
            {
                string DrType = ""; string Dr = ""; string DrAmount1 = ""; string DrAmount2 = "";
                string CrType = ""; string Cr = ""; string CrAmount1 = ""; string CrAmount2 = "";
                if (DgDr.Rows[i].Visible == true)
                {
                    DrType = DgDr.Rows[i].Cells["ColDrType"].EditedFormattedValue.ToString();
                    Dr = DgDr.Rows[i].Cells["ColParticularDr"].EditedFormattedValue.ToString();
                    DrAmount1 = DgDr.Rows[i].Cells["ColParticularDrAmount1"].EditedFormattedValue.ToString();
                    DrAmount2 = DgDr.Rows[i].Cells["ColParticularDrAmount2"].EditedFormattedValue.ToString();
                }

                if (DgCr.Rows[i].Visible == true)
                {
                    CrType = DgCr.Rows[i].Cells["ColCrType"].EditedFormattedValue.ToString();
                    Cr = DgCr.Rows[i].Cells["ColParticularCr"].EditedFormattedValue.ToString();
                    CrAmount1 = DgCr.Rows[i].Cells["ColParticularCrAmount1"].EditedFormattedValue.ToString();
                    CrAmount2 = DgCr.Rows[i].Cells["ColParticularCrAmount2"].EditedFormattedValue.ToString();
                }

                if (Cr.Trim().Length > 0)
                {
                    DtPrintCr.Rows.Add(CrType, Cr, CrAmount1, CrAmount2);
                }
                if (Dr.Trim().Length > 0)
                {
                    DtPrintDr.Rows.Add(DrType, Dr, DrAmount1, DrAmount2);
                }
            }

            DataTable Dt = new DataTable();

            Dt.Columns.Add("Col1");
            Dt.Columns.Add("ParticularCr");
            Dt.Columns.Add("Amount1Cr");
            Dt.Columns.Add("Amount2Cr");
            Dt.Columns.Add("Col2");
            Dt.Columns.Add("ParticularDr");
            Dt.Columns.Add("Amount1Dr");
            Dt.Columns.Add("Amount2Dr");

            int DrCnt = DtPrintDr.Rows.Count;
            int CrCnt = DtPrintCr.Rows.Count;

            int Cnt = CrCnt;
            if (DrCnt > CrCnt)
            {
                Cnt = DrCnt;
            }

            int CrIndexTotal = 0;
            int DrIndexTotal = 0;

            for (int i = 0; i < Cnt; i++)
            {
                string DrType = ""; string Dr = ""; string DrAmount1 = ""; string DrAmount2 = "";
                string CrType = ""; string Cr = ""; string CrAmount1 = ""; string CrAmount2 = "";

                if (i < CrCnt)
                {
                    CrType = DtPrintCr.Rows[i]["Col1"].ToString();
                    Cr = DtPrintCr.Rows[i]["ParticularCr"].ToString();
                    CrAmount1 = DtPrintCr.Rows[i]["Amount1Cr"].ToString();
                    CrAmount2 = DtPrintCr.Rows[i]["Amount2Cr"].ToString();
                }
                if (i < DrCnt)
                {
                    DrType = DtPrintDr.Rows[i]["Col2"].ToString();
                    Dr = DtPrintDr.Rows[i]["ParticularDr"].ToString();
                    DrAmount1 = DtPrintDr.Rows[i]["Amount1Dr"].ToString();
                    DrAmount2 = DtPrintDr.Rows[i]["Amount2Dr"].ToString();
                }
                Dt.Rows.Add(CrType, Cr, CrAmount1, CrAmount2, DrType, Dr, DrAmount1, DrAmount2);
            }

            Dt.Rows[Cnt - 1]["Col1"] = Dt.Rows[CrCnt - 1]["Col1"];
            Dt.Rows[Cnt - 1]["ParticularCr"] = Dt.Rows[CrCnt - 1]["ParticularCr"];
            Dt.Rows[Cnt - 1]["Amount1Cr"] = Dt.Rows[CrCnt - 1]["Amount1Cr"];
            Dt.Rows[Cnt - 1]["Amount2Cr"] = Dt.Rows[CrCnt - 1]["Amount2Cr"];

            Dt.Rows[Cnt - 1]["Col2"] = Dt.Rows[DrCnt - 1]["Col2"];
            Dt.Rows[Cnt - 1]["ParticularDr"] = Dt.Rows[DrCnt - 1]["ParticularDr"];
            Dt.Rows[Cnt - 1]["Amount1Dr"] = Dt.Rows[DrCnt - 1]["Amount1Dr"];
            Dt.Rows[Cnt - 1]["Amount2Dr"] = Dt.Rows[DrCnt - 1]["Amount2Dr"];

            //int Cnt = CrCnt;
            if (DrCnt > CrCnt)
            {
                Dt.Rows[CrCnt - 1]["Col1"] = "";
                Dt.Rows[CrCnt - 1]["ParticularCr"] = "";
                Dt.Rows[CrCnt - 1]["Amount1Cr"] = "";
                Dt.Rows[CrCnt - 1]["Amount2Cr"] = "";
            }
            else if (DrCnt < CrCnt)
            {
                Dt.Rows[DrCnt - 1]["Col2"] = "";
                Dt.Rows[DrCnt - 1]["ParticularDr"] = "";
                Dt.Rows[DrCnt - 1]["Amount1Dr"] = "";
                Dt.Rows[DrCnt - 1]["Amount2Dr"] = "";
            }
         
            dataGridView1.DataSource = Dt;
            dataGridView1.Columns["Col1"].Visible = false;
            dataGridView1.Columns["Col2"].Visible = false;
            clsGeneral.ExcelExport(dataGridView1, lblHead.Text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_TradingProfitLoss_Load(object sender, EventArgs e)
        {
            lblTop.Text = clsGeneral.CompanyName + Environment.NewLine + "Balance Sheet";
            //dateTimePicker1.MinDate = Program.FromDate.Date;
            //dateTimePicker1.MaxDate = Program.FromDate.AddYears(1).AddDays(-1).Date;
            //dateTimePicker1.Value = dateTimePicker1.SetDate();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //if (DgCr.Rows.Count == 0 && DgDr.Rows.Count == 0)
                //{
                //    return;
                //}

                //DataTable DtPrintCr = new DataTable();
                //DataTable DtPrintDr = new DataTable();

                //DtPrintCr.Columns.Add("Col1");
                //DtPrintCr.Columns.Add("ParticularCr");
                //DtPrintCr.Columns.Add("Amount1Cr");
                //DtPrintCr.Columns.Add("Amount2Cr");

                //DtPrintDr.Columns.Add("Col2");
                //DtPrintDr.Columns.Add("ParticularDr");
                //DtPrintDr.Columns.Add("Amount1Dr");
                //DtPrintDr.Columns.Add("Amount2Dr");

                

                //for (int i = 0; i < DgDr.Rows.Count; i++)
                //{
                //    string DrType = ""; string Dr = ""; string DrAmount1 = ""; string DrAmount2 = "";
                //    string CrType = ""; string Cr = ""; string CrAmount1 = ""; string CrAmount2 = "";
                //    if (DgDr.Rows[i].Visible == true)
                //    {
                //        DrType = DgDr.Rows[i].Cells["ColDrType"].EditedFormattedValue.ToString();
                //        Dr = DgDr.Rows[i].Cells["ColParticularDr"].EditedFormattedValue.ToString();
                //        DrAmount1 = DgDr.Rows[i].Cells["ColParticularDrAmount1"].EditedFormattedValue.ToString();
                //        DrAmount2 = DgDr.Rows[i].Cells["ColParticularDrAmount2"].EditedFormattedValue.ToString();
                //    }

                //    if (DgCr.Rows[i].Visible == true)
                //    {
                //        CrType = DgCr.Rows[i].Cells["ColCrType"].EditedFormattedValue.ToString();
                //        Cr = DgCr.Rows[i].Cells["ColParticularCr"].EditedFormattedValue.ToString();
                //        CrAmount1 = DgCr.Rows[i].Cells["ColParticularCrAmount1"].EditedFormattedValue.ToString();
                //        CrAmount2 = DgCr.Rows[i].Cells["ColParticularCrAmount2"].EditedFormattedValue.ToString();
                //    }

                //    if (Cr.Trim().Length > 0 )
                //    {
                //        DtPrintCr.Rows.Add(CrType, Cr, CrAmount1, CrAmount2);
                //    }
                //    if (Dr.Trim().Length > 0)
                //    {
                //        DtPrintDr.Rows.Add(DrType, Dr, DrAmount1, DrAmount2);
                //    }
                //}

                //DataTable Dt = new DataTable();

                //Dt.Columns.Add("Col1");
                //Dt.Columns.Add("ParticularCr");
                //Dt.Columns.Add("Amount1Cr");
                //Dt.Columns.Add("Amount2Cr");
                //Dt.Columns.Add("Col2");
                //Dt.Columns.Add("ParticularDr");
                //Dt.Columns.Add("Amount1Dr");
                //Dt.Columns.Add("Amount2Dr");

                //int DrCnt = DtPrintDr.Rows.Count;
                //int CrCnt = DtPrintCr.Rows.Count;

                //int Cnt = CrCnt;
                //if (DrCnt > CrCnt)
                //{
                //    Cnt = DrCnt;
                //}

                //int CrIndexTotal = 0;
                //int DrIndexTotal = 0;

                //for (int i = 0; i < Cnt; i++)
                //{
                //    string DrType = ""; string Dr = ""; string DrAmount1 = ""; string DrAmount2 = "";
                //    string CrType = ""; string Cr = ""; string CrAmount1 = ""; string CrAmount2 = "";

                //    if (i < CrCnt)
                //    {
                //        CrType = DtPrintCr.Rows[i]["Col1"].ToString();
                //        Cr = DtPrintCr.Rows[i]["ParticularCr"].ToString();
                //        CrAmount1 = DtPrintCr.Rows[i]["Amount1Cr"].ToString();
                //        CrAmount2 = DtPrintCr.Rows[i]["Amount2Cr"].ToString();
                //    }
                //    if (i < DrCnt)
                //    {
                //        DrType = DtPrintDr.Rows[i]["Col2"].ToString();
                //        Dr = DtPrintDr.Rows[i]["ParticularDr"].ToString();
                //        DrAmount1 = DtPrintDr.Rows[i]["Amount1Dr"].ToString();
                //        DrAmount2 = DtPrintDr.Rows[i]["Amount2Dr"].ToString();
                //    }
                //    Dt.Rows.Add(CrType,Cr, CrAmount1, CrAmount2,DrType, Dr, DrAmount1, DrAmount2);
                //}

                //Dt.Rows[Cnt - 1]["Col1"] = Dt.Rows[CrCnt - 1]["Col1"];
                //Dt.Rows[Cnt - 1]["ParticularCr"] = Dt.Rows[CrCnt - 1]["ParticularCr"];
                //Dt.Rows[Cnt - 1]["Amount1Cr"] = Dt.Rows[CrCnt - 1]["Amount1Cr"];
                //Dt.Rows[Cnt - 1]["Amount2Cr"] = Dt.Rows[CrCnt - 1]["Amount2Cr"];

                //Dt.Rows[Cnt - 1]["Col2"] = Dt.Rows[DrCnt - 1]["Col2"];
                //Dt.Rows[Cnt - 1]["ParticularDr"] = Dt.Rows[DrCnt - 1]["ParticularDr"];
                //Dt.Rows[Cnt - 1]["Amount1Dr"] = Dt.Rows[DrCnt - 1]["Amount1Dr"];
                //Dt.Rows[Cnt - 1]["Amount2Dr"] = Dt.Rows[DrCnt - 1]["Amount2Dr"];

                ////int Cnt = CrCnt;
                //if (DrCnt > CrCnt)
                //{
                //    Dt.Rows[CrCnt - 1]["Col1"] = "";
                //    Dt.Rows[CrCnt - 1]["ParticularCr"] = "";
                //    Dt.Rows[CrCnt - 1]["Amount1Cr"] = "";
                //    Dt.Rows[CrCnt - 1]["Amount2Cr"] = "";
                //}
                //else  if(DrCnt < CrCnt)
                //{
                //    Dt.Rows[DrCnt - 1]["Col2"] = "";
                //    Dt.Rows[DrCnt - 1]["ParticularDr"] = "";
                //    Dt.Rows[DrCnt - 1]["Amount1Dr"] = "";
                //    Dt.Rows[DrCnt - 1]["Amount2Dr"] = "";
                //}

                //if (chkDetailed.Checked == true)
                //{
                //    FORM.NewAccounting.BalanceSheetDetailed S = new Finance_Management_System.FORM.NewAccounting.BalanceSheetDetailed();
                //    #region Code Of Company

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjHeading;
                //    ObjHeading = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtHeading"];
                //    ObjHeading.Text = lblHead.Text;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjCompany;
                //    ObjCompany = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtCompany"];
                //    ObjCompany.Text = clsGeneral.CompanyName;
                //    ObjCompany.ApplyFont(Program.FontStyle);

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd1;
                //    ObjAdd1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtAddress1"];
                //    ObjAdd1.Text = clsGeneral.Address;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd2;
                //    ObjAdd2 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtAddress2"];
                //    ObjAdd2.Text = clsGeneral.Address2;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjCity;
                //    ObjCity = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtCity"];
                //    ObjCity.Text = clsGeneral.City;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjContact;
                //    ObjContact = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtContact"];
                //    ObjContact.Text = clsGeneral.ContactNo;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjEmail;
                //    ObjEmail = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtEmail"];
                //    ObjEmail.Text = clsGeneral.Email;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjTin;
                //    ObjTin = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtTin"];
                //    ObjTin.Text = "Tin No : " + clsGeneral.TinNo;

                //    #endregion
                //    Frm_Preview F = new Frm_Preview(S, Dt);
                //    F.Show();
                //}
                //else
                //{
                //    FORM.NewAccounting.BalanceSheet S = new Finance_Management_System.FORM.NewAccounting.BalanceSheet();
                //    #region Code Of Company

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjHeading;
                //    ObjHeading = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtHeading"];
                //    ObjHeading.Text = lblHead.Text;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjCompany;
                //    ObjCompany = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtCompany"];
                //    ObjCompany.Text = clsGeneral.CompanyName;
                //    ObjCompany.ApplyFont(Program.FontStyle);

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd1;
                //    ObjAdd1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtAddress1"];
                //    ObjAdd1.Text = clsGeneral.Address;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd2;
                //    ObjAdd2 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtAddress2"];
                //    ObjAdd2.Text = clsGeneral.Address2;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjCity;
                //    ObjCity = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtCity"];
                //    ObjCity.Text = clsGeneral.City;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjContact;
                //    ObjContact = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtContact"];
                //    ObjContact.Text = clsGeneral.ContactNo;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjEmail;
                //    ObjEmail = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtEmail"];
                //    ObjEmail.Text = clsGeneral.Email;

                //    CrystalDecisions.CrystalReports.Engine.TextObject ObjTin;
                //    ObjTin = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtTin"];
                //    ObjTin.Text = "Tin No : " + clsGeneral.TinNo;

                //    #endregion
                //    Frm_Preview F = new Frm_Preview(S, Dt);
                //    F.Show();
                //}
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Frm_NewBalanceSheet_Resize(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = splitContainer1.Width / 2;
        }

        private void DgDr_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void DgDr_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                

                if (DgDr.CurrentRow != null)
                {
                    int GId = DgDr.CurrentRow.Cells["ColParticularDrGId"].EditedFormattedValue.ToString().IntParse();
                    int AId = DgDr.CurrentRow.Cells["ColParticularDrAId"].EditedFormattedValue.ToString().IntParse();

                    if (e.KeyData == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;

                        //if (AId != 0)
                        //{
                        //    clsGeneral.Accounting_Main.OpMonthlySummary(AId, dateTimePicker1.Value, this);
                        //}
                        //else
                        //{
                        //    clsGeneral.Accounting_Main.OpgroupSummary(GId, dateTimePicker1.Value, this);
                        //}
                    }
                    if (e.KeyData == Keys.Space)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;

                        DataGridViewCellStyle CompStyle1 = new DataGridViewCellStyle();
                        CompStyle1.ForeColor = Color.Red;
                        CompStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        DataGridViewCellStyle CompStyle2 = new DataGridViewCellStyle();
                        CompStyle2.ForeColor = Color.Black;
                        CompStyle2.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                        if (DgDr.CurrentRow.DefaultCellStyle.ToString() == CompStyle1.ToString() || DgDr.CurrentRow.DefaultCellStyle.ToString() == CompStyle2.ToString())
                        {
                            int Index = DgDr.CurrentRow.Index;
                            for (int i = Index + 1; i < DgDr.Rows.Count; i++)
                            {
                                int GCId = DgDr.Rows[i].Cells["ColParticularDrGId"].EditedFormattedValue.ToString().IntParse();
                                if (GId == GCId)
                                {
                                    DgDr.Rows[i].Visible = !DgDr.Rows[i].Visible;
                                }
                            }
                            DgDr.Refresh();
                        }
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void DgCr_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (DgDr.CurrentRow != null)
                {
                    int GId = DgCr.CurrentRow.Cells["ColParticularCrGId"].EditedFormattedValue.ToString().IntParse();
                    int AId = DgCr.CurrentRow.Cells["ColParticularCrAId"].EditedFormattedValue.ToString().IntParse();
                    
                    if (e.KeyData == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;

                        //if (AId != 0)
                        //{
                        //    clsGeneral.Accounting_Main.OpMonthlySummary(AId, dateTimePicker1.Value, this);
                        //}
                        //else
                        //{
                        //    clsGeneral.Accounting_Main.OpgroupSummary(GId, dateTimePicker1.Value, this);
                        //}
                    }
                    if (e.KeyData == Keys.Space)
                    {
                        e.SuppressKeyPress = true;
                        e.Handled = true;

                        DataGridViewCellStyle CompStyle1 = new DataGridViewCellStyle();
                        CompStyle1.ForeColor = Color.Red;
                        CompStyle1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                        DataGridViewCellStyle CompStyle2 = new DataGridViewCellStyle();
                        CompStyle2.ForeColor = Color.Black;
                        CompStyle2.Font = new System.Drawing.Font("Tahoma", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));


                        if (DgCr.CurrentRow.DefaultCellStyle.ToString() == CompStyle1.ToString() || DgCr.CurrentRow.DefaultCellStyle.ToString() == CompStyle2.ToString())
                        {
                            int Index = DgCr.CurrentRow.Index;
                            for (int i = Index + 1; i < DgCr.Rows.Count; i++)
                            {
                                int GCId = DgCr.Rows[i].Cells["ColParticularCrGId"].EditedFormattedValue.ToString().IntParse();
                                if (GId == GCId)
                                {
                                    DgCr.Rows[i].Visible = !DgCr.Rows[i].Visible;
                                }
                            }
                            DgCr.Refresh();
                        }
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void chkDetailed_CheckedChanged(object sender, EventArgs e)
        {
            ColParticularCrAmount1.Visible = chkDetailed.Checked;
            ColParticularDrAmount1.Visible = chkDetailed.Checked;
            DgCr.Rows.Clear(); DgDr.Rows.Clear();
        }

        private void DgDr_Leave(object sender, EventArgs e)
        {
            DgDr.ClearSelection();
        }

        private void DgCr_Leave(object sender, EventArgs e)
        {
            DgCr.ClearSelection();
        }
    }
}
