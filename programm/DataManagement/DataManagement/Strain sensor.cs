using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class Strain_sensor: Sensor<ushort>
    {
        public List<ushort> values;


        public override Dictionary<DateTime, List<ushort>> Getvalue()
        {
            Dictionary<DateTime, List<ushort>> Sensorvalues = new Dictionary<DateTime, List<ushort>>();
            Sensorvalues.Add(CreationDate, values);
            return Sensorvalues;
        }
    }
}
