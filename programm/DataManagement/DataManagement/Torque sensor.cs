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
    public class TorqueSensor:Sensor<double>
    {
        public TorqueSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors

            this.Unit = "Drehmoment in Nm";
            this.Sensortype = "Drehmomentsensor";
            this.Sensor_id = IdGenerator.ToString();
        }

    
    }
}
