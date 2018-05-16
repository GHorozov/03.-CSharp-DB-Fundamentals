CREATE VIEW V_EmployeeNameJobTitle AS
SELECT FirstName +' '+ ISNULL(CONVERT(VARCHAR(50), MiddleName),'') + ' ' + LastName AS FullName,
	   JobTitle
FROM Employees