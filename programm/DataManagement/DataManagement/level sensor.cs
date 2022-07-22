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
    // Füllstandssensor
    public class LevelSensor:Sensor<double>
    {
        public LevelSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors
            this.Unit = "Füllstand in %";
            this.Sensortype = "Füllstandssensor";
            this.Sensor_id = IdGenerator.ToString();
        }
        public override ISensor<double> JsonDeserialize(string filepath, string Sensor_id)
        {
            ISensor<double> data = new LevelSensor();
            var serializer = new JsonSerializer();
            if (File.Exists(filepath+Sensor_id))
            {
                using (TextReader reader = File.OpenText(filepath+Sensor_id))
                {
                    data = (LevelSensor)serializer.Deserialize(reader, typeof(LevelSensor));
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
