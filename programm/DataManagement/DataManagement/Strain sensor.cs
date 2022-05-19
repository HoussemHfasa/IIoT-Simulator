using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class Strain_sensor: Sensor<ushort>
    {
        public ushort[] values;


        public override Dictionary<DateTime, ushort[]> Getvalue()
        {
            Dictionary<DateTime, ushort[]> Sensorvalues = new Dictionary<DateTime, ushort[]>();
            Sensorvalues.Add(CreationDate, values);
            return Sensorvalues;
        }
    }
}
