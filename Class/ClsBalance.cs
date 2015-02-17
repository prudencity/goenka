using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Finance_Management_System
{
    public class ClsBalance
    {
        public double GetCashBalance(System.DateTime Dt)
        {
            double Balance = 0;
            try
            {
                string SS = "Select Cash, AId, [Voucher No], ";
                SS = SS + " CDate,Date, ";
                SS = SS + " Particular, ";
                SS = SS + " [Amount Cr],[Amount Dr],Remarks From Vw_Voucher ";
                SS = SS + " Where Vr_Date   <= Convert(DateTime,'" + Dt.DateFormat() + "') ";
                SS = SS + " Order By Vr_Date,Vr_No,ACNAME";
                DataTable DtData = new ClsDataAccess().GetDataTable(SS);
                double OpCr = DtData.Compute("Sum([Amount Cr])", "CDate < " + Dt.DateFormat() + "").ToString().DoubleParse();
                double OpDr = DtData.Compute("Sum([Amount Dr])", "CDate < " + Dt.DateFormat() + "").ToString().DoubleParse();
                Balance = OpDr - OpCr;
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
            }
            return Balance;
        }
    }
}
