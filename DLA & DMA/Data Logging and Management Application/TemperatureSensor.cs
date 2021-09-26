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
                float rawVoltage = GetVoltageValue(ChanIdentifier, "tempSensorChannel", 0.0, VoltageRating);

                Console.WriteLine("The raw voltage over the temperature sensor is: " + rawVoltage);

                lastMeasurementValue = ConvertVoltageToTemp(rawVoltage);

                Console.WriteLine("This voltage is equivalent to: " + lastMeasurementValue + " degrees celcius.");

                //UploadData(temp.ToString());
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
