using System;

namespace Data_Logging_and_Management_Application
{
    class TemperatureSensor : Sensor
    {

        public TemperatureSensor(int sensorID, float voltageRating, int measureFrequency, string chanIdentifier)
        : base(sensorID, voltageRating, measureFrequency, chanIdentifier)
        {

        }


        private float ConvertVoltageToTemp(float voltage)
        {
            return (float)(voltage * 10 - Math.Round(voltage, 1));
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

                float rawVoltage = GetAnalogValue(ChannelIdentifier, "tempSensorChannel", 0.0, voltageRating);

                System.Threading.Thread.Sleep(500);
                AnalogReadingInProgress = false;

                lastMeasurementValue = (float) Math.Round(ConvertVoltageToTemp(rawVoltage), 1);
                UploadData(lastMeasurementValue.ToString());

            }
            catch (Exception e)
            {
                throw new Exception("Couldn't run data-collection! " + e.Message);
            }
            
        }
    }
}
