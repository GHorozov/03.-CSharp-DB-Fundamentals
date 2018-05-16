CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber @AboveNumber DECIMAL(18,4)
AS
SELECT FirstName,
	   LastName
FROM Employees
WHERE Salary >= @AboveNumber;
