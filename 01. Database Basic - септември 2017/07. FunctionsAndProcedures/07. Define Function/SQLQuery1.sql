CREATE FUNCTION ufn_IsWordComprised(@setOfLetters VARCHAR(MAX), @word VARCHAR(MAX))
RETURNS BIT
AS
BEGIN
	DECLARE @WordSymbolsCount INT = LEN(@word); 
	DECLARE @CurrentSymbol VARCHAR;
	DECLARE @SymbolIncrement INT=1;

	WHILE(@WordSymbolsCount > 0)
	BEGIN
		SET @CurrentSymbol =  SUBSTRING(@word, @SymbolIncrement, 1);
		IF(CHARINDEX(@CurrentSymbol, @setOfLetters, 1) <= 0)
		BEGIN
			RETURN 0;
		END

		SET @WordSymbolsCount -= 1;
		SET @SymbolIncrement += 1;
	END

	RETURN 1;
END 