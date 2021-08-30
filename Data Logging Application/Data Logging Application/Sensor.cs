using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Logging_Application
{
    abstract class Sensor
    {

        private DatabaseManager dbm = DatabaseManager.Singleton;
        private int sensorID;
        private TimeSpan measureFrequency;

        public int SensorID { get; }

        public TimeSpan MeasureFrequency
        {
            get { return measureFrequency; }
            set { measureFrequency = value; }
        }

        public Sensor()
        {
            
        }

        // public abstract int GetData();

    }
}
