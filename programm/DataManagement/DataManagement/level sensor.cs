using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    // Füllstandssensor
    class LevelSensor:Sensor<double>
    {
        public LevelSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors
            this.Unit = "Füllstand in %";
            this.Sensortype = "Füllstandssensor";
            this.Sensor_id = IdGenerator.ToString();
        }
    }
}
