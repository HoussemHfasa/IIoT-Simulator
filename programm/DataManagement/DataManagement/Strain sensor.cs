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
    public class StrainSensor : Sensor<double>
    {
        public StrainSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors

            this.Unit = "Dehnung in µm/m";
            this.Sensortype = "Dehnungssensor";
            this.Sensor_id = IdGenerator.ToString();
        }

       
    }
}
