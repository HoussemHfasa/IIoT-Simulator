using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class TemperatureSensor:Sensor<double>
    {
        public TemperatureSensor()
        {
            Guid IdGenerator = Guid.NewGuid();
            // Besonderheiten des Sensors

            this.Unit = "Temperatur in °C";
            this.Sensortype = "Temperatursensor";
            this.Sensor_id = IdGenerator.ToString();
        }
        
    }
}
