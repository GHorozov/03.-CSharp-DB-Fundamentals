CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan @Number DECIMAL(10,2)
AS
SELECT FirstName AS [First Name], 
	   LastName AS [Last Name]
FROM AccountHolders AS ah
JOIN Accounts AS a ON a.AccountHolderId= ah.Id
GROUP BY FirstName,LastName
HAVING  SUM(a.Balance) > @Number  

--Works but judge don't like it
--CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan @Number DECIMAL(10,2)
--AS
--SELECT FirstName AS [First Name], 
--	   LastName AS [Last Name],
--	   t.TotalBalance
--FROM AccountHolders AS ah
--JOIN Accounts AS a ON a.AccountHolderId= ah.Id
--JOIN 
--(
--SELECT AccountHolderId,
--		sum(Balance) AS TotalBalance
--FROM Accounts
--group by AccountHolderId
--) as t ON t.AccountHolderId = a.AccountHolderId
--WHERE t.TotalBalance > @Number