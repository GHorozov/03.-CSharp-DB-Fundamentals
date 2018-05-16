SELECT jb.JobId,
	(SELECT ISNULL(SUM(p.Price * op.Quantity), 0) AS Price 
       FROM Parts AS p
       JOIN OrderParts AS op ON op.PartId = p.PartId
       JOIN Orders AS o ON o.OrderId = op.OrderId
       JOIN Jobs AS j ON j.JobId = o.JobId
      WHERE j.JobId = jb.JobId) AS Total
FROM Jobs AS jb
WHERE jb.Status = 'Finished'
ORDER BY Total DESC, jb.JobId





