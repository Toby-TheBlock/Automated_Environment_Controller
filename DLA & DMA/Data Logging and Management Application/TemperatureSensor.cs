using System;
using System.Timers;

namespace Data_Logging_and_Management_Application
{
    class TemperatureSensor : Sensor
    {

        public TemperatureSensor(int sensorID, float voltageRating, int measureFrequency, string chanIdentifier)
        : base(sensorID, voltageRating, measureFrequency, chanIdentifier)
        {

        }

        public override void GetData()
        {
            try
            {
                AnalogReadingInProgress = true;

                float rawVoltage = GetAnalogValue(ChannelIdentifier, "tempSensorChannel", 0.0, VoltageRating);

                System.Threading.Thread.Sleep(100);
                AnalogReadingInProgress = false;

                Console.WriteLine("The raw voltage over the temperature sensor is: " + rawVoltage);

                lastMeasurementValue = (float) Math.Round(ConvertVoltageToTemp(rawVoltage), 1);

                Console.WriteLine("This voltage is equivalent to: " + lastMeasurementValue + " degrees celcius.");

                UploadData(lastMeasurementValue.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Couldn't run data-collection! Ensure that the DAQ is connected to the PC.");
            }
            
        }

        private float ConvertVoltageToTemp(float voltage)
        {
            return voltage * 100 - 50;
        }

    }
}
