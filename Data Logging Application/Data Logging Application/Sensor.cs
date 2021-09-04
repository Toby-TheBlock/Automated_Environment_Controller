using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.DAQmx;

namespace Data_Logging_Application
{
    abstract class Sensor
    {

        protected static DatabaseManager dbm = DatabaseManager.Singleton;

        private int sensorID;
        private string modell;
        private string producer;
        private float voltageRating;
        private TimeSpan measureFrequency;
        private string chanIdentifier;
        private float currentMeasurement = 0;
        private Dictionary<string, string> thresholdValues;

        protected int SensorID { get; }

        protected float VoltageRating { get; }

        protected TimeSpan MeasureFrequency
        {
            get { return measureFrequency; }
            set { measureFrequency = value; }
        }

        protected string ChanIdentifier { get; }

        public Sensor(int sensorID, string modell, string producer, float voltageRating, TimeSpan measureFrequency, string chanIdentifier)
        {
            this.sensorID = sensorID;
            this.modell = modell;
            this.producer = producer;
            this.voltageRating = voltageRating;
            this.measureFrequency = measureFrequency;
            this.chanIdentifier = chanIdentifier;
        }

        public Dictionary<string, string> CreateParameterDic(int dicSize)
        {
            Dictionary<string, string> parameterDic = new Dictionary<string, string>() { {"SensorID", sensorID.ToString() } };

            switch (dicSize)
            {
                case 3:
                    parameterDic.Add("Timestamp", DateTime.Now.ToString());
                    parameterDic.Add("MeasureValue", currentMeasurement.ToString());
                    break;
                case 4:
                    parameterDic.Add("Threshold", );
                    parameterDic.Add("MinThreshold", "0");
                    parameterDic.Add("MaxThreshold", "0");
                    break;
                default:
                    throw new ArgumentException("The provided parameter dictionary size has not been defined!");
            }

            return parameterDic;
        }

        public abstract void GetData();

        protected double GetVoltageValue(string pChanName, string gChanName, double minVolt, double maxVolt)
        {
            Task analogInTask = new Task();
            analogInTask.AIChannels.CreateVoltageChannel(pChanName, gChanName, AITerminalConfiguration.Rse, minVolt, maxVolt, AIVoltageUnits.Volts);
            AnalogSingleChannelReader reader = new AnalogSingleChannelReader(analogInTask.Stream);
            double daqValue = reader.ReadSingleSample();

            return daqValue;
        }



    }
}
