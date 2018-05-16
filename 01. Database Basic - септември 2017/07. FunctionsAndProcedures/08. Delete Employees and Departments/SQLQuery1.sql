CREATE PROCEDURE usp_DeleteEmployeesFromDepartment (@departmetId INT)
AS
BEGIN
	--delete employeeId from EployeeProject
	DELETE FROM EmployeesProjects
	WHERE EmployeeID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmetId)

	--make colum ManagerID to accept null in table Departments
	ALTER TABLE Departments
	ALTER COLUMN ManagerId INT NULL; 

	--set ManagerID in Depertments to null where managerID is equal to @departmetId
	UPDATE Departments
	SET ManagerID = NULL
	WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmetId)
	
	--set managerId from epmloyee to null bacause there are eployees that are manager to other employees
	UPDATE Employees
	SET ManagerID = NULL
	WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmetId)

	--delete all from employees where departmentId is equal to @departmetId
	DELETE FROM Employees
	WHERE DepartmentID = @departmetId

	--delete department from departments
	DELETE FROM Departments
	WHERE DepartmentID = @departmetId

	--check if departmentId that is input is deleted
	SELECT COUNT(*)
	FROM Employees
	WHERE DepartmentID = @departmetId
END


