USE Enviroment_Controller

-- ADDING DATA TO TABLES
INSERT USER_ROLE (RoleID, RoleName, Description, AccessLevel) VALUES (0, 'User', 'Normal user which can access the data monitoring.', 1)
INSERT USER_ROLE (RoleID, RoleName, Description, AccessLevel) VALUES (1, 'Admin', 'Elevated user who can access the data monitoring and change the sensor configuration values.', 2)
INSERT USER_ROLE (RoleID, RoleName, Description, AccessLevel) VALUES (2, 'SuperAdmin', 'Top level user who can access and change everything.', 3)

INSERT USER_ACCOUNT (Username, Password, RoleID) VALUES ('Tobias', 'slippmeginn123', 2)
INSERT USER_ACCOUNT (Username, Password, RoleID) VALUES ('Testbruker', 'slippmeginn123', 1)

INSERT SENSOR_TYPE (SensorType) VALUES ('Light level')
INSERT SENSOR_TYPE (SensorType) VALUES ('Temperature')

INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/ai0', 1)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/ai1', 1)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/ai2', 1)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/ai3', 1)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port0', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port1', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port2', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port3', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port4', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port5', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port6', 0)
INSERT DAQ_CHANNEL (ChanIdentifier, InputChannel) VALUES ('Dev1/port7', 0)

INSERT SENSOR (SensorID, SensorType, VoltageRating, MeasureFrequency, ChanIdentifier) VALUES (0, 'Light level', 5.0, 60000, 'Dev1/ai0')
INSERT SENSOR (SensorID, SensorType, VoltageRating, MeasureFrequency, ChanIdentifier) VALUES (1, 'Temperature', 5.0, 300000, 'Dev1/ai1')
--INSERT SENSOR (SensorID, SensorType, VoltageRating, MeasureFrequency, ChanIdentifier) VALUES (2, 'Temperature', 5.0, '00.05.00.00', 'Dev1/ai2')

INSERT THRESHOLD_VALUE (Threshold, SensorID, MaxThreshold, MinThreshold) VALUES (2.0, 0, 0, 1)
INSERT THRESHOLD_VALUE (Threshold, SensorID, MaxThreshold, MinThreshold) VALUES (3.0, 0, 0, 0)
INSERT THRESHOLD_VALUE (Threshold, SensorID, MaxThreshold, MinThreshold) VALUES (4.0, 0, 1, 0)
INSERT THRESHOLD_VALUE (Threshold, SensorID, MaxThreshold, MinThreshold) VALUES (2.0, 1, 0, 1)
INSERT THRESHOLD_VALUE (Threshold, SensorID, MaxThreshold, MinThreshold) VALUES (4.0, 1, 1, 0)

INSERT COLOR (Color) VALUES ('Green')
INSERT COLOR (Color) VALUES ('Red')
INSERT COLOR (Color) VALUES ('Blue')

INSERT LED (LED_ID, Description, State, Color, ChanIdentifier) VALUES (0, 'Indicates if the plantlight is turned on.', 0, 'Green', 'Dev1/port0')
INSERT LED (LED_ID, Description, State, Color, ChanIdentifier) VALUES (1, 'Indicates if the AC cooling is turned on.', 0, 'Red', 'Dev1/port1')
INSERT LED (LED_ID, Description, State, Color, ChanIdentifier) VALUES (2, 'Indicates if the AC heating is turned on.', 0, 'Blue', 'Dev1/port2')

