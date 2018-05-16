CREATE TABLE Logs
(
	LogId INT PRIMARY KEY IDENTITY,
    AccountId INT FOREIGN KEY REFERENCES Accounts(Id),
    OldSum DECIMAL(12,2),
    NewSum DECIMAL(12,2)
)

SELECT * FROM Logs
SELECT * FROM Accounts

GO

CREATE TRIGGER tr_SaveToLogs ON Accounts FOR UPDATE
AS
BEGIN
	INSERT INTO Logs (AccountId, OldSum,NewSum)

	SELECT d.AccountHolderId,
		   d.Balance,
		   i.Balance
	FROM deleted AS d
	JOIN inserted AS i ON i.Id = d.Id
END;

UPDATE Accounts
SET Balance = 120
WHERE Id = 1

SELECT * FROM Accounts
SELECT * FROM Logs