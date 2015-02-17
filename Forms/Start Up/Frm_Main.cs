using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;

namespace Finance_Management_System
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();
        }

        ClsDataAccess ObjData = new ClsDataAccess();
        ClsDataAccess GetCode = new ClsDataAccess();
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Party F = new Frm_Party();
            F.ShowDialog();
        }

        private void guaranToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Gurantors F = new Frm_Gurantors();
            F.ShowDialog();
        }

        private void loanApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LoanApplication F = new Frm_LoanApplication();
            F.Show();
        }

        private void loanSantionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LoanSanction F = new Frm_LoanSanction();
            F.Show();
        }

        private void eMIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Frm_CashBank F = new Frm_CashBank();
            //F.Type = 1;
            //F.Show();
        }

        private void penaltyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Frm_CashBank F = new Frm_CashBank();
            //F.Type = 2;
            //F.Show();
        }

        private void otherChargesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Frm_CashBank F = new Frm_CashBank();
            //F.Type = 3;
            //F.Show();
        }

        private void officeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_OfficeMaster F = new Frm_OfficeMaster();
            F.ShowDialog();
        }

        private void agentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AgentMaster F = new Frm_AgentMaster();
            F.ShowDialog();
        }

        private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_DocumentsMaster F = new Frm_DocumentsMaster();
            F.ShowDialog();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            string SelSMS = " Select * from CompanyProfile ";
            DataTable DtCompany = ObjData.GetDataTable(SelSMS);
            if (DtCompany.Rows.Count > 0)
            {
                clsGeneral.ContactName = DtCompany.Rows[0]["Contact"].ToString();
                clsGeneral.CompanyName = DtCompany.Rows[0]["Name"].ToString();
                clsGeneral.TinNo = DtCompany.Rows[0]["Tin"].ToString();
                clsGeneral.Address = DtCompany.Rows[0]["Add1"].ToString() + " " + DtCompany.Rows[0]["Add2"].ToString();
                clsGeneral.Address2 = ""; //DtCompany.Rows[0]["P_Add2"].ToString();
                clsGeneral.City = DtCompany.Rows[0]["City"].ToString() + " " + DtCompany.Rows[0]["State"].ToString() + "  " + DtCompany.Rows[0]["Pincode"].ToString();
                //clsGeneral.ContactNo = "Contact : - " + DtCompany.Rows[0]["Phone"].ToString() + ", " + DtCompany.Rows[0]["Mobile"].ToString();
                clsGeneral.ContactNo = DtCompany.Rows[0]["Mobile"].ToString();
                clsGeneral.Email = "E-Mail : - " + DtCompany.Rows[0]["Email"].ToString();
                clsGeneral.PanNo = DtCompany.Rows[0]["PAN"].ToString();
                clsGeneral.TaxCategory = DtCompany.Rows[0]["Category"].ToString();
                clsGeneral.LicenseNo = DtCompany.Rows[0]["TaxNo"].ToString();
                if (DtCompany.Rows[0]["Website"].ToString().Trim().Length > 0)
                {
                    clsGeneral.Email = clsGeneral.Email + " Website : - " + DtCompany.Rows[0]["Website"].ToString();
                }
            }

            #region
            SelSMS = " Select * from Slogan ";
            DataTable DtSlogan = ObjData.GetDataTable(SelSMS);
            if (DtSlogan.Rows.Count > 0)
            {
                clsGeneral.Slogan1 = DtSlogan.Rows[0]["Slogan1"].ToString().Trim();
                clsGeneral.Slogan2 = DtSlogan.Rows[0]["Slogan2"].ToString().Trim();
                clsGeneral.Slogan3 = DtSlogan.Rows[0]["Slogan3"].ToString().Trim();
                clsGeneral.Slogan4 = DtSlogan.Rows[0]["Slogan4"].ToString().Trim();
                clsGeneral.Slogan5 = DtSlogan.Rows[0]["Slogan5"].ToString().Trim();
                if (clsGeneral.Slogan1.Length > 0)
                {
                    clsGeneral.Slogan1 = "**" + clsGeneral.Slogan1;
                }
                if (clsGeneral.Slogan2.Length > 0)
                {
                    clsGeneral.Slogan2 = "**" + clsGeneral.Slogan2;
                }
                if (clsGeneral.Slogan3.Length > 0)
                {
                    clsGeneral.Slogan3 = "**" + clsGeneral.Slogan3;
                }
                if (clsGeneral.Slogan4.Length > 0)
                {
                    clsGeneral.Slogan4 = "**" + clsGeneral.Slogan4;
                }
                if (clsGeneral.Slogan5.Length > 0)
                {
                    clsGeneral.Slogan5 = "**" + clsGeneral.Slogan5;
                }
            }
            #endregion

            Frm_Login F = new Frm_Login();
            F.ShowDialog();
            if (ClsLogin.LoginId == 0)
            {
                Application.Exit();
            }
            if (ClsLogin.LoginId != 1)
            {
                toolsToolStripMenuItem.Visible = false;
            }
        }

        private void companyInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_CompanyMaster F = new Frm_CompanyMaster();
            F.ShowDialog();
        }

        private void loanApplicationChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LoanReturnChart F = new Frm_LoanReturnChart();
            F.Show();
        }

        private void loanSanctionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LoanSanctionReport F = new Frm_LoanSanctionReport();
            F.Show();
        }

        private void loanApplicationHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LoanApplicationReport F = new Frm_LoanApplicationReport();
            F.Show();
        }

        private void guarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Find f = new Frm_Find();
            f.heading = "Guarantors Master";
            f.Text = "Guarantors List";
            f.IsDialog = false;
            //f.F =new Frm_Gurantors();
            f.StatementPass = "Select G_Id As Id,G_Code As Code,G_FirmName As [Firm Name],G_Name As [Person Name],G_City As City,G_Pin As Pin,G_Mobile1 + ' ' + G_Mobile2 As Mobile,G_Address1 + ' ' + G_Address2 + ' ' + G_City + ' ' + G_Pin As Address From GuarantorsMaster Order By G_Code";
            f.Show();
        }

        private void customerListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Find F = new Frm_Find();
            F.heading = "Customer Master";
            F.Text = "Customer List";
            F.IsDialog = false;
            //F.F = new Frm_Party();
            F.StatementPass = "Select P_Id As Id,P_Code As Code,P_FirmName As [Firm Name],P_Name As [Person Name],P_City As City,P_Pin As Pin,P_Mobile1 + ' ' + P_Mobile2 As Mobile,P_Address1 + ' ' + P_Address2 + ' ' + P_City + ' ' + P_Pin As Address From PartyMaster Order By P_FirmName";
            F.Show();
        }

        private void eMIReceivedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_EMIReceived F = new Frm_EMIReceived();
            F.Show();
        }

        private void loanLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_LoanLedger F = new Frm_LoanLedger();
            F.Show();
        }

        private void penaltyReceivedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_PenaltyReceived F = new Frm_PenaltyReceived();
            F.Show();
        }

        private void chargesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Charges F = new Frm_Charges();
            F.Show();
        }

        private void chargesReceivedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ChargesReceived F = new Frm_ChargesReceived();
            F.Show();
        }

        private void eMIDueStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_EMIDueReport F = new Frm_EMIDueReport();
            F.Show();
        }


        private void cashSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Frm_CashSummary F = new Frm_CashSummary();
            //F.Show();
        }

        private void sendReminderMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string SS = "Sp_MonthlyLedger  '" + DateTime.Now.ToString("yyyyMMdd") + "','" + DateTime.Now.AddDays(7).ToString("yyyyMMdd") + "'";
            //DataTable DtDue = ObjData.GetDataTable(SS);
            //if (DtDue.Rows.Count > 0)
            //{

            //    for (int i = 0; i < DtDue.Rows.Count; i++)
            //    {
            //        double Amount = DtDue.Rows[i]["Amount Due"].ToString().DoubleParse();
            //        string Date = DtDue.Rows[i]["Due Date"].ToString();
            //        int EMI = DtDue.Rows[i]["EMI No"].ToString().IntParse();
            //        string Contact = DtDue.Rows[i]["Contact Details"].ToString();

            //        string[] ContactNo = Contact.Split('/');
            //        for (int j = 0; j < ContactNo.Length; j++)
            //        {
            //            string CNo = ContactNo[j].ToString();
            //            if (CNo.Length > 5 && Program.PortNo.Length > 0)
            //            {
            //                string Body = "Dear Customer";
            //                Body = Body + Environment.NewLine;
            //                Body = Body + "      Your " + EMI.FormatOrdinalNumber() + "  EMI Rs " + clsGeneral.CurFormat(Amount) + "/-  is Due on ";
            //                Body = Body + "" + Date + ". Please pay in time to avoid penalty.";
            //                Body = Body + Environment.NewLine;
            //                Body = Body + "Thanks";
            //                Body = Body + Environment.NewLine;
            //                Body = Body + clsGeneral.CompanyName;
            //                clsGeneral.SendSMSMobile(CNo, Body);
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    clsGeneral.ShowMessage("No Due Reminder!!!");
            //}
        }

        private void cashReceiptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Frm_PenaltyReceipt F = new Frm_PenaltyReceipt();
            F.Show();
        }

        private void cashPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_PenaltyCharged F = new Frm_PenaltyCharged();
            F.Show();
        }

        private void customerHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_CustomerHistory F = new Frm_CustomerHistory();
            F.Show();
        }

        private void agentHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AgentHistory F = new Frm_AgentHistory();
            F.Show();
        }

        private void databaseBackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGeneral.ShowMessage("Back Made On Server Machine");
            FolderBrowserDialog Fd = new FolderBrowserDialog();
            DialogResult Dr = Fd.ShowDialog(); ;
            if (Dr == DialogResult.OK)
            {
                string DbPath = Fd.SelectedPath + "\\" + Program.DataBaseName + "_" + System.DateTime.Now.ToString("dd-MM-yyyy_HH_mm") + ".bak" + "";
                string DbQry = "BACKUP DATABASE [" + Program.DataBaseName + "] ";
                DbQry = DbQry + "TO  DISK = N'" + DbPath + "' ";
                DbQry = DbQry + "WITH NOFORMAT, NOINIT, ";
                DbQry = DbQry + "NAME = N'" + Program.DataBaseName + "-Full Database Backup', ";
                DbQry = DbQry + "SKIP, NOREWIND, NOUNLOAD,  STATS = 10 ";
                ObjData.ExecuteQuery(DbQry);
                clsGeneral.ShowMessage("Back Up Made Successfully");
            }
        }

        private void form13ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Form13 F = new Frm_Form13();
            F.Show();
        }

        private void controlPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void restoreDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Restore Database.exe", Program.ServerName + "," + Program.ServerUserId + "," + Program.ServerPassword + "," + Program.DataBaseName + "," + "");
                Application.Exit();
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void accountForeclosureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ForeClosure F = new Frm_ForeClosure();
            F.Show();
        }

        private void reIndexDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ObjData.ExecuteQuery("DBCC shrinkDatabase(" + Program.DataBaseName + ")");

                string SS = "CREATE PROCEDURE [dbo].[spUtil_ReIndexDatabase_UpdateStats] ";
                SS = SS + " AS DECLARE @MyTable VARCHAR(255) ";
                SS = SS + " DECLARE myCursor ";
                SS = SS + " CURSOR FOR ";
                SS = SS + " SELECT table_name ";
                SS = SS + " FROM information_schema.tables ";
                SS = SS + " WHERE table_type = 'base table' ";
                SS = SS + " OPEN myCursor FETCH NEXT ";
                SS = SS + " FROM myCursor INTO @MyTable ";
                SS = SS + " WHILE @@FETCH_STATUS = 0 BEGIN ";
                SS = SS + " PRINT 'Reindexing Table:  ' + @MyTable ";
                SS = SS + " DBCC DBREINDEX(@MyTable, '', 80) ";
                SS = SS + " FETCH NEXT ";
                SS = SS + " FROM myCursor INTO @MyTable ";
                SS = SS + " END CLOSE myCursor ";
                SS = SS + " DEALLOCATE myCursor ";
                SS = SS + " EXEC sp_updatestats ";

                ObjData.ExecuteQuery(SS);

                clsGeneral.ShowMessage("Re-Index Completed !!!");
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        private void userConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_UserConfiguration F = new Frm_UserConfiguration();
            F.ShowDialog();
        }

        private void repairDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string SS ="";
                try
                {
                    SS = "Create Table TmpDataCal ";
                    SS = SS + " (Id Int,EMINo Int, ";
                    SS = SS + " EMI Numeric(18,2), ";
                    SS = SS + " Interest Numeric(18,2), ";
                    SS = SS + " Principal Numeric(18,2), ";
                    SS = SS + " Balance Numeric(18,2), ";
                    SS = SS + " InterestPer Numeric(18,2)) ";
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    string Path = "Finance_Management_System.SQL.Sp_InsTmpData.sql";
                    Assembly A = Assembly.GetExecutingAssembly();
                    StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                    SS = D.ReadToEnd();
                    ObjData.ExecuteQuery(SS);
                }
                catch { }

                try
                {
                    string Path = "Finance_Management_System.SQL.CashBook.SQL";
                    Assembly A = Assembly.GetExecutingAssembly();
                    StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                    SS = D.ReadToEnd();
                    ObjData.ExecuteQuery(SS);
                }
                catch { }

                try
                {
                    SS ="Delete TmpDataCal";
                    ObjData.ExecuteQuery(SS);
                    string Path = "select L_Id From LoanSanction ";
                    DataTable DtId = ObjData.GetDataTable(Path);
                    for (int i = 0; i < DtId.Rows.Count; i++)
                    {
                        int LoanSanctionId = DtId.Rows[i]["L_Id"].ToString().IntParse();
                        SS = "Sp_InsTmpData " + LoanSanctionId + "";
                        ObjData.ExecuteQuery(SS);
                    }
                }
                catch { }
                try
                {
                    string Path = "Finance_Management_System.SQL.InterestReport.sql";
                    Assembly A = Assembly.GetExecutingAssembly();
                    StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                    SS = D.ReadToEnd();
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    SS = "Alter Table EMIReceived ADD C_BankId Int";
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    SS = "Alter Table LoanSanction ADD L_BankId Int";
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    SS = "Alter Table AccountForeClosure ADD F_BankId Int";
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    SS = "Alter Table CashPayment ADD C_BankId Int";
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    SS = "Alter Table CashReceipt ADD C_BankId Int";
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    SS = "Alter Table AgentPayment ADD C_BankId Int";
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    SS = "Alter Table PartyMaster ADD P_Group Int";
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    SS = "Alter Table LoanSanction ADD L_Comm Float";
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    SS = "Create Table GroupCashBook(AcId Int)";
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    string Path = "Finance_Management_System.SQL.AlterPartyMasterProc.sql";
                    Assembly A = Assembly.GetExecutingAssembly();
                    StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                    SS = D.ReadToEnd();
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    string Path = "Finance_Management_System.SQL.CreateTableGroup.sql";
                    Assembly A = Assembly.GetExecutingAssembly();
                    StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                    SS = D.ReadToEnd();
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                //try
                //{
                //    string Path = "Finance_Management_System.SQL.CreateProcAgentComm.sql";
                //    Assembly A = Assembly.GetExecutingAssembly();
                //    StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                //    SS = D.ReadToEnd();
                //    ObjData.ExecuteQuery(SS);
                //    //CreateProcAgentComm.sql
                //}
                //catch { }
                try
                {
                    SS = "Update PartyMaster Set P_Group = 31 Where P_Group Is Null";
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
                try
                {
                    string Path = "Finance_Management_System.SQL.InsertDataProc.sql";
                    Assembly A = Assembly.GetExecutingAssembly();
                    StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                    SS = D.ReadToEnd();
                    ObjData.ExecuteQuery(SS);
                }
                catch { }
            }
            catch { }
        }

        private void agentCommissionCumInterestStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AgentCommissonInterest F = new Frm_AgentCommissonInterest();
            F.Show();
        }

        private void agentPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AgentPayment F = new Frm_AgentPayment();
            F.Show();
        }

        private void agentLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_AgentLedger F = new Frm_AgentLedger();
            F.Show();
        }

        private void interestStatementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_InterestReport F = new Frm_InterestReport();
            F.Show();
        }

        private void cashBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_CashBook F = new Frm_CashBook();
            F.Show();
        }

        private void exportLedgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Finance_Management_System.Forms.Frm_Export FE = new Forms.Frm_Export();


            if (FE.ShowDialog() == DialogResult.OK) //Not clicked the red close button on the form
            {
                DateTime userEnteredDate = FE.DatePicker.Value;

                String input_date = userEnteredDate.ToString("yyyy-MM-dd");
                Export_Ledgers(input_date);
                Export_Transactions(input_date);

            }

            FE.Dispose();
        }

        #region Export all Ledgers in XML Format for Tally
        private void Export_Ledgers(String input_date)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode envelopeNode = doc.CreateElement("ENVELOPE");   //ENVELOPE
            doc.AppendChild(envelopeNode);

            XmlNode headerNode = doc.CreateElement("HEADER");       //HEADER
            envelopeNode.AppendChild(headerNode);

            XmlNode tallyRequestNode = doc.CreateElement("TALLYREQUEST");   //TALLY REQUEST
            tallyRequestNode.AppendChild(doc.CreateTextNode("Import Data"));
            headerNode.AppendChild(tallyRequestNode);


            XmlNode bodyNode = doc.CreateElement("BODY");           //BODY
            envelopeNode.AppendChild(bodyNode);

            XmlNode importDataNode = doc.CreateElement("IMPORTDATA");       //IMPORT DATA
            bodyNode.AppendChild(importDataNode);

            XmlNode requestDescNode = doc.CreateElement("REQUESTDESC");     //REQUEST DESC
            importDataNode.AppendChild(requestDescNode);

            XmlNode reportNameNode = doc.CreateElement("REPORTNAME");       //REPORT NAME
            reportNameNode.AppendChild(doc.CreateTextNode("All Masters"));              //Name of the Report
            requestDescNode.AppendChild(reportNameNode);

            XmlNode staticVariablesNode = doc.CreateElement("STATICVARIABLES");
            requestDescNode.AppendChild(staticVariablesNode);

            XmlNode svCurrentCompnayNode = doc.CreateElement("SVCURRENTCOMPANY");       //SVCURRENTCOMPANY

            svCurrentCompnayNode.AppendChild(doc.CreateTextNode("company_name"));      //Datapull 
            staticVariablesNode.AppendChild(svCurrentCompnayNode);

            XmlNode requestDataNode = doc.CreateElement("REQUESTDATA");
            importDataNode.AppendChild(requestDataNode);

            XmlNode tallyMessageNode = doc.CreateElement("TALLYMESSAGE");
            XmlAttribute tallyMessageAttribute = doc.CreateAttribute("xmlns:UDF");
            tallyMessageAttribute.Value = "TallyUDF";
            tallyMessageNode.Attributes.Append(tallyMessageAttribute);
            requestDataNode.AppendChild(tallyMessageNode);

            XmlNode currencyNode = doc.CreateElement("CURRENCY");
            XmlAttribute currencyAttribute1 = doc.CreateAttribute("NAME");
            currencyAttribute1.Value = "Rs.";
            currencyNode.AppendChild(doc.CreateTextNode(""));
            XmlAttribute currencyAttribute2 = doc.CreateAttribute("RESERVEDNAME");
            currencyAttribute2.Value = "";
            currencyNode.Attributes.Append(currencyAttribute1);
            currencyNode.Attributes.Append(currencyAttribute2);
            tallyMessageNode.AppendChild(currencyNode);

            XmlNode mailingNameNode = doc.CreateElement("MAILINGNAME");
            mailingNameNode.AppendChild(doc.CreateTextNode("Indian Rupees"));
            currencyNode.AppendChild(mailingNameNode);

            XmlNode expandedSymbolNode = doc.CreateElement("EXPANDEDSYMBOL");
            expandedSymbolNode.AppendChild(doc.CreateTextNode("Indian Rupees"));
            currencyNode.AppendChild(expandedSymbolNode);

            XmlNode decimalSymbolNode = doc.CreateElement("DECIMALSYMBOL");
            decimalSymbolNode.AppendChild(doc.CreateTextNode("paise"));
            currencyNode.AppendChild(decimalSymbolNode);

            XmlNode originalSymbolNode = doc.CreateElement("ORIGINALSYMBOL");
            originalSymbolNode.AppendChild(doc.CreateTextNode("Rs."));
            currencyNode.AppendChild(originalSymbolNode);

            XmlNode isSuffixNode = doc.CreateElement("ISSUFFIX");
            isSuffixNode.AppendChild(doc.CreateTextNode("No"));
            currencyNode.AppendChild(isSuffixNode);

            XmlNode hasSpaceNode = doc.CreateElement("HASSPACE");
            hasSpaceNode.AppendChild(doc.CreateTextNode("Yes"));
            currencyNode.AppendChild(hasSpaceNode);

            XmlNode inMillionsNode = doc.CreateElement("INMILLIONS");
            inMillionsNode.AppendChild(doc.CreateTextNode("No"));
            currencyNode.AppendChild(inMillionsNode);

            XmlNode decimalPlacesNode = doc.CreateElement("DECIMALPLACES");
            decimalPlacesNode.AppendChild(doc.CreateTextNode(" 2"));
            currencyNode.AppendChild(decimalPlacesNode);

            XmlNode decimalPlacesForPrintingNode = doc.CreateElement("DECIMALPLACESFORPRINTING");
            decimalPlacesForPrintingNode.AppendChild(doc.CreateTextNode(" 2"));
            currencyNode.AppendChild(decimalPlacesForPrintingNode);

            String S1 = "Select * from PartyMaster";
            DataTable Dt1 = ObjData.GetDataTable(S1);

            for (int i = 0; i < Dt1.Rows.Count; i++)
            {

                String T1 = Dt1.Rows[i]["P_Name"].ToString();
                String T2 = Dt1.Rows[i]["P_Group"].ToString();

                String S2 = "Select * from AccountGroups where ag_code =" + T2;
                DataTable Dt2 = GetCode.GetDataTable(S2);

                String T3 = Dt2.Rows[0]["ag_name"].ToString();

                XmlNode tallyMessage1Node = doc.CreateElement("TALLYMESSAGE");
                XmlAttribute tallyMessage1Attribute = doc.CreateAttribute("xmlns:UDF");
                tallyMessage1Attribute.Value = "TallyUDF";
                tallyMessage1Node.Attributes.Append(tallyMessage1Attribute);
                requestDataNode.AppendChild(tallyMessage1Node);

                XmlNode ledgerNode = doc.CreateElement("LEDGER");
                XmlAttribute ledgerAttribute = doc.CreateAttribute("NAME");
                ledgerAttribute.Value = T1;
                XmlAttribute ledgerAttribute1 = doc.CreateAttribute("RESERVEDNAME");
                ledgerAttribute1.Value = "";
                ledgerNode.Attributes.Append(ledgerAttribute);
                ledgerNode.Attributes.Append(ledgerAttribute1);
                ledgerNode.AppendChild(doc.CreateTextNode(""));

                tallyMessage1Node.AppendChild(ledgerNode);


                XmlNode mailingNameListNode = doc.CreateElement("MAILINGNAME.LIST");
                XmlAttribute mailingNameListAttribute = doc.CreateAttribute("TYPE");
                mailingNameListAttribute.Value = "String";
                mailingNameListNode.AppendChild(doc.CreateTextNode(""));
                mailingNameListNode.Attributes.Append(mailingNameListAttribute);
                ledgerNode.AppendChild(mailingNameListNode);

                XmlNode mailingListMailingNameNode = doc.CreateElement("MAILINGNAME");
                mailingListMailingNameNode.AppendChild(doc.CreateTextNode(T1));
                mailingNameListNode.Attributes.Append(mailingNameListAttribute);
                mailingNameListNode.AppendChild(mailingListMailingNameNode);

                XmlNode currencyNameNode = doc.CreateElement("CURRENCYNAME");
                currencyNameNode.AppendChild(doc.CreateTextNode("Rs."));
                ledgerNode.AppendChild(currencyNameNode);

                XmlNode parentNode = doc.CreateElement("PARENT");
                parentNode.AppendChild(doc.CreateTextNode(T3));
                ledgerNode.AppendChild(parentNode);

                XmlNode exciseLedgerClassificationNode = doc.CreateElement("EXCISELEDGERCLASSIFICATION");
                exciseLedgerClassificationNode.AppendChild(doc.CreateTextNode("Default"));
                ledgerNode.AppendChild(exciseLedgerClassificationNode);

                XmlNode isBillwiseOnNode = doc.CreateElement("ISBILLWISEON");
                isBillwiseOnNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isBillwiseOnNode);

                XmlNode isCostCentresOnNode = doc.CreateElement("ISCOSTCENTRESON");
                isCostCentresOnNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isCostCentresOnNode);

                XmlNode isInterestOnNode = doc.CreateElement("ISINTERESTON");
                isInterestOnNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isInterestOnNode);

                XmlNode allowInMobileNode = doc.CreateElement("ALLOWINMOBILE");
                allowInMobileNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(allowInMobileNode);

                XmlNode isCondensedNode = doc.CreateElement("ISCONDENSED");
                isCondensedNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isCondensedNode);

                XmlNode affectsStockNode = doc.CreateElement("AFFECTSSTOCK");
                affectsStockNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(affectsStockNode);

                XmlNode forPayrollNode = doc.CreateElement("FORPAYROLL");
                forPayrollNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(forPayrollNode);

                XmlNode interestOnBillwiseNode = doc.CreateElement("INTERESTONBILLWISE");
                interestOnBillwiseNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(interestOnBillwiseNode);

                XmlNode overrideInterestNode = doc.CreateElement("OVERRIDEINTEREST");
                overrideInterestNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(overrideInterestNode);

                XmlNode overrideAdvInterestNode = doc.CreateElement("OVERRIDEADVINTEREST");
                overrideAdvInterestNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(overrideAdvInterestNode);


                XmlNode useForVatNode = doc.CreateElement("USEFORVAT");
                useForVatNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(useForVatNode);

                XmlNode ignoreTdsExemptNode = doc.CreateElement("IGNORETDSEXEMPT");
                ignoreTdsExemptNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(ignoreTdsExemptNode);

                XmlNode isTcsApplicableNode = doc.CreateElement("ISTCSAPPLICABLE");
                isTcsApplicableNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isTcsApplicableNode);

                XmlNode isTdsApplicableNode = doc.CreateElement("ISTDSAPPLICABLE");
                isTdsApplicableNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isTdsApplicableNode);

                XmlNode isFbtApplicableNode = doc.CreateElement("ISFBTAPPLICABLE");
                isFbtApplicableNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isFbtApplicableNode);

                XmlNode isGstApplicable = doc.CreateElement("ISGSTAPPLICABLE");
                isGstApplicable.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isGstApplicable);

                XmlNode isexciseApplicable = doc.CreateElement("ISEXCISEAPPLICABLE");
                isexciseApplicable.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isexciseApplicable);

                XmlNode showInPayslipNode = doc.CreateElement("SHOWINPAYSLIP");
                showInPayslipNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(showInPayslipNode);

                XmlNode useForGratuityNode = doc.CreateElement("USEFORGRATUITY");
                useForGratuityNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(useForGratuityNode);


                XmlNode forServiceTaxNode = doc.CreateElement("FORSERVICETAX");
                forServiceTaxNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(forServiceTaxNode);


                XmlNode isInputCreditNode = doc.CreateElement("ISINPUTCREDIT");
                isInputCreditNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isInputCreditNode);


                XmlNode isExemptedNode = doc.CreateElement("ISEXEMPTED");
                isExemptedNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isExemptedNode);


                XmlNode isAbatementApplicableNode = doc.CreateElement("ISABATEMENTAPPLICABLE");
                isAbatementApplicableNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(isAbatementApplicableNode);


                XmlNode tdsDeducteeIsSpecialRateNode = doc.CreateElement("TDSDEDUCTEEISSPECIALRATE");
                tdsDeducteeIsSpecialRateNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(tdsDeducteeIsSpecialRateNode);


                XmlNode auditedNode = doc.CreateElement("AUDITED");
                auditedNode.AppendChild(doc.CreateTextNode("No"));
                ledgerNode.AppendChild(auditedNode);


                XmlNode sortPositionNode = doc.CreateElement("SORTPOSITION");
                sortPositionNode.AppendChild(doc.CreateTextNode(" 1000"));
                ledgerNode.AppendChild(sortPositionNode);


                XmlNode languageNameListNode = doc.CreateElement("LANGUAGENAME.LIST");
                languageNameListNode.AppendChild(doc.CreateTextNode(""));
                ledgerNode.AppendChild(languageNameListNode);


                XmlNode nameListNode = doc.CreateElement("NAME.LIST");
                XmlAttribute nameListAttribute = doc.CreateAttribute("TYPE");
                nameListAttribute.Value = "String";
                nameListNode.Attributes.Append(nameListAttribute);
                languageNameListNode.AppendChild(nameListNode);

                XmlNode nameListnameNode = doc.CreateElement("NAME");
                nameListnameNode.AppendChild(doc.CreateTextNode(T1));
                nameListNode.AppendChild(nameListnameNode);

                XmlNode languageIdNode = doc.CreateElement("LANGUAGEID");
                languageIdNode.AppendChild(doc.CreateTextNode(" 1033"));
                languageNameListNode.AppendChild(languageIdNode);
            }

            Dt1.Dispose();

            doc.Save("Ledger-" + input_date + ".xml");
        }
        #endregion

        private void cashReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #region Export all the Transactions in Tally XML Format
        private void Export_Transactions(String input_date)
        {
           
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode envelopeNode = doc.CreateElement("ENVELOPE");   //ENVELOPE
            doc.AppendChild(envelopeNode);

            XmlNode headerNode = doc.CreateElement("HEADER");       //HEADER
            envelopeNode.AppendChild(headerNode);

            XmlNode tallyRequestNode = doc.CreateElement("TALLYREQUEST");   //TALLY REQUEST
            tallyRequestNode.AppendChild(doc.CreateTextNode("Import Data"));
            headerNode.AppendChild(tallyRequestNode);


            XmlNode bodyNode = doc.CreateElement("BODY");           //BODY
            envelopeNode.AppendChild(bodyNode);

            XmlNode importDataNode = doc.CreateElement("IMPORTDATA");       //IMPORT DATA
            bodyNode.AppendChild(importDataNode);

            XmlNode requestDescNode = doc.CreateElement("REQUESTDESC");     //REQUEST DESC
            importDataNode.AppendChild(requestDescNode);

            XmlNode reportNameNode = doc.CreateElement("REPORTNAME");       //REPORT NAME
            reportNameNode.AppendChild(doc.CreateTextNode("All Masters"));              //Name of the Report
            requestDescNode.AppendChild(reportNameNode);

            XmlNode staticVariablesNode = doc.CreateElement("STATICVARIABLES");
            requestDescNode.AppendChild(staticVariablesNode);

            XmlNode svCurrentCompnayNode = doc.CreateElement("SVCURRENTCOMPANY");       //SVCURRENTCOMPANY

            svCurrentCompnayNode.AppendChild(doc.CreateTextNode("company_name"));      //Datapull 
            staticVariablesNode.AppendChild(svCurrentCompnayNode);

            XmlNode requestDataNode = doc.CreateElement("REQUESTDATA");
            importDataNode.AppendChild(requestDataNode);

            String S1 = "SELECT case when cr ='0' then 'Cash' else cr end as cr, dr, amount, CONVERT(VARCHAR(10), dt, 112) as dt FROM (select l.L_Id as id, ISNULL(p.P_Name,'0') as cr, l.L_LoanAmount as amount, l.L_ApplicableDate as dt from LoanSanction l left outer join  PartyMaster as p on l.L_BankId = p.P_Id) as t_cr, (select l.L_Id as id, p.P_Name as dr from LoanSanction l, PartyMaster as p WHERE l.L_PartyId = p.P_Id) as t_dr where t_cr.id = t_dr.id AND dt in ('" + input_date + "');";
            DataTable Dt1 = ObjData.GetDataTable(S1);

            for (int i = 0; i < Dt1.Rows.Count; i++)
            {
                String cr = Dt1.Rows[i]["cr"].ToString();
                String dr = Dt1.Rows[i]["dr"].ToString();
                String amount = Dt1.Rows[i]["amount"].ToString();
                String dt = Dt1.Rows[i]["dt"].ToString();

                String guid = input_date + "-DISBURSE-" + cr + "-" + dr + "-" + amount + "-" + i.ToString();            //REMOTEID

                XmlNode tallyMessageNode = doc.CreateElement("TALLYMESSAGE");
                XmlAttribute productAttribute = doc.CreateAttribute("xmlns:UDF");
                productAttribute.Value = "TallyUDF";
                tallyMessageNode.Attributes.Append(productAttribute);
                requestDataNode.AppendChild(tallyMessageNode);

                XmlNode voucherNode = doc.CreateElement("VOUCHER");
                XmlAttribute nameAttribute = doc.CreateAttribute("REMOTEID");
                nameAttribute.Value = guid;
                XmlAttribute nameAttribute1 = doc.CreateAttribute("VCHTYPE");
                nameAttribute1.Value = "Payment";                                              //Data Pull
                XmlAttribute nameAttribute2 = doc.CreateAttribute("ACTION");
                nameAttribute2.Value = "Create";
                voucherNode.Attributes.Append(nameAttribute);
                voucherNode.Attributes.Append(nameAttribute1);
                voucherNode.Attributes.Append(nameAttribute2);
                tallyMessageNode.AppendChild(voucherNode);

                XmlNode dateNode = doc.CreateElement("DATE");
                dateNode.AppendChild(doc.CreateTextNode(dt));                           //Data Pull
                voucherNode.AppendChild(dateNode);


                XmlNode guidNode = doc.CreateElement("GUID");
                guidNode.AppendChild(doc.CreateTextNode(guid));
                voucherNode.AppendChild(guidNode);

                XmlNode narrationNode = doc.CreateElement("NARRATION");
                narrationNode.AppendChild(doc.CreateTextNode("Loan Disbursed to " + dr + " of amount " + amount));                  //Data Pull
                voucherNode.AppendChild(narrationNode);


                XmlNode voucherTypeNameNode = doc.CreateElement("VOUCHERTYPENAME");     //Data Pull
                voucherTypeNameNode.AppendChild(doc.CreateTextNode("Payment"));
                voucherNode.AppendChild(voucherTypeNameNode);


                XmlNode voucherNumberNode = doc.CreateElement("VOUCHERNUMBER");
                voucherNumberNode.AppendChild(doc.CreateTextNode(i.ToString()));
                voucherNode.AppendChild(voucherNumberNode);

                XmlNode partyLedgerNameNode = doc.CreateElement("PARTYLEDGERNAME");
                partyLedgerNameNode.AppendChild(doc.CreateTextNode(dr));
                voucherNode.AppendChild(partyLedgerNameNode);

                XmlNode fbtPaymentTypeNode = doc.CreateElement("FBTPAYMENTTYPE");
                fbtPaymentTypeNode.AppendChild(doc.CreateTextNode("Default"));
                voucherNode.AppendChild(fbtPaymentTypeNode);

                XmlNode diffactualQtyNode = doc.CreateElement("DIFFACTUALQTY");
                diffactualQtyNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(diffactualQtyNode);

                XmlNode auditedNode = doc.CreateElement("AUDITED");
                auditedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(auditedNode);

                XmlNode forJobCostingNode = doc.CreateElement("FORJOBCOSTING");
                forJobCostingNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(forJobCostingNode);

                XmlNode isOptionalNode = doc.CreateElement("ISOPTIONAL");
                isOptionalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isOptionalNode);

                XmlNode effectivedateNode = doc.CreateElement("EFFECTIVEDATE");
                effectivedateNode.AppendChild(doc.CreateTextNode(dt));
                voucherNode.AppendChild(effectivedateNode);

                XmlNode useForInterestNode = doc.CreateElement("USEFORINTEREST");
                useForInterestNode.AppendChild(doc.CreateTextNode("NO"));
                voucherNode.AppendChild(useForInterestNode);

                XmlNode useforgainlossNode = doc.CreateElement("USEFORGAINLOSS");
                useforgainlossNode.AppendChild(doc.CreateTextNode("NO"));
                voucherNode.AppendChild(useforgainlossNode);

                XmlNode useforgodowntransferNode = doc.CreateElement("USEFORGODOWNTRANSFER");
                useforgodowntransferNode.AppendChild(doc.CreateTextNode("NO"));
                voucherNode.AppendChild(useforgodowntransferNode);

                XmlNode useforcompoundNode = doc.CreateElement("USEFORCOMPOUND");
                useforcompoundNode.AppendChild(doc.CreateTextNode("NO"));
                voucherNode.AppendChild(useforcompoundNode);

                XmlNode alteridNode = doc.CreateElement("ALTERID");
                alteridNode.AppendChild(doc.CreateTextNode(i.ToString()));
                voucherNode.AppendChild(alteridNode);

                XmlNode exciseopeningNode = doc.CreateElement("EXCISEOPENING");
                exciseopeningNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(exciseopeningNode);

                XmlNode useforfinalproductionNode = doc.CreateElement("USEFORFINALPRODUCTION");
                useforfinalproductionNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforfinalproductionNode);

                XmlNode iscancelledNode = doc.CreateElement("ISCANCELLED");
                iscancelledNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(iscancelledNode);

                XmlNode hascashflowNode = doc.CreateElement("HASCASHFLOW");
                hascashflowNode.AppendChild(doc.CreateTextNode("Yes"));
                voucherNode.AppendChild(hascashflowNode);

                XmlNode ispostdatedNode = doc.CreateElement("ISPOSTDATED");
                ispostdatedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(ispostdatedNode);

                XmlNode usetrackingnumberNode = doc.CreateElement("USETRACKINGNUMBER");
                usetrackingnumberNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(usetrackingnumberNode);

                XmlNode isinvoiceNode = doc.CreateElement("ISINVOICE");
                isinvoiceNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isinvoiceNode);

                XmlNode mfgjournalNode = doc.CreateElement("MFGJOURNAL");
                mfgjournalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(mfgjournalNode);

                XmlNode hasdiscountsNode = doc.CreateElement("HASDISCOUNTS");
                hasdiscountsNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(hasdiscountsNode);

                XmlNode aspayslipNode = doc.CreateElement("ASPAYSLIP");
                aspayslipNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(aspayslipNode);

                XmlNode iscostcentreNode = doc.CreateElement("ISCOSTCENTRE");
                iscostcentreNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(iscostcentreNode);

                XmlNode isdeletedNode = doc.CreateElement("ISDELETED");
                isdeletedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isdeletedNode);

                XmlNode asoriginalNode = doc.CreateElement("ASORIGINAL");
                asoriginalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(asoriginalNode);

                XmlNode allledgerentriesNode = doc.CreateElement("ALLLEDGERENTRIES.LIST");
                voucherNode.AppendChild(allledgerentriesNode);

                //AllLEDGERENTRIES child nodes
                XmlNode ledgernameNode = doc.CreateElement("LEDGERNAME");
                ledgernameNode.AppendChild(doc.CreateTextNode(dr));
                allledgerentriesNode.AppendChild(ledgernameNode);

                XmlNode isdeemedpositiveNode = doc.CreateElement("ISDEEMEDPOSITIVE");
                isdeemedpositiveNode.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode.AppendChild(isdeemedpositiveNode);

                XmlNode ledgerfromitemNode = doc.CreateElement("LEDGERFROMITEM");
                ledgerfromitemNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(ledgerfromitemNode);

                XmlNode removezeroentriesNode = doc.CreateElement("REMOVEZEROENTRIES");
                removezeroentriesNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(removezeroentriesNode);

                XmlNode ispartyledgerNode = doc.CreateElement("ISPARTYLEDGER");
                ispartyledgerNode.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode.AppendChild(ispartyledgerNode);

                XmlNode amountNode = doc.CreateElement("AMOUNT");
                amountNode.AppendChild(doc.CreateTextNode("-" + amount));
                allledgerentriesNode.AppendChild(amountNode);

                XmlNode allledgerentriesNode1 = doc.CreateElement("ALLLEDGERENTRIES.LIST");
                voucherNode.AppendChild(allledgerentriesNode1);

                //AllLEDGERENTRIES1 child nodes
                XmlNode ledgername1Node = doc.CreateElement("LEDGERNAME");
                ledgername1Node.AppendChild(doc.CreateTextNode(cr));
                allledgerentriesNode1.AppendChild(ledgername1Node);

                XmlNode isdeemedpositive1Node = doc.CreateElement("ISDEEMEDPOSITIVE");
                isdeemedpositive1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(isdeemedpositive1Node);

                XmlNode ledgerfromitem1Node = doc.CreateElement("LEDGERFROMITEM");
                ledgerfromitem1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(ledgerfromitem1Node);

                XmlNode removezeroentries1Node = doc.CreateElement("REMOVEZEROENTRIES");
                removezeroentries1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(removezeroentries1Node);

                XmlNode ispartyledger1Node = doc.CreateElement("ISPARTYLEDGER");
                ispartyledger1Node.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode1.AppendChild(ispartyledger1Node);

                XmlNode amount1Node = doc.CreateElement("AMOUNT");
                amount1Node.AppendChild(doc.CreateTextNode(amount));
                allledgerentriesNode1.AppendChild(amount1Node);

            }

            String S2 = "SELECT cr, case when dr ='0' then 'Cash' else dr end as dr, amount, CONVERT(VARCHAR(10), dt, 112) as dt  FROM (SELECT emi.C_Id as id, party.P_Name cr, emi.C_AmountRec as amount, emi.C_Date as dt  FROM EMIReceived emi, LoanSanction sanc, LoanApplication app, PartyMaster party  WHERE emi.C_SId = sanc.L_Id and sanc.L_ApplicationId = app.L_Id and app.L_PartyId = party.P_id) as t_cr, (SELECT emi.C_Id as id, ISNULL(party.P_Name,'0') AS dr FROM EMIReceived emi LEFT OUTER JOIN PartyMaster party  on emi.C_BankId = party.P_Id) as t_dr WHERE t_cr.id = t_dr.id AND dt in ('" + input_date + "');";
            DataTable Dt2 = ObjData.GetDataTable(S2);

            for (int i = 0; i < Dt2.Rows.Count; i++)
            {
                String cr = Dt2.Rows[i]["cr"].ToString();
                String dr = Dt2.Rows[i]["dr"].ToString();
                String amount = Dt2.Rows[i]["amount"].ToString();
                String dt = Dt2.Rows[i]["dt"].ToString();

                String guid = input_date + "-EMI_RECEIVED-" + cr + "-" + dr + "-" + amount + "-" + i.ToString();

                XmlNode tallyMessageNode = doc.CreateElement("TALLYMESSAGE");
                XmlAttribute productAttribute = doc.CreateAttribute("xmlns:UDF");
                productAttribute.Value = "TallyUDF";
                tallyMessageNode.Attributes.Append(productAttribute);
                requestDataNode.AppendChild(tallyMessageNode);

                XmlNode voucherNode = doc.CreateElement("VOUCHER");
                XmlAttribute nameAttribute = doc.CreateAttribute("REMOTEID");
                nameAttribute.Value = guid;
                XmlAttribute nameAttribute1 = doc.CreateAttribute("VCHTYPE");
                nameAttribute1.Value = "Receipt";                                              //Data Pull
                XmlAttribute nameAttribute2 = doc.CreateAttribute("ACTION");
                nameAttribute2.Value = "Create";
                voucherNode.Attributes.Append(nameAttribute);
                voucherNode.Attributes.Append(nameAttribute1);
                voucherNode.Attributes.Append(nameAttribute2);
                tallyMessageNode.AppendChild(voucherNode);

                XmlNode dateNode = doc.CreateElement("DATE");
                dateNode.AppendChild(doc.CreateTextNode(dt));                           //Data Pull
                voucherNode.AppendChild(dateNode);


                XmlNode guidNode = doc.CreateElement("GUID");
                guidNode.AppendChild(doc.CreateTextNode(guid));
                voucherNode.AppendChild(guidNode);

                XmlNode narrationNode = doc.CreateElement("NARRATION");
                narrationNode.AppendChild(doc.CreateTextNode("EMI Received " + amount + " by " + cr));                  //Data Pull
                voucherNode.AppendChild(narrationNode);


                XmlNode voucherTypeNameNode = doc.CreateElement("VOUCHERTYPENAME");     //Data Pull
                voucherTypeNameNode.AppendChild(doc.CreateTextNode("Receipt"));
                voucherNode.AppendChild(voucherTypeNameNode);


                XmlNode voucherNumberNode = doc.CreateElement("VOUCHERNUMBER");
                voucherNumberNode.AppendChild(doc.CreateTextNode(i.ToString()));
                voucherNode.AppendChild(voucherNumberNode);

                XmlNode partyLedgerNameNode = doc.CreateElement("PARTYLEDGERNAME");
                partyLedgerNameNode.AppendChild(doc.CreateTextNode(cr));
                voucherNode.AppendChild(partyLedgerNameNode);

                XmlNode fbtPaymentTypeNode = doc.CreateElement("FBTPAYMENTTYPE");
                fbtPaymentTypeNode.AppendChild(doc.CreateTextNode("Default"));
                voucherNode.AppendChild(fbtPaymentTypeNode);

                XmlNode diffactualQtyNode = doc.CreateElement("DIFFACTUALQTY");
                diffactualQtyNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(diffactualQtyNode);

                XmlNode auditedNode = doc.CreateElement("AUDITED");
                auditedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(auditedNode);

                XmlNode forJobCostingNode = doc.CreateElement("FORJOBCOSTING");
                forJobCostingNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(forJobCostingNode);

                XmlNode isOptionalNode = doc.CreateElement("ISOPTIONAL");
                isOptionalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isOptionalNode);

                XmlNode effectivedateNode = doc.CreateElement("EFFECTIVEDATE");
                effectivedateNode.AppendChild(doc.CreateTextNode(dt));
                voucherNode.AppendChild(effectivedateNode);

                XmlNode useForInterestNode = doc.CreateElement("USEFORINTEREST");
                useForInterestNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useForInterestNode);

                XmlNode useforgainlossNode = doc.CreateElement("USEFORGAINLOSS");
                useforgainlossNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforgainlossNode);

                XmlNode useforgodowntransferNode = doc.CreateElement("USEFORGODOWNTRANSFER");
                useforgodowntransferNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforgodowntransferNode);

                XmlNode useforcompoundNode = doc.CreateElement("USEFORCOMPOUND");
                useforcompoundNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforcompoundNode);

                XmlNode alteridNode = doc.CreateElement("ALTERID");
                alteridNode.AppendChild(doc.CreateTextNode(i.ToString()));
                voucherNode.AppendChild(alteridNode);

                XmlNode exciseopeningNode = doc.CreateElement("EXCISEOPENING");
                exciseopeningNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(exciseopeningNode);

                XmlNode useforfinalproductionNode = doc.CreateElement("USEFORFINALPRODUCTION");
                useforfinalproductionNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforfinalproductionNode);

                XmlNode iscancelledNode = doc.CreateElement("ISCANCELLED");
                iscancelledNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(iscancelledNode);

                XmlNode hascashflowNode = doc.CreateElement("HASCASHFLOW");
                hascashflowNode.AppendChild(doc.CreateTextNode("Yes"));
                voucherNode.AppendChild(hascashflowNode);

                XmlNode ispostdatedNode = doc.CreateElement("ISPOSTDATED");
                ispostdatedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(ispostdatedNode);

                XmlNode usetrackingnumberNode = doc.CreateElement("USETRACKINGNUMBER");
                usetrackingnumberNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(usetrackingnumberNode);

                XmlNode isinvoiceNode = doc.CreateElement("ISINVOICE");
                isinvoiceNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isinvoiceNode);

                XmlNode mfgjournalNode = doc.CreateElement("MFGJOURNAL");
                mfgjournalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(mfgjournalNode);

                XmlNode hasdiscountsNode = doc.CreateElement("HASDISCOUNTS");
                hasdiscountsNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(hasdiscountsNode);

                XmlNode aspayslipNode = doc.CreateElement("ASPAYSLIP");
                aspayslipNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(aspayslipNode);

                XmlNode iscostcentreNode = doc.CreateElement("ISCOSTCENTRE");
                iscostcentreNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(iscostcentreNode);

                XmlNode isdeletedNode = doc.CreateElement("ISDELETED");
                isdeletedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isdeletedNode);

                XmlNode asoriginalNode = doc.CreateElement("ASORIGINAL");
                asoriginalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(asoriginalNode);

                XmlNode allledgerentriesNode = doc.CreateElement("ALLLEDGERENTRIES.LIST");
                voucherNode.AppendChild(allledgerentriesNode);

                //AllLEDGERENTRIES child nodes
                XmlNode ledgernameNode = doc.CreateElement("LEDGERNAME");
                ledgernameNode.AppendChild(doc.CreateTextNode(cr));
                allledgerentriesNode.AppendChild(ledgernameNode);

                XmlNode isdeemedpositiveNode = doc.CreateElement("ISDEEMEDPOSITIVE");
                isdeemedpositiveNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(isdeemedpositiveNode);

                XmlNode ledgerfromitemNode = doc.CreateElement("LEDGERFROMITEM");
                ledgerfromitemNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(ledgerfromitemNode);

                XmlNode removezeroentriesNode = doc.CreateElement("REMOVEZEROENTRIES");
                removezeroentriesNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(removezeroentriesNode);

                XmlNode ispartyledgerNode = doc.CreateElement("ISPARTYLEDGER");
                ispartyledgerNode.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode.AppendChild(ispartyledgerNode);

                XmlNode amountNode = doc.CreateElement("AMOUNT");
                amountNode.AppendChild(doc.CreateTextNode(amount));
                allledgerentriesNode.AppendChild(amountNode);

                XmlNode allledgerentriesNode1 = doc.CreateElement("ALLLEDGERENTRIES.LIST");
                voucherNode.AppendChild(allledgerentriesNode1);

                //AllLEDGERENTRIES1 child nodes
                XmlNode ledgername1Node = doc.CreateElement("LEDGERNAME");
                ledgername1Node.AppendChild(doc.CreateTextNode(dr));
                allledgerentriesNode1.AppendChild(ledgername1Node);

                XmlNode isdeemedpositive1Node = doc.CreateElement("ISDEEMEDPOSITIVE");
                isdeemedpositive1Node.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode1.AppendChild(isdeemedpositive1Node);

                XmlNode ledgerfromitem1Node = doc.CreateElement("LEDGERFROMITEM");
                ledgerfromitem1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(ledgerfromitem1Node);

                XmlNode removezeroentries1Node = doc.CreateElement("REMOVEZEROENTRIES");
                removezeroentries1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(removezeroentries1Node);

                XmlNode ispartyledger1Node = doc.CreateElement("ISPARTYLEDGER");
                ispartyledger1Node.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode1.AppendChild(ispartyledger1Node);

                XmlNode amount1Node = doc.CreateElement("AMOUNT");
                amount1Node.AppendChild(doc.CreateTextNode("-" + amount));
                allledgerentriesNode1.AppendChild(amount1Node);

            }

            String S3 = "SELECT 'Interest A/c' as cr, party.P_Name as dr, pmts.F_InterestAmount amount, CONVERT(VARCHAR(10), pmts.F_Date, 112) as dt  from AccountForeClosure as pmts, LoanSanction as loan, PartyMaster as party where pmts.F_SId = loan.L_Id AND loan.L_PartyId = party.P_Id AND pmts.F_Date in ('" + input_date + "');";
            DataTable Dt3 = ObjData.GetDataTable(S3);

            for (int i = 0; i < Dt3.Rows.Count; i++)
            {
                String cr = Dt3.Rows[i]["cr"].ToString();
                String dr = Dt3.Rows[i]["dr"].ToString();
                String amount = Dt3.Rows[i]["amount"].ToString();
                String dt = Dt3.Rows[i]["dt"].ToString();

                String guid = input_date + "-FORECLOSURE_INTEREST-" + cr + "-" + dr + "-" + amount + "-" + i.ToString();

                XmlNode tallyMessageNode = doc.CreateElement("TALLYMESSAGE");
                XmlAttribute productAttribute = doc.CreateAttribute("xmlns:UDF");
                productAttribute.Value = "TallyUDF";
                tallyMessageNode.Attributes.Append(productAttribute);
                requestDataNode.AppendChild(tallyMessageNode);

                XmlNode voucherNode = doc.CreateElement("VOUCHER");
                XmlAttribute nameAttribute = doc.CreateAttribute("REMOTEID");
                nameAttribute.Value = guid;
                XmlAttribute nameAttribute1 = doc.CreateAttribute("VCHTYPE");
                nameAttribute1.Value = "Journal";                                              //Data Pull
                XmlAttribute nameAttribute2 = doc.CreateAttribute("ACTION");
                nameAttribute2.Value = "Create";
                voucherNode.Attributes.Append(nameAttribute);
                voucherNode.Attributes.Append(nameAttribute1);
                voucherNode.Attributes.Append(nameAttribute2);
                tallyMessageNode.AppendChild(voucherNode);

                XmlNode dateNode = doc.CreateElement("DATE");
                dateNode.AppendChild(doc.CreateTextNode(dt));                           //Data Pull
                voucherNode.AppendChild(dateNode);


                XmlNode guidNode = doc.CreateElement("GUID");
                guidNode.AppendChild(doc.CreateTextNode(guid));
                voucherNode.AppendChild(guidNode);

                XmlNode narrationNode = doc.CreateElement("NARRATION");
                narrationNode.AppendChild(doc.CreateTextNode("ForeClosure Interest " + amount + " by " + dr));                  //Data Pull
                voucherNode.AppendChild(narrationNode);


                XmlNode voucherTypeNameNode = doc.CreateElement("VOUCHERTYPENAME");     //Data Pull
                voucherTypeNameNode.AppendChild(doc.CreateTextNode("Journal"));
                voucherNode.AppendChild(voucherTypeNameNode);


                XmlNode voucherNumberNode = doc.CreateElement("VOUCHERNUMBER");
                voucherNumberNode.AppendChild(doc.CreateTextNode(i.ToString()));
                voucherNode.AppendChild(voucherNumberNode);

                XmlNode partyLedgerNameNode = doc.CreateElement("PARTYLEDGERNAME");
                partyLedgerNameNode.AppendChild(doc.CreateTextNode(dr));
                voucherNode.AppendChild(partyLedgerNameNode);

                XmlNode fbtPaymentTypeNode = doc.CreateElement("FBTPAYMENTTYPE");
                fbtPaymentTypeNode.AppendChild(doc.CreateTextNode("Default"));
                voucherNode.AppendChild(fbtPaymentTypeNode);

                XmlNode diffactualQtyNode = doc.CreateElement("DIFFACTUALQTY");
                diffactualQtyNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(diffactualQtyNode);

                XmlNode auditedNode = doc.CreateElement("AUDITED");
                auditedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(auditedNode);

                XmlNode forJobCostingNode = doc.CreateElement("FORJOBCOSTING");
                forJobCostingNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(forJobCostingNode);

                XmlNode isOptionalNode = doc.CreateElement("ISOPTIONAL");
                isOptionalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isOptionalNode);

                XmlNode effectivedateNode = doc.CreateElement("EFFECTIVEDATE");
                effectivedateNode.AppendChild(doc.CreateTextNode(dt));
                voucherNode.AppendChild(effectivedateNode);

                XmlNode useForInterestNode = doc.CreateElement("USEFORINTEREST");
                useForInterestNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useForInterestNode);

                XmlNode useforgainlossNode = doc.CreateElement("USEFORGAINLOSS");
                useforgainlossNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforgainlossNode);

                XmlNode useforgodowntransferNode = doc.CreateElement("USEFORGODOWNTRANSFER");
                useforgodowntransferNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforgodowntransferNode);

                XmlNode useforcompoundNode = doc.CreateElement("USEFORCOMPOUND");
                useforcompoundNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforcompoundNode);

                XmlNode alteridNode = doc.CreateElement("ALTERID");
                alteridNode.AppendChild(doc.CreateTextNode(i.ToString()));
                voucherNode.AppendChild(alteridNode);

                XmlNode exciseopeningNode = doc.CreateElement("EXCISEOPENING");
                exciseopeningNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(exciseopeningNode);

                XmlNode useforfinalproductionNode = doc.CreateElement("USEFORFINALPRODUCTION");
                useforfinalproductionNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforfinalproductionNode);

                XmlNode iscancelledNode = doc.CreateElement("ISCANCELLED");
                iscancelledNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(iscancelledNode);

                XmlNode hascashflowNode = doc.CreateElement("HASCASHFLOW");
                hascashflowNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(hascashflowNode);

                XmlNode ispostdatedNode = doc.CreateElement("ISPOSTDATED");
                ispostdatedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(ispostdatedNode);

                XmlNode usetrackingnumberNode = doc.CreateElement("USETRACKINGNUMBER");
                usetrackingnumberNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(usetrackingnumberNode);

                XmlNode isinvoiceNode = doc.CreateElement("ISINVOICE");
                isinvoiceNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isinvoiceNode);

                XmlNode mfgjournalNode = doc.CreateElement("MFGJOURNAL");
                mfgjournalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(mfgjournalNode);

                XmlNode hasdiscountsNode = doc.CreateElement("HASDISCOUNTS");
                hasdiscountsNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(hasdiscountsNode);

                XmlNode aspayslipNode = doc.CreateElement("ASPAYSLIP");
                aspayslipNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(aspayslipNode);

                XmlNode iscostcentreNode = doc.CreateElement("ISCOSTCENTRE");
                iscostcentreNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(iscostcentreNode);

                XmlNode isdeletedNode = doc.CreateElement("ISDELETED");
                isdeletedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isdeletedNode);

                XmlNode asoriginalNode = doc.CreateElement("ASORIGINAL");
                asoriginalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(asoriginalNode);

                XmlNode allledgerentriesNode = doc.CreateElement("ALLLEDGERENTRIES.LIST");
                voucherNode.AppendChild(allledgerentriesNode);

                //AllLEDGERENTRIES child nodes
                XmlNode ledgernameNode = doc.CreateElement("LEDGERNAME");
                ledgernameNode.AppendChild(doc.CreateTextNode(dr));
                allledgerentriesNode.AppendChild(ledgernameNode);

                XmlNode isdeemedpositiveNode = doc.CreateElement("ISDEEMEDPOSITIVE");
                isdeemedpositiveNode.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode.AppendChild(isdeemedpositiveNode);

                XmlNode ledgerfromitemNode = doc.CreateElement("LEDGERFROMITEM");
                ledgerfromitemNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(ledgerfromitemNode);

                XmlNode removezeroentriesNode = doc.CreateElement("REMOVEZEROENTRIES");
                removezeroentriesNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(removezeroentriesNode);

                XmlNode ispartyledgerNode = doc.CreateElement("ISPARTYLEDGER");
                ispartyledgerNode.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode.AppendChild(ispartyledgerNode);

                XmlNode amountNode = doc.CreateElement("AMOUNT");
                amountNode.AppendChild(doc.CreateTextNode("-"+amount));
                allledgerentriesNode.AppendChild(amountNode);

                XmlNode allledgerentriesNode1 = doc.CreateElement("ALLLEDGERENTRIES.LIST");
                voucherNode.AppendChild(allledgerentriesNode1);

                //AllLEDGERENTRIES1 child nodes
                XmlNode ledgername1Node = doc.CreateElement("LEDGERNAME");
                ledgername1Node.AppendChild(doc.CreateTextNode(cr));
                allledgerentriesNode1.AppendChild(ledgername1Node);

                XmlNode isdeemedpositive1Node = doc.CreateElement("ISDEEMEDPOSITIVE");
                isdeemedpositive1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(isdeemedpositive1Node);

                XmlNode ledgerfromitem1Node = doc.CreateElement("LEDGERFROMITEM");
                ledgerfromitem1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(ledgerfromitem1Node);

                XmlNode removezeroentries1Node = doc.CreateElement("REMOVEZEROENTRIES");
                removezeroentries1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(removezeroentries1Node);

                XmlNode ispartyledger1Node = doc.CreateElement("ISPARTYLEDGER");
                ispartyledger1Node.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode1.AppendChild(ispartyledger1Node);

                XmlNode amount1Node = doc.CreateElement("AMOUNT");
                amount1Node.AppendChild(doc.CreateTextNode(amount));
                allledgerentriesNode1.AppendChild(amount1Node);
            }

            String S4 = "SELECT 'Interest A/c' as cr, party.P_Name as dr, pmts.Interest amount,CONVERT(VARCHAR(10), loan.L_ApplicableDate, 112) dt from TmpDataCal as pmts, LoanSanction as loan, PartyMaster as party where pmts.Id = loan.L_Id AND loan.L_PartyId = party.P_Id AND DATEADD(mm, pmts.EMINo-1,loan.L_ApplicableDate) in ('" + input_date + "');";
            DataTable Dt4 = ObjData.GetDataTable(S4);

            for (int i = 0; i < Dt4.Rows.Count; i++)
            {
                String cr = Dt4.Rows[i]["cr"].ToString();
                String dr = Dt4.Rows[i]["dr"].ToString();
                String amount = Dt4.Rows[i]["amount"].ToString();
                String dt = Dt4.Rows[i]["dt"].ToString();

                String guid = input_date + "-INTEREST_ACCRUAL-" + cr + "-" + dr + "-" + amount + "-" + i.ToString();

                XmlNode tallyMessageNode = doc.CreateElement("TALLYMESSAGE");
                XmlAttribute productAttribute = doc.CreateAttribute("xmlns:UDF");
                productAttribute.Value = "TallyUDF";
                tallyMessageNode.Attributes.Append(productAttribute);
                requestDataNode.AppendChild(tallyMessageNode);

                XmlNode voucherNode = doc.CreateElement("VOUCHER");
                XmlAttribute nameAttribute = doc.CreateAttribute("REMOTEID");
                nameAttribute.Value = guid;
                XmlAttribute nameAttribute1 = doc.CreateAttribute("VCHTYPE");
                nameAttribute1.Value = "Journal";                                              //Data Pull
                XmlAttribute nameAttribute2 = doc.CreateAttribute("ACTION");
                nameAttribute2.Value = "Create";
                voucherNode.Attributes.Append(nameAttribute);
                voucherNode.Attributes.Append(nameAttribute1);
                voucherNode.Attributes.Append(nameAttribute2);
                tallyMessageNode.AppendChild(voucherNode);

                XmlNode dateNode = doc.CreateElement("DATE");
                dateNode.AppendChild(doc.CreateTextNode(dt));                           //Data Pull
                voucherNode.AppendChild(dateNode);


                XmlNode guidNode = doc.CreateElement("GUID");
                guidNode.AppendChild(doc.CreateTextNode(guid));
                voucherNode.AppendChild(guidNode);

                XmlNode narrationNode = doc.CreateElement("NARRATION");
                narrationNode.AppendChild(doc.CreateTextNode("Interest Accrual of " + amount + " by " + dr));                  //Data Pull
                voucherNode.AppendChild(narrationNode);


                XmlNode voucherTypeNameNode = doc.CreateElement("VOUCHERTYPENAME");     //Data Pull
                voucherTypeNameNode.AppendChild(doc.CreateTextNode("Journal"));
                voucherNode.AppendChild(voucherTypeNameNode);


                XmlNode voucherNumberNode = doc.CreateElement("VOUCHERNUMBER");
                voucherNumberNode.AppendChild(doc.CreateTextNode(i.ToString()));
                voucherNode.AppendChild(voucherNumberNode);

                XmlNode partyLedgerNameNode = doc.CreateElement("PARTYLEDGERNAME");
                partyLedgerNameNode.AppendChild(doc.CreateTextNode(dr));
                voucherNode.AppendChild(partyLedgerNameNode);

                XmlNode fbtPaymentTypeNode = doc.CreateElement("FBTPAYMENTTYPE");
                fbtPaymentTypeNode.AppendChild(doc.CreateTextNode("Default"));
                voucherNode.AppendChild(fbtPaymentTypeNode);

                XmlNode diffactualQtyNode = doc.CreateElement("DIFFACTUALQTY");
                diffactualQtyNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(diffactualQtyNode);

                XmlNode auditedNode = doc.CreateElement("AUDITED");
                auditedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(auditedNode);

                XmlNode forJobCostingNode = doc.CreateElement("FORJOBCOSTING");
                forJobCostingNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(forJobCostingNode);

                XmlNode isOptionalNode = doc.CreateElement("ISOPTIONAL");
                isOptionalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isOptionalNode);

                XmlNode effectivedateNode = doc.CreateElement("EFFECTIVEDATE");
                effectivedateNode.AppendChild(doc.CreateTextNode(dt));
                voucherNode.AppendChild(effectivedateNode);

                XmlNode useForInterestNode = doc.CreateElement("USEFORINTEREST");
                useForInterestNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useForInterestNode);

                XmlNode useforgainlossNode = doc.CreateElement("USEFORGAINLOSS");
                useforgainlossNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforgainlossNode);

                XmlNode useforgodowntransferNode = doc.CreateElement("USEFORGODOWNTRANSFER");
                useforgodowntransferNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforgodowntransferNode);

                XmlNode useforcompoundNode = doc.CreateElement("USEFORCOMPOUND");
                useforcompoundNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforcompoundNode);

                XmlNode alteridNode = doc.CreateElement("ALTERID");
                alteridNode.AppendChild(doc.CreateTextNode(i.ToString()));
                voucherNode.AppendChild(alteridNode);

                XmlNode exciseopeningNode = doc.CreateElement("EXCISEOPENING");
                exciseopeningNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(exciseopeningNode);

                XmlNode useforfinalproductionNode = doc.CreateElement("USEFORFINALPRODUCTION");
                useforfinalproductionNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforfinalproductionNode);

                XmlNode iscancelledNode = doc.CreateElement("ISCANCELLED");
                iscancelledNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(iscancelledNode);

                XmlNode hascashflowNode = doc.CreateElement("HASCASHFLOW");
                hascashflowNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(hascashflowNode);

                XmlNode ispostdatedNode = doc.CreateElement("ISPOSTDATED");
                ispostdatedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(ispostdatedNode);

                XmlNode usetrackingnumberNode = doc.CreateElement("USETRACKINGNUMBER");
                usetrackingnumberNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(usetrackingnumberNode);

                XmlNode isinvoiceNode = doc.CreateElement("ISINVOICE");
                isinvoiceNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isinvoiceNode);

                XmlNode mfgjournalNode = doc.CreateElement("MFGJOURNAL");
                mfgjournalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(mfgjournalNode);

                XmlNode hasdiscountsNode = doc.CreateElement("HASDISCOUNTS");
                hasdiscountsNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(hasdiscountsNode);

                XmlNode aspayslipNode = doc.CreateElement("ASPAYSLIP");
                aspayslipNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(aspayslipNode);

                XmlNode iscostcentreNode = doc.CreateElement("ISCOSTCENTRE");
                iscostcentreNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(iscostcentreNode);

                XmlNode isdeletedNode = doc.CreateElement("ISDELETED");
                isdeletedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isdeletedNode);

                XmlNode asoriginalNode = doc.CreateElement("ASORIGINAL");
                asoriginalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(asoriginalNode);

                XmlNode allledgerentriesNode = doc.CreateElement("ALLLEDGERENTRIES.LIST");
                voucherNode.AppendChild(allledgerentriesNode);

                //AllLEDGERENTRIES child nodes
                XmlNode ledgernameNode = doc.CreateElement("LEDGERNAME");
                ledgernameNode.AppendChild(doc.CreateTextNode(dr));
                allledgerentriesNode.AppendChild(ledgernameNode);

                XmlNode isdeemedpositiveNode = doc.CreateElement("ISDEEMEDPOSITIVE");
                isdeemedpositiveNode.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode.AppendChild(isdeemedpositiveNode);

                XmlNode ledgerfromitemNode = doc.CreateElement("LEDGERFROMITEM");
                ledgerfromitemNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(ledgerfromitemNode);

                XmlNode removezeroentriesNode = doc.CreateElement("REMOVEZEROENTRIES");
                removezeroentriesNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(removezeroentriesNode);

                XmlNode ispartyledgerNode = doc.CreateElement("ISPARTYLEDGER");
                ispartyledgerNode.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode.AppendChild(ispartyledgerNode);

                XmlNode amountNode = doc.CreateElement("AMOUNT");
                amountNode.AppendChild(doc.CreateTextNode("-" + amount));
                allledgerentriesNode.AppendChild(amountNode);

                XmlNode allledgerentriesNode1 = doc.CreateElement("ALLLEDGERENTRIES.LIST");
                voucherNode.AppendChild(allledgerentriesNode1);

                //AllLEDGERENTRIES1 child nodes
                XmlNode ledgername1Node = doc.CreateElement("LEDGERNAME");
                ledgername1Node.AppendChild(doc.CreateTextNode(cr));
                allledgerentriesNode1.AppendChild(ledgername1Node);

                XmlNode isdeemedpositive1Node = doc.CreateElement("ISDEEMEDPOSITIVE");
                isdeemedpositive1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(isdeemedpositive1Node);

                XmlNode ledgerfromitem1Node = doc.CreateElement("LEDGERFROMITEM");
                ledgerfromitem1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(ledgerfromitem1Node);

                XmlNode removezeroentries1Node = doc.CreateElement("REMOVEZEROENTRIES");
                removezeroentries1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(removezeroentries1Node);

                XmlNode ispartyledger1Node = doc.CreateElement("ISPARTYLEDGER");
                ispartyledger1Node.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode1.AppendChild(ispartyledger1Node);

                XmlNode amount1Node = doc.CreateElement("AMOUNT");
                amount1Node.AppendChild(doc.CreateTextNode(amount));
                allledgerentriesNode1.AppendChild(amount1Node);
            }

            String S5 = "SELECT cr, case when dr ='0' then 'Cash' else dr end as dr, amount, CONVERT(VARCHAR(10), dt, 112) dt  FROM(Select fp.F_Id as id, p.P_Name as cr, (fp.F_Principal + fp.F_InterestAmount + fp.F_Previous) as amount, fp.F_Date as dt from AccountForeClosure as fp, LoanSanction as ls, PartyMaster as p WHERE fp.F_SId = ls.L_Id AND ls.L_PartyId = P.P_Id) as t_cr,(SELECT fp.F_Id as id, ISNULL(party.P_Name,'0') AS dr FROM AccountForeClosure fp LEFT OUTER JOIN PartyMaster party  on fp.F_BankId = party.P_Id) as t_dr WHERE t_cr.id = t_dr.id AND dt in ('" + input_date + "');";
            DataTable Dt5 = ObjData.GetDataTable(S5);

            for (int i = 0; i < Dt5.Rows.Count; i++)
            {
                String cr = Dt5.Rows[i]["cr"].ToString();
                String dr = Dt5.Rows[i]["dr"].ToString();
                String amount = Dt5.Rows[i]["amount"].ToString();
                String dt = Dt5.Rows[i]["dt"].ToString();

                String guid = input_date + "-FORECLOSED-" + cr + "-" + dr + "-" + amount + "-" + i.ToString();

                XmlNode tallyMessageNode = doc.CreateElement("TALLYMESSAGE");
                XmlAttribute productAttribute = doc.CreateAttribute("xmlns:UDF");
                productAttribute.Value = "TallyUDF";
                tallyMessageNode.Attributes.Append(productAttribute);
                requestDataNode.AppendChild(tallyMessageNode);

                XmlNode voucherNode = doc.CreateElement("VOUCHER");
                XmlAttribute nameAttribute = doc.CreateAttribute("REMOTEID");
                nameAttribute.Value = guid;
                XmlAttribute nameAttribute1 = doc.CreateAttribute("VCHTYPE");
                nameAttribute1.Value = "Receipt";                                              //Data Pull
                XmlAttribute nameAttribute2 = doc.CreateAttribute("ACTION");
                nameAttribute2.Value = "Create";
                voucherNode.Attributes.Append(nameAttribute);
                voucherNode.Attributes.Append(nameAttribute1);
                voucherNode.Attributes.Append(nameAttribute2);
                tallyMessageNode.AppendChild(voucherNode);

                XmlNode dateNode = doc.CreateElement("DATE");
                dateNode.AppendChild(doc.CreateTextNode(dt));                           //Data Pull
                voucherNode.AppendChild(dateNode);


                XmlNode guidNode = doc.CreateElement("GUID");
                guidNode.AppendChild(doc.CreateTextNode(guid));
                voucherNode.AppendChild(guidNode);

                XmlNode narrationNode = doc.CreateElement("NARRATION");
                narrationNode.AppendChild(doc.CreateTextNode("Foreclosure " + amount + " by " + cr));                  //Data Pull
                voucherNode.AppendChild(narrationNode);


                XmlNode voucherTypeNameNode = doc.CreateElement("VOUCHERTYPENAME");     //Data Pull
                voucherTypeNameNode.AppendChild(doc.CreateTextNode("Receipt"));
                voucherNode.AppendChild(voucherTypeNameNode);


                XmlNode voucherNumberNode = doc.CreateElement("VOUCHERNUMBER");
                voucherNumberNode.AppendChild(doc.CreateTextNode(i.ToString()));
                voucherNode.AppendChild(voucherNumberNode);

                XmlNode partyLedgerNameNode = doc.CreateElement("PARTYLEDGERNAME");
                partyLedgerNameNode.AppendChild(doc.CreateTextNode(cr));
                voucherNode.AppendChild(partyLedgerNameNode);

                XmlNode fbtPaymentTypeNode = doc.CreateElement("FBTPAYMENTTYPE");
                fbtPaymentTypeNode.AppendChild(doc.CreateTextNode("Default"));
                voucherNode.AppendChild(fbtPaymentTypeNode);

                XmlNode diffactualQtyNode = doc.CreateElement("DIFFACTUALQTY");
                diffactualQtyNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(diffactualQtyNode);

                XmlNode auditedNode = doc.CreateElement("AUDITED");
                auditedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(auditedNode);

                XmlNode forJobCostingNode = doc.CreateElement("FORJOBCOSTING");
                forJobCostingNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(forJobCostingNode);

                XmlNode isOptionalNode = doc.CreateElement("ISOPTIONAL");
                isOptionalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isOptionalNode);

                XmlNode effectivedateNode = doc.CreateElement("EFFECTIVEDATE");
                effectivedateNode.AppendChild(doc.CreateTextNode(dt));
                voucherNode.AppendChild(effectivedateNode);

                XmlNode useForInterestNode = doc.CreateElement("USEFORINTEREST");
                useForInterestNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useForInterestNode);

                XmlNode useforgainlossNode = doc.CreateElement("USEFORGAINLOSS");
                useforgainlossNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforgainlossNode);

                XmlNode useforgodowntransferNode = doc.CreateElement("USEFORGODOWNTRANSFER");
                useforgodowntransferNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforgodowntransferNode);

                XmlNode useforcompoundNode = doc.CreateElement("USEFORCOMPOUND");
                useforcompoundNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforcompoundNode);

                XmlNode alteridNode = doc.CreateElement("ALTERID");
                alteridNode.AppendChild(doc.CreateTextNode(i.ToString()));
                voucherNode.AppendChild(alteridNode);

                XmlNode exciseopeningNode = doc.CreateElement("EXCISEOPENING");
                exciseopeningNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(exciseopeningNode);

                XmlNode useforfinalproductionNode = doc.CreateElement("USEFORFINALPRODUCTION");
                useforfinalproductionNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(useforfinalproductionNode);

                XmlNode iscancelledNode = doc.CreateElement("ISCANCELLED");
                iscancelledNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(iscancelledNode);

                XmlNode hascashflowNode = doc.CreateElement("HASCASHFLOW");
                hascashflowNode.AppendChild(doc.CreateTextNode("Yes"));
                voucherNode.AppendChild(hascashflowNode);

                XmlNode ispostdatedNode = doc.CreateElement("ISPOSTDATED");
                ispostdatedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(ispostdatedNode);

                XmlNode usetrackingnumberNode = doc.CreateElement("USETRACKINGNUMBER");
                usetrackingnumberNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(usetrackingnumberNode);

                XmlNode isinvoiceNode = doc.CreateElement("ISINVOICE");
                isinvoiceNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isinvoiceNode);

                XmlNode mfgjournalNode = doc.CreateElement("MFGJOURNAL");
                mfgjournalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(mfgjournalNode);

                XmlNode hasdiscountsNode = doc.CreateElement("HASDISCOUNTS");
                hasdiscountsNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(hasdiscountsNode);

                XmlNode aspayslipNode = doc.CreateElement("ASPAYSLIP");
                aspayslipNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(aspayslipNode);

                XmlNode iscostcentreNode = doc.CreateElement("ISCOSTCENTRE");
                iscostcentreNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(iscostcentreNode);

                XmlNode isdeletedNode = doc.CreateElement("ISDELETED");
                isdeletedNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(isdeletedNode);

                XmlNode asoriginalNode = doc.CreateElement("ASORIGINAL");
                asoriginalNode.AppendChild(doc.CreateTextNode("No"));
                voucherNode.AppendChild(asoriginalNode);

                XmlNode allledgerentriesNode = doc.CreateElement("ALLLEDGERENTRIES.LIST");
                voucherNode.AppendChild(allledgerentriesNode);

                //AllLEDGERENTRIES child nodes
                XmlNode ledgernameNode = doc.CreateElement("LEDGERNAME");
                ledgernameNode.AppendChild(doc.CreateTextNode(cr));
                allledgerentriesNode.AppendChild(ledgernameNode);

                XmlNode isdeemedpositiveNode = doc.CreateElement("ISDEEMEDPOSITIVE");
                isdeemedpositiveNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(isdeemedpositiveNode);

                XmlNode ledgerfromitemNode = doc.CreateElement("LEDGERFROMITEM");
                ledgerfromitemNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(ledgerfromitemNode);

                XmlNode removezeroentriesNode = doc.CreateElement("REMOVEZEROENTRIES");
                removezeroentriesNode.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode.AppendChild(removezeroentriesNode);

                XmlNode ispartyledgerNode = doc.CreateElement("ISPARTYLEDGER");
                ispartyledgerNode.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode.AppendChild(ispartyledgerNode);

                XmlNode amountNode = doc.CreateElement("AMOUNT");
                amountNode.AppendChild(doc.CreateTextNode(amount));
                allledgerentriesNode.AppendChild(amountNode);

                XmlNode allledgerentriesNode1 = doc.CreateElement("ALLLEDGERENTRIES.LIST");
                voucherNode.AppendChild(allledgerentriesNode1);

                //AllLEDGERENTRIES1 child nodes
                XmlNode ledgername1Node = doc.CreateElement("LEDGERNAME");
                ledgername1Node.AppendChild(doc.CreateTextNode(dr));
                allledgerentriesNode1.AppendChild(ledgername1Node);

                XmlNode isdeemedpositive1Node = doc.CreateElement("ISDEEMEDPOSITIVE");
                isdeemedpositive1Node.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode1.AppendChild(isdeemedpositive1Node);

                XmlNode ledgerfromitem1Node = doc.CreateElement("LEDGERFROMITEM");
                ledgerfromitem1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(ledgerfromitem1Node);

                XmlNode removezeroentries1Node = doc.CreateElement("REMOVEZEROENTRIES");
                removezeroentries1Node.AppendChild(doc.CreateTextNode("No"));
                allledgerentriesNode1.AppendChild(removezeroentries1Node);

                XmlNode ispartyledger1Node = doc.CreateElement("ISPARTYLEDGER");
                ispartyledger1Node.AppendChild(doc.CreateTextNode("Yes"));
                allledgerentriesNode1.AppendChild(ispartyledger1Node);

                XmlNode amount1Node = doc.CreateElement("AMOUNT");
                amount1Node.AppendChild(doc.CreateTextNode("-" + amount));
                allledgerentriesNode1.AppendChild(amount1Node);

            }

            doc.Save("Tx-" + input_date + ".xml");

            Dt1.Dispose();
            Dt2.Dispose();
            Dt3.Dispose();
            Dt4.Dispose();
            Dt5.Dispose();
        }
        #endregion
    }
}
