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
    class HumiditySensor:Sensor<double>
    {
        public HumiditySensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors
            this.Unit = "rel. Luftfeuchtigkeit in Vol.-%";
            this.Sensortype = "Feuchtigkeitssensor";
            this.Sensor_id = IdGenerator.ToString();
        }

        public override ISensor<double> JsonDeserialize(string filepath, string Sensor_id)
        {
            ISensor<double> data = new HumiditySensor();
            var serializer = new JsonSerializer();
            if (File.Exists(filepath+Sensor_id))
            {
                using (TextReader reader = File.OpenText(filepath+Sensor_id))
                {
                    data = (HumiditySensor)serializer.Deserialize(reader, typeof(HumiditySensor));
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
