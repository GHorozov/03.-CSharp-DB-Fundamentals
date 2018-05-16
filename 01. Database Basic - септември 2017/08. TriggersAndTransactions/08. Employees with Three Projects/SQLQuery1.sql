CREATE PROCEDURE usp_AssignProject(@emloyeeId INT, @projectID INT) 
AS
BEGIN
	
	BEGIN TRAN trans_AddProjectToEmployee
	INSERT INTO EmployeesProjects(EmployeeID, ProjectID) VALUES
	(@emloyeeId,@projectID)
	
	IF(
	(
	SELECT COUNT(e.EmployeeID)
	FROM EmployeesProjects AS e
	WHERE e.EmployeeID = @emloyeeId
	) >3)
	BEGIN
		ROLLBACK;
		RAISERROR('The employee has too many projects!', 16, 1)	
	END;

	COMMIT
END

