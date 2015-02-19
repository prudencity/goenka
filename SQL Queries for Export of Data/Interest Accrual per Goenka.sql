SELECT 'Interest A/c' AS cr
	,party.P_Name AS dr
	,(pmts.InterestPer / 100) * emi.C_AmountRec AS amount
	,CONVERT(VARCHAR(10), emi.C_Date, 112) AS dt
FROM EmiReceived emi
	,TmpDataCal AS pmts
	,LoanSanction AS loan
	,PartyMaster AS party
WHERE emi.C_SId = loan.L_Id
	AND pmts.Id = loan.L_Id
	AND loan.L_PartyId = party.P_Id
	AND emi.C_EMINo = pmts.EMINo
	AND emi.C_Date IN ('20131204');