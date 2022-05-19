using System;
using System.Collections.Generic;
using System.Text;
using CommonInterfaces;
using SensorAndSensorgroup;

namespace SensorAndSensorgroup
{
    public class Currentsensor : Sensor<double>
    {
        public double[] values;


        public override Dictionary<DateTime, double[]> Getvalue()
        {
            Dictionary<DateTime, double[]> Sensorvalues = new Dictionary<DateTime, double[]>();
            Sensorvalues.Add(CreationDate, values);
            return Sensorvalues;
        }
    }
}
