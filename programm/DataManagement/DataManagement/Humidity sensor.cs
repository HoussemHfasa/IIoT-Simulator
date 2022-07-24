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
    public class HumiditySensor:Sensor<double>
    {
        public HumiditySensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors
            this.Unit = "rel. Luftfeuchtigkeit in Vol.-%";
            this.Sensortype = "Feuchtigkeitssensor";
            this.Sensor_id = IdGenerator.ToString();
        }

        
    }
}
