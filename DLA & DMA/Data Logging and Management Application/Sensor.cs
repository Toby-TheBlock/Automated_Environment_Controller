using System;
using System.Collections.Generic;
using NationalInstruments.DAQmx;
using System.Timers;
using System.Data;

namespace Data_Logging_and_Management_Application
{
    abstract class Sensor
    {
        public static List<Sensor> allSensors = new List<Sensor>();
        protected static DatabaseManager dbm = DatabaseManager.Singleton;

        private int sensorID;
        private float voltageRating;
        private int measureFrequency;
        private string chanIdentifier;
        private Timer measuringTimer;
        private string lastMeasurementTimestamp;
        protected float lastMeasurementValue;

        public int SensorID 
        {
            get { return sensorID; } 
        }

        public string LastMeasurementTimestamp
        {
            get { return lastMeasurementTimestamp; }
        }

        public float LastMeasurementValue
        {
            get { return lastMeasurementValue; }
        }

        protected float VoltageRating 
        {
            get { return voltageRating; }
        }

        protected int MeasureFrequency
        {
            get { return measureFrequency; }
            set { measureFrequency = value; }
        }

        protected string ChanIdentifier 
        {
            get { return chanIdentifier; }
        }

        public Sensor(int sensorID, float voltageRating, int measureFrequency, string chanIdentifier)
        {
            this.sensorID = sensorID;
            this.voltageRating = voltageRating;
            this.measureFrequency = measureFrequency;
            this.chanIdentifier = chanIdentifier;

            measuringTimer = new Timer(measureFrequency);
            measuringTimer.Elapsed += RunMeasurements;
            measuringTimer.Enabled = true;

            Dictionary<string, string> parameters = new Dictionary<string, string>() { { "SensorID", sensorID.ToString() } };
            string data = dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "GetLastDatasample", parameters))[0]["Timestamp"];

            lastMeasurementTimestamp = data.Length > 0 ? data : "N/A";
        }

        public static List<Dictionary<string, string>> GetStoredSensorInformation()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "TableName", "SENSOR" },
                { "NumberOfRows", "100" }
            };

            return dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "SelectAllFromTable", parameters));
        }

        public string GetNextMeasuringTimestamp()
        {
            return !lastMeasurementTimestamp.Contains("N/A") ? Convert.ToDateTime(lastMeasurementTimestamp).AddSeconds(measureFrequency).ToString() : lastMeasurementTimestamp;
        }


        protected void RunMeasurements(Object source, ElapsedEventArgs e)
        {
            GetData();
        }

        public virtual void GetData() {}
        
        protected void UploadData(string data)
        {
            lastMeasurementTimestamp = DateTime.Now.ToString();

            Dictionary<string, string> parameters = new Dictionary<string, string>() 
            { 
                { "TableName", "DATA" }, 
                { "ColumnName1", "Timestamp" },
                { "Value1", lastMeasurementTimestamp},
                { "ColumnName2", "Value" },
                { "Value2", data},
                { "ColumnName3", "SensorID" },
                { "Value3", sensorID.ToString()}
            };

            dbm.CallProcedureWithoutReturn(dbm.DbName, "InsertIntoTable3Columns", parameters);
        }

        protected float GetVoltageValue(string pChanName, string gChanName, double minVolt, double maxVolt)
        {
            Task analogInTask = new Task();
            analogInTask.AIChannels.CreateVoltageChannel(pChanName, gChanName, AITerminalConfiguration.Rse, minVolt, maxVolt, AIVoltageUnits.Volts);
            AnalogSingleChannelReader reader = new AnalogSingleChannelReader(analogInTask.Stream);
            double daqValue = reader.ReadSingleSample();

            analogInTask.Stop();

            return (float) daqValue;
        }



    }
}
