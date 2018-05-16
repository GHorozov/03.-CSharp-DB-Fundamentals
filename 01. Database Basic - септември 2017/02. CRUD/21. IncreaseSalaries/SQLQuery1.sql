DECLARE @Engineering INT
DECLARE @ToolDesign INT
DECLARE @Marketing INT
DECLARE @InformationServices INT

SELECT TOP 1 @Engineering = DepartmentID 
FROM Departments
WHERE [Name] = 'Engineering'

SELECT TOP 1 @ToolDesign = DepartmentID 
FROM Departments
WHERE [Name] = 'Tool Design'

SELECT TOP 1 @Marketing = DepartmentID 
FROM Departments
WHERE [Name] = 'Marketing'

SELECT TOP 1 @InformationServices = DepartmentID 
FROM Departments
WHERE [Name] = 'Information Services'

UPDATE Employees
   SET Salary *= 1.12
   WHERE DepartmentID IN (@Engineering,@ToolDesign,@Marketing,@InformationServices)

SELECT Salary
FROM Employees




