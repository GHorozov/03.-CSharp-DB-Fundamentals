SELECT COUNT(c.CountryCode) AS CountryCode 
FROM Countries AS c
LEFT OUTER JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
WHERE mc.CountryCode IS NULL
