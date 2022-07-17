using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SensorAndSensorgroup;



namespace DataStorage
{

    public class DataStorage<T> : IDatastorage<T> 
    {
       
        // Speicherung der Sensorgroups Daten(Nodename-Ids)
        public void SaveSensorgroup(Dictionary<string, List<string>> SensorListe, string Base, string FolderPath)
        {
                var serializer = new JsonSerializer();
                using (TextWriter writer = File.CreateText(FolderPath + Base))
                {
                    serializer.Serialize(writer, SensorListe);
                }
            
        }
       
        // Ladung der Sensorgroups Daten(Nodename-Ids)
        public Dictionary<string, List<string>> LoadSensorgroup(string Base, string FolderPath)
        {
            
                Dictionary<string, List<string>> data = new Dictionary<string, List<string>>(); ;
                var serializer = new JsonSerializer();
            if (File.Exists(FolderPath + Base))
            {
                using (TextReader reader = File.OpenText(FolderPath + Base))
                {
                    data = (Dictionary<string, List<string>>)serializer.Deserialize(reader, typeof(Dictionary<string, List<string>>));

                }
            }
                return data; 
            
        }
        
        //Speicherung der Brokerdaten
        public void SavebrokerProfile(IBrokerProfile data, string filepath)
        {
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(filepath+ "BrokerProfileTest"))
            {
                serializer.Serialize(writer, data);
            }
        }
        
        //Ladung der Brokerdaten
        public IBrokerProfile LoadBrokerProfile(string filepath)
        {

            IBrokerProfile data = new MQTTCommunicator.BrokerProfile();
            var serializer = new JsonSerializer();
            using (TextReader reader = File.OpenText(filepath+"BrokerProfileTest"))
            {
               data  = (MQTTCommunicator.BrokerProfile)serializer.Deserialize(reader, typeof(MQTTCommunicator.BrokerProfile));
            }
            
              
            return data;
        }
        //Ladung der Liste von Basename
        public List<string> BasenameDeserialize(string filepath)
        {
            List<string> data = new List<string>();
            var serializer = new JsonSerializer();
            if (File.Exists(filepath + "List of Basenames"))
            {
                using (TextReader reader = File.OpenText(filepath + "List of Basenames"))
                {
                    data = (List<string>)serializer.Deserialize(reader, typeof(List<string>));
                }
            }
            return data;
        }
        //Speicherung der Liste von Basename
        public void BasenamSerialize(List<string> data, string filepath)
        {
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(filepath + "List of Basenames"))
            {
                serializer.Serialize(writer, data);
            }
        }



        // Änderungen im Gruppenmeeting
        //die Methode Basename/Nodename/Sensor nicht geeignjet für unsere Programm ,da der Nutzer mehrere Unterordner erstellen kann
        public void SaveTree(List<TreeNode<T>.NAryTree> Trees, List<string> Basenames)
        {
            var serializer = new JsonSerializer();
            foreach (TreeNode<T>.NAryTree k in Trees)
            {
                using (TextWriter writer = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "Tree" + Trees.IndexOf(k)))
                {
                    serializer.Serialize(writer, k);
                }

            }
            using (TextWriter writer = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "Basenames"))
            {
                serializer.Serialize(writer, Basenames);
            }
        }
        public List<string> Loadbasenameliste(string Filepath)
        {
            var serializer = new JsonSerializer();
            List<string> basenemaesliste = new List<string>();
            using (TextReader reader = File.OpenText(Filepath))
            {
                basenemaesliste = (List<string>)serializer.Deserialize(reader, typeof(List<string>));
            }
            return basenemaesliste;
        }
        public List<TreeNode<T>.NAryTree> LoadTree(List<string> Basenameliste)
        {
            var serializer = new JsonSerializer();
            List<TreeNode<T>.NAryTree> Trees = new List<TreeNode<T>.NAryTree>();
            foreach (string k in Basenameliste)
            {
                using (TextReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "Tree" + Basenameliste.IndexOf(k)))
                {
                    Trees.Add((TreeNode<T>.NAryTree)serializer.Deserialize(reader, typeof(TreeNode<T>.NAryTree)));
                }
            }
            return Trees;
        }

    }
}
