SELECT Peaks.PeakName,
	   Rivers.RiverName,
	   Lower(CONCAT(Peaks.PeakName, Rivers.RiverName)) AS Mix
FROM Peaks, Rivers
WHERE RIGHT(PeakName, 1) = LEFT (RiverName, 1)
ORDER BY Mix


--SELECT Peaks.PeakName, 
--	   Rivers.RiverName, 
--	   LOWER(CONCAT(LEFT(Peaks.PeakName, LEN(Peaks.PeakName) - 1), Rivers.RiverName)) as Mix
--FROM Peaks
--JOIN Rivers ON RIGHT(Peaks.PeakName, 1) = LEFT(Rivers.RiverName, 1)
--ORDER BY Mix; 