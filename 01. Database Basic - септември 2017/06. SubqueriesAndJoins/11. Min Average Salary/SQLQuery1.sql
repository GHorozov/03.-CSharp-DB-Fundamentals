SELECT MIN(AverageSalary) 
FROM
(
  SELECT AVG(em.Salary) AS AverageSalary
  FROM Employees AS em 
  GROUP BY em.DepartmentID
) AS MinAverageSalary

