using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Globalization;
using System.Data.SqlClient;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Data;
using System.Management;

namespace Finance_Management_System
{
    public class clsGeneral
    {

        public static int UID = 1;
        public static string LoginName = "Admin";

        public static bool Save = true;
        public static bool Delete = true;
        public static bool Print = true;
        public static bool Export = true;
        public static bool SMS = true;
        public static bool EMail = true;
        public static bool Import = true;
        public static bool PrintWithoutTax = false;
        public static double InvoiceAmount = 0;
        public static double CashReceiveAmount = 0;
        public static double CashReturnAmount = 0;

        public static int DFocus = 0;
        public static int DCursor = 0;
        public static int CalculationType = 0;

        public static string ContactName = "";
        public static string CompanyName = "Goenka Finance Company";
        public static string Address = "";
        public static string Address2 = "";
        public static string City = "";
        public static string ContactNo = "";
        public static string TinNo = "";
        public static string Email = "";

        public static string LicenseNo = "";
        public static string TaxCategory = "";
        public static string PanNo = "";

        public static bool MultiInvoiceType = false;
        public static bool DelhiStyle = false;
        public static bool FilterList = false;

        public static bool ListDis = false;

        public static int NoofNode = 0;

        public static int WPRP = 0;
        public static bool PrintCompanyDetails = false;

        public static string TransportGoods = "";
        public static bool TransportLetter = false;
        public static bool UseMCode = false;
        public static string DefaultCity = "";
        public static string MailSignature = "";

        public static bool OrderToSales = false;

        public static string A_Sales = "";
        public static string A_Performa = "";
        public static string A_Quotation = "";
        public static string A_Purchaseorder = "";

        public static string Slogan1 = "";
        public static string Slogan2 = "";
        public static string Slogan3 = "";
        public static string Slogan4 = "";
        public static string Slogan5 = "";
        public static string Slogan6 = "";
        public static string Slogan7 = "";
       

        public static bool ISRound = true;

        #region Code Of Message Box
        public static void CheckTextBoxIsEmpty(System.Windows.Forms.TextBox T)
        {
            try
            {
                if (T.Text.Trim().Length == 0)
                {
                    T.Text = "0";
                    T.Text = CurFormat(double.Parse(T.Text));
                }
                else
                {
                    double A = double.Parse(T.Text);
                    T.Text = string.Format("{0:0.00}", A);
                }
            }
            catch (Exception Erp)
            {
                ShowErrMsg(Erp.Message);
            }
        }

        public static void IsNumberic(KeyPressEventArgs e, System.Windows.Forms.TextBox Tb)
        {
            try
            {
                int Dec = Tb.Text.IndexOf(".");
                if (Dec < 0)
                {
                    if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar) || e.KeyChar == 46)
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
            catch (Exception Erp) { clsGeneral.ShowErrMsg(Erp.Message); }
        }

        public static string DataRowToString(DataRow dr)
        {
            return dr[0].ToString();
        }

        public static string[] DataTableToArray(System.Data.DataTable dt)
        {
            DataRow[] dr = dt.Select();
            string[] strArr = Array.ConvertAll(dr, new Converter<DataRow, string>(DataRowToString));
            return strArr;
        }

        public static void IsNumbericWithOutDecimal(KeyPressEventArgs e, System.Windows.Forms.TextBox Tb)
        {
            try
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
            catch (Exception Erp) { clsGeneral.ShowErrMsg(Erp.Message); }
        }

        public static string MaxAlfaNumeric(string KeyCode)
        {
            byte[] ASCIIValues = ASCIIEncoding.ASCII.GetBytes(KeyCode);
            int StringLength = ASCIIValues.Length;
            bool isAllZed = true;
            bool isAllNine = true;
            //Check if all has ZZZ... then do nothing just return empty string.
            for (int i = 0; i < StringLength - 1; i++)
            {
                if (ASCIIValues[i] != 90)
                {
                    isAllZed = false;
                    break;
                }
            }
            if (isAllZed && ASCIIValues[StringLength - 1] == 57)
            {
                ASCIIValues[StringLength - 1] = 64;
            }

            // Check if all has 999... then make it A0
            for (int i = 0; i < StringLength; i++)
            {
                if (ASCIIValues[i] != 57)
                {
                    isAllNine = false;
                    break;
                }
            }
            if (isAllNine)
            {
                ASCIIValues[StringLength - 1] = 47;
                ASCIIValues[0] = 65;
                for (int i = 1; i < StringLength - 1; i++)
                {
                    ASCIIValues[i] = 48;
                }
            }

            for (int i = StringLength; i > 0; i--)
            {
                if (i - StringLength == 0)
                {
                    ASCIIValues[i - 1] += 1;
                }
                if (ASCIIValues[i - 1] == 58)
                {
                    ASCIIValues[i - 1] = 48;
                    if (i - 2 == -1)
                    {
                        break;
                    }
                    ASCIIValues[i - 2] += 1;
                }
                else if (ASCIIValues[i - 1] == 91)
                {
                    ASCIIValues[i - 1] = 65;
                    if (i - 2 == -1)
                    {
                        break;
                    }
                    ASCIIValues[i - 2] += 1;

                }
                else
                {
                    break;
                }

            }
            KeyCode = ASCIIEncoding.ASCII.GetString(ASCIIValues);
            return KeyCode;
        }

