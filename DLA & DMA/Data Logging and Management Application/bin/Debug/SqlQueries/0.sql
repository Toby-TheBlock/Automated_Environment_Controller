USE Enviroment_Controller

--SETTING UP THE TABLE STRUCTURE
CREATE TABLE USER_ROLE
( 
	RoleID               int  NOT NULL ,
	Description          char(150)  NULL ,
	AccessLevel          int  NOT NULL ,
	RoleName             char(20)  NULL ,
	PRIMARY KEY  CLUSTERED (RoleID ASC)
)

CREATE TABLE USER_ACCOUNT
( 
	Username             char(15)  NOT NULL ,
	Password             char(20)  NOT NULL ,
	RoleID               int  NOT NULL ,
	PRIMARY KEY  CLUSTERED (Username ASC),
	 FOREIGN KEY (RoleID) REFERENCES USER_ROLE(RoleID)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)

CREATE TABLE SENSOR_TYPE
( 
	SensorType           char(30)  NOT NULL ,
	PRIMARY KEY  CLUSTERED (SensorType ASC)
)

CREATE TABLE DAQ_CHANNEL
( 
	ChanIdentifier       char(20)  NOT NULL ,
	InputChannel         bit  NOT NULL ,
	PRIMARY KEY  CLUSTERED (ChanIdentifier ASC)
)

CREATE TABLE SENSOR
( 
	SensorID             int  NOT NULL ,
	SensorType           char(30)  NOT NULL ,
	VoltageRating        float  NULL ,
	MeasureFrequency     int  NOT NULL ,
	ChanIdentifier       char(20)  NOT NULL ,
	PRIMARY KEY  CLUSTERED (SensorID ASC),
	 FOREIGN KEY (SensorType) REFERENCES SENSOR_TYPE(SensorType)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	 FOREIGN KEY (ChanIdentifier) REFERENCES DAQ_CHANNEL(ChanIdentifier)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)

CREATE TABLE THRESHOLD_VALUE
( 
	MaxThreshold         bit  NOT NULL ,
	MinThreshold         bit  NOT NULL ,
	Threshold            float  NOT NULL ,
	SensorID             int  NOT NULL ,
	PRIMARY KEY  CLUSTERED (Threshold ASC,SensorID ASC),
	 FOREIGN KEY (SensorID) REFERENCES SENSOR(SensorID)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)

CREATE TABLE COLOR
( 
	Color                char(15)  NOT NULL ,
	PRIMARY KEY  CLUSTERED (Color ASC)
)

CREATE TABLE LED
( 
	LED_ID               int  NOT NULL ,
	Description          char(150)  NULL ,
	State                bit  NOT NULL ,
	Color                char(15)  NOT NULL ,
	ChanIdentifier       char(20)  NOT NULL ,
	PRIMARY KEY  CLUSTERED (LED_ID ASC),
	 FOREIGN KEY (Color) REFERENCES COLOR(Color)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION,
	 FOREIGN KEY (ChanIdentifier) REFERENCES DAQ_CHANNEL(ChanIdentifier)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)

CREATE TABLE DATA
( 
	Timestamp            datetime  NOT NULL ,
	MeasureValue         float  NOT NULL ,
	SensorID             int  NOT NULL ,
	PRIMARY KEY  CLUSTERED (Timestamp ASC,SensorID ASC),
	 FOREIGN KEY (SensorID) REFERENCES SENSOR(SensorID)
		ON DELETE NO ACTION
		ON UPDATE NO ACTION
)


