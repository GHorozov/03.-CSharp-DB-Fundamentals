SELECT  ppp.ContinentCode,
		ppp.CurrencyCode,
		ppp.CurrencyUsage
FROM 
(
	SELECT ggg.ContinentCode,
		   ggg.CurrencyCode,
		   ggg.CurrencyUsage,
		   DENSE_RANK() OVER(PARTITION BY ggg.ContinentCode ORDER BY ggg.CurrencyUsage DESC) AS RankUsage
	FROM
	(
		SELECT  c.ContinentCode,
				c.CurrencyCode,
				COUNT(c.CurrencyCode) AS CurrencyUsage
		FROM Countries AS c
		GROUP BY c.ContinentCode, c.CurrencyCode
		HAVING COUNT(c.CurrencyCode) > 1
	) AS ggg
) AS ppp
WHERE ppp.RankUsage = 1
ORDER BY ppp.ContinentCode