        public static void ShowErrMsg(string str)
        {
            System.Windows.Forms.MessageBox.Show(str, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        }

        public static void ShowMessage(string str)
        {
            System.Windows.Forms.MessageBox.Show(str, "Information", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
        #endregion

        public static string CapitalCaseString(string ValueGet)
        {
            if (ValueGet != "")
            {
                string proper = ValueGet;
                CultureInfo properCase = System.Threading.Thread.CurrentThread.CurrentCulture;
                TextInfo currentInfo = properCase.TextInfo;
                proper = currentInfo.ToTitleCase(proper);
                ValueGet = proper;
            }
            return ValueGet;
        }

        public static bool EmailValidation(string st)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            System.Text.RegularExpressions.Match match = Regex.Match(st.Trim(), pattern, RegexOptions.IgnoreCase);
            if (match.Success)
                return true;
            else
                return false;
        }

        public static void SendEmail(string TypeOf, string MailId, string Subject, string Body, string AttachmentPath, string UserName, string Password)
        {
            MailMessage msg = new MailMessage();
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            StreamWriter SW;
            StreamWriter SW1;
            Attachment attachment;
          
            StringBuilder mailBody;
            try
            {


                client.Host = "smtp.gmail.com";
                //client.Port = 25;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(UserName, Password);
                client.Timeout = 50000;
                client.EnableSsl = true;
                msg.From = new System.Net.Mail.MailAddress(UserName,Program.SenderName);

                string[] MailIdSplit = MailId.Split(',');
                if (MailIdSplit.Length > 0)
                {
                    msg.To.Add(new System.Net.Mail.MailAddress(MailIdSplit[0].ToString()));
                }
                for (int i = 1; i < MailIdSplit.Length; i++)
                {
                    msg.CC.Add(new System.Net.Mail.MailAddress(MailIdSplit[i].ToString()));
                }

                if (AttachmentPath.Trim().Length > 0)
                {

                    attachment = new System.Net.Mail.Attachment(AttachmentPath);
                    msg.Attachments.Add(attachment);
                }

                string SignatureMail = "";
                if (clsGeneral.MailSignature.Trim().Length > 0)
                {
                    File.Delete("SIGNATURE.rtf");

                    if (System.IO.File.Exists("SIGNATURE.rtf") == false)
                    {

                        SW = File.CreateText("SIGNATURE.rtf");
                        SW.WriteLine("");
                        SW.Close();
                        SW.Dispose();

                        SW1 = new StreamWriter("SIGNATURE.rtf");
                        SW1.Write(clsGeneral.MailSignature);
                        SW1.Close();
                        SW1.Dispose();


                        //StreamWriter SW;
                        //SW = File.CreateText("SIGNATURE.rtf");
                        //SW.WriteLine(clsGeneral.MailSignature);
                        //SW.Close();
                        //SW.Dispose();
                    }

                    
                }


                mailBody = new StringBuilder();

                mailBody.AppendFormat(Body);
                mailBody.AppendFormat("<br />");

                string[] lines = null;

                string Editbody = "";

                if (TypeOf == clsGeneral.FormName.Sale.ToString())
                {
                    Editbody = clsGeneral.A_Sales;
                }
                if (TypeOf == clsGeneral.FormName.PerformaInvoice.ToString())
                {
                    Editbody = clsGeneral.A_Performa;
                }
                if (TypeOf == clsGeneral.FormName.Quotation.ToString())
                {
                    Editbody = clsGeneral.A_Quotation;
                }
                if (TypeOf == clsGeneral.FormName.PO.ToString())
                {
                    Editbody = clsGeneral.A_Purchaseorder;
                }

                lines = Regex.Split(Editbody, "\r\n");

                if (lines != null)
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        mailBody.AppendFormat("<b>");
                        mailBody.AppendFormat(lines[i].ToString());
                        mailBody.AppendFormat("<b/>");
                        mailBody.AppendFormat("<br />");
                    }
                }

                mailBody.AppendFormat(SignatureMail);

                msg.Subject = Subject;
                msg.Body = mailBody.ToString();
                msg.IsBodyHtml = true;
                client.Send(msg);
                msg.Dispose();
                if (AttachmentPath.Trim().Length > 0)
                {
                    File.Delete(AttachmentPath);
                }
                File.Delete("SIGNATURE.rtf");
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
            msg.Dispose();
        }

        #region Code Encode String
        public static string SQLEncode(string str)
        {
            string RetVal = "";
            try
            {
                RetVal = str.Replace("'", "''");
            }
            catch (Exception Erp) { clsGeneral.ShowErrMsg(Erp.Message); }
            return RetVal;
        }
        #endregion

        #region Code Of Excel
        public static void ExcelExport(DataGridView Dg, string TypePass)
        {

            Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            Excel.ApplicationClass oExcel = null;           //Excel Application
            Excel.Workbook oBook = null;                       // Excel Workbook
            Excel.Sheets oSheetsColl = null;                   // Excel Worksheets collection
            Excel.Worksheet oSheet = null;                     // Excel Worksheet
            Excel.Range oRange = null;                         // Cell or Range in worksheet
            Object oMissing = System.Reflection.Missing.Value;
            oExcel = new Excel.ApplicationClass();
            oExcel.UserControl = true;
            oBook = oExcel.Workbooks.Add(oMissing);
            oSheetsColl = oExcel.Worksheets;
            oSheet = (Excel.Worksheet)oSheetsColl.get_Item("Sheet1");


            oRange = (Excel.Range)oSheet.Cells[1, 1];
            oRange.Value2 = clsGeneral.CompanyName;
            oRange.Font.Name = "Tahoma";
            oRange.Font.Size = 12;
            //(oRange).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            //(oRange).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray);

            if (TypePass.Trim().Length > 0)
            {
                oRange = (Excel.Range)oSheet.Cells[2, 1];
                oRange.Value2 = TypePass;
                oRange.Font.Name = "Tahoma";
                oRange.Font.Size = 10;
            }

            int c = 0;

            if (Dg.ColumnHeadersVisible == true)
            {
                for (int j = 0; j < Dg.Columns.Count; j++)
                {
                    if (Dg.Columns[j].Visible == true)
                    {
                        oRange = (Excel.Range)oSheet.Cells[4, c + 1];
                        oRange.Value2 = Dg.Columns[j].HeaderText + "  ";
                        oRange.Font.Bold = true;
                        oRange.Font.Name = "Tahoma";
                        oRange.Font.Size = 9;
                        (oRange).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        (oRange).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Teal);
                        oExcel.Columns.AutoFit();
                        c++;
                    }
                }
            }

            c = 0;

            for (int i = 0; i < Dg.Rows.Count; i++)
            {
                for (int j = 0; j < Dg.Columns.Count; j++)
                {
                    if (Dg.Columns[j].Visible == true)
                    {
                        oRange = (Excel.Range)oSheet.Cells[i + 5, c + 1];
                        if (Dg[j, i].Value == null)
                        {
                            oRange.Value2 = " ";
                        }
                        else
                        {
                            oRange.Value2 = Dg[j, i].Value.ToString().Replace('\n', ' ') + "  ";
                        }

                        oRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                        oRange.Font.Name = "Tahoma";
                        oRange.Font.Size = 8;
                        oExcel.Columns.AutoFit();
                        // oRange.NumberFormat = "dd/MM/yyyy";
                        c++;
                    }
                }
                c = 0;
            }

            oExcel.Visible = true;
            oBook = null;
            oExcel = null;

            GC.Collect();

        }
        #endregion

