DECLARE @userId INT = (SELECT Id FROM Users WHERE Username = 'Stamat')
DECLARE @gameId INT = (SELECT Id FROM Games WHERE Name = 'Safflower')
DECLARE @usergameId INT = (SELECT Id FROM UsersGames WHERE UserId = @userId AND GameId = @gameId)

BEGIN TRY
BEGIN TRANSACTION tr_ForFirstLevels
	UPDATE UsersGames
	SET Cash -= (SELECT SUM(PRICE) FROM Items WHERE MinLevel IN (11, 12))
	WHERE Id = @usergameId

	DECLARE @userBalance DECIMAL(14, 4)= (SELECT Cash FROM UsersGames WHERE Id = @usergameId)
	
	IF(@userBalance < 0)
	BEGIN
		ROLLBACK 
		RETURN
	END
	 
	INSERT INTO UserGameItems
	SELECT Id, @usergameId FROM Items WHERE MinLevel IN (11,12)

COMMIT
END TRY
BEGIN CATCH
	ROLLBACK
END CATCH


BEGIN TRY
BEGIN TRANSACTION tr_ForSecondLevels
	UPDATE UsersGames
	SET Cash -= (SELECT SUM(PRICE) FROM Items WHERE MinLevel BETWEEN 19 AND 21)
	WHERE Id = @usergameId

	SET @userBalance = (SELECT Cash FROM UsersGames WHERE Id = @usergameId)
	
	IF(@userBalance < 0)
	BEGIN
		ROLLBACK 
		RETURN
	END
	 
	INSERT INTO UserGameItems
	SELECT Id, @usergameId FROM Items WHERE MinLevel BETWEEN 19 AND 21

COMMIT
END TRY
BEGIN CATCH
	ROLLBACK
END CATCH



SELECT i.Name 
FROM Items AS i
JOIN UserGameItems AS ugi ON ugi.ItemId= i.Id
WHERE ugi.UserGameId = @usergameId
ORDER BY i.Name
