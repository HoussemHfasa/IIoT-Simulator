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
    public class VoltageSensor : Sensor<double>
    {
        public VoltageSensor() 
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors

            this.Unit = "Spannung in V";
            this.Sensortype = "Spannungssensor";
            this.Sensor_id = IdGenerator.ToString();
        }

    }
}
