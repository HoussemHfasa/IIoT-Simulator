using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class BrightnessSensor:Sensor<int>
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
