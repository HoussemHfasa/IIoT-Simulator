using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class Fire_sensor: Sensor<bool>
    {
        public bool[] values;


        public override Dictionary<DateTime, bool[]> Getvalue()
        {
            Dictionary<DateTime, bool[]> Sensorvalues = new Dictionary<DateTime, bool[]>();
            Sensorvalues.Add(CreationDate, values);
            return Sensorvalues;
        }
    }
}
