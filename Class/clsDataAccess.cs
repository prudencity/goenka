using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient ;
using System.Drawing;
using System.IO;

namespace Finance_Management_System
{
    public class ClsDataAccess : IDisposable
    {
        private bool disposedValue = false;
        public SqlConnection Conn;
       
        public void CreateConnection()
        {
            if (Conn == null)
            {
                Conn = new SqlConnection(Program.connectionString);
            }
            if (Conn.State!=ConnectionState.Open)
            {
                Conn.Open();
            }
        }

        public string ImageToBase64(Image image)
        {
            if (image == null)
            {
                return "";
            }
            System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Jpeg;
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public Image Base64ToImage(string base64String)
        {
            if (base64String.Trim().Length > 0)
            {
                // Convert Base64 String to byte[]
                byte[] imageBytes = Convert.FromBase64String(base64String);
                MemoryStream ms = new MemoryStream(imageBytes, 0,
                  imageBytes.Length);

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                return image;
            }
            else
            {
                Image image = null;
                return image;
            }
        }

        public SqlConnection GetConnectionObj()
        {
            CreateConnection();
            return Conn;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public void CloseConnection()
        {
            if (Conn != null)
            {
                if (Conn.State != ConnectionState.Closed)
                {
                    Conn.Close();
                }
                Conn = null;
            }
        }

        public string GetPartyBalanceStr(int PartyId)
        {
            string Balance = "";
            try
            {
                string SS = "SELECT L_Id As Id,Row_Number()Over(Order By L_FormNo) As [S.No.], ";
                SS = SS + " 'Loan Sanction No : ' + Convert(Varchar,L_FormNo) As [Loan No], ";
                SS = SS + " Convert(Varchar,L_FormDate,106) As [Loan Date] ";
                SS = SS + " FROM LoanSanction WHERE L_PartyId = " + PartyId + " Order By L_FormNo ";
                DataTable Dt = GetDataTable(SS);
               
                double TotalBalance = 0;

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    int Id = Dt.Rows[i]["Id"].ToString().IntParse();
                    double OpBalance = GetLoanbalance(Id);
                    TotalBalance = TotalBalance + OpBalance;
                }
                if (TotalBalance > 0)
                {
                    Balance = clsGeneral.CurFormat(Math.Abs(TotalBalance)) + " Dr";
                }
                else if (TotalBalance < 0)
                {
                    Balance = clsGeneral.CurFormat(Math.Abs(TotalBalance)) + " Cr";
                }
                else
                {
                    Balance = "0.00";
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
            return Balance;
        }

        public double GetPartyBalance(int PartyId)
        {
            double Balance = 0;
            try
            {
                string SS = "SELECT L_Id As Id,Row_Number()Over(Order By L_FormNo) As [S.No.], ";
                SS = SS + " 'Loan Sanction No : ' + Convert(Varchar,L_FormNo) As [Loan No], ";
                SS = SS + " Convert(Varchar,L_FormDate,106) As [Loan Date] ";
                SS = SS + " FROM LoanSanction WHERE L_PartyId = " + PartyId + " Order By L_FormNo ";
                DataTable Dt = GetDataTable(SS);

                double TotalBalance = 0;

                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    int Id = Dt.Rows[i]["Id"].ToString().IntParse();
                    double OpBalance = GetLoanbalance(Id);
                    TotalBalance = TotalBalance + OpBalance;
                }
                Balance = TotalBalance;
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
            return Balance;
        }

        public string GetLoanbalanceStr(int LoanId)
        {
            string Balance = "";
            try
            {
                double DrBalance = 0;
                double CrBalance = 0;
                string SS = "Sp_Calculation " + LoanId;
                DataTable Dtdata = GetDataTable(SS);

                Dtdata.DefaultView.RowFilter = "Iden <> 1 And Amount <> 0";
                Dtdata.DefaultView.Sort = "[Date Received] Asc,Iden Asc, [EMI No] Asc";
                DataTable D = Dtdata.DefaultView.ToTable();

                SS = "Select F_Date From AccountForeClosure Where F_SId = " + LoanId + "";
                DataTable DD = GetDataTable(SS);
                if (DD.Rows.Count > 0)
                {
                    DateTime Dtp = DateTime.Parse(DD.Rows[0]["F_Date"].ToString());
                    D = D.Delete("[Date Received] > '" + Dtp.ToShortDateString() + "'");
                    D.AcceptChanges();
                }

                object DrTotal = D.Compute("Sum(Amount)", "Iden In (-2,3,5,10,11)");
                object CrTotal = D.Compute("Sum(Amount)", "Iden In (2,4,12)");

                DrBalance = DrTotal.ToString().DoubleParse();
                CrBalance = CrTotal.ToString().DoubleParse();

                double OpBalance = DrBalance - CrBalance;
                if (OpBalance > 0)
                {
                    Balance = clsGeneral.CurFormat(Math.Abs(OpBalance)) + " Dr";
                }
                else if (OpBalance < 0)
                {
                    Balance = clsGeneral.CurFormat(Math.Abs(OpBalance)) + " Cr";
                }
                else
                {
                    Balance = "0.00";
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
            return Balance;
        }

        public void GetLoanbalance(int LoanId, out double Balance, out DataTable Dt)
        {
            double B = 0;
            DataTable D = null;
            try
            {
                double DrBalance = 0;
                double CrBalance = 0;
                string SS = "Sp_Calculation " + LoanId;
                DataTable Dtdata = GetDataTable(SS);

                Dtdata.DefaultView.RowFilter = "Iden <> 1 And Amount <> 0";
                Dtdata.DefaultView.Sort = "[Date Received] Asc,Iden Asc, [EMI No] Asc";
                D = Dtdata.DefaultView.ToTable();

                SS = "Select F_Date From AccountForeClosure Where F_SId = " + LoanId + "";
                DataTable DD = GetDataTable(SS);
                if (DD.Rows.Count > 0)
                {
                    DateTime Dtp = DateTime.Parse(DD.Rows[0]["F_Date"].ToString());
                    D = D.Delete("[Date Received] > '" + Dtp.ToShortDateString() + "'");
                    D.AcceptChanges();
                }

                object DrTotal = D.Compute("Sum(Amount)", "Iden In (-2,3,5,10,11)");
                object CrTotal = D.Compute("Sum(Amount)", "Iden In (2,4,12)");


                DrBalance = DrTotal.ToString().DoubleParse();
                CrBalance = CrTotal.ToString().DoubleParse();

                B = DrBalance - CrBalance;
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
            Balance = B;
            Dt = D;
        }

        public double GetLoanbalance(int LoanId)
        {
            double Balance = 0;
            try
            {
                double DrBalance = 0;
                double CrBalance = 0;
                string SS = "Sp_Calculation " + LoanId;
                DataTable Dtdata = GetDataTable(SS);

                Dtdata.DefaultView.RowFilter = "Iden <> 1 And Amount <> 0";
                Dtdata.DefaultView.Sort = "[Date Received] Asc,Iden Asc, [EMI No] Asc";
                DataTable D = Dtdata.DefaultView.ToTable();

                SS = "Select F_Date From AccountForeClosure Where F_SId = " + LoanId + "";
                DataTable DD = GetDataTable(SS);
                if (DD.Rows.Count > 0)
                {
                    DateTime Dtp = DateTime.Parse(DD.Rows[0]["F_Date"].ToString());
                    D = D.Delete("[Date Received] > '" + Dtp.ToShortDateString() + "'");
                    D.AcceptChanges();
                }

                object DrTotal = D.Compute("Sum(Amount)", "Iden In (-2,3,5,10,11)");
                object CrTotal = D.Compute("Sum(Amount)", "Iden In (2,4,12)");

                DrBalance = DrTotal.ToString().DoubleParse();
                CrBalance = CrTotal.ToString().DoubleParse();

                Balance = DrBalance - CrBalance;
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
            return Balance;
        }

        public void GetPenalty(double PreviousPenalty, float PenaltyRate, System.DateTime DtpPay, System.DateTime[] Dtp, double[] EMI, out double TotalPenalty, out double TotalEMI)
        {
            TotalPenalty = 0;
            TotalEMI = 0;
            double Penalty = 0;
            double EMIIncPenalty = 0;
            try
            {
                for (int i = 0; i < Dtp.Length; i++)
                {
                    System.DateTime D = Dtp[i];
                    double E = EMI[i];
                    TotalEMI = Dtp.Length * E;
                    if (D < DtpPay)
                    {
                        TimeSpan Ts = DtpPay.Date - D.Date;
                        int daysDiff = Ts.Days;
                        if (daysDiff > 30 || D.Month == 2)
                        {
                            E = E + EMIIncPenalty;
                            Penalty = (E * PenaltyRate) / 100;
                            TotalPenalty = Penalty + TotalPenalty;
                            EMIIncPenalty = E + Penalty;
                        }
                        else if (daysDiff < 30)
                        {
                            E = E + EMIIncPenalty;
                            Penalty = (E * PenaltyRate) / 100;
                            Penalty = (Penalty / 30) * daysDiff;
                            TotalPenalty = Penalty + TotalPenalty;
                            EMIIncPenalty = E + Penalty;
                        }
                    }
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
        }

        public bool GetPermission(string Module, int Id)
        {
            bool Permission = true;
            try
            {
                string SelData = "Select Permission From ModulePermission Where LoginId = " + Id + " And ModuleName = '" + Module.SqlEncode() + "'";
                DataTable DtData = GetDataTable(SelData);
                if (DtData.Rows.Count > 0)
                {
                    Permission = bool.Parse(DtData.Rows[0]["Permission"].ToString());
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
            return Permission;
        }

        public DataSet GetDataSet(string strsql)
        {
            CreateConnection();
            SqlDataAdapter da = new SqlDataAdapter(strsql, Conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public DataTable GetDataTable(string strsql)
        {
            CreateConnection();
            SqlDataAdapter da = new SqlDataAdapter(strsql, Conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public Boolean DuplicateChackingInvoice(string TblName, string FieldName, string Value, string MasterFeild, int MasterId)
        {
            try
            {
                string Qry = "select " + FieldName + " from " + TblName + " where " + FieldName + "=" + clsGeneral.SQLEncode(Value) + " and " + MasterFeild + " <> " + MasterId + "";
                if (GetDataTable(Qry).Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                clsGeneral.ShowMessage(ex.Message);
                return false;
            }

        }


        public SqlDataReader GetReader(string strsql)
        {
            CreateConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strsql;
            cmd.Connection = Conn;
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public bool reader(string strsql)
        {
            CreateConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strsql;
            SqlDataReader reader = cmd.ExecuteReader();
            bool ans = reader.Read();
            reader.Close();
            return ans;
        }

        public int ExecuteQuery(string strsql)
        {
            CreateConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strsql;
            int i;
            i = cmd.ExecuteNonQuery();
            return i;
        }

        public int ExecuteQueryIdentity(string strsql)
        {
            CreateConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strsql + "; select scope_identity();";
            int i;
            i = Convert.ToInt32(cmd.ExecuteScalar());
            return i;
        }

        public Boolean DuplicateChacking(string TblName, string FieldName, string Value, string MasterFeild, int MasterId)
        {
            try
            {
                if (FieldName.Trim().Length > 0)
                {
                    string Qry = "select " + FieldName + " from " + TblName + " where " + FieldName + "='" + clsGeneral.SQLEncode(Value) + "' and " + MasterFeild + " <> " + MasterId + "";
                    if (GetDataTable(Qry).Rows.Count > 0)
                    {
                        return true;
                    }
                    else
                    {

                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }

            catch (Exception ex)
            {
                clsGeneral.ShowMessage(ex.Message);
                return false;
            }

        }


        public int GetInsertIndentity(string strsql)
        {
            CreateConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strsql + "; select scope_identity();";
            int i;
            i = Convert.ToInt32(cmd.ExecuteScalar());
            return i;
        }


        #region " IDisposable Support "
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    CloseConnection();

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            if (!(Conn.State == ConnectionState.Closed))
            {
                CloseConnection();
            }
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }

}

