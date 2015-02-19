SELECT cr, case when dr ='0' then 'Cash' else dr end as dr, amount, CONVERT(VARCHAR(10), dt, 112) as dt  FROM

(SELECT emi.C_Id as id, party.P_Name cr, emi.C_AmountRec as amount, emi.C_Date as dt  FROM EMIReceived emi, LoanSanction sanc, LoanApplication app, PartyMaster party  WHERE emi.C_SId = sanc.L_Id and sanc.L_ApplicationId = app.L_Id and app.L_PartyId = party.P_id) as t_cr, 



(SELECT emi.C_Id as id, ISNULL(party.P_Name,'0') AS dr FROM EMIReceived emi LEFT OUTER JOIN PartyMaster party  on emi.C_BankId = party.P_Id) as t_dr

WHERE t_cr.id = t_dr.id AND dt in ('2013-04-15');