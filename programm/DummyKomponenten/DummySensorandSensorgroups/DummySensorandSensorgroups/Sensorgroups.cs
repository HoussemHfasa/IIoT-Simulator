using System;
using System.Collections.Generic;
using CommonInterfaces;
namespace SensorAndSensorgroups
{
    public class SensorGroups : ISensorGroups
    {
        public string Adresse { get { return this.Adresse; } set { this.Adresse = "funktioniert"; } }
     

        public string Node { get { return this.Node; } set { this.Adresse = "funktioniert"; } }

        public Dictionary<string, List<string>> SensorIds
        {
            get { return this.SensorIds; }
        }
        
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

