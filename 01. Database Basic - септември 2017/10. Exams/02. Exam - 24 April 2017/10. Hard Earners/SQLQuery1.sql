SELECT TOP (3)
	   m.FirstName + ' ' + m.LastName AS [Mechanic],
	   COUNT(j.Status) AS Jobs
FROM Mechanics AS m
JOIN Jobs AS j ON j.MechanicId = m.MechanicId
WHERE Status <> 'Finished'
GROUP BY j.Status, m.FirstName + ' ' + m.LastName, m.MechanicId
HAVING COUNT(j.Status) > 1
ORDER BY COUNT(j.Status) DESC, m.MechanicId


