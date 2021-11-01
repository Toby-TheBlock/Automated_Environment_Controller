-- ADDING A PROCEDURE
CREATE PROCEDURE SelectAllFromView
@ViewName char(30), @NumberOfRows char(30) AS 

DECLARE 
    @sql NVARCHAR(MAX);

SET @sql = N'SELECT TOP(' + @NumberOfRows + ') * FROM ' + @ViewName;

EXEC sp_executesql @sql;