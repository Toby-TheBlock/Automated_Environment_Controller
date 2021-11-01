-- ADDING A PROCEDURE
CREATE PROCEDURE SelectAllFromTable
@TableName char(30), @NumberOfRows char(30) AS 

DECLARE 
    @table NVARCHAR(128),
    @sql NVARCHAR(MAX);

SET @table = N'' + @TableName + '';
SET @sql = N'SELECT TOP(' + @NumberOfRows + ') * FROM ' + @table;

EXEC sp_executesql @sql;