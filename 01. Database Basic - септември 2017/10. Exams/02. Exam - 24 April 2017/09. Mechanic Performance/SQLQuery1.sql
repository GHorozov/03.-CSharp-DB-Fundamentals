SELECT m.FirstName + ' ' + m.LastName AS [Mechanic],
	   AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate)) AS [Average Days]
FROM Mechanics AS m
JOIN Jobs AS j ON j.MechanicId = m.MechanicId
GROUP BY J.MechanicId, m.FirstName + ' ' + m.LastName
ORDER BY j.MechanicId

