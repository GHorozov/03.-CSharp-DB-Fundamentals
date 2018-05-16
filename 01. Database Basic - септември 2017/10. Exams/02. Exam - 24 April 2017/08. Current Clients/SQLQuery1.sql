SELECT c.FirstName + ' ' + c.LastName AS [Client],
	   DATEDIFF(DAY, j.IssueDate, '04/24/2017') AS [Days going],
	   Status
FROM Jobs AS j
JOIN Clients AS c ON c.ClientId = j.ClientId
WHERE j.Status IN ('In Progress', 'Pending')
ORDER BY [Days going] DESC, j.ClientId