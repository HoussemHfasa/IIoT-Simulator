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
        public string Einheit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime CreationDate { get; }

        public TimeSpan CreationTime { get; }

        public int Werteanzahl { get; }

        public int Timeinterval { get; }

        public List<T> Getvalues()
        {
            throw new NotImplementedException();
        }
        public abstract Dictionary<DateTime, List<T>> Getvalue();
        
    }
}

