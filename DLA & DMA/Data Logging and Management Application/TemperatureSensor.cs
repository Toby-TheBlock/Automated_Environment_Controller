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

            double Vout = voltage;
            double Vin = 5;
            double Ro = 330; // 10k Resistor
            double Rt = (Vout * Ro) / (Vin - Vout);

            double A = 0.001129148;
            double B = 0.000234125;
            double C = 0.0000000876741;

            double TempK = 1 / (A + (B * Math.Log(Rt)) + C * Math.Pow(Math.Log(Rt), 3));
            //Convert from Kelvin to Celsius
            double thermistorTempC = TempK - 273.15;


            Console.WriteLine(voltage + " " + thermistorTempC);
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
