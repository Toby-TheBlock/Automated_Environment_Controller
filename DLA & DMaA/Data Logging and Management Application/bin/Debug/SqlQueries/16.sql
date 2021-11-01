-- ADDING A PROCEDURE
CREATE PROCEDURE GetLastDatasample
@SensorID char(30) AS 

DECLARE 
    @sql NVARCHAR(MAX);

SET @sql = N'SELECT MAX(Timestamp) AS "Timestamp" FROM DATA WHERE SensorID = ''' + @SensorID + '''';

EXEC sp_executesql @sql;