        #region Code Of Data Back Up
        public static string DataBaseBackUp()
        {
            string strBackupLocation = "";
            try
            {
                bool chk_file = false;
                chk_file = System.IO.File.Exists("BackUp.txt");
                if (chk_file == false)
                {
                    StreamWriter SW;
                    SW = File.CreateText("BackUp.txt");
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    MessageBox.Show("Select Location To Create Back of DataBase", "Back Up");
                    DialogResult dr = fbd.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        string pth = fbd.SelectedPath;
                        SW.WriteLine(pth);
                        SW.Close();
                        StreamWriter SWC;
                        SWC = File.CreateText(pth);
                        SWC.Close();
                    }
                }
                else
                {
                    StreamReader objReader = new StreamReader("BackUp.txt");
                    string myName = "";
                    myName = objReader.ReadLine();
                    objReader.Close();
                    objReader.Dispose();
                    if (myName == null)
                    {
                        File.Delete("BackUp.txt");
                        //MessageBox.Show("Database Not Found");
                        StreamWriter SW;
                        SW = File.CreateText("BackUp.txt");
                        //  OpenFileDialog op = new System.Windows.Forms.OpenFileDialog();
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        MessageBox.Show("Select Location To Create Back of DataBase", "Back Up");
                        DialogResult dr = fbd.ShowDialog();
                        if (dr == DialogResult.OK)
                        {
                            string pth = fbd.SelectedPath;
                            SW.WriteLine(pth);
                            SW.Close();
                            StreamWriter SWC;
                            SWC = File.CreateText(pth);
                            SWC.Close();
                        }
                    }
                }
                StreamReader objReader_red = new StreamReader("BackUp.txt");

                strBackupLocation = objReader_red.ReadLine();
            }
            catch (Exception Erp)
            {
                ShowErrMsg(Erp.Message);
            }
            return strBackupLocation;
        }
        #endregion

        public static string DateFormat(string DatePass)
        {
            string DateVal  = "";
            try
            {
                if (DatePass.Trim().Length > 0)
                {
                    DateVal = DatePass.Substring(6, 2) + "/" + DatePass.Substring(4, 2) + "/" + DatePass.Substring(0, 4);
                }
            }
            catch (Exception Erp) { clsGeneral.ShowErrMsg(Erp.Message); }
            return DateVal;
        }

