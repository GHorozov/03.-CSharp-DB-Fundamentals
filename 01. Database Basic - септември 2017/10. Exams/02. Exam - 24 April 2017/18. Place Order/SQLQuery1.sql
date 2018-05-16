CREATE PROCEDURE usp_PlaceOrder (@jobId INT, @serialNumber NVARCHAR(50), @quantity INT)
AS
BEGIN
	-- CHECH @QUANTITY IS <= 0
	IF(@quantity <= 0)
	BEGIN
		RAISERROR('Part quantity must be more than zero!',16, 1)
		RETURN 
	END

	--CHECH @JOBID IF EXIST
	IF((SELECT JobId FROM Jobs WHERE JobId = @jobId) IS NULL )
	BEGIN
		RAISERROR('Job not found!',16 ,1)
		RETURN
	END

	-- CHECK IF ORDER EXIST
	IF((SELECT Status
		FROM Jobs
		WHERE JobId = @jobId) = 'Finished')
		BEGIN
			RAISERROR('This job is not active!', 16 ,1) 
			RETURN
		END	

	--CHECH IF PART SERIAL NUMBER EXIST
	DECLARE @PartId  INT = (SELECT PartId FROM Parts WHERE SerialNumber= @serialNumber);
	IF(@PartId IS NULL)
	BEGIN 
		RAISERROR('Part not found!', 16 , 1)
		RETURN
	END

	DECLARE @OrderId INT =
	(
		SELECT o.OrderId
		FROM Orders AS o
		JOIN OrderParts AS op ON op.OrderId = o.OrderId
		JOIN Parts AS p ON p.PartId = op.PartId
		WHERE o.JobId = @jobId AND p.PartId = @PartId AND IssueDate IS NULL
	)
	 
	 
	--ADD NEW ORDER
	IF(@OrderId IS NULL)
	BEGIN
		INSERT INTO Orders (JobId, IssueDate) VALUES
		(@jobId, NULL)

		INSERT INTO OrderParts (OrderId, PartId, Quantity) VALUES
		(IDENT_CURRENT('Orders'),
		@PartId,
		@quantity)
	END
	ELSE
	BEGIN
		DECLARE @PartExistInOrder INT= (SELECT @@ROWCOUNT FROM OrderParts WHERE OrderId = @OrderId AND PartId = @PartId)
		
		IF(@PartExistInOrder IS NULL)
		BEGIN
			INSERT OrderParts (OrderId, PartId , Quantity) VALUES
			(@OrderId, @PartId, @quantity)
		END
		ELSE
		BEGIN
			UPDATE OrderParts
			SET Quantity += @quantity
			WHERE OrderId = @OrderId AND PartId = @PartId
		END 
	END
END



