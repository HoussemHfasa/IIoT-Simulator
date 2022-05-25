using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class Fire_sensor: Sensor<bool>
    {
        public List<bool> values;


        public override Dictionary<DateTime, List<bool>> Getvalue()
        {
            Dictionary<DateTime, List<bool>> Sensorvalues = new Dictionary<DateTime, List<bool>>();
            Sensorvalues.Add(CreationDate, values);
            return Sensorvalues;
        }
    }
}
