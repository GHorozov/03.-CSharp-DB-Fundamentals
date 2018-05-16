CREATE PROCEDURE usp_GetTownsStartingWith (@String VARCHAR(MAX))
AS
SELECT Name AS Town 
FROM Towns
WHERE LEFT(Name, LEN(@String)) = @String


--CREATE PROCEDURE usp_GetTownsStartingWith (@String VARCHAR(MAX))
--AS
--SELECT Name AS Town 
--FROM Towns
--WHERE Name LIKE(@String + '%')
