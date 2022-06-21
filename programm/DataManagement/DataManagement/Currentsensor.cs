using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SensorAndSensorgroup
{
    public class CurrentSensor : Sensor<double>
    {
       public CurrentSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors
            this.Unit = "Strom in A";
            this.Sensortype = "Stromsensor";
            this.Sensor_id = IdGenerator.ToString();
        }
        public override ISensor<double> JsonDeserialize(string filepath)
        {
            ISensor<double> data = new CurrentSensor();
            var serializer = new JsonSerializer();
            if (File.Exists(filepath))
            {
                using (TextReader reader = File.OpenText(filepath))
                {
                    data = (CurrentSensor)serializer.Deserialize(reader, typeof(CurrentSensor));
                }
            }
            return data;
        }

        public override void JsonSerialize(ISensor<double> data, string filepath)
        {
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(filepath))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}
