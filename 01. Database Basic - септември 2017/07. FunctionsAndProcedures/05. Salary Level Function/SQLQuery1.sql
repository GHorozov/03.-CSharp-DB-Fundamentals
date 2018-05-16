CREATE FUNCTION ufn_GetSalaryLevel (@salary DECIMAL(18,4)) 
RETURNS VARCHAR(7)
BEGIN 
	DECLARE @Result varchar(7);
	IF(@salary < 30000)
	BEGIN
		RETURN 'Low'
	END

	IF(@salary >= 30000 AND @salary <= 50000)
	BEGIN
		RETURN 'Average'
	END
	
	RETURN 'High'
END


--GO
--SELECT Salary,
--	   dbo.ufn_GetSalaryLevel(Salary) AS SalaryLevel  
--FROM Employees 