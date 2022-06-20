using System;
using System.Collections.Generic;
using System.Text;
using CommonInterfaces;

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
        
 
    }
}
