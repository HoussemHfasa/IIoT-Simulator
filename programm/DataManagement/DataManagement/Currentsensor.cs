using System;
using System.Collections.Generic;
using System.Text;
using CommonInterfaces;
using SensorAndSensorgroup;

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
