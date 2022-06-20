using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;

namespace SensorAndSensorgroup
{
    public abstract class Sensor<T> : ISenor<T>
    {
        public string Sensor_id { get ; set ; }
        public string Sensortype { get ; set ; }
        
        public string Unit { get ; set; }


        public int AmmountofValues { get { return Values.Count;} }

        public int Timeinterval { get; set; }
        public string Topic { get; set; }

        // Sensordaten, Zugriff nur über GetValues und SetValues
        private List<T> Values;

        // gibt die Sensordaten zurück
        public List<T> GetValues()
        {
            return this.Values;
        }

     
        public void SetValues(List<T> Values)
        {
            this.Values = Values;
        }
    }
}

