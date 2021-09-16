using System;
using System.Timers;

namespace Data_Logging_and_Management_Application
{
    class TemperatureSensor : Sensor
    {

        public TemperatureSensor(int sensorID, string modell, string producer, float voltageRating, int measureFrequency, string chanIdentifier)
        : base(sensorID, modell, producer, voltageRating, measureFrequency, chanIdentifier)
        {

        }

        public override void GetData()
        {
            Console.WriteLine("test2");

            float rawVoltage = GetVoltageValue(ChanIdentifier, "lightSensorChannel", 0.0, VoltageRating);

            Console.WriteLine("The raw voltage over the light sensor is: " + rawVoltage);

            float temp = ConvertVoltageToTemp(rawVoltage);

            Console.WriteLine("This voltage is equivalent to: " + temp + " lumen per 1 m^2.");

            UploadData(temp.ToString());
        }

        private float ConvertVoltageToTemp(float voltage)
        {
            return voltage * 100 - 50;
        }

    }
}
