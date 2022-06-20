using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class VoltageSensor : Sensor<double>
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
