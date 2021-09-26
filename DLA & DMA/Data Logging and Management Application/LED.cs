using System;
using System.Collections.Generic;
using NationalInstruments.DAQmx;
using System.Timers;
using System.Threading.Tasks;

namespace Data_Logging_and_Management_Application
{
    class LED : IDaqOutput
    {
        public static List<LED> allLEDs = new List<LED>();
        private static DatabaseManager dbm = DatabaseManager.Singleton;

        private int LED_ID;
        private bool stateOfLED;
        private bool flashingLED;
        private Timer channelThread; 

        public string OutputPort { get; set; }
        public bool StateOfLED { set { stateOfLED = value; } }

        public LED(int LED_ID, string chanIdentifier, bool flashingState = false)
        {
            this.LED_ID = LED_ID;
            stateOfLED = false;
            flashingLED = flashingState;
            OutputPort = chanIdentifier;
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

        public void SetOutputPortState()
        {
            try
            {
                NationalInstruments.DAQmx.Task digitalOutTask = new NationalInstruments.DAQmx.Task();

                digitalOutTask.DOChannels.CreateChannel(OutputPort, "LED" + LED_ID + "Channel", ChannelLineGrouping.OneChannelForEachLine);
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


        public void StartNewOutputThread()
        {
            channelThread = new Timer(500);
            channelThread.Elapsed += LEDLightningState;
            channelThread.AutoReset = true;
            channelThread.Enabled = true;
        }


        public void TerminateOutputThread()
        {
            channelThread.Stop();
            channelThread.Dispose();

            stateOfLED = false;
            SetOutputPortState();
        }


        private async void LEDLightningState(Object source, ElapsedEventArgs e)
        {
            //await CheckThresholds();

            if (flashingLED)
            {
                stateOfLED = stateOfLED ? false : true;
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

            List<Dictionary<string, string>> thresholdValues = dbm.ConvertDataTableToDictionary(dbm.CallProcedureWithReturn(dbm.DbName, "SelectSpecificFromTable", parameters));

            foreach (Dictionary<string, string> el in thresholdValues)
            {
                foreach (Sensor s in Sensor.allSensors)
                {
                    if (s.SensorID == int.Parse(el["SensorID"]))
                    {
                        Console.WriteLine(el["MinThreshold"]);
                    }
                }
                
            }

            return true;
        }

    }
}
