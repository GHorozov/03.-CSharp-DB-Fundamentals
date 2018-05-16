SELECT mc.CountryCode,
	   COUNT(mc.MountainId) AS MountainRange
FROM Countries AS c
JOIN MountainsCountries AS mc
ON mc.CountryCode= c.CountryCode
GROUP BY mc.CountryCode, c.CountryCode
HAVING c.CountryCode IN('BG', 'RU', 'US')