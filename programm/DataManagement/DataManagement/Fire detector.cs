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
    class firedetector : Sensor<bool>
    {
        public firedetector()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors
            this.Unit = "Rauch vorhanden";
            this.Sensortype = "Rauchmelder";
            this.Sensor_id = IdGenerator.ToString();
        }

        public override ISensor<bool> JsonDeserialize(string filepath, string Sensor_id)
        {
            ISensor<bool> data = new firedetector();
            var serializer = new JsonSerializer();
            if (File.Exists(filepath+Sensor_id))
            {
                using (TextReader reader = File.OpenText(filepath+Sensor_id))
                {
                    data = (firedetector)serializer.Deserialize(reader, typeof(firedetector));
                }
            }
            return data;
        }

        public override void JsonSerialize(ISensor<bool> data, string filepath)
        {
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(filepath))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}
