SELECT m.ModelId,
	   m.Name,
	   CONCAT(AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate)), ' days') AS [Average Service Time]
FROM Models AS m
JOIN Jobs AS j ON j.ModelId = m.ModelId
GROUP BY m.ModelId, m.Name
ORDER BY AVG(DATEDIFF(DAY, j.IssueDate, j.FinishDate))
