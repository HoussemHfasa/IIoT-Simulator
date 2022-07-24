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
    public class BrightnessSensor:Sensor<double>
    {
        public BrightnessSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors
            
            this.Unit = "Lichtmenge in lm";
            this.Sensortype = "Helligkeitssensor";
            this.Sensor_id = IdGenerator.ToString();
        }

       
    }
}
