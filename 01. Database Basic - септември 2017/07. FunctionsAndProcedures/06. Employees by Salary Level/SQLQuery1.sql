CREATE PROCEDURE usp_EmployeesBySalaryLevel (@SalaryLevel VARCHAR(7))
AS
SELECT FirstName AS [First Name],
		LastName as [Last Name]
FROM Employees
WHERE dbo.ufn_GetSalaryLevel(Salary) = @SalaryLevel
