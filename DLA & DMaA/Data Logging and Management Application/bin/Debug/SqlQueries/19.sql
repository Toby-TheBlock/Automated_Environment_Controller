-- ADDING A PROCEDURE
CREATE PROCEDURE UpdateEntry1Columns
@TableName char(30), @ColumnName1 char(30), @Value1 char(30), 
@ColumnName char(30), @SearchData char(30) AS 

DECLARE 
    @table NVARCHAR(128),
    @sql NVARCHAR(MAX);

SET @table = N''+ @TableName + '';
SET @sql = N'UPDATE ' + @table + ' SET ' + QUOTENAME(@ColumnName1) + ' = ''' + @Value1 + ''' WHERE ' + QUOTENAME(@ColumnName) + ' = ''' + @SearchData + '''';

EXEC sp_executesql @sql;