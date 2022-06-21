using System;
using System.Collections.Generic;
using System.Text;
using CommonInterfaces;

namespace DummySensorandSensorgroups
{
    public class Sensor : ISenor<double>
    {

        public Sensor()
        {
            // Sensor wird durch Konstruktor mit Beispieldaten befüllt.
            // Kann durch set jeweils nochmal verändert werden
            Sensortype = "Temperatursensor";
            Unit = "Temperatur in °C";
            Sensor_id = "6552778f";
            Topic = "Haus/Wohnzimmer/TemperaturSensor";
            Timeinterval = 5;
            Values = new List<double> { 154, 848, 79549, 95.4, 4185.48 };
        }
        public string Sensor_id { get; set; }

        public string Topic { get; set; }

        public string Sensortype { get; set; }

        public string Unit { get; set; }

        private List<double> Values;
        public int AmmountofValues
        {
            get { return Values.Count; }
        }

        public int Timeinterval { get; set; }



        public List<double> GetValues()
        {
            return Values;
        }

        public void SetValues(List<double> Values)
        {
            this.Values = Values;
        }
    }
}
