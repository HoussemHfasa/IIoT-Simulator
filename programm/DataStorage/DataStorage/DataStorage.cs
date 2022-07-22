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

    public class DataStorage : IDatastorage
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
       
            public void SaveTree(Dictionary<string, NAryTree> allTree, Dictionary<string, TreeNode> allchildren, Dictionary<string, dynamic> allsensor, List<string> basenames, Dictionary<string, int> basenames_children)
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
            foreach (TreeNode i in allchildren.Values)
            {
                i.child = null;
            }
            using (TextWriter writer = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "allchildren"))
            {
                serializer.Serialize(writer, allchildren);
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
            var serializer = new JsonSerializer();
            Dictionary<string, NAryTree> alltree = new Dictionary<string, NAryTree>();

            using (TextReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "alltree"))
            {
                alltree = ((Dictionary<string, NAryTree>)serializer.Deserialize(reader, typeof(Dictionary<string, NAryTree>)));
            }

            return alltree;
        }
        public Dictionary<string, TreeNode> Load_allchildren()
            {
                var serializer = new JsonSerializer();
                Dictionary<string, TreeNode> alltree = new Dictionary<string, TreeNode>();

                using (TextReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "allchildren"))
                {
                    alltree = ((Dictionary<string, TreeNode>)serializer.Deserialize(reader, typeof(Dictionary<string, TreeNode>)));
                }

                return alltree;
            }
           
        public Dictionary<string, int> Load_Basenames_children()
        {
            var serializer = new JsonSerializer();
            Dictionary<string, int> basenames = new Dictionary<string, int>();

            using (TextReader reader = File.OpenText(AppDomain.CurrentDomain.BaseDirectory + "basenames_children"))
            {
                basenames = ((Dictionary<string, int>)serializer.Deserialize(reader, typeof(Dictionary<string, int>)));
            }

            return basenames;
        }

       

    }
}
