SELECT CONCAT(c.FirstName, ' ', c.LastName) AS [CustomerName],
	   c.PhoneNumber,
	   c.Gender
FROM Customers AS c
LEFT JOIN Feedbacks AS f ON f.CustomerId = c.Id
WHERE f.Id IS NULL 

--SECOND WAY
SELECT CONCAT(c.FirstName, ' ', c.LastName) AS [CustomerName],
	   c.PhoneNumber,
	   c.Gender
FROM Customers AS c
WHERE c.Id NOT IN (SELECT CustomerId FROM Feedbacks)
