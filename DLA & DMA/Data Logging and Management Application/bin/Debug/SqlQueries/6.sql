-- ADDING A PROCEDURE
CREATE PROCEDURE InsertIntoTable3Columns
@TableName char(30), @ColumnName1 char(30), @Value1 char(30), @ColumnName2 char(30), @Value2 char(30), @ColumnName3 char(30), @Value3 char(30) AS 

DECLARE 
    @table NVARCHAR(128),
    @sql NVARCHAR(MAX);

SET @table = N''+ @TableName + '';
SET @sql = N'INSERT ' + @table + ' (' + QUOTENAME(@ColumnName1) + ', ' + QUOTENAME(@ColumnName2) + ', ' + QUOTENAME(@ColumnName3) +
') VALUES (''' + @Value1 + ''', ''' + @Value2 + ''', ''' + @Value3 + ''')';

EXEC sp_executesql @sql;

