using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;

namespace SensorAndSensorgroup
{
    public class Sensorgroups : ISensorGroups
    {
        //allgemeine Adresse für die Sensoren ,die in der Liste sind
        public string Adresse { get; set; }
        //List von Sensoren und ihren Type
        public Dictionary<string,string> SensorIds { get; }
        // Unterordner Name
        public string Node { get; set; }
        
     

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
            string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\" + Basename+"\\"+NodeName;
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);

            }
        }

        public void DeleteNodeBase(string NodeName, string Basename)
        {
            string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\" + Basename + NodeName;
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath);
            }
        }

        public void Sensorhinzufuegen(List<string> sensorids, string sensorid)
        {
            sensorids.Append(sensorid);
        } 

        public void Sensorloeschen(List<string> sensorids,string sensorid)
        {
            sensorids.Remove(sensorid);
        }
    }
}
