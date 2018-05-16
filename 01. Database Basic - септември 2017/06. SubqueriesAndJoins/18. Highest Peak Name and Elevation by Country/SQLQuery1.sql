SELECT TOP (5)
		peaks.CountryName,
		CASE
			WHEN PeakName IS NULL
			THEN '(no highest peak)'
			ELSE peaks.PeakName 
		END AS [Highest Peak Name],
		CASE 
			WHEN peaks.Elevation IS NULL
			THEN '0'
			ELSE peaks.Elevation
		END AS [Highest Peak Elevation],
		CASE 
			WHEN mountains.MountainRange IS NULL
			THEN '(no mountain)'
			ELSE mountains.MountainRange
		END AS [Mountain]
FROM
(
	SELECT c.CountryName,
		   p.PeakName,
		   p.Elevation,
		   DENSE_RANK() OVER(PARTITION BY c.CountryName ORDER BY p.Elevation DESC) AS DescendingPeakElevationRank,
		   m.MountainRange
	FROM Countries AS c
	FULL OUTER JOIN MountainsCountries AS mc ON mc.CountryCode = c.CountryCode
	FULL OUTER JOIN Mountains AS m ON m.Id = mc.MountainId
	FULL OUTER JOIN Peaks AS p ON p.MountainId = m.Id
) AS peaks
FULL OUTER JOIN
(
	SELECT c.CountryName,
		   m.MountainRange
	FROM Countries AS c
	FULL OUTER JOIN MountainsCountries AS mc ON mc.CountryCode= c.CountryCode
	FULL OUTER JOIN Mountains AS m ON m.Id = mc.MountainId
) AS mountains ON mountains.MountainRange = peaks.MountainRange
WHERE peaks.DescendingPeakElevationRank = 1 AND peaks.CountryName IS NOT NULL
ORDER BY peaks.CountryName