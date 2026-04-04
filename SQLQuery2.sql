--SELECT * FROM Directors WHERE EXISTS (SELECT * FROM Directors WHERE first_name = N'Test' AND last_name = N'Testovich');
--PRINT (SELECT COUNT(first_name) FROM AS (SELECT * FROM Directors WHERE (first_name = N'Test' AND last_name = N'Testovich'))
--CREATE OR ALTER FUNCTION Check_Movies(@title AS NVARCHAR(50), @release_date AS DATE, @director AS INT)RETURNS INT
--                            AS
--                            BEGIN
--                            DECLARE @res AS INT;
--                            SET @res = (SELECT COUNT(mouvie_id) FROM Movies WHERE (title = @title AND release_date = @release_date AND director = @director));
--                            RETURN @res
--                            END;
USE Movies_PV_522;
GO
--dbo.Check_Directors N'Test',N'Testovich'

SELECT * FROM Directors WHERE (first_name = N'Test' AND last_name = N'Testovich');
--SELECT * FROM Movies WHERE (title = N'Test Film' AND release_date = N'2026-04-01' AND director = 12);