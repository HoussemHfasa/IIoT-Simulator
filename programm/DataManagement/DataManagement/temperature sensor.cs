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
    public class TemperatureSensor:Sensor<double>
    {
        public TemperatureSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors

            this.Unit = "Temperatur in °C";
            this.Sensortype = "Temperatursensor";
            this.Sensor_id = IdGenerator.ToString();
        }

      
    }
}
