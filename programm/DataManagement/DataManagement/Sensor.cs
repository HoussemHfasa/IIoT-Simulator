using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using Newtonsoft.Json;

namespace SensorAndSensorgroup
{
    public abstract class Sensor<T> : ISensor<T>
    {
        public string Sensor_id { get ; set ; }
        public string Sensortype { get ; set ; }
        
        public string Unit { get ; set; }

        public List<int> path = new List<int> { };
        public int childnumber = 0;
        public string Mothername;
        public int AmmountofValues { get { return Values.Count;} }

        public int Timeinterval { get; set; }
        public string Topic { get; set; }

        // Sensordaten, Zugriff nur über GetValues und SetValues
        //JsonProperty für den Zugriff zu Jsonserialization
        [JsonProperty]
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
        //Ladung des SensorProperties
        public abstract ISensor<T> JsonDeserialize(string filepath, string Sensor_id);
        //Speicherung des SensorProperties in JsonDatei
        public abstract void JsonSerialize(ISensor<T> data, string filepath);

     
    }
}

