SELECT cus.FirstName,
	   cus.Age,
	   cus.PhoneNumber
FROM  Customers AS cus
JOIN Countries AS c ON c.Id= cus.CountryId
WHERE Age >= 21 AND (FirstName LIKE '%an%') OR (RIGHT(PhoneNumber, 2) = '38')  AND (c.Name <> 'Greece')
ORDER BY cus.FirstName, cus.Age DESC  