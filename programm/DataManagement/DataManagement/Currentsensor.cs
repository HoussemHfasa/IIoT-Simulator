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
    public class CurrentSensor : Sensor<double>
    {
       public CurrentSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors
            this.Unit = "Strom in A";
            this.Sensortype = "Stromsensor";
            this.Sensor_id = IdGenerator.ToString();
        }
        
    }
}
