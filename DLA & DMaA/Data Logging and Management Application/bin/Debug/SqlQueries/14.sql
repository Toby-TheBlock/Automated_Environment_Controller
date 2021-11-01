-- ADDING A PROCEDURE
CREATE PROCEDURE UpdateEntry6Columns
@TableName char(30), @ColumnName1 char(30), @Value1 char(30), @ColumnName2 char(30), @Value2 char(30), @ColumnName3 char(30), @Value3 char(30), @ColumnName4 char(30), @Value4 char(30),
@ColumnName5 char(30), @Value5 char(30), @ColumnName6 char(30), @Value6 char(30), @ColumnName char(30), @SearchData char(30) AS 

DECLARE 
    @table NVARCHAR(128),
    @sql NVARCHAR(MAX);

SET @table = N''+ @TableName + '';
SET @sql = N'UPDATE ' + @table + ' SET ' + QUOTENAME(@ColumnName1) + ' = ''' + @Value1 + ''', ' + QUOTENAME(@ColumnName2) + ' = ''' + @Value2 + ''', '
+ QUOTENAME(@ColumnName3) + ' = ''' + @Value3 + ''', ' + QUOTENAME(@ColumnName4) + ' = ''' + @Value4 + ''', ' + QUOTENAME(@ColumnName5) + ' = ''' + @Value5 + ''', '
+ QUOTENAME(@ColumnName6) + ' = ''' + @Value6 + ''' WHERE ' + QUOTENAME(@ColumnName) + ' = ''' + @SearchData + '''';

EXEC sp_executesql @sql;