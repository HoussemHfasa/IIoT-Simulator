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
        // Unterordner Name
        public string Node { get; set; }
        //List von Sensoren und ihren Node
        //Feedback Rodner
        public Dictionary<string,List<string>> SensorIds { get; }
        
        //Methode Ordner erstellen
        //Feedback Rodner
        public void Create_Folder(string folderpath)
        {
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);

            }
        }
     

        public void AddBase(string BaseName)
        {
            string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\" +BaseName;
            Create_Folder(folderPath);
        }

        public void AddNode(string NodeName, string Basename)
        {
            string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\" + Basename+"\\"+NodeName;
            Create_Folder(folderPath);
        }

        public void DeleteNodeBase(string NodeName, string Basename)
        {
            string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\" + Basename + NodeName;
            Create_Folder(folderPath);
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
