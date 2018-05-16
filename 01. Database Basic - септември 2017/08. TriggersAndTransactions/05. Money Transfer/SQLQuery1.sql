CREATE PROCEDURE usp_TransferMoney(@SenderId INT, @ReceiverId INT, @Amount DECIMAL(14, 4)) 
AS
BEGIN
    IF(@Amount < 0)
	BEGIN
		RAISERROR('Negative amount. Try again', 16, 1)
		RETURN
	END
	
	IF((SELECT Balance FROM Accounts WHERE Id = @SenderId) <= @Amount)
	BEGIN 
		RAISERROR('Unsufficient funds', 16, 2)
		RETURN
	END

	BEGIN TRAN trans_TransferMoneyFromAtoB
	
	UPDATE Accounts
	SET Balance -= @Amount
	WHERE Id = @SenderId
	
	UPDATE Accounts
	SET Balance += @Amount
	WHERE Id = @ReceiverId 

	COMMIT
END

SELECT * FROM Accounts

EXEC usp_TransferMoney 5, 1, 500