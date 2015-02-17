using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;
using System.IO;
using System.Management;
using System.Reflection;

namespace Finance_Management_System
{
    static class Program
    {
        public static string connectionString = null;

        public static string DataBaseName = "";
        public static string ServerName = "";
        public static string ServerUserId = "";
        public static string ServerPassword = "";

        public static string Path1 = "";
        public static string Path2 = "";
        public static string Path3 = "";
        public static string Path4 = "";
        public static string Path5 = "";
        public static string Path6 = "";
        public static string Path7 = "";
        public static string Path8 = "";
        public static string PortNo = "";
        public static string SenderId = "";
        public static string PasswordId = "";
        public static string SenderName = "";
        public static string UserId = "";
        public static int SMSBY = 0;
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                RegistryKey key;
                key = Registry.CurrentUser.OpenSubKey("Control Panel\\International", true);

                if (key.ToString().Trim() != "")
                {
                    key.SetValue("sShortDate", "dd/MM/yyyy");
                }
            }
            catch
            {
            }

            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ClsDataAccess ObjDataAccess = new ClsDataAccess();
            bool chk_file = false;
            chk_file = System.IO.File.Exists("Finance.stl");
            if (chk_file == false)
            {
                Frm_SqlSettings sqlSetting = new Frm_SqlSettings();
                sqlSetting.ShowDialog();
            }
            else
            {
                StreamReader objReader = new StreamReader("Finance.stl");
                Program.connectionString = objReader.ReadLine();
                objReader.Close();
                objReader.Dispose();
                try
                {
                    ObjDataAccess.CreateConnection();
                }
                catch
                {
                    clsGeneral.ShowErrMsg("Error In Connection");
                    File.Delete("Finance.stl");
                    Frm_SqlSettings sqlSetting = new Frm_SqlSettings();
                    sqlSetting.ShowDialog();
                    if (sqlSetting.DialogResult == DialogResult.Yes)
                    {
                        StreamWriter SW;
                        SW = File.CreateText("Finance.stl");
                        SW.WriteLine(Program.connectionString);
                        SW.Close();
                        SW.Dispose();
                    }
                    else
                    {
                        Application.Exit();
                        return;
                    }
                }
            }

            string[] connSplit = Program.connectionString.Split(';');
            if (connSplit.Length == 6)
            {
                string[] DataSourceName = connSplit[0].Split('=');
                string[] DataBaseName = connSplit[1].Split('=');
                Program.DataBaseName = DataBaseName[1].ToString();
            }

            else if (connSplit.Length > 6)
            {
                string[] DataSourceName = connSplit[0].Split('=');
                string[] DataBaseName = connSplit[1].Split('=');
                string[] UserName = connSplit[2].Split('=');
                string[] PassWord = connSplit[3].Split('=');
                Program.DataBaseName = DataBaseName[1].ToString();
                Program.ServerName = DataSourceName[1].ToString();
                Program.ServerUserId = UserName[1].ToString();
                Program.ServerPassword = PassWord[1].ToString();
            }

            //========================================================================

            try
            {

                string SS = "Create View vw_Form13 As ";
                SS = SS + " Select '' As CompanyName,'' As Address1, ";
                SS = SS + " '' As Address2,'' As Date,'' As ContactName, ";
                SS = SS + " '' As LicenseNo,'' As SNo,'' As Debtor, ";
                SS = SS + " '' As AddressDebtor,'' As LoanAmount, ";
                SS = SS + " '' As DateLoan,'' As DateMaturity, ";
                SS = SS + " '' As Balance,'' As Rate,'' As [Security], ";
                SS = SS + " '' As Documents,'' As Information ";
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }



            try
            {

                string SS = "CREATE TABLE [dbo].[LoanDocuments]([DId] [int] ";
                SS = SS + " IDENTITY(1,1) NOT NULL,	[D_LId] [int] NULL,	";
                SS = SS + " [D_DId] [int] NULL, CONSTRAINT [PK_LoanDocuments] ";
                SS = SS + " PRIMARY KEY CLUSTERED ( [DId] ASC )WITH ";
                SS = SS + " (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]";
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            try
            {
                string SS = "CREATE TABLE [ChequeDetails]( ";
                SS = SS + " [S_Id] [int] , ";
                SS = SS + " [S_SNo] [int] , ";
                SS = SS + " [S_ChequeDate] [varchar](max), ";
                SS = SS + " [S_ChequeNo] [varchar](max) ) ";
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            try
            {
                string SS = "CREATE TABLE [dbo].[UserMaster]( ";
                SS = SS + " [UId] [int] IDENTITY(1,1) NOT NULL, ";
                SS = SS + " [UName] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL, ";
                SS = SS + " [ULoginName] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL, ";
                SS = SS + " [UPassword] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL, ";
                SS = SS + " CONSTRAINT [PK_UserMaster] PRIMARY KEY CLUSTERED ";
                SS = SS + " ([UId] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY] ";
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            try
            {
                string SS = "CREATE TABLE [dbo].[UserConfiguration]( ";
                SS = SS + " [U_UId] [int] NULL,[U_MId] [int] NULL, ";
                SS = SS + " [U_Save] [bit] NULL,[U_Delete] [bit] NULL, ";
                SS = SS + " [U_Print] [bit] NULL,[U_View] [bit] NULL ";
                SS = SS + " ) ON [PRIMARY] ";
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            try
            {

                string SS = "Alter Table LoanSanction ADD L_Known Varchar(Max)";
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            try
            {
                string SS = "CREATE TABLE [dbo].[AccountForeClosure]( ";
                SS = SS + " [F_Id] [int] IDENTITY(1,1) NOT NULL, ";
                SS = SS + " [F_SId] [int] NULL, ";
                SS = SS + " [F_Date] [datetime] NULL, ";
                SS = SS + " [F_Principal] [numeric](18, 2) NULL, ";
                SS = SS + " [F_InterestRate] [float] NULL, ";
                SS = SS + " [F_InterestAmount] [numeric](18, 2) NULL, ";
                SS = SS + " [F_Previous] [numeric](18, 2) NULL, ";
                SS = SS + " [F_Remarks] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL, ";
                SS = SS + " CONSTRAINT [PK_AccountForeClosure] PRIMARY KEY CLUSTERED ";
                SS = SS + " ([F_Id] ASC)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]) ON [PRIMARY]";
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            //========================================================================

            try
            {
                string Path = "Finance_Management_System.SQL.Calculation.sql";
                Assembly A = Assembly.GetExecutingAssembly();
                StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                string SS = D.ReadToEnd();
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            try
            {
                string Path = "Finance_Management_System.SQL.AlterCalculation.sql";
                Assembly A = Assembly.GetExecutingAssembly();
                StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                string SS = D.ReadToEnd();
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            try
            {
                string Path = "Finance_Management_System.SQL.CashReceipt.sql";
                Assembly A = Assembly.GetExecutingAssembly();
                StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                string SS = D.ReadToEnd();
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            try
            {
                string Path = "Finance_Management_System.SQL.CashPayment.sql";
                Assembly A = Assembly.GetExecutingAssembly();
                StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                string SS = D.ReadToEnd();
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            try
            {
                string Path = "Finance_Management_System.SQL.AlterLoanApplication.sql";
                Assembly A = Assembly.GetExecutingAssembly();
                StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                string SS = D.ReadToEnd();
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            try
            {
                string Path = "Finance_Management_System.SQL.AlterLoanSanction.sql";
                Assembly A = Assembly.GetExecutingAssembly();
                StreamReader D = new StreamReader(A.GetManifestResourceStream(Path));
                string SS = D.ReadToEnd();
                ObjDataAccess.ExecuteQuery(SS);
            }
            catch { }

            //========================================================================

            Application.Run(new Frm_Main());
        }
    }
}
