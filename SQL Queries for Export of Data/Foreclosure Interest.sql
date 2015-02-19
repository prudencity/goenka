SELECT 'Interest A/c' AS cr
	,party.P_Name AS dr
	,pmts.F_InterestAmount amount
	,CONVERT(VARCHAR(10), pmts.F_Date, 112) AS dt
FROM AccountForeClosure AS pmts
	,LoanSanction AS loan
	,PartyMaster AS party
WHERE pmts.F_SId = loan.L_Id
	AND loan.L_PartyId = party.P_Id
	AND pmts.F_Date IN ('20150415');