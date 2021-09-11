using System;
using System.Collections.Generic;
using NationalInstruments.DAQmx;
using System.Timers;

namespace Data_Logging_and_Management_Application
{
    abstract class Sensor
    {

        protected static DatabaseManager dbm = DatabaseManager.Singleton;

        private int sensorID;
        private string modell;
        private string producer;
        private float voltageRating;
        private int measureFrequency;
        private string chanIdentifier;
        private Timer measuringTimer;

        protected int SensorID 
        {
            get { return sensorID; } 
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

        protected Timer MeasuringTimer
        {
            get { return measuringTimer; }
            set { measuringTimer = value; }
        }

        public Sensor(int sensorID, string modell, string producer, float voltageRating, int measureFrequency, string chanIdentifier)
        {
            this.sensorID = sensorID;
            this.modell = modell;
            this.producer = producer;
            this.voltageRating = voltageRating;
            this.measureFrequency = measureFrequency;
            this.chanIdentifier = chanIdentifier;
        }
        
        protected abstract void StartMeasuring();
        
        
        protected abstract void GetData(Object source, ElapsedEventArgs e);

        
        protected void UploadData(string data)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>() 
            { 
                { "TableName", "DATA" }, 
                { "ColumnName1", "Timestamp" },
                { "Value1", DateTime.Now.ToString()},
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

            return (float) daqValue;
        }



    }
}
