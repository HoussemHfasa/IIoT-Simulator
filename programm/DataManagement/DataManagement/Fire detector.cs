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
    public class firedetector : Sensor<bool>
    {
        public firedetector()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors
            this.Unit = "Rauch vorhanden";
            this.Sensortype = "Rauchmelder";
            this.Sensor_id = IdGenerator.ToString();
        }

       
    }
}
