using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace DataStorage
{

    public class DataStorage<T> : IDatastorage<T> 
    {
       
        // Speicherung der Sensorgroups Daten(Ids)
        public void SaveSensorgroup(Dictionary<string, List<string>> SensorListe, string Base, string FolderPath)
        {/*
            if (File.Exists(FolderPath + Base))
            {
                Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
                data = LoadSensorgroup(Base, FolderPath);
                File.Delete(FolderPath+Base);
                
                //Fehler
                    var serializer = new JsonSerializer();
                    using (TextWriter writer = File.CreateText(FolderPath + Base))
                    {
                        serializer.Serialize(writer, SensorListe);
                    
                }

            }
            else
            {*/
                var serializer = new JsonSerializer();
                using (TextWriter writer = File.CreateText(FolderPath + Base))
                {
                    serializer.Serialize(writer, SensorListe);
                }
            
        }

        // Ladung der Sensorgroups Daten(Ids)
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
            using (TextWriter writer = File.CreateText(filepath))
            {
                serializer.Serialize(writer, data);
            }
        }
        
        //Ladung der Brokerdaten
        public IBrokerProfile LoadBrokerProfile(string filepath)
        {

            IBrokerProfile data = new MQTTCommunicator.BrokerProfile();
            var serializer = new JsonSerializer();
            using (TextReader reader = File.OpenText(filepath))
            {
               data  = (IBrokerProfile)serializer.Deserialize(reader, typeof(IBrokerProfile));
            }
            
              
            return data;
        }

       
    }
}
