CREATE TABLE Deleted_Employees
(
	EmployeeId INT PRIMARY KEY IDENTITY, 
	FirstName VARCHAR(50), 
	LastName VARCHAR(50), 
	MiddleName VARCHAR(50), 
	JobTitle VARCHAR(50) NOT NULL, 
	DeparmentId INT FOREIGN KEY REFERENCES Departments(departmentId), 
	Salary DECIMAL(10,2)
)

GO

CREATE TRIGGER tr_Deleted_Employees ON Employees FOR DELETE
AS
BEGIN
	INSERT INTO  Deleted_Employees 	
	SELECT FirstName,
		   LastName,
		   MiddleName,
		   JobTitle,
		   DepartmentID,
		   Salary
	FROM deleted
	
END