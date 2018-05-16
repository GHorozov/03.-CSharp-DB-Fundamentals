SELECT TOP(1) WITH TIES
	   c.Name,
	   AVG(f.Rate) AS [FeedbackRate]
FROM Feedbacks AS f
JOIN Customers AS cus ON cus.Id = f.CustomerId
JOIN Countries AS c ON c.Id = cus.CountryId
GROUP BY c.Name
ORDER BY  AVG(f.Rate) DESC

