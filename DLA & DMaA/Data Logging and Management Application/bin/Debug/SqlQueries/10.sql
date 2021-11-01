-- ADDING A PROCEDURE
CREATE PROCEDURE InsertIntoTable7Columns
@TableName char(30), @ColumnName1 char(30), @Value1 char(30), @ColumnName2 char(30), @Value2 char(30), @ColumnName3 char(30), @Value3 char(30), @ColumnName4 char(30),  @Value4 char(30), 
@ColumnName5 char(30),  @Value5 char(30), @ColumnName6 char(30),  @Value6 char(30), @ColumnName7 char(30),  @Value7 char(30) AS 

DECLARE 
    @table NVARCHAR(128),
    @sql NVARCHAR(MAX);

SET @table = N''+ @TableName + '';
SET @sql = N'INSERT ' + @table + ' (' + QUOTENAME(@ColumnName1) + ', ' + QUOTENAME(@ColumnName2) + ', ' + QUOTENAME(@ColumnName3) + ', ' + QUOTENAME(@ColumnName4) +
', ' + QUOTENAME(@ColumnName5) + ', ' + QUOTENAME(@ColumnName6) + ', ' + QUOTENAME(@ColumnName7) + ') VALUES (''' + @Value1 + ''', ''' + @Value2 + ''', ''' + @Value3 + ''', ''' + @Value4 + ''', ''' 
+ @Value5 + ''', ''' + @Value6 + ''', ''' + @Value7 + ''')';

EXEC sp_executesql @sql;

