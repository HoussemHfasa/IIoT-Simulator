using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class Light_sensor:Sensor<int>
    {
        public int[] values;


        public override Dictionary<DateTime, int[]> Getvalue()
        {
            Dictionary<DateTime, int[]> Sensorvalues = new Dictionary<DateTime, int[]>();
            Sensorvalues.Add(CreationDate, values);
            return Sensorvalues;
        }
    }
}
