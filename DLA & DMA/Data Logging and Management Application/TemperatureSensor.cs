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
                Console.WriteLine("test2");

                float rawVoltage = GetVoltageValue(ChanIdentifier, "lightSensorChannel", 0.0, VoltageRating);

                Console.WriteLine("The raw voltage over the light sensor is: " + rawVoltage);

                float temp = ConvertVoltageToTemp(rawVoltage);

                Console.WriteLine("This voltage is equivalent to: " + temp + " lumen per 1 m^2.");

                UploadData(temp.ToString());
            }
            catch
            {
                throw new Exception("Couldn't run data-collection! Ensure that the DAQ is connected to the PC.");
            }
            
        }

        private float ConvertVoltageToTemp(float voltage)
        {
            return voltage * 100 - 50;
        }

    }
}
