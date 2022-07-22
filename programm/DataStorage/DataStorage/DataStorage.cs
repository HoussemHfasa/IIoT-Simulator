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
<<<<<<< HEAD
       
            public void Save(Dictionary<string, NAryTree> allTree, Dictionary<string, Node> allchildren, Dictionary<string, dynamic> allsensor, List<string> basenames, Dictionary<string, int> basenames_children)
            {
                var serializer = new JsonSerializer();
                using (TextWriter writer = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "basenames_children"))
                {
                    serializer.Serialize(writer, basenames_children);
                }

                using (TextWriter writer = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "Basenames"))
                {
                    serializer.Serialize(writer, basenames);
                }
                using (TextWriter writer = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "alltree"))
                {
                    serializer.Serialize(writer, allTree);
                }
                using (TextWriter writer = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "allchildren"))
                {
                    serializer.Serialize(writer, allchildren);
                }
                using (TextWriter writer = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "allsensor"))
                {
                    serializer.Serialize(writer, allsensor);
                }
            }
            public List<string> Load_Basenames()
            {
                var serializer = new JsonSerializer();
                List<string> basenames = new List<string>();

                using (TextReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "Basenames"))
                {
                    basenames = ((List<string>)serializer.Deserialize(reader, typeof(List<string>)));
                }

                return basenames;
            }
        public Dictionary<string, NAryTree> Load_alltree()
        {
            Dictionary<string, NAryTree> allTree = new Dictionary<string, NAryTree>();
            Dictionary<string, Node> allchildren = new Dictionary<string, Node>();
            Dictionary<string, dynamic> allsensor = new Dictionary<string, dynamic>();
            Dictionary<string, int> basenames_children = new Dictionary<string, int>();
            List<string> basenames = new List<string>();
            allchildren = Load_allchildren();
            allsensor = Load_allsensor();
            basenames = Load_Basenames();
            Sensorgroups Loading = new Sensorgroups();
            foreach (string k in basenames)
            {
                Loading.Add_new_Base(k);
            }
            foreach (Node k in allchildren.Values)
            {
                Loading.Add_new_Node(k.Mothername, k.name);
            }
            foreach (dynamic k in allsensor.Values)
            {
                Loading.Add_new_sensor(k.Mothername, k);
            }
            return Loading.allTree;
        }
        public Dictionary<string, Node> Load_allchildren()
            {
                var serializer = new JsonSerializer();
                Dictionary<string, Node> alltree = new Dictionary<string, Node>();

                using (TextReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "allchildren"))
                {
                    alltree = ((Dictionary<string, Node>)serializer.Deserialize(reader, typeof(Dictionary<string, Node>)));
                }

                return alltree;
            }
            public Dictionary<string, dynamic> Load_allsensor()
            {
                var serializer = new JsonSerializer();
                Dictionary<string, dynamic> alltree = new Dictionary<string, dynamic>();

                using (TextReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "allsensor"))
                {
                    alltree = ((Dictionary<string, dynamic>)serializer.Deserialize(reader, typeof(Dictionary<string, dynamic>)));
                }

                return alltree;
            

        }
        public Dictionary<string, int> Load_Basenames_children()
=======
        public void SaveTree(List<TreeNode<T>.NAryTree> Trees, List<string> Basenames)
>>>>>>> parent of 4e757bc (Gruppen Meeting TreeStructure bearbeitet)
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
