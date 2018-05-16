USE Geography

SELECT m.MountainRange,
	   p.PeakName,
	   p.Elevation
FROM Mountains AS m
JOIN Peaks AS p ON M.Id = P.MountainId 
WHERE MountainRange = 'Rila'
ORDER BY p.Elevation DESC