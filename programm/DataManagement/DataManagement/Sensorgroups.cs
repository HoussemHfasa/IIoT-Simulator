using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using DataStorage;

namespace SensorAndSensorgroup
{
    public class Sensorgroups : ISensorGroups
    {
        //allgemeine Adresse für die Sensoren ,die in der Liste sind
        public string Base { get; set; }
        // Unterordner Name
        public string Node { get; set; }
        string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SensorGroups");
        //List von Sensoren und ihren Node
        //Feedback Rodner
        DataStorage<string> store = new DataStorage<string>();
        public Dictionary<string,List<string>> SensorIds 
        { 
            get
            {
                return this.SensorIds;
            }
            set
            {
               
                this.SensorIds = store.LoadSensorgroup(Base,folderPath);
            }
            }
        
        //Methode Ordner erstellen
        //Feedback Rodner
        public void Create_File(string folderpath,string Basename)
        {
            if (!Directory.Exists(Path.Combine(folderpath,Basename)))
            {
                TextWriter tw = new StreamWriter(Path.Combine(folderpath, Basename));
                tw.Close();

            }
        }
     

        public void AddBase(string BaseName)
        {
          //  string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\NunitTestDatamanagement\bin\Debug\netcoreapp3.1\SensorGroups";
            
            Create_File(folderPath, BaseName);
        }

        public void AddNode(string NodeName, string Basename)
        {
            
            if (!File.Exists(Path.Combine(folderPath,Basename)))
            {
                AddBase(Basename);
            }
            Dictionary<string, List<string>> Sensorids = store.LoadSensorgroup(Basename, folderPath);
            Sensorids.Add(NodeName,new List<string> { });
            store.SaveSensorgroup(Sensorids,Basename,folderPath);
        }

        public void DeleteNodeBase(string NodeName, string Basename)
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Basename);
           // Create_File(folderPath);
            store.SaveSensorgroup(SensorIds, Basename,folderPath);
        }

        public void Sensorhinzufuegen(string sensorid, string NodeName, string Basename)
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Basename);
            SensorIds[NodeName].Add(sensorid);
            store.SaveSensorgroup(SensorIds,Basename,folderPath);
        } 

        public void Sensorloeschen(string sensorid, string NodeName, string Basename)
        {
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Basename);
            SensorIds[NodeName].Remove(sensorid);
            store.SaveSensorgroup(SensorIds, Basename,folderPath);
        }
    }
}
