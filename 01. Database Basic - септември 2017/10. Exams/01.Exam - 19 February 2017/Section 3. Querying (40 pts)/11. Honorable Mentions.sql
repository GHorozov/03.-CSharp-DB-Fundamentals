SELECT  ProductId,
		CONCAT(c.FirstName, ' ', c.LastName) AS [CustomerName],
		F.Description	
FROM Feedbacks AS f
JOIN Customers AS c ON c.Id = f.CustomerId
WHERE c.Id IN (
				SELECT CustomerId
				FROM Feedbacks
				GROUP BY CustomerId
				HAVING COUNT(Id) >= 3)
ORDER BY ProductId, [CustomerName], f.Id


