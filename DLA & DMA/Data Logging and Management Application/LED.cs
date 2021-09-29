using System;
using System.Collections.Generic;
using NationalInstruments.DAQmx;
using System.Timers;
using System.Threading.Tasks;

namespace Data_Logging_and_Management_Application
{
    class LED : IDaqDigitalOutput
    {
        public static List<LED> allLEDs = new List<LED>();
        private static DatabaseManager dbm = DatabaseManager.Singleton;

        private int LED_ID;
        private bool stateOfLED;
        private bool flashingLED;
        private Timer channelThread; 

        public string ChannelIdentifier { get; set; }

        public bool StateOfLED { set { stateOfLED = value; } }

        public LED(int LED_ID, string chanIdentifier, bool flashingState = false)
        {
            this.LED_ID = LED_ID;
            stateOfLED = false;
            flashingLED = flashingState;
            ChannelIdentifier = chanIdentifier;
        }

        public static List<Dictionary<string, string>> GetStoredLEDInformation()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "TableName", "LED" },
                { "NumberOfRows", "100" }
            };

            return dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "SelectAllFromTable", parameters));
        }

        private void UpdateStoredLEDState(bool newLEDState)
        {
            foreach (Dictionary<string, string> el in GetStoredLEDInformation())
            {
                if (int.Parse(el["LED_ID"]) == LED_ID)
                {
                    Dictionary<string, string> updateParameters = new Dictionary<string, string>()
                                {
                                    { "TableName", "LED" },
                                    { "ColumnName1", "LED_ID" },
                                    { "Value1", el["LED_ID"] },
                                    { "ColumnName2", "Description" },
                                    { "Value2", el["Description"] },
                                    { "ColumnName3", "State" },
                                    { "Value3", newLEDState.ToString() },
                                    { "ColumnName4", "Color" },
                                    { "Value4", el["Color"] },
                                    { "ColumnName5", "ChanIdentifier" },
                                    { "Value5", el["ChanIdentifier"] }
                                };

                    dbm.CallProcedureWithoutReturn(dbm.DbName, "UpdateEntry5Columns", updateParameters);
                }
            }
        }


        public void SetOutputPortState()
        {
            try
            {
                NationalInstruments.DAQmx.Task digitalOutTask = new NationalInstruments.DAQmx.Task();

                digitalOutTask.DOChannels.CreateChannel(ChannelIdentifier, "LED" + LED_ID + "Channel", ChannelLineGrouping.OneChannelForEachLine);
                digitalOutTask.Control(TaskAction.Verify);

                DigitalSingleChannelWriter writer = new DigitalSingleChannelWriter(digitalOutTask.Stream);
                writer.WriteSingleSampleSingleLine(true, stateOfLED);
                digitalOutTask.Stop();
            } 
            catch (Exception e)
            {
                Console.WriteLine("Couldn't update the output channel! " + e.Message);
            }     
        }


        public void StartNewChannelThread()
        {
            channelThread = new Timer(500);
            channelThread.Elapsed += LEDLightningState;
            channelThread.AutoReset = true;
            channelThread.Enabled = true;
        }


        public void TerminateChannelThread()
        {
            channelThread.Stop();
            channelThread.Dispose();

            stateOfLED = false;
            SetOutputPortState();
        }


        private async void LEDLightningState(Object source, ElapsedEventArgs e)
        {
            if (flashingLED)
            {
                stateOfLED = stateOfLED ? false : true;
            } 
            else
            {
                stateOfLED = await CheckThresholds();
            }

            SetOutputPortState();
        }


        private async Task<bool> CheckThresholds()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "TableName", "THRESHOLD_VALUE" },
                { "ColumnName", "LED_ID" },
                { "SearchData", LED_ID.ToString() }
            };

            bool newState = false;
            List<Dictionary<string, string>> thresholdValues = dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "SelectSpecificFromTable", parameters));

            foreach (Dictionary<string, string> el in thresholdValues)
            {
                foreach (Sensor s in Sensor.allSensors)
                {
                    if (s.SensorID == int.Parse(el["SensorID"]))
                    {
                        if ((el["MaxThreshold"] == "true" && s.LastMeasurementValue > float.Parse(el["Threshold"])) || 
                            (el["MinThreshold"] == "true" && s.LastMeasurementValue < float.Parse(el["Threshold"])))
                        {
                            newState = true;
                        }
                    }
                }
            }

            if (stateOfLED != newState)
            {
                UpdateStoredLEDState(newState);
            }

            return newState;
        }

    }
}
