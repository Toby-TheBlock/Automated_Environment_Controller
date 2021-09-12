using System;
using System.Timers;

namespace Data_Logging_and_Management_Application
{
    class LightSensor : Sensor
    {

        public LightSensor(int sensorID, string modell, string producer, float voltageRating, int measureFrequency, string chanIdentifier)
        : base(sensorID, modell, producer, voltageRating, measureFrequency, chanIdentifier)
        {
            StartMeasuring();
        }

        protected override void GetData(Object source, ElapsedEventArgs e)
        {
            float rawVoltage = GetVoltageValue(ChanIdentifier, "lightSensorChannel", 0.0, VoltageRating);

            Console.WriteLine("The raw voltage over the light sensor is: " + rawVoltage);
            
            float lux = ConvertVoltageToLux(rawVoltage);

            Console.WriteLine("This voltage is equivalent to: " + lux + " lumen per 1 m^2.");

            UploadData(lux.ToString());
        }

        private float ConvertVoltageToLux(float voltage)
        {

            // https://emant.com/316002 

            float rs = 10000;
            float v_in = 5;

            float lux = (float)((500 * (v_in - voltage)) / (voltage * rs));

            return lux;
        }
    }
}
