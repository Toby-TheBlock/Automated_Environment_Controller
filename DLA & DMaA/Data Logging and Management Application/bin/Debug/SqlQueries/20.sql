-- ADDING A VIEW
CREATE VIEW GraphData AS SELECT 
D.Timestamp, D.MeasureValue, D.SensorID, S.SensorType
FROM DATA AS D
INNER JOIN SENSOR AS S ON (D.SensorID = S.SensorID)