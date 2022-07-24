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

    }
}
