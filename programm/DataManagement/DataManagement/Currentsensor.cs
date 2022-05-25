using System;
using System.Collections.Generic;
using System.Text;
using CommonInterfaces;
using SensorAndSensorgroup;

namespace SensorAndSensorgroup
{
    public class Currentsensor : Sensor<double>
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
