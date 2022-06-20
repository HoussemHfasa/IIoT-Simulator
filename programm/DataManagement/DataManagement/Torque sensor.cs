using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class TorqueSensor:Sensor<double>
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
