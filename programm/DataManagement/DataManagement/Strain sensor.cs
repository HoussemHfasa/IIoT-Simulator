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
    //Dehnungssensor
    public class StrainSensor : Sensor<ushort>
    {
        public StrainSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors

            this.Unit = "Dehnung in µm/m";
            this.Sensortype = "Dehnungssensor";
            this.Sensor_id = IdGenerator.ToString();
        }

        public override ISensor<ushort> JsonDeserialize(string filepath, string Sensor_id)
        {
            ISensor<ushort> data = new StrainSensor();
            var serializer = new JsonSerializer();
            if (File.Exists(filepath+Sensor_id))
            {
                using (TextReader reader = File.OpenText(filepath+Sensor_id))
                {
                    data = (StrainSensor)serializer.Deserialize(reader, typeof(StrainSensor));
                }
            }
            return data;
        }

        public override void JsonSerialize(ISensor<ushort> data, string filepath)
        {
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(filepath))
            {
                serializer.Serialize(writer, data);
            }
        }
    }
}
