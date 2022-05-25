using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
    class Voltage_sensor : Sensor<double>
    {
        public List<double> values;


        public override Dictionary<DateTime, List<double>> Getvalue()
        {
            Dictionary<DateTime, List<double>> Sensorvalues = new Dictionary<DateTime, List<double>>();
            Sensorvalues.Add(CreationDate, values);
            return Sensorvalues;
        }
    }
}
