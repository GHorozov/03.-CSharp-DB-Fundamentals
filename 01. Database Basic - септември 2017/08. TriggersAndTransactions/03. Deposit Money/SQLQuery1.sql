CREATE PROC usp_DepositMoney(@AccountId INT, @MoneyAmount DECIMAL(14,4))
AS
BEGIN

	IF(@MoneyAmount < 0)
	BEGIN
	 ROLLBACK;
		RAISERROR('Error! You input negative money amount!', 16, 1)
	END

	BEGIN TRANSACTION	trans_Transfer
	UPDATE Accounts
	SET Balance += @MoneyAmount
	WHERE Id = @AccountId

	IF(@@ROWCOUNT <> 1)
	BEGIN
		ROLLBACK;
		RAISERROR('Invalid account', 16, 2)
	END

	COMMIT
END 

SELECT * FROM Accounts

EXEC dbo.usp_DepositMoney 1, 10