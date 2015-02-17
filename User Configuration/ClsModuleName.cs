using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Finance_Management_System
{
    public class ClsModuleName
    {
        public static bool ChechRights(int UId, object RightsOn,string ColName)
        {
            bool IsRightGiven = false;
            try
            {
                string SS = "Select " + ColName + " From UserConfiguration Where U_UId = " + UId + " And U_MId = " + RightsOn.ToString() + "";
                System.Data.DataTable Dt = new ClsDataAccess().GetDataTable(SS);
                if (Dt.Rows.Count > 0)
                {
                    IsRightGiven = bool.Parse(Dt.Rows[0][ColName].ToString());
                }
            }
            catch (Exception Erp)
            {
                clsGeneral.ShowErrMsg(Erp.Message);
                IsRightGiven = false;
            }
            if (IsRightGiven == false)
            {
                clsGeneral.ShowMessage("You Do not Have Rights to Access This Module");
            }
            return IsRightGiven;
        }

        public enum Rights
        {
            U_Save,
            U_Delete,
            U_Print,
            U_View
        }

        public enum ModuleName
        {
            Customer = 101,
            Guarantor = 102,
            Office = 103,
            Agent = 104,
            Documents = 105,
            Company_Info = 106,

            Loan_Application = 201,
            Loan_Sanction = 202,
            EMI_Received = 203,
            Penalty_Receive = 204,
            Penalty_Charged = 205,
            Account_ForeClosure = 206,
            Agent_Payment = 207,
           
            Customer_List = 301,
            Guarantor_List = 302,
            EMI_Due_Statement = 303,
            Loan_Application_Chart = 304,
            Loan_Application_Report = 305,
            Loan_Sanction_Report = 306,
            Loan_Ledger = 307,
            Cash_Summary = 308,
            Customer_History = 309,
            Agent_History = 310,
            Form_13 = 311,
            Agent_Commission_Cum_Interest_Statement = 312,
            Agent_Ledger = 313,
            Interest_Report = 314
        }
    }
}