        public static void DatagridSort(System.Windows.Forms.DataGridView DgView)
        {
            try
            {
                for (int q = 0; q < DgView.Columns.Count; q++)
                {
                    DgView.Columns[q].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception Erp)
            {
                ShowErrMsg(Erp.Message);
            }
        }

        public enum FormName
        {
            Sale = 1,
            Purchase = 2,
            SaleReturn = 3,
            PurchaseReturn = 4,
            RoughEstimate = 5,
            PerformaInvoice = 6,
            Quotation = 7,
            RoughEstimatePurchase = 8,
            Challan = 9,
            PO = 10,
            RoughTrans = 11,
            CashPayment = 12,
            CashReceipt = 13,
            ChequeIssue = 14,
            ExpenseEntry = 15,
            PayInSlip = 16
        };

        public static string CurFormat(double ValuePass)
        {
           return string.Format("{0:0.00}", ValuePass);
        }

        public static string CurFormat(string Value)
        {
            if (Value.Trim().Length == 0 || Value == null)
            {
                Value = "0";
            }
            return string.Format("{0:0.00}", double.Parse(Value));
        }

        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

        private static extern void Sleep(long dwMilliseconds);


        public static void SendSMS(string P_Id, string MessageBody)
        {
            ClsDataAccess Obj = new ClsDataAccess();
            try
            {
                string SSData = "Select a_mobile + ',' + a_mobile2 From AccountMaster Where A_id = " + P_Id + "";
                if (Obj.GetDataTable(SSData).Rows.Count > 0)
                {
                    string[] Mo = Obj.GetDataTable(SSData).Rows[0][0].ToString().Split(',');
                    for (int i = 0; i < Mo.Length; i++)
                    {
                        try
                        {
                            string MobNo = Mo[i].ToString();
                            MobNo = MobNo.Replace("(", "");
                            MobNo = MobNo.Replace(")", "");
                            MobNo = MobNo.Replace("+", "");
                            MobNo = MobNo.Replace("-", "");
                            MobNo = MobNo.Replace(",", "");

                            if (MobNo.Substring(0, 1) == "0" && MobNo.Length > 5)
                            {
                                MobNo = MobNo.Substring(1, MobNo.Length - 1);
                            }


                            if (MobNo.Trim().Length >= 10)
                            {
                                if (Program.SMSBY == 0 && Program.PortNo.Trim().Length > 0)
                                {
                                    #region MOBILE
                                    char[] arr = new char[1];
                                    arr[0] = (char)26;
                                    SerialPort sp = new SerialPort();
                                    sp.PortName = Program.PortNo;
                                    sp.BaudRate = 96000;
                                    sp.Parity = Parity.None;
                                    sp.DataBits = 8;
                                    sp.StopBits = StopBits.One;
                                    sp.Handshake = Handshake.XOnXOff;
                                    sp.DtrEnable = true;
                                    sp.RtsEnable = true;
                                    sp.NewLine = Environment.NewLine;
                                    sp.Open();
                                    int mSpeed = 1;
                                    sp.Write("AT+CMGF=1" + Environment.NewLine);
                                    System.Threading.Thread.Sleep(200);
                                    sp.Write("AT+CSCS=GSM" + Environment.NewLine);
                                    System.Threading.Thread.Sleep(200);
                                    sp.Write("AT+CMGS=" + (char)34 + "+91" + MobNo
                                    + (char)34 + Environment.NewLine);
                                    System.Threading.Thread.Sleep(200);
                                    sp.Write(MessageBody + (char)26);
                                    System.Threading.Thread.Sleep(mSpeed);
                                    sp.Close();
                                    #endregion
                                }
                                else if (Program.SMSBY == 1)
                                {
                                    #region WEB

                                    string UserId = Program.UserId;
                                    string UserPassword = Program.PasswordId;
                                    string UserSenderId = Program.SenderId;
                                    string Path1 = Program.Path1;
                                    string Path2 = Program.Path2;
                                    string Path3 = Program.Path3;
                                    string Path4 = Program.Path4;
                                    string Path5 = Program.Path5;
                                    string Path6 = Program.Path6;
                                    string Path7 = Program.Path7;


                                    WebClient client = new WebClient();
                                    string baseurl = Path1 + Path2 + UserId + Path3 + UserPassword + Path4 + UserSenderId + Path5 + MobNo + Path6 + MessageBody + Path7;
                                    Stream data = client.OpenRead(baseurl);
                                    StreamReader reader = new StreamReader(data);
                                    string s = reader.ReadToEnd();
                                    data.Close();
                                    reader.Close();

                                    #endregion
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);

            }
        }

        public static bool SendSMSMobile(string MobNo, string MessageBody)
        {
            bool IsSend = false;
            try
            {
                string PortNo = "";
                ManagementObjectSearcher mos = new ManagementObjectSearcher("SELECT * FROM Win32_POTSModem");

                foreach (ManagementObject mo in mos.Get())
                {
                    string COMPort = mo["AttachedTo"].ToString();
                    SerialPort serialPort = null;
                    try
                    {
                        serialPort = new SerialPort();
                        serialPort.PortName = COMPort;
                        serialPort.BaudRate = 9600;
                        serialPort.DataBits = 8;
                        serialPort.Parity = Parity.None;
                        serialPort.ReadTimeout = 300;
                        serialPort.WriteTimeout = 300;
                        serialPort.StopBits = StopBits.One;
                        serialPort.Handshake = Handshake.None;
                        serialPort.Open();
                        if (serialPort.IsOpen == true)
                        {
                            PortNo = COMPort;
                            break;
                        }
                    }
                    catch { }
                    finally { serialPort.Close(); serialPort.Dispose(); }
                }


                if (PortNo.Trim().Length > 0)
                {
                    MobNo = MobNo.Replace("(", "");
                    MobNo = MobNo.Replace(")", "");
                    MobNo = MobNo.Replace("+", "");
                    MobNo = MobNo.Replace("-", "");
                    MobNo = MobNo.Replace(",", "");

                    if (MobNo.Substring(0, 1) == "0" && MobNo.Length > 5)
                    {
                        MobNo = MobNo.Substring(1, MobNo.Length - 1);
                    }

                    if (MobNo.Trim().Length >= 10 && PortNo.Length > 0)
                    {
                        int MsgLength = Convert.ToInt32(MessageBody.Length);
                        int Q = MsgLength / 160;
                        int R = MsgLength % 160;
                        if (R > 0)
                        {
                            Q = Q + 1;
                        }

                        for (int i = 0; i < Q; i++)
                        {
                            int StartIndex = i * 160;
                            int EndIndex = 160;
                            if (i == Q - 1)
                            {
                                EndIndex = R;
                            }
                            string Msg = MessageBody.Substring(StartIndex, EndIndex);

                            SerialPort sp = new SerialPort();

                            try
                            {
                                #region MOBILE
                                //System.Threading.Thread.Sleep(1000);
                                char[] arr = new char[1];
                                arr[0] = (char)26;
                                sp.PortName = PortNo;
                                sp.BaudRate = 96000;
                                sp.Parity = Parity.None;
                                sp.DataBits = 8;
                                sp.StopBits = StopBits.One;
                                sp.Handshake = Handshake.XOnXOff;
                                sp.DtrEnable = true;
                                sp.RtsEnable = true;
                                sp.NewLine = Environment.NewLine;
                                sp.Open();
                                int mSpeed = 1;
                                sp.Write("AT+CMGF=1" + Environment.NewLine);
                                System.Threading.Thread.Sleep(200);
                                sp.Write("AT+CSCS=GSM" + Environment.NewLine);
                                System.Threading.Thread.Sleep(200);
                                sp.Write("AT+CMGS=" + (char)34 + "+91" + MobNo
                                + (char)34 + Environment.NewLine);
                                System.Threading.Thread.Sleep(200);
                                sp.Write(Msg + (char)26);
                                System.Threading.Thread.Sleep(mSpeed);
                                #endregion
                            }
                            catch { IsSend = false; }
                            finally { sp.Close(); ; sp.Dispose(); }
                        }
                    }
                }
            }
            catch
            {
                IsSend = false;
            }
            return IsSend;
        }

        public static bool SendSMSBalance(string MobileNo, string MessageBody)
        {
            bool IsSend = true;
            try
            {
                try
                {
                    string MobNo = MobileNo;
                    MobNo = MobNo.Replace("(", "");
                    MobNo = MobNo.Replace(")", "");
                    MobNo = MobNo.Replace("+", "");
                    MobNo = MobNo.Replace("-", "");
                    MobNo = MobNo.Replace(",", "");
                    if (MobNo.Trim().Length >= 10)
                    {
                        SerialPort sp = new SerialPort();
                        sp.PortName = Program.PortNo;
                        sp.BaudRate = 96000;
                        sp.Parity = Parity.None;
                        sp.DataBits = 8;
                        sp.StopBits = StopBits.One;
                        sp.Handshake = Handshake.XOnXOff;
                        sp.DtrEnable = true;
                        sp.RtsEnable = true;
                        //sp.NewLine = Environment.NewLine;
                        sp.Open();
                        int mSpeed = 1;

                        //  AT+CUSD=1,"*101#"

                        string S= "AT+CUSD=1,\"*125#\",15";

                        sp.WriteLine("AT+CMGF=1" + Environment.NewLine);
                        System.Threading.Thread.Sleep(200);
                        sp.WriteLine("AT+CSCS=GSM" + Environment.NewLine);
                        System.Threading.Thread.Sleep(200);
                        //sp.Write("AT+CMGS=" + (char)34 + "+91" + MobNo
                        //+ (char)34 + Environment.NewLine);

                        sp.Write(S + (char)34 + Environment.NewLine);

                        System.Threading.Thread.Sleep(200);
                        sp.Write(MessageBody + (char)26);
                        System.Threading.Thread.Sleep(mSpeed);
                        S = sp.ReadLine();
                        System.Threading.Thread.Sleep(200);
                        S = sp.ReadLine();
                        System.Threading.Thread.Sleep(200);
                        S = sp.ReadLine();
                        System.Threading.Thread.Sleep(200);
                        S = sp.ReadLine();
                        System.Threading.Thread.Sleep(200);
                        S = sp.ReadLine();
                        System.Threading.Thread.Sleep(200);
                        S = sp.ReadLine();
                        System.Threading.Thread.Sleep(200);
                        S = sp.ReadLine();
                        System.Threading.Thread.Sleep(200);
                        S = sp.ReadLine();
                        System.Threading.Thread.Sleep(200);
                        S = sp.ReadLine();
                        System.Threading.Thread.Sleep(200);
                        S = sp.ReadLine();
                        System.Threading.Thread.Sleep(200);
                        S = sp.ReadLine();
                        sp.Close();
                    }
                }
                catch (Exception Erp)
                {
                    IsSend = false;
                    clsGeneral.ShowErrMsg(Erp.Message);
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
                IsSend = false;
            }
            return IsSend;
        }


        public static string FormatDate(System.DateTime nDate)
        {
            string RetVal = "";
            try
            {
                RetVal = nDate.ToString("yyyyMMdd");

            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
            return RetVal;
        }

        public static string ComapreDateFormat(System.DateTime nDate)
        {
            string RetVal = "";
            try
            {
                RetVal = "convert(datetime, '" + nDate.ToString("yyyyMMdd") + "')";

            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
            return RetVal;
        }

        public static string StringEmpty(string Val)
        {
            if (Val.Trim().Length == 0)
            {
                return Val = "0";
            }
            return Val;
        }

        //public static bool BoolEmpty(string Val)
        //{
        //    bool retval = false;
        //    if (Val.Trim().Length == 0)
        //    {
        //        return retval = false;
        //    }
        //    return retval;
        //}

        public static string CapitalCase(string ValuePass)
        {
            if (ValuePass != "")
            {
                string proper = ValuePass;
                CultureInfo properCase = System.Threading.Thread.CurrentThread.CurrentCulture;
                TextInfo currentInfo = properCase.TextInfo;
                proper = currentInfo.ToTitleCase(proper);
                ValuePass = proper;
            }
            return ValuePass;
        }
    }
}
