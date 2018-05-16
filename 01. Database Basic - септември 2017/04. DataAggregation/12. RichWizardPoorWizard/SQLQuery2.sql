SELECT SUM([DIFFERENCE]) AS SumDifference
FROM
	(
		SELECT DepositAmount -  LEAD(DepositAmount) OVER(ORDER BY id) AS [Difference]
		FROM WizzardDeposits
	) AS Diff