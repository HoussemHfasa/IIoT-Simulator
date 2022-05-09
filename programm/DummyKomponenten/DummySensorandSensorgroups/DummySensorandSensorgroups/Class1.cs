using System;
using CommonInterfaces;

namespace DummySensorandSensorgroups
{
    public class Sensorgroup : ISensorGroups
    {
        public string Adresse { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string[] SensorIds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Sensorhinzufuegen()
        {
            throw new NotImplementedException();
        }

        public void Sensorloeschen()
        {
            throw new NotImplementedException();
        }
    }
}
