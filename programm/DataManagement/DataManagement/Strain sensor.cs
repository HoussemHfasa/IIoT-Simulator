using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    //Dehnungssensor
    class StrainSensor : Sensor<ushort>
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
