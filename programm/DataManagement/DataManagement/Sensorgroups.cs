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
    public  class Sensorgroups : ISensorGroups
    {
        DataStorage<string> store = new DataStorage<string>();
        //allgemeine Adresse für die Sensoren ,die in der Liste sind
        public string Base { get; set; }
        // Unterordner Name
        public string Node { get; set; }
        //Ordnerpfad von die Sensorgruppen gespeichert
        string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SensorGroups\");

        //List von Sensoren und ihren Node
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
        public void Create_File(string folderpath,string Basename)
        {
            if (!Directory.Exists(Path.Combine(folderpath,Basename)))
            {
                TextWriter tw = new StreamWriter(folderpath+ Basename);
                tw.Close();

            }
        }

        // allgemeine Adresse für die Sensoren hinzufügen
        public void AddBase(string BaseName)
        {
            List<string> List_of_Basename = new List<string>();
            List_of_Basename = store.BasenameDeserialize(folderPath );
            if (!File.Exists(folderPath + "List of Basenames"))
            {
                Create_File(folderPath, "List of Basenames");
            }
              if (!File.Exists(folderPath+BaseName))
            {
                Create_File(folderPath, BaseName);
            }
              if(!List_of_Basename.Contains(BaseName))
            {
                List_of_Basename.Add(BaseName);
            }
            
            store.BasenamSerialize(List_of_Basename,folderPath);
            
            
        }
        // allgemeine Adresse für die Sensoren loeschen
        public void DeleteBase(string BaseName)
        {
            List<string> List_of_Basename = new List<string>();
            List_of_Basename = store.BasenameDeserialize(folderPath );
            if (File.Exists(folderPath + BaseName))
            {
                File.Delete(folderPath + BaseName);
                List_of_Basename.Remove(BaseName);
            }
            store.BasenamSerialize(List_of_Basename, folderPath);
        }
        // Unterordner hinzufügen unter die Adresse
        public void AddNode(string NodeName, string Basename)
        {
            Dictionary<string, List<string>> Sensorids = new Dictionary<string, List<string>>();
            if (File.Exists(Path.Combine(folderPath, Basename)))
            {
                if(!(store.LoadSensorgroup(Basename, folderPath)==null))
                {
                    Sensorids = store.LoadSensorgroup(Basename, folderPath);
                }
                
                if (!Sensorids.ContainsKey(NodeName))
                {
                    Sensorids.Add(NodeName, new List<string> { });
                    store.SaveSensorgroup(Sensorids, Basename, folderPath);
                }
            }
        }

        // Unterordner Löschen
        public void DeleteNode(string NodeName, string Basename)
        {
            Dictionary<string, List<string>> Sensorids = new Dictionary<string, List<string>>();
            if (!(store.LoadSensorgroup(Basename, folderPath) == null))
            {
                Sensorids = store.LoadSensorgroup(Basename, folderPath);
            }
            if (File.Exists(folderPath + Basename))
            {
                if (Sensorids.ContainsKey(NodeName))
                {
                    Sensorids.Remove(NodeName);
                }
                
                store.SaveSensorgroup(Sensorids, Basename, folderPath);
            }
          
        }
        // Sensor in eimen bestimmten Adresse und Unterordner hinzufügen
        public void Sensorhinzufuegen(string sensorid, string NodeName, string Basename)
        {
            Dictionary<string, List<string>> Sensorids = new Dictionary<string, List<string>>();
            if (!(store.LoadSensorgroup(Basename, folderPath) == null))
            {
                Sensorids = store.LoadSensorgroup(Basename, folderPath);
            }
            if ((File.Exists(folderPath + Basename))&& (Sensorids.Keys.Contains(NodeName)))
            {
                if(!Sensorids[NodeName].Contains(sensorid))
                {
                    Sensorids[NodeName].Add(sensorid);
                }

                store.SaveSensorgroup(Sensorids, Basename, folderPath);
            }
        }
        // Sensor von eimen bestimmten Adresse und Unterordner löschen
        public void Sensorloeschen(string sensorid, string NodeName, string Basename)
        {
            Dictionary<string, List<string>> Sensorids = new Dictionary<string, List<string>>();
            if (!(store.LoadSensorgroup(Basename, folderPath) == null))
            {
                Sensorids = store.LoadSensorgroup(Basename, folderPath);
            }
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
