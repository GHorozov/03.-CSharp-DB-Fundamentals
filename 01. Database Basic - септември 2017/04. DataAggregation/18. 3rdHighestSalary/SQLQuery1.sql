SELECT DepartmentID, Salary
FROM 
    (
     SELECT DepartmentID,
    		MAX(Salary) AS Salary,
    		DENSE_RANK () OVER(PARTITION BY DepartmentID ORDER BY Salary DESC) RANK
    	FROM Employees
    	GROUP BY DepartmentID, Salary
    ) AS SALARIES
WHERE RANK = 3