using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class FireSensor: Sensor<bool>
    {
        public FireSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors
            this.Unit = "Rauch vorhanden";
            this.Sensortype = "Rauchmelder";
            this.Sensor_id = IdGenerator.ToString();
        }
        
    }
}
