-- ADDING A PROCEDURE
CREATE PROCEDURE InsertIntoTable5Columns
@TableName char(30), @ColumnName1 char(30), @Value1 char(30), @ColumnName2 char(30), @Value2 char(30), @ColumnName3 char(30), @Value3 char(30), @ColumnName4 char(30),  @Value4 char(30), 
@ColumnName5 char(30),  @Value5 char(30) AS 

DECLARE 
    @table NVARCHAR(128),
    @sql NVARCHAR(MAX);

SET @table = N''+ @TableName + '';
SET @sql = N'INSERT ' + @table + ' (' + QUOTENAME(@ColumnName1) + ', ' + QUOTENAME(@ColumnName2) + ', ' + QUOTENAME(@ColumnName3) + ', ' + QUOTENAME(@ColumnName4) +
', ' + QUOTENAME(@ColumnName5) + ') VALUES (''' + @Value1 + ''', ''' + @Value2 + ''', ''' + @Value3 + ''', ''' + @Value4 + ''', ''' + @Value5 + ''')';

EXEC sp_executesql @sql;

