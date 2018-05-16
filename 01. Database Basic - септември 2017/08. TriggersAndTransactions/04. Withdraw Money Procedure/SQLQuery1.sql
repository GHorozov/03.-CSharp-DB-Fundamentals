CREATE PROCEDURE usp_WithdrawMoney (@AccountId INT, @MoneyAmount DECIMAL(14,4)) 
AS
BEGIN
	
	IF(@MoneyAmount < 0)
	BEGIN
		RAISERROR('Negative amount. Try again', 16, 1)
		RETURN
	END

	BEGIN TRANSACTION trans_WitdrawMoney
	UPDATE Accounts
	SET Balance -= @MoneyAmount
	WHERE Id = @AccountId

	IF(@@ROWCOUNT <> 1)
	BEGIN 
		ROLLBACK;
		RAISERROR('Invalid account', 16, 2)
	END
	COMMIT
END