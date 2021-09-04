-- ADDING A PROCEDURE
CREATE PROCEDURE SelectSpecificFromTable
@TableName char(30), @ColumnName char(30), @SearchData char(30) AS 

DECLARE 
    @table NVARCHAR(128),
    @sql NVARCHAR(MAX);

SET @table = N''+ @TableName + '';
SET @sql = N'SELECT * FROM ' + @table + ' WHERE ' + QUOTENAME(@ColumnName) + ' = ''' + @SearchData + '''';

EXEC sp_executesql @sql;
