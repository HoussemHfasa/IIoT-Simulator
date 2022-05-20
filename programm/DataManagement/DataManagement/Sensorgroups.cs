using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;

namespace SensorAndSensorgroup
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
            string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\" +BaseName;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); 

            }
        }

        public void AddNode(string NodeName, string Basename)
        {
            string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\" + Basename+NodeName;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);

            }
        }

        public void DeleteNodeBase(string NodeName, string Basename)
        {
            string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\" + Basename + NodeName;
            Directory.Delete(folderPath);
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
