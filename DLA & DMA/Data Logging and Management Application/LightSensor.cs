using System;

namespace Data_Logging_and_Management_Application
{
    class LightSensor : Sensor
    {

        public LightSensor(int sensorID, float voltageRating, int measureFrequency, string chanIdentifier)
        : base(sensorID, voltageRating, measureFrequency, chanIdentifier)
        {
            
        }


        private float ConvertVoltageToLux(float voltage)
        {
            Console.WriteLine(voltage);
            return (float)(voltage / 3.5) * 100;
        }


        /// <summary>
        /// Reads the raw voltage value currently present across the sensor-object, 
        /// converts it to the sensors specific target value, and sends that value to the database.
        /// </summary>
        public override void GetData()
        {
            try
            {
                AnalogReadingInProgress = true;

                float rawVoltage = GetAnalogValue(ChannelIdentifier, "lightSensorChannel", 0.0, voltageRating);

                System.Threading.Thread.Sleep(500);
                AnalogReadingInProgress = false;

                lastMeasurementValue = (float) Math.Round(ConvertVoltageToLux(rawVoltage), 1);
                UploadData(lastMeasurementValue.ToString());
            }
            catch (Exception e)
            {
                throw new Exception("Couldn't run data-collection! " + e.Message);
            }

        }
    }
}
