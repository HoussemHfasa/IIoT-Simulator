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
        string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SensorGroups\");
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
                TextWriter tw = new StreamWriter(folderpath+ Basename);
                tw.Close();

            }
        }
     

        public void AddBase(string BaseName)
        {
            if(!File.Exists(folderPath+BaseName))
            {
                Create_File(folderPath, BaseName);
            }
            
        }

        public void AddNode(string NodeName, string Basename)
        {
            Dictionary<string, List<string>> Sensorids = new Dictionary<string, List<string>>();
            if (File.Exists(Path.Combine(folderPath, Basename)))
            {
                Sensorids = store.LoadSensorgroup(Basename, folderPath);
                if (!Sensorids.ContainsKey(NodeName))
                {
                    Sensorids.Add(NodeName, new List<string> { });
                    store.SaveSensorgroup(Sensorids, Basename, folderPath);
                }
            }
        }
    

        public void DeleteNodeBase(string NodeName, string Basename)
        {
            Dictionary<string, List<string>> Sensorids = new Dictionary<string, List<string>>();
            Sensorids = store.LoadSensorgroup(Basename, folderPath);
            if (File.Exists(folderPath + Basename))
            {
                if (Sensorids.ContainsKey(NodeName))
                {
                    Sensorids.Remove(NodeName);
                }
                
                store.SaveSensorgroup(Sensorids, Basename, folderPath);
            }
          
        }

        public void Sensorhinzufuegen(string sensorid, string NodeName, string Basename)
        {
            Dictionary<string, List<string>> Sensorids = new Dictionary<string, List<string>>();
            Sensorids = store.LoadSensorgroup(Basename, folderPath);
            if ((File.Exists(folderPath + Basename))&& (Sensorids.ContainsKey(NodeName)))
            {
                if(!Sensorids[NodeName].Contains(sensorid))
                {
                    Sensorids[NodeName].Add(sensorid);
                }

                store.SaveSensorgroup(Sensorids, Basename, folderPath);
            }
        } 

        public void Sensorloeschen(string sensorid, string NodeName, string Basename)
        {
            Dictionary<string, List<string>> Sensorids = new Dictionary<string, List<string>>();
            Sensorids = store.LoadSensorgroup(Basename, folderPath);
            if ((File.Exists(folderPath + Basename)) && (Sensorids.ContainsKey(NodeName)))
            {
                
                if (Sensorids[NodeName].Contains(sensorid))
                {
                    Sensorids[NodeName].Remove(sensorid);
                }
                store.SaveSensorgroup(Sensorids, Basename, folderPath);
            }

        }
    }
}
