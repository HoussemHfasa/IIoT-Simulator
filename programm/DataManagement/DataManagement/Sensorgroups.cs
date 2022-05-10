using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;

namespace DataManagement
{
     public class Sensorgroups : ISensorGroups
    {
        //allgemeine Adresse für die Sensoren ,die in der Liste sind
        public string Adresse { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //List von Sensoren
        public string[] SensorIds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public void Sensorhinzufuegen(string[] sensorids, string sensorid)
        {
            sensorids.Append(sensorid);
        }

        public void Sensorloeschen(string[] sensorids, string sensorid)
        {
            if (sensorids.Contains(sensorid))
            {

            }
        }
    }
}
