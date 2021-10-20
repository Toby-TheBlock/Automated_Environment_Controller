-- ADDING A VIEW
CREATE VIEW CurrentDataAndThreshold AS SELECT 
T.Threshold, T.MinThreshold, T.MaxThreshold, T.SensorID, D.Timestamp, D.MeasureValue, S.SensorType
FROM THRESHOLD_VALUE AS T
INNER JOIN (
	SELECT D.SensorID, MAX(D.Timestamp) AS 'Timestamp' 
	FROM DATA AS D
	GROUP BY D.SensorID
) SUBQUERY ON T.SensorID = SUBQUERY.SensorID 
INNER JOIN DATA AS D ON (D.Timestamp = SUBQUERY.Timestamp)
INNEr JOIN SENSOR AS S ON (T.SensorID = S.SensorID)