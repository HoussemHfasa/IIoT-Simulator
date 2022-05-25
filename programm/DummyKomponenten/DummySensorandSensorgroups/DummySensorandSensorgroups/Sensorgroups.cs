using System;
using System.Collections.Generic;
using CommonInterfaces;
namespace SensorAndSensorgroups
{
    public class SensorGroups : ISensorGroups<double> 
    {
        public string Adresse { get { return this.Adresse; } set { this.Adresse = "funktioniert"; } }
        public string[] SensorIds
        {
            get { return this.SensorIds; }
            set
            {
                string[] test = { "funktioniert" };
                this.SensorIds = test;

            }
        }

        public string Node { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Dictionary<string, string> ISensorGroups<T>.SensorIds => throw new NotImplementedException();

        Dictionary<string, string> ISensorGroups<double>.SensorIds => throw new NotImplementedException();

        public void AddBase(string BaseName)
        {
            throw new NotImplementedException();
        }

        public void AddNode(string NodeName, string Basename)
        {
            throw new NotImplementedException();
        }

        public void DeleteNodeBase(string NodeName, string Basename)
        {
            throw new NotImplementedException();
        }

        public void Sensorhinzufuegen()
        {
            throw new NotImplementedException();
        }

        public void Sensorhinzufuegen(List<string> sensorids, string sensorid)
        {
            throw new NotImplementedException();
        }

        public void Sensorloeschen()
        {
            throw new NotImplementedException();
        }

        public void Sensorloeschen(List<string> sensorids, string sensorid)
        {
            throw new NotImplementedException();
        }
    }
}

