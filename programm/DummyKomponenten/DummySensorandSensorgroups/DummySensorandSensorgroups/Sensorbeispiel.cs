using System;
using System.Collections.Generic;
using System.Text;
using CommonInterfaces;

namespace DummySensorandSensorgroups
{
    public class Sensorbeispiel:Sensor<double>
    {
        public Sensorbeispiel()
        {
            // Sensor wird durch Konstruktor mit Beispieldaten befüllt.
            // Kann durch set jeweils nochmal verändert werden
            Sensortype = "Temperatursensor";
            Unit = "Temperatur in °C";
            Sensor_id = "6552778f";
            Topic = "Haus/Wohnzimmer/TemperaturSensor";
            Timeinterval = 5;
            SetValues(new List<double> { 154, 848, 79549, 95.4, 4185.48 });
        }
        
        public override ISensor<double> JsonDeserialize(string filepath, string Sensor_id)
        {
            ISensor<double> Test = new Sensorbeispiel();
            return Test;
        }

        public override void JsonSerialize(ISensor<double> data, string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
