SELECT cr
	,CASE 
		WHEN dr = '0'
			THEN 'Cash'
		ELSE dr
		END AS dr
	,amount
	,CONVERT(VARCHAR(10), dt, 112) AS dt
	,eno
	,ck_amt
	,ck_no
	, party_bank
	, party_bank_addr
FROM (
	SELECT emi.C_Id AS id
		,party.P_Name cr
		,emi.C_AmountRec AS amount
		,emi.C_Date AS dt
		,EMI.C_EMINo AS eno
		,cks.S_ChequeDate AS ck_amt
		,cks.S_ChequeNo   AS ck_no
		,sanc.L_PartyBank AS party_bank
		,sanc.L_PartyBankAdd AS party_bank_addr
	FROM 
		LoanSanction sanc
		,LoanApplication app
		,PartyMaster party
		, EMIReceived emi LEFT JOIN ChequeDetails as cks ON C_SId = cks.S_Id AND C_EMINo = cks.S_SNo
	WHERE emi.C_SId = sanc.L_Id
		AND sanc.L_ApplicationId = app.L_Id
		AND app.L_PartyId = party.P_id
	) AS t_cr
	,(
		SELECT emi.C_Id AS id
			,ISNULL(party.P_Name, '0') AS dr
		FROM EMIReceived emi
		LEFT JOIN PartyMaster party ON emi.C_BankId = party.P_Id
		) AS t_dr
WHERE t_cr.id = t_dr.id
	AND dt IN ('20150215');