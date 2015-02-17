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
    public partial class Frm_TradingProfitLoss : Form
    {
        public Frm_TradingProfitLoss()
        {
            InitializeComponent();
        }

        ClsDataAccess ObjData = new ClsDataAccess();
        DataSet DSData = null;

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Refresh();
                Application.DoEvents();

                DgData.Rows.Clear();

                ColParticularCrAmount1.DefaultCellStyle.Format = "N2";
                ColParticularCrAmount2.DefaultCellStyle.Format = "N2";
                ColParticularDrAmount1.DefaultCellStyle.Format = "N2";
                ColParticularDrAmount2.DefaultCellStyle.Format = "N2";

                string Upto = "(Upto " + dateTimePicker1.Value.ToString("dd/MM/yyyy") + ")";

                //if (Program.FromDate.AddYears(1).AddDays(-1).Date == dateTimePicker1.Value.Date)
                //{
                //    Upto = "";
                //}

                lblHead.Text = "Trading And Profit & Loss A/C for the year ended " + dateTimePicker1.Value.ToString("dd/MM/yyyy");

                ColParticularCrAmount1.Visible = chkDetailed.Checked;
                ColParticularDrAmount1.Visible = chkDetailed.Checked;

                Application.DoEvents();

                progressBar1.Visible = true;

                #region BackGround Worker
                BackgroundWorker bw = new BackgroundWorker();

                // this allows our worker to report progress during work
                bw.WorkerReportsProgress = true;
                bw.RunWorkerAsync();
                // what to do in the background thread
                bw.DoWork += new DoWorkEventHandler(
                delegate(object o, DoWorkEventArgs args)
                {
                    //bw.ReportProgress(1);


                    string SS = "Declare @Date Varchar(10) ";
                    SS = SS + " Set @Date = '" + dateTimePicker1.DateFormat() + "' ";
                    SS = SS + " SELECT * Into #Tmpdata FROM( ";
                    //SS = SS + " Select A_Id As Id, ";
                    //SS = SS + " 'Opening Stock' As AccountName, ";
                    //SS = SS + " a_GCode As GCode, ";
                    //SS = SS + " 'Opening Stock' As GName, ";
                    //SS = SS + " a_opbal As Balance  ";
                    //SS = SS + " From AccountMaster Left Join ";
                    //SS = SS + " AccountGroups on ag_code = a_GCode Where A_Id = -6 ";
                    //SS = SS + " UNION ALL ";
                    SS = SS + " Select a_id As Id, ";
                    SS = SS + " a_name As AccountName, ";
                    SS = SS + " a_GCode As GCode, ";
                    SS = SS + " ag_name As GName, ";
                    SS = SS + " 0.00 As Balance ";
                    // SS = SS + " dbo.udf_GetAccountBalance(a_id,@Date) As Balance ";
                    SS = SS + " From Accountmaster Left Join ";
                    SS = SS + " AccountGroups on ag_code = a_GCode ";
                    SS = SS + " Where a_GCode in (23,24,25,26,21,22) ";
                    SS = SS + " ) A  ";
                    SS = SS + " Update #Tmpdata Set Balance = Balance * -1 Where GCode In (24,26,22) ";
                    SS = SS + " Delete #Tmpdata Where Balance = 0 ";
                    SS = SS + " Select GName,GCode, ";
                    SS = SS + " IsNull(Sum(Balance),0) As Balance From #Tmpdata ";
                    SS = SS + " Group By GName,GCode ";
                    SS = SS + " Select * From #Tmpdata Order by AccountName ";
                    SS = SS + " Drop table #Tmpdata ";

                    DSData = ObjData.GetDataSet(SS);

                });

                // what to do when progress changed (update the progress bar for example)
                bw.ProgressChanged += new ProgressChangedEventHandler(
                delegate(object o, ProgressChangedEventArgs args)
                {
                   // this.Text = args.ProgressPercentage.ToString();
                });

                // what to do when worker completes its task (notify the user)
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(
                delegate(object o, RunWorkerCompletedEventArgs args)
                {
                    if (DSData.Tables.Count > 1)
                    {
                        DataTable DtHead = DSData.Tables[0];

                        double ObjDr = DtHead.Compute("Sum(Balance)", "GCode In (6,23,25)").ToString().DoubleParse();
                        double ObjCr =  DtHead.Compute("Sum(Balance)", "GCode In (24,26)").ToString().DoubleParse();

                        double Diff = ObjDr - ObjCr;

                        DataTable DtHeadDetails = DSData.Tables[1];

                        #region Opening
                        DataRow[] DrOp = DtHead.Select("GCode = 6");//OPENING
                        if (DrOp.Length > 0)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = DrOp[0]["GName"].ToString();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrGId"].Value = DrOp[0]["GCode"].ToString();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = DrOp[0]["Balance"].ToString();
                        }
                        #endregion

                        #region Purchase
                        DataRow[] DrPur = DtHead.Select("GCode = 23");//PURCHASE
                        if (DrPur.Length > 0)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = DrPur[0]["GName"].ToString();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrGId"].Value = DrPur[0]["GCode"].ToString();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = DrPur[0]["Balance"].ToString();

                            //Details
                            if (chkDetailed.Checked == true)
                            {
                                DataRow[] DrPurDetails = DtHeadDetails.Select("GCode = 23");
                                for (int i = 0; i < DrPurDetails.Length; i++)
                                {
                                    DgData.Rows.Add();
                                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = "      " + DrPurDetails[i]["AccountName"].ToString();
                                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAId"].Value = DrPurDetails[i]["Id"].ToString();
                                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount1"].Value = DrPurDetails[i]["Balance"].ToString();

                                }
                            }
                        }
                        #endregion

                        #region DirectExp
                        DataRow[] DrDrexp = DtHead.Select("GCode = 25");//DIRECT EXP
                        if (DrDrexp.Length > 0)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = DrDrexp[0]["GName"].ToString();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrGId"].Value = DrDrexp[0]["GCode"].ToString();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = DrDrexp[0]["Balance"].ToString();

                            //Details
                            if (chkDetailed.Checked == true)
                            {
                                DataRow[] DrDrexpDetails = DtHeadDetails.Select("GCode = 25");
                                for (int i = 0; i < DrDrexpDetails.Length; i++)
                                {
                                    DgData.Rows.Add();
                                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = "      " + DrDrexpDetails[i]["AccountName"].ToString();
                                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAId"].Value = DrDrexpDetails[i]["Id"].ToString();
                                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount1"].Value = DrDrexpDetails[i]["Balance"].ToString();

                                }
                            }
                        }
                        #endregion

                        #region Sales
                        DataRow[] DrSales = DtHead.Select("GCode = 24");//SALES
                        if (DrSales.Length > 0)
                        {
                            int index = -1;
                            for (int i = 0; i < DgData.Rows.Count; i++)
                            {
                                int ACId = DgData.Rows[i].Cells["ColParticularCrAId"].EditedFormattedValue.ToString().IntParse();
                                int GId = DgData.Rows[i].Cells["ColParticularCrGId"].EditedFormattedValue.ToString().IntParse();

                                if (ACId == 0 && GId == 0)
                                {
                                    index = i;
                                    break;
                                }
                            }
                            if (index == -1)
                            {
                                DgData.Rows.Add();
                                index = DgData.Rows.Count - 1;
                            }
                            DgData.Rows[index].Cells["ColParticularCr"].Value = DrSales[0]["GName"].ToString();
                            DgData.Rows[index].Cells["ColParticularCrGId"].Value = DrSales[0]["GCode"].ToString();
                            DgData.Rows[index].Cells["ColParticularCrAmount2"].Value = DrSales[0]["Balance"].ToString();

                            //Details
                            if (chkDetailed.Checked == true)
                            {
                                DataRow[] DrSalesDetails = DtHeadDetails.Select("GCode = 24");
                                for (int i = 0; i < DrSalesDetails.Length; i++)
                                {
                                    index = -1;
                                    for (int j = 0; j < DgData.Rows.Count; j++)
                                    {
                                        int ACId = DgData.Rows[j].Cells["ColParticularCrAId"].EditedFormattedValue.ToString().IntParse();
                                        int GId = DgData.Rows[j].Cells["ColParticularCrGId"].EditedFormattedValue.ToString().IntParse();

                                        if (ACId == 0 && GId == 0)
                                        {
                                            index = j;
                                            break;
                                        }
                                    }
                                    if (index == -1)
                                    {
                                        DgData.Rows.Add();
                                        index = DgData.Rows.Count - 1;
                                    }
                                    DgData.Rows[index].Cells["ColParticularCr"].Value = "      " + DrSalesDetails[i]["AccountName"].ToString();
                                    DgData.Rows[index].Cells["ColParticularCrAId"].Value = DrSalesDetails[i]["Id"].ToString();
                                    DgData.Rows[index].Cells["ColParticularCrAmount1"].Value = DrSalesDetails[i]["Balance"].ToString();
                                }
                            }
                        }
                        #endregion

                        #region Direct Income
                        DataRow[] DrDrIncome = DtHead.Select("GCode = 26");//DIR INCOME
                        if (DrDrIncome.Length > 0)
                        {
                            int index = -1;
                            for (int i = 0; i < DgData.Rows.Count; i++)
                            {
                                int ACId = DgData.Rows[i].Cells["ColParticularCrAId"].EditedFormattedValue.ToString().IntParse();
                                int GId = DgData.Rows[i].Cells["ColParticularCrGId"].EditedFormattedValue.ToString().IntParse();

                                if (ACId == 0 && GId == 0)
                                {
                                    index = i;
                                    break;
                                }
                            }
                            if (index == -1)
                            {
                                DgData.Rows.Add();
                                index = DgData.Rows.Count - 1;
                            }

                            DgData.Rows[index].Cells["ColParticularCr"].Value = DrDrIncome[0]["GName"].ToString();
                            DgData.Rows[index].Cells["ColParticularCrGId"].Value = DrDrIncome[0]["GCode"].ToString();
                            DgData.Rows[index].Cells["ColParticularCrAmount2"].Value = DrDrIncome[0]["Balance"].ToString();

                            //Details
                            if (chkDetailed.Checked == true)
                            {
                                DataRow[] DrDrIncomeDetails = DtHeadDetails.Select("GCode = 26");
                                for (int i = 0; i < DrDrIncomeDetails.Length; i++)
                                {
                                    index = -1;
                                    for (int j = 0; j < DgData.Rows.Count; j++)
                                    {
                                        int ACId = DgData.Rows[j].Cells["ColParticularCrAId"].EditedFormattedValue.ToString().IntParse();
                                        int GId = DgData.Rows[j].Cells["ColParticularCrGId"].EditedFormattedValue.ToString().IntParse();

                                        if (ACId == 0 && GId == 0)
                                        {
                                            index = j;
                                            break;
                                        }
                                    }
                                    if (index == -1)
                                    {
                                        DgData.Rows.Add();
                                        index = DgData.Rows.Count - 1;
                                    }
                                    DgData.Rows[index].Cells["ColParticularCr"].Value = "      " + DrDrIncomeDetails[i]["AccountName"].ToString();
                                    DgData.Rows[index].Cells["ColParticularCrAId"].Value = DrDrIncomeDetails[i]["Id"].ToString();
                                    DgData.Rows[index].Cells["ColParticularCrAmount1"].Value = DrDrIncomeDetails[i]["Balance"].ToString();

                                }
                            }
                        }
                        #endregion

                        #region Closing
                        //if (ClosingStock > 0)
                        //{
                        //    int index = -1;
                        //    for (int i = 0; i < DgData.Rows.Count; i++)
                        //    {
                        //        int ACId = DgData.Rows[i].Cells["ColParticularCrAId"].EditedFormattedValue.ToString().IntParse();
                        //        int GId = DgData.Rows[i].Cells["ColParticularCrGId"].EditedFormattedValue.ToString().IntParse();
                               
                        //        if (ACId == 0 && GId == 0)
                        //        {
                        //            index = i;
                        //            break;
                        //        }
                        //    }
                        //    if (index == -1)
                        //    {
                        //        DgData.Rows.Add();
                        //        index = DgData.Rows.Count - 1;
                        //    }
                        //    DgData.Rows[index].Cells["ColParticularCr"].Value = "Closing Stock";
                        //    DgData.Rows[index].Cells["ColParticularCrGId"].Value = "6";
                        //    DgData.Rows[index].Cells["ColParticularCrAmount2"].Value = ClosingStock.ToString().CurrFormat();
                        //}
                        #endregion

                        #region GP C/D
                        if (Diff < 0)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = "Gross Profit C/D";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrGId"].Value = "-1988";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = Math.Abs(Diff).ToString().CurrFormat();
                        }
                        else if (Diff > 0)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCr"].Value = "Gross Loss C/D";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCrGId"].Value = "-1988";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCrAmount2"].Value = Math.Abs(Diff).ToString().CurrFormat();
                        }
                        #endregion

                        #region Total
                        double DTotal = 0;
                        double CTotal = 0;

                        for (int i = 0; i < DgData.Rows.Count; i++)
                        {
                            int CrGId = DgData.Rows[i].Cells["ColParticularCrGId"].EditedFormattedValue.ToString().IntParse();
                            int DrGId = DgData.Rows[i].Cells["ColParticularDrGId"].EditedFormattedValue.ToString().IntParse();

                            if (CrGId != 0)
                            {
                                DgData.Rows[i].Cells["ColParticularCr"].Style.ForeColor = Color.Red;
                            }
                            if (DrGId != 0)
                            {
                                DgData.Rows[i].Cells["ColParticularDr"].Style.ForeColor = Color.Red;
                            }

                            double TC = DgData.Rows[i].Cells["ColParticularCrAmount2"].EditedFormattedValue.ToString().DoubleParse();
                            double TD = DgData.Rows[i].Cells["ColParticularDrAmount2"].EditedFormattedValue.ToString().DoubleParse();
                            DTotal = DTotal + TD;
                            CTotal = CTotal + TC;
                        }
                        DgData.Rows.Add();
                        DgData.Rows.Add();
                        DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Teal;
                        DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;

                        DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.Teal;
                        DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.White;

                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = "Total";
                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCr"].Value = "Total";

                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCr"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCrAmount2"].Value = CTotal.ToString().CurrFormat();
                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = DTotal.ToString().CurrFormat();
                        DgData.Rows.Add();
                        #endregion

                        ///Region Profit And Loss

                        int CntStart = 0;

                        #region GP B/D
                        if (Diff < 0)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCr"].Value = "Gross Profit B/D";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCrGId"].Value = "-1988";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCrAmount2"].Value = Math.Abs(Diff).ToString().CurrFormat();
                        }
                        else if (Diff > 0)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = "Gross Loss B/D";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrGId"].Value = "-1988";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = Math.Abs(Diff).ToString().CurrFormat();
                        }
                        CntStart = DgData.Rows.Count - 1;
                        #endregion

                        #region Indirect Expenses
                        DataRow[] DrInExp = DtHead.Select("GCode = 21");//PURCHASE
                        if (DrInExp.Length > 0)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = DrInExp[0]["GName"].ToString();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrGId"].Value = DrInExp[0]["GCode"].ToString();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = DrInExp[0]["Balance"].ToString();

                            //Details
                            if (chkDetailed.Checked == true)
                            {
                                DataRow[] DrInExpDetails = DtHeadDetails.Select("GCode = 21");
                                for (int i = 0; i < DrInExpDetails.Length; i++)
                                {
                                    DgData.Rows.Add();
                                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = "      " + DrInExpDetails[i]["AccountName"].ToString();
                                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAId"].Value = DrInExpDetails[i]["Id"].ToString();
                                    DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount1"].Value = DrInExpDetails[i]["Balance"].ToString();

                                }
                            }
                        }
                        #endregion

                        #region Indirect Income
                        DataRow[] DrInDrIncome = DtHead.Select("GCode = 22");
                        if (DrInDrIncome.Length > 0)
                        {
                            int index = CntStart;
                            for (int i = CntStart; i < DgData.Rows.Count; i++)
                            {
                                int ACId = DgData.Rows[i].Cells["ColParticularCrAId"].EditedFormattedValue.ToString().IntParse();
                                int GId = DgData.Rows[i].Cells["ColParticularCrGId"].EditedFormattedValue.ToString().IntParse();

                                if (ACId == 0 && GId == 0)
                                {
                                    index = i;
                                    break;
                                }
                            }
                            if (index == 0)
                            {
                                DgData.Rows.Add();
                                index = DgData.Rows.Count - 1;
                            }
                            DgData.Rows[index].Cells["ColParticularCr"].Value = DrInDrIncome[0]["GName"].ToString();
                            DgData.Rows[index].Cells["ColParticularCrGId"].Value = DrInDrIncome[0]["GCode"].ToString();
                            DgData.Rows[index].Cells["ColParticularCrAmount2"].Value = DrInDrIncome[0]["Balance"].ToString();

                            //Details
                            if (chkDetailed.Checked == true)
                            {
                                DataRow[] DrInDrIncomeDetails = DtHeadDetails.Select("GCode = 22");
                                for (int i = 0; i < DrInDrIncomeDetails.Length; i++)
                                {
                                    index = CntStart;
                                    for (int j = CntStart; j < DgData.Rows.Count; j++)
                                    {
                                        int ACId = DgData.Rows[j].Cells["ColParticularCrAId"].EditedFormattedValue.ToString().IntParse();
                                        int GId = DgData.Rows[j].Cells["ColParticularCrGId"].EditedFormattedValue.ToString().IntParse();
                                        index = j;
                                        if (ACId == 0 && GId == 0)
                                        {
                                            break;
                                        }
                                    }

                                    DgData.Rows[index].Cells["ColParticularCr"].Value = "      " + DrInDrIncomeDetails[i]["AccountName"].ToString();
                                    DgData.Rows[index].Cells["ColParticularCrAId"].Value = DrInDrIncomeDetails[i]["Id"].ToString();
                                    DgData.Rows[index].Cells["ColParticularCrAmount1"].Value = DrInDrIncomeDetails[i]["Balance"].ToString();
                                }
                            }
                        }
                        #endregion

                        ObjDr = DtHead.Compute("Sum(Balance)", "GCode In (21)").ToString().DoubleParse();
                        ObjCr = DtHead.Compute("Sum(Balance)", "GCode In (22)").ToString().DoubleParse();

                        Diff = Diff + ObjDr - ObjCr;

                        #region NP C/D
                        if (Diff < 0)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = "Net Profit C/D";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrGId"].Value = "-1988";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = Math.Abs(Diff).ToString().CurrFormat();
                        }
                        else if (Diff > 0)
                        {
                            DgData.Rows.Add();
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCr"].Value = "Net Loss C/D";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCrGId"].Value = "-1988";
                            DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCrAmount2"].Value = Math.Abs(Diff).ToString().CurrFormat();
                        }
                        #endregion

                        DTotal = 0;
                        CTotal = 0;

                        #region Total
                        for (int i = CntStart; i < DgData.Rows.Count; i++)
                        {
                            int CrGId = DgData.Rows[i].Cells["ColParticularCrGId"].EditedFormattedValue.ToString().IntParse();
                            int DrGId = DgData.Rows[i].Cells["ColParticularDrGId"].EditedFormattedValue.ToString().IntParse();

                            if (CrGId != 0)
                            {
                                DgData.Rows[i].Cells["ColParticularCr"].Style.ForeColor = Color.Red;
                            }
                            if (DrGId != 0)
                            {
                                DgData.Rows[i].Cells["ColParticularDr"].Style.ForeColor = Color.Red;
                            }

                            double TC = DgData.Rows[i].Cells["ColParticularCrAmount2"].EditedFormattedValue.ToString().DoubleParse();
                            double TD = DgData.Rows[i].Cells["ColParticularDrAmount2"].EditedFormattedValue.ToString().DoubleParse();
                            DTotal = DTotal + TD;
                            CTotal = CTotal + TC;
                        }
                        DgData.Rows.Add();
                        DgData.Rows.Add();
                        DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Teal;
                        DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;

                        DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.Teal;
                        DgData.Rows[DgData.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.White;

                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCr"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDr"].Value = "Total";
                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCr"].Value = "Total";
                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularCrAmount2"].Value = CTotal.ToString().CurrFormat();
                        DgData.Rows[DgData.Rows.Count - 1].Cells["ColParticularDrAmount2"].Value = DTotal.ToString().CurrFormat();

                        #endregion

                        ///#endregion
                    }

                    progressBar1.Visible = false;
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
            clsGeneral.ExcelExport(DgData, lblHead.Text);
        }

        private void DgData_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == ColParticularCr.Index || e.ColumnIndex == ColParticularCrAmount1.Index || e.ColumnIndex == ColParticularCrAmount2.Index)
                {
                    DgData[ColParticularCr.Index, DgData.CurrentRow.Index].Selected = true;
                    DgData[ColParticularCrAmount1.Index, DgData.CurrentRow.Index].Selected = true;
                    DgData[ColParticularCrAmount2.Index, DgData.CurrentRow.Index].Selected = true;
                }
                else if (e.ColumnIndex == ColParticularDr.Index || e.ColumnIndex == ColParticularDrAmount1.Index || e.ColumnIndex == ColParticularDrAmount2.Index)
                {
                    DgData[ColParticularDr.Index, DgData.CurrentRow.Index].Selected = true;
                    DgData[ColParticularDrAmount1.Index, DgData.CurrentRow.Index].Selected = true;
                    DgData[ColParticularDrAmount2.Index, DgData.CurrentRow.Index].Selected = true;
                }
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

        public void EditForm()
        {
            try
            {
                int AcId = 0;

                if (DgData.CurrentCell.ColumnIndex == ColParticularDr.Index || DgData.CurrentCell.ColumnIndex == ColParticularDrAmount1.Index || DgData.CurrentCell.ColumnIndex == ColParticularDrAmount2.Index)
                {
                    AcId = DgData.CurrentRow.Cells["ColParticularDrAId"].EditedFormattedValue.ToString().IntParse();
                    DgData[ColParticularDr.Index, DgData.CurrentRow.Index].Selected = true;
                    DgData[ColParticularDrAmount1.Index, DgData.CurrentRow.Index].Selected = true;
                    DgData[ColParticularDrAmount2.Index, DgData.CurrentRow.Index].Selected = true;
                }
                else if (DgData.CurrentCell.ColumnIndex == ColParticularCr.Index || DgData.CurrentCell.ColumnIndex == ColParticularCrAmount1.Index || DgData.CurrentCell.ColumnIndex == ColParticularCrAmount2.Index)
                {
                    AcId = DgData.CurrentRow.Cells["ColParticularCrAId"].EditedFormattedValue.ToString().IntParse();
                    DgData[ColParticularCr.Index, DgData.CurrentRow.Index].Selected = true;
                    DgData[ColParticularCrAmount1.Index, DgData.CurrentRow.Index].Selected = true;
                    DgData[ColParticularCrAmount2.Index, DgData.CurrentRow.Index].Selected = true;
                }
                if (AcId != 0)
                {
                    //clsGeneral.Accounting_Main.OpMonthlySummary(AcId, dateTimePicker1.Value, this);
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void DgData_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter && DgData.CurrentRow != null)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    EditForm();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void DgData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (DgData.CurrentRow != null)
                {
                    
                    EditForm();
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void Frm_TradingProfitLoss_Load(object sender, EventArgs e)
        {
            lblTop.Text = clsGeneral.CompanyName + Environment.NewLine + "Trading And Profit & Loss Account";
            //dateTimePicker1.MinDate = Program.FromDate.Date;
            //dateTimePicker1.MaxDate = Program.FromDate.AddYears(1).AddDays(-1).Date;
            //dateTimePicker1.Value = dateTimePicker1.SetDate();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //DataTable Dt = new DataTable();

                //Dt.Columns.Add("Col1");
                //Dt.Columns.Add("Col2");
                //Dt.Columns.Add("ParticularDr");
                //Dt.Columns.Add("Amount1Dr");
                //Dt.Columns.Add("Amount2Dr");
                //Dt.Columns.Add("ParticularCr");
                //Dt.Columns.Add("Amount1Cr");
                //Dt.Columns.Add("Amount2Cr");

                //for (int i = 0; i < DgData.Rows.Count; i++)
                //{
                //    string Col1 = DgData.Rows[i].Cells["ColParticularDrGId"].EditedFormattedValue.ToString();
                //    string Col2 = DgData.Rows[i].Cells["ColParticularCrGId"].EditedFormattedValue.ToString();
                //    string ParticularDr = DgData.Rows[i].Cells["ColParticularDr"].EditedFormattedValue.ToString();
                //    string Amount1Dr = DgData.Rows[i].Cells["ColParticularDrAmount1"].EditedFormattedValue.ToString();
                //    string Amount2Dr = DgData.Rows[i].Cells["ColParticularDrAmount2"].EditedFormattedValue.ToString();
                //    string ParticularCr = DgData.Rows[i].Cells["ColParticularCr"].EditedFormattedValue.ToString();
                //    string Amount1Cr = DgData.Rows[i].Cells["ColParticularCrAmount1"].EditedFormattedValue.ToString();
                //    string Amount2Cr = DgData.Rows[i].Cells["ColParticularCrAmount2"].EditedFormattedValue.ToString();
                //    Dt.Rows.Add(Col1, Col2, ParticularDr, Amount1Dr, Amount2Dr, ParticularCr, Amount1Cr, Amount2Cr);
                //}
                //if (Dt.Rows.Count > 0)
                //{
                //    if (chkDetailed.Checked == true)
                //    {
                //        FORM.NewAccounting.TradingProfitLossDetailed S = new Finance_Management_System.FORM.NewAccounting.TradingProfitLossDetailed();
                //        #region Code Of Company

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjHeading;
                //        ObjHeading = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtHeading"];
                //        ObjHeading.Text = lblHead.Text;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjCompany;
                //        ObjCompany = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtCompany"];
                //        ObjCompany.Text = clsGeneral.CompanyName;
                //        ObjCompany.ApplyFont(Program.FontStyle);

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd1;
                //        ObjAdd1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtAddress1"];
                //        ObjAdd1.Text = clsGeneral.Address;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd2;
                //        ObjAdd2 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtAddress2"];
                //        ObjAdd2.Text = clsGeneral.Address2;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjCity;
                //        ObjCity = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtCity"];
                //        ObjCity.Text = clsGeneral.City;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjContact;
                //        ObjContact = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtContact"];
                //        ObjContact.Text = clsGeneral.ContactNo;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjEmail;
                //        ObjEmail = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtEmail"];
                //        ObjEmail.Text = clsGeneral.Email;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjTin;
                //        ObjTin = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtTin"];
                //        ObjTin.Text = "Tin No : " + clsGeneral.TinNo;

                //        #endregion
                //        Frm_Preview F = new Frm_Preview(S, Dt);
                //        F.Show();
                //    }
                //    else
                //    {
                //        FORM.NewAccounting.TradingProfitLoss S = new Finance_Management_System.FORM.NewAccounting.TradingProfitLoss();
                //        #region Code Of Company

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjHeading;
                //        ObjHeading = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtHeading"];
                //        ObjHeading.Text = lblHead.Text;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjCompany;
                //        ObjCompany = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtCompany"];
                //        ObjCompany.Text = clsGeneral.CompanyName;
                //        ObjCompany.ApplyFont(Program.FontStyle);

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd1;
                //        ObjAdd1 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtAddress1"];
                //        ObjAdd1.Text = clsGeneral.Address;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjAdd2;
                //        ObjAdd2 = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtAddress2"];
                //        ObjAdd2.Text = clsGeneral.Address2;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjCity;
                //        ObjCity = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtCity"];
                //        ObjCity.Text = clsGeneral.City;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjContact;
                //        ObjContact = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtContact"];
                //        ObjContact.Text = clsGeneral.ContactNo;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjEmail;
                //        ObjEmail = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtEmail"];
                //        ObjEmail.Text = clsGeneral.Email;

                //        CrystalDecisions.CrystalReports.Engine.TextObject ObjTin;
                //        ObjTin = (CrystalDecisions.CrystalReports.Engine.TextObject)S.Section2.ReportObjects["txtTin"];
                //        ObjTin.Text = "Tin No : " + clsGeneral.TinNo;

                //        #endregion
                //        Frm_Preview F = new Frm_Preview(S, Dt);
                //        F.Show();
                //    }
                //}
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }
    }
}
