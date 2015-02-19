SELECT 'Interest A/c' as cr, party.P_Name as dr, pmts.Interest as amount, CONVERT(VARCHAR(10), emi.C_Date, 112) as dt from EmiReceived emi, TmpDataCal as pmts, LoanSanction as loan, PartyMaster as party where emi.C_SId = loan.L_Id AND pmts.Id = loan.L_Id AND loan.L_PartyId = party.P_Id  AND emi.C_EMINo = pmts.EMINo AND emi.C_Date in ('20150415');