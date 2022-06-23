using System;
using System.Collections.Generic;
using CommonInterfaces;
using System.Text;
using Newtonsoft.Json;


namespace DummySensorandSensorgroups
{
    public abstract class Sensor<T> : ISensor<T>
    {

      
        public string Sensor_id { get; set; }

        public string Topic { get; set; }

        public string Sensortype { get; set; }

        public string Unit { get; set; }
        [JsonProperty]
        private List<double> Values;
        public int AmmountofValues
        {
            get { return Values.Count; }
        }

        public int Timeinterval { get; set; }



        public List<double> GetValues()
        {
            return Values;
        }

        public void SetValues(List<double> Values)
        {
            this.Values = Values;
        }

        List<T> ISensor<T>.GetValues()
        {
            throw new NotImplementedException();
        }

        public void SetValues(List<T> Values)
        {
            throw new NotImplementedException();
        }

        public abstract void JsonSerialize(ISensor<T> data, string filepath);


        public abstract ISensor<T> JsonDeserialize(string filepath, string Sensor_id);
       
    }
}
