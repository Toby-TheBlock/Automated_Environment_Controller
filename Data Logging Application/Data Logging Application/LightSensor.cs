using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Logging_Application
{
    class LightSensor : Sensor
    {

        public LightSensor()
        {

        }


        public override void GetData()
        {


            Console.WriteLine();
        }

        private double CovertVoltageToLux(double voltage)
        {

            // https://emant.com/316002 

            double rs = 10000;
            double v_in = 5.0;

            double lux = (500 * (v_in - voltage)) / (voltage * rs);

            return lux;
        }
    }
}
