using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;

namespace DataManagement
{
     public class Sensorgroups<T> : ISensorGroups<T> where T : ISenor<T>
    {
        //allgemeine Adresse für die Sensoren ,die in der Liste sind
        public string Adresse { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //List von Sensoren
        public string[] SensorIds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<List<object>> GroupDirectory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddBase(string BaseName)
        {
            throw new NotImplementedException();
        }

        public void AddNode(string NodeName, string[] NodeAdress)
        {
            throw new NotImplementedException();
        }

        public void DeleteNodeBase(string[] Adress)
        {
            throw new NotImplementedException();
        }

        public void Sensorhinzufuegen(string[] sensorids, string sensorid)
        {
            sensorids.Append(sensorid);
        }

        public void Sensorhinzufuegen(string Sensor)
        {
            throw new NotImplementedException();
        }


        public void Sensorloeschen(string sensorid)
        {
            throw new NotImplementedException();
        }
    }
}
