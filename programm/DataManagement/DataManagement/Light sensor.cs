using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class Light_sensor:Sensor<int>
    {
        public List<int> values;


        public override Dictionary<DateTime, List<int>> Getvalue()
        {
            Dictionary<DateTime, List<int>> Sensorvalues = new Dictionary<DateTime, List<int>>();
            Sensorvalues.Add(CreationDate, values);
            return Sensorvalues;
        }
    }
}
