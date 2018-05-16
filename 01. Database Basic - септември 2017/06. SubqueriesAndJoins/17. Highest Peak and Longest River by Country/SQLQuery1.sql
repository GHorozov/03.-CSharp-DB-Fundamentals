SELECT  Top 5
		peaks.CountryName,
		peaks.Elevation as HighestPeakElevation,
		rivers.Length AS LongestRiverLength
FROM
(
	SELECT c.CountryName,
		   p.Elevation,
		   DENSE_RANK() OVER(PARTITION BY c.CountryName  ORDER BY p.Elevation DESC) AS DescendingElevationRank
	FROM Countries AS c
	FULL OUTER JOIN MountainsCountries AS mc ON mc.CountryCode= c.CountryCode
	FULL OUTER JOIN Mountains AS m ON m.Id = mc.MountainId
	FULL OUTER JOIN Peaks AS p ON p.MountainId = m.Id
) AS peaks
FULL OUTER JOIN 
(
	SELECT c.CountryName,
		   r.RiverName,
		   r.Length,
		   DENSE_RANK() OVER(PARTITION BY c.CountryName ORDER BY r.Length DESC) AS DescendingRiverLengthRank 
	FROM Countries AS c
	FULL OUTER JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
	FULL OUTER JOIN Rivers AS r ON r.Id = cr.RiverId 
) AS rivers ON peaks.CountryName = rivers.CountryName
WHERE peaks.DescendingElevationRank=1
 AND rivers.DescendingRiverLengthRank=1 
 AND peaks.Elevation IS NOT NULL  
 AND rivers.Length IS NOT NULL
ORDER BY HighestPeakElevation DESC,
		 LongestRiverLength DESC,
		 CountryName
		 