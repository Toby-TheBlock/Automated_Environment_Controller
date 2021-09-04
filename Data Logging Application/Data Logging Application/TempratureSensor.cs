using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data_Logging_Application
{
    class TempratureSensor : Sensor
    {


        public override void GetData()
        {

            Console.WriteLine();
        }

        private double CovertVoltageToTemp(double voltage)
        {
            return voltage * 100 - 50;
        }

    }
}
