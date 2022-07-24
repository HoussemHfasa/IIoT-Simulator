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

         void JsonSerialize(string Folderpath, string Filename, string Sensorgroupname, dynamic Data)
        {
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(Path.Combine(Folderpath + Sensorgroupname + @"\" + Filename)))
            {
                serializer.Serialize(writer, Data);
            }
        }
        public void SaveTree(Sensorgroups Sensorgroup,string Filepath)
            {
            string Folderpath=(Filepath.Remove(Filepath.LastIndexOf("\\") + 1));
            string Sensorgroupname= Filepath.Remove(0, Filepath.LastIndexOf("\\") + 1);
            if (!Directory.Exists(Folderpath + Sensorgroupname))
            {
                Directory.CreateDirectory(Folderpath + Sensorgroupname);
            }
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(Path.Combine(Folderpath + @"\Load " + Sensorgroupname + " From Here")))
            {
                serializer.Serialize(writer, Path.Combine(Folderpath,Sensorgroupname));
            }
            foreach (TreeNode i in Sensorgroup.allchildren.Values)
            {
                i.child = null;
            }
            Sensorgroup.Sensorgroupname = Sensorgroupname;
            JsonSerialize(Folderpath, "alltree", Sensorgroupname, Sensorgroup.allTree);
            JsonSerialize(Folderpath, "allchildren", Sensorgroupname, Sensorgroup.allchildren);
            JsonSerialize(Folderpath, "Basenames", Sensorgroupname, Sensorgroup.basenames);
            JsonSerialize(Folderpath, "basenames_children", Sensorgroupname, Sensorgroup.basenames_children);
            JsonSerialize(Folderpath, "Sensorgroupname", Sensorgroupname, Sensorgroup.Sensorgroupname);
            
        }
             List<string> Load_Basenames(string Filepath)
            {
                var serializer = new JsonSerializer();
                List<string> basenames = new List<string>();

                using (TextReader reader = File.OpenText(Filepath + "Basenames"))
                {
                    basenames = ((List<string>)serializer.Deserialize(reader, typeof(List<string>)));
                }

                return basenames;
            }
         Dictionary<string, NAryTree> Load_alltree(string Filepath)
        {
            var serializer = new JsonSerializer();
            Dictionary<string, NAryTree> alltree = new Dictionary<string, NAryTree>();

            using (TextReader reader = File.OpenText(Filepath + "alltree"))
            {
                alltree = ((Dictionary<string, NAryTree>)serializer.Deserialize(reader, typeof(Dictionary<string, NAryTree>)));
            }

            return alltree;
        }
         Dictionary<string, TreeNode> Load_allchildren(string Filepath)
            {
                var serializer = new JsonSerializer();
                Dictionary<string, TreeNode> alltree = new Dictionary<string, TreeNode>();

                using (TextReader reader = File.OpenText(Filepath + "allchildren"))
                {
                    alltree = ((Dictionary<string, TreeNode>)serializer.Deserialize(reader, typeof(Dictionary<string, TreeNode>)));
                }

                return alltree;
            }
           
         Dictionary<string, int> Load_Basenames_children(string Filepath)
        {
            var serializer = new JsonSerializer();
            Dictionary<string, int> basenames = new Dictionary<string, int>();

            using (TextReader reader = File.OpenText(Filepath + "basenames_children"))
            {
                basenames = ((Dictionary<string, int>)serializer.Deserialize(reader, typeof(Dictionary<string, int>)));
            }

            return basenames;
        }
        string Load_Sensorgroupname(string Filepath)
        {
            var serializer = new JsonSerializer();
            string basenames;

            using (TextReader reader = File.OpenText(Filepath + "Sensorgroupname"))
            {
                basenames = ((string)serializer.Deserialize(reader, typeof(string)));
            }

            return basenames;
        }
        public Sensorgroups LoadTree(string Filepath)
        {
            Sensorgroups sensorgroup = new Sensorgroups();
            var serializer = new JsonSerializer();
            string Path;
            using (TextReader reader = File.OpenText(Filepath))
            {
                Path = ((string)serializer.Deserialize(reader, typeof(string)));
            }
            sensorgroup.allTree = Load_alltree(Path);
            sensorgroup.allchildren = Load_allchildren(Path);
            sensorgroup.basenames = Load_Basenames(Path);
            sensorgroup.basenames_children = Load_Basenames_children(Path);
            sensorgroup.Sensorgroupname = Load_Sensorgroupname(Path);
            return sensorgroup;
             
        }
       

    }
}
