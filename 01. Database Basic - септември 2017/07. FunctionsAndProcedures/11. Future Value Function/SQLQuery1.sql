CREATE FUNCTION ufn_CalculateFutureValue (@Sum MONEY, @YearlyInterestRate FLOAT, @NumberOfYears INT)
RETURNS DECIMAL(12,4)
AS
BEGIN
		RETURN  @Sum * (POWER((1+@YearlyInterestRate), @NumberOfYears));
END


