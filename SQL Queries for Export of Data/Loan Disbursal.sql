SELECT case when cr ='0' then 'Cash' else cr end as cr, dr, amount, CONVERT(VARCHAR(10), dt, 112) as dt FROM 
(select l.L_Id as id, ISNULL(p.P_Name,'0') as cr, l.L_LoanAmount as amount, l.L_FormDate as dt from LoanSanction l left outer join  PartyMaster as p on l.L_BankId = p.P_Id) as t_cr,

(select l.L_Id as id, p.P_Name as dr from LoanSanction l, PartyMaster as p WHERE l.L_PartyId = p.P_Id) as t_dr

where

t_cr.id = t_dr.id AND dt in ('2015-02-18');