SELECT TOP 5
       c.CountryName,
	   r.RiverName
FROM Countries AS c
FULL JOIN CountriesRivers AS cr
ON cr.CountryCode = c.CountryCode
FULL OUTER JOIN Rivers AS r
ON r.Id= cr.RiverId
WHERE c.ContinentCode = 'AF' 
ORDER BY c.CountryName

