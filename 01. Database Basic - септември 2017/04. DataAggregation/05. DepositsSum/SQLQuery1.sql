SELECT DepositGroup,
	   SUM(DepositAmount) AS [TotalSum]
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY DepositGroup 