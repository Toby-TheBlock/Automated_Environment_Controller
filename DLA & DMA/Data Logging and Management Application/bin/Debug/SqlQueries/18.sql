-- ADDING A PROCEDURE
CREATE PROCEDURE InsertIntoTable1Columns
@TableName char(30), @ColumnName1 char(30), @Value1 char(30) AS 

DECLARE 
    @table NVARCHAR(128),
    @sql NVARCHAR(MAX);

SET @table = N''+ @TableName + '';
SET @sql = N'INSERT ' + @table + ' (' + QUOTENAME(@ColumnName1) + ') VALUES (''' + @Value1 + ''')';

EXEC sp_executesql @sql;