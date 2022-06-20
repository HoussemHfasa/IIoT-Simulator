using System;
using System.Collections.Generic;
using System.Text;
using CommonInterfaces;

namespace DummySensorandSensorgroups
{
    public class Sensor : ISenor<double>
    {
        public string Sensor_id { get { return Sensor_id; } set { this.Sensor_id = "6552778f"; } }

        public string Sensortype { get { return Sensortype; } set { this.Sensor_id = "Tempreturesensor"; } }

        public string Einheit { get { return Einheit; } set { this.Sensor_id = "mm"; } }

        DateTime ISenor<double>.CreationDate
        {
            get { return CreationDate; }
        }

        TimeSpan ISenor<double>.CreationTime
        {
            get { return CreationTime; }
        }

        int ISenor<double>.Werteanzahl
        {
            get { return Werteanzahl; }
        }

        int ISenor<double>.Timeinterval
        {
            get { return Timeinterval; }
        }

        public DateTime CreationDate = DateTime.Today;

        public TimeSpan CreationTime = TimeSpan.Zero;

        public int Werteanzahl =20;

        public int Timeinterval =5;

        public List<double> Getvalues()
        {
            return new List<double> { 154, 848, 79549, 95.4, 4185.48 };
        }
    }
}
