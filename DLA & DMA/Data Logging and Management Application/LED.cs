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


        /// <summary>
        /// Compares the LEDs stored threshold value up against the current values gathered by the sensors. 
        /// </summary>
        /// <returns>The state of the LED based on if a threshold has been exceeded.</returns>
        private async Task<bool> CheckThreshold()
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
                        if ((el["MaxThreshold"] == "True" && s.LastMeasurementValue > float.Parse(el["Threshold"])) ||
                            (el["MinThreshold"] == "True" && s.LastMeasurementValue < float.Parse(el["Threshold"])))
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


        /// <summary>
        /// Gets all of the stored LED-information from the local database.
        /// </summary>
        /// <returns>A list of dictionaries who store the row information.</returns>
        public static List<Dictionary<string, string>> GetStoredLEDInformation()
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>()
            {
                { "TableName", "LED" },
                { "NumberOfRows", "100" }
            };

            return dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "SelectAllFromTable", parameters));
        }


        /// <summary>
        /// Initiates the enabling/disabling of the LED light.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private async void LEDLightningState(Object source, ElapsedEventArgs e)
        {
            if (flashingLED)
            {
                stateOfLED = stateOfLED ? false : true;
            }
            else
            {
                stateOfLED = await CheckThreshold();
            }

            SetOutputPortState();
        }


        /// <summary>
        /// Sets the output voltage for the LEDs physical output port, base of the stateOfLED field. 
        /// </summary>
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


        /// <summary>
        /// Starts a new "thread" (timer) in which the DAQ communication is being held.
        /// </summary>
        public void StartNewChannelThread()
        {
            channelThread = new Timer(1500);
            channelThread.Elapsed += LEDLightningState;
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

            stateOfLED = false;
            SetOutputPortState();
        }


        /// <summary>
        /// Sends the new LED-state to the database via. SQL-query.
        /// </summary>
        /// <param name="newLEDState">The new boolean state of the LED.</param>
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
                                    { "Value5", el["ChanIdentifier"] },
                                    { "ColumnName", "ChanIdentifier" },
                                    { "SearchData", el["LED_ID"] }
                                };

                    dbm.CallProcedureWithoutReturn(dbm.DbName, "UpdateEntry5Columns", updateParameters);
                }
            }
        }
    }
}
