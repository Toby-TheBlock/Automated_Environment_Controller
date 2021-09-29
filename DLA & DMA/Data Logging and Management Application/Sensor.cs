using System;
using System.Collections.Generic;
using NationalInstruments.DAQmx;
using System.Timers;

namespace Data_Logging_and_Management_Application
{
    abstract class Sensor : IDaqAnalogInput
    {
        public static List<Sensor> allSensors = new List<Sensor>();
        protected static DatabaseManager dbm = DatabaseManager.Singleton;

        private int sensorID;
        private float voltageRating;
        private int measureFrequency;
        private Timer channelThread;
        private string lastMeasurementTimestamp;
        protected float lastMeasurementValue;

        public string ChannelIdentifier { get; set; }

        public bool AnalogReadingInProgress { get; set; }

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

        public Sensor(int sensorID, float voltageRating, int measureFrequency, string chanIdentifier)
        {
            this.sensorID = sensorID;
            this.voltageRating = voltageRating;
            this.measureFrequency = measureFrequency;
            ChannelIdentifier = chanIdentifier;

            StartNewChannelThread();

            Dictionary<string, string> parameters = new Dictionary<string, string>() { { "SensorID", sensorID.ToString() } };
            string data = dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "GetLastDatasample", parameters))[0]["Timestamp"];

            lastMeasurementTimestamp = data.Length > 0 ? data : "N/A";
        }


        /// <summary>
        /// Accesses a specific analog channel on the DAQ, and gets the voltage value of said channel. 
        /// </summary>
        /// <param name="pChanName">The actual channel name/identifier provided by the DAQ.</param>
        /// <param name="gChanName">A self specific name for the voltage channel</param>
        /// <param name="minVolt">Minimum voltage to use for the analog-channel.</param>
        /// <param name="maxVolt">Maximum voltage to use for the analog-channel</param>
        /// <returns>The raw voltage value of the analog channel.</returns>
        public float GetAnalogValue(string pChanName, string gChanName, double minVolt, double maxVolt)
        {
            Task analogInTask = new Task();
            analogInTask.AIChannels.CreateVoltageChannel(pChanName, gChanName, AITerminalConfiguration.Rse, minVolt, maxVolt, AIVoltageUnits.Volts);
            AnalogSingleChannelReader reader = new AnalogSingleChannelReader(analogInTask.Stream);
            double daqValue = reader.ReadSingleSample();

            analogInTask.Stop();

            return (float)daqValue;
        }


        public virtual void GetData() {}


        /// <summary>
        /// Calculates when the next scheduled measurement will happen, based on the sensors measure frequency.
        /// </summary>
        /// <returns>Timestamp of the next measurement.</returns>
        public string GetNextMeasuringTimestamp()
        {
            return !lastMeasurementTimestamp.Contains("N/A") ? Convert.ToDateTime(lastMeasurementTimestamp).AddSeconds(measureFrequency).ToString() : lastMeasurementTimestamp;
        }


        /// <summary>
        /// Gets all of the stored sensor information from the database.
        /// </summary>
        /// <returns>A list containing all of the attribute names (column names) and corresponding values.</returns>
        public static List<Dictionary<string, string>> GetStoredSensorInformation()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "TableName", "SENSOR" },
                { "NumberOfRows", "100" }
            };

            return dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "SelectAllFromTable", parameters));
        }


        /// <summary>
        /// Initiates the data gathering from a analog channel, if no other analog channels currently are being read from.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void RunMeasurements(Object source, ElapsedEventArgs e)
        {
            while (AnalogReadingInProgress)
            {
                System.Threading.Thread.Sleep(500);
            }

            GetData();
        }


        /// <summary>
        /// Starts a new "thread" (timer) in which the DAQ communication is being held.
        /// </summary>
        public void StartNewChannelThread()
        {
            channelThread = new Timer(measureFrequency);
            channelThread.Elapsed += RunMeasurements;
            channelThread.AutoReset = true;
            channelThread.Enabled = true;
        }


        /// <summary>
        /// Stops the "thread" (timer) in which the DAQ communication is being held.
        /// </summary>
        public void TerminateChannelThread()
        {
            channelThread.Stop();
            channelThread.Dispose();
        }


        protected void UploadData(string data)
        {
            lastMeasurementTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            Dictionary<string, string> parameters = new Dictionary<string, string>() 
            { 
                { "TableName", "DATA" }, 
                { "ColumnName1", "Timestamp" },
                { "Value1", lastMeasurementTimestamp },
                { "ColumnName2", "MeasureValue" },
                { "Value2", "f@" + data},
                { "ColumnName3", "SensorID" },
                { "Value3", sensorID.ToString()}
            };

            dbm.CallProcedureWithoutReturn(dbm.DbName, "InsertIntoTable3Columns", parameters);
        }
    }
}
