CREATE FUNCTION ufn_CashInUsersGames (@Gamename VARCHAR(MAX))
RETURNS TABLE
AS
	RETURN
	(
		SELECT SUM(gameAndCash.Cash) AS SumCash
		FROM
		(
		    SELECT g.Name,
		    	   ug.Cash,
		    	   ROW_NUMBER() OVER(ORDER BY ug.Cash DESC) AS RowsDescending 
		    FROM Games AS g 
		    JOIN UsersGames AS ug ON ug.GameId = g.Id
		    WHERE g.Name = @Gamename
		) AS gameAndCash
		WHERE gameAndCash.RowsDescending % 2 != 0 
	);



