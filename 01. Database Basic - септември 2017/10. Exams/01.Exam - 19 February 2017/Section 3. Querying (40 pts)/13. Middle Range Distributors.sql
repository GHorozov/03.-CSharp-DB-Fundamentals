SELECT d.Name AS [DistributorName],
	   ing.Name AS [IngredientName],
	   p.Name AS [ProductName],
	   AVG(f.Rate) AS [AverageRate]
FROM Distributors AS d
JOIN Ingredients AS ing ON ing.DistributorId= d.Id
JOIN ProductsIngredients AS pii ON pii.IngredientId = ing.Id
JOIN Products AS p ON p.Id = pii.ProductId
JOIN Feedbacks AS f ON f.ProductId = p.Id
GROUP BY d.Name,
	   ing.Name,
	   p.Name
HAVING AVG(f.Rate) BETWEEN 5 AND 8
ORDER BY d.Name, ing.Name, p.Name

