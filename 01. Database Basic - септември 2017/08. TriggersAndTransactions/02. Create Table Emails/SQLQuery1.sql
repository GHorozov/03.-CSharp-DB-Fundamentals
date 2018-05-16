CREATE TABLE NotificationEmails
(
	Id INT PRIMARY KEY IDENTITY,
    Recipient INT NOT NULL,
	Subject VARCHAR(MAX), 
	Body VARCHAR(MAX)
)
GO

CREATE TRIGGER tr_WhenLogsInsertedData ON dbo.Logs FOR INSERT
AS
BEGIN
	INSERT INTO NotificationEmails (Recipient, Subject, Body) VALUES
	(

	(SELECT AccountId FROM inserted),
	
	CONCAT('Balance change for account: ', (SELECT  AccountId FROM inserted)),

	
	CONCAT('On ', FORMAT(GETDATE(), 'dd-MM-yyyy HH:mm'), (' your balance was changed from '),
	(SELECT OldSum FROM Logs),
    (' to '),
    (SELECT NewSum FROM Logs), ' .')
	
	);
END

GO

select * from Logs
SELECT * FROM Accounts

