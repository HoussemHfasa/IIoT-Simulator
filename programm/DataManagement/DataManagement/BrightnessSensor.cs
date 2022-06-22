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
    public class BrightnessSensor:Sensor<int>
    {
        public BrightnessSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors
            
            this.Unit = "Lichtmenge in lm";
            this.Sensortype = "Helligkeitssensor";
            this.Sensor_id = IdGenerator.ToString();
        }

        public override ISensor<int> JsonDeserialize(string filepath, string Sensor_id)
        {
            ISensor<int> data = new BrightnessSensor();
            var serializer = new JsonSerializer();
            if (File.Exists(filepath+Sensor_id))
            {
                using (TextReader reader = File.OpenText(filepath+Sensor_id))
                {
                    data = (BrightnessSensor)serializer.Deserialize(reader, typeof(BrightnessSensor));
                }
            }
            return data;
        }

        public override void JsonSerialize(ISensor<int> data, string filepath)
        {
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(filepath+data.Sensor_id))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}
