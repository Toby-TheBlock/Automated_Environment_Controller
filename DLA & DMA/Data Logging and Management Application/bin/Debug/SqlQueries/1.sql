USE Enviroment_Controller

-- ADDING DATA TO TABLES
INSERT SENSOR_TYPE (SensorType) VALUES ('Light level')
INSERT SENSOR_TYPE (SensorType) VALUES ('Temperature')

INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/ai0', 1)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/ai1', 1)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/ai2', 1)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/ai3', 1)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port0/line0', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port0/line1', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port0/line2', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port0/line3', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port0/line4', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port0/line5', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port0/line6', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port0/line7', 0)

INSERT SENSOR (SensorID, SensorType, VoltageRating, MeasureFrequency, ChanIdentifier) VALUES (0, 'Light level', 5.0, 20000, 'Dev1/ai0')
INSERT SENSOR (SensorID, SensorType, VoltageRating, MeasureFrequency, ChanIdentifier) VALUES (1, 'Temperature', 5.0, 30000, 'Dev1/ai1')
--INSERT SENSOR (SensorID, SensorType, VoltageRating, MeasureFrequency, ChanIdentifier) VALUES (2, 'Temperature', 5.0, '00.05.00.00', 'Dev1/ai2')

INSERT COLOR (Color) VALUES ('Green')
INSERT COLOR (Color) VALUES ('Red')
INSERT COLOR (Color) VALUES ('Yellow')

INSERT LED (LED_ID, Description, State, Color, ChanIdentifier) VALUES (0, 'Indicates if datalogging is in progress.', 0, 'Green', 'Dev1/port0/line0')
INSERT LED (LED_ID, Description, State, Color, ChanIdentifier) VALUES (1, 'Indicates if the plantlight is turned on.', 0, 'Green', 'Dev1/port0/line1')
INSERT LED (LED_ID, Description, State, Color, ChanIdentifier) VALUES (2, 'Indicates if the AC cooling is turned on.', 0, 'Yellow', 'Dev1/port0/line2')
INSERT LED (LED_ID, Description, State, Color, ChanIdentifier) VALUES (3, 'Indicates if the AC heating is turned on.', 0, 'Red', 'Dev1/port0/line3')

INSERT THRESHOLD_VALUE (Threshold, SensorID, MaxThreshold, MinThreshold, LED_ID) VALUES (30.0, 0, 0, 1, 1)
INSERT THRESHOLD_VALUE (Threshold, SensorID, MaxThreshold, MinThreshold, LED_ID) VALUES (50.0, 0, 0, 0, NULL)
INSERT THRESHOLD_VALUE (Threshold, SensorID, MaxThreshold, MinThreshold, LED_ID) VALUES (70.0, 0, 1, 0, NULL)
INSERT THRESHOLD_VALUE (Threshold, SensorID, MaxThreshold, MinThreshold, LED_ID) VALUES (16.0, 1, 0, 1, 3)
INSERT THRESHOLD_VALUE (Threshold, SensorID, MaxThreshold, MinThreshold, LED_ID) VALUES (23.0, 1, 1, 0, 2)

