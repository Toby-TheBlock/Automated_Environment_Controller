USE Environment_Controller

--SETTING UP THE TABLE STRUCTURE
CREATE TABLE SENSOR_TYPE
( 
	SensorType           char(30)  NOT NULL ,
	CONSTRAINT XPKSensorType PRIMARY KEY  CLUSTERED (SensorType ASC)
)

CREATE TABLE DAQ_CHANNEL
( 
	ChanIdentifier       char(50)  NOT NULL ,
	InputChannel         bit  NOT NULL ,
	CONSTRAINT XPKANALOG_INPUT PRIMARY KEY  CLUSTERED (ChanIdentifier ASC)
)

CREATE TABLE SENSOR
( 
	SensorID             int  NOT NULL ,
	SensorType           char(30)  NOT NULL ,
	VoltageRating        float  NULL ,
	MeasureFrequency     int  NOT NULL ,
	ChanIdentifier       char(50)  NOT NULL ,
	CONSTRAINT XPKSensor PRIMARY KEY  CLUSTERED (SensorID ASC),
	CONSTRAINT R_1 FOREIGN KEY (SensorType) REFERENCES SENSOR_TYPE(SensorType)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT R_7 FOREIGN KEY (ChanIdentifier) REFERENCES DAQ_CHANNEL(ChanIdentifier)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)

CREATE TABLE COLOR
( 
	Color                char(15)  NOT NULL ,
	CONSTRAINT XPKCOLOR PRIMARY KEY  CLUSTERED (Color ASC)
)

CREATE TABLE LED
( 
	LED_ID               int  NOT NULL ,
	Description          char(150)  NULL ,
	State                bit  NOT NULL ,
	Color                char(15)  NOT NULL ,
	ChanIdentifier       char(50)  NOT NULL ,
	CONSTRAINT XPKLED PRIMARY KEY  CLUSTERED (LED_ID ASC),
	CONSTRAINT R_6 FOREIGN KEY (Color) REFERENCES COLOR(Color)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT R_8 FOREIGN KEY (ChanIdentifier) REFERENCES DAQ_CHANNEL(ChanIdentifier)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)

CREATE TABLE THRESHOLD_VALUE
( 
	MaxThreshold         bit  NOT NULL ,
	MinThreshold         bit  NOT NULL ,
	Threshold            float  NOT NULL ,
	SensorID             int  NOT NULL ,
	LED_ID               int  NULL ,
	CONSTRAINT XPKThresholdValue PRIMARY KEY  CLUSTERED (Threshold ASC,SensorID ASC),
	CONSTRAINT R_3 FOREIGN KEY (SensorID) REFERENCES SENSOR(SensorID)
		ON DELETE CASCADE
		ON UPDATE CASCADE,
	CONSTRAINT R_9 FOREIGN KEY (LED_ID) REFERENCES LED(LED_ID)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)

CREATE TABLE DATA
( 
	Timestamp            char(20) NOT NULL ,
	MeasureValue         float  NOT NULL ,
	SensorID             int  NOT NULL ,
	CONSTRAINT XPKData PRIMARY KEY  CLUSTERED (Timestamp ASC,SensorID ASC),
	CONSTRAINT R_5 FOREIGN KEY (SensorID) REFERENCES SENSOR(SensorID)
		ON DELETE CASCADE
		ON UPDATE CASCADE
)