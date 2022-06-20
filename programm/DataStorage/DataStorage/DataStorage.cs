﻿using System;
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
        // Die Daten von den Sensor kommen
        public Dictionary<DateTime,List<T>> Data { get; set; }
       //Methode verifiziert die gegebene Data ist schon gespeichert oder nicht
       public void Verfizierung( Dictionary<DateTime, List<T>> Olddata, Dictionary<DateTime, List<T>> NewData)
        {
            foreach (DateTime key in NewData.Keys)
            {
                if (!Olddata.ContainsKey(key))
                {
                    Olddata.Add(key, NewData[key]);
                }
            }
        }
        // Sensor Zurückgeben
        // Speicherung der SensorDaten in der Dateipfad
        /*
        public void JsonSerialize(Dictionary<DateTime, List<T>> data, string filepath,string Sensortype)
        {
            //Ladung der vorhandenen Daten
            Dictionary<DateTime, List<T>> already_data = new Dictionary<DateTime, List<T>>();
            if(File.Exists(filepath+Sensortype))
            {
                already_data = JsonDeserialize(filepath, Sensortype);
            }
            Verfizierung(already_data, data);
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(filepath + Sensortype))
            {
                serializer.Serialize(writer, already_data);
            }

        }*/

        // Ladung der SensorDaten von der Dateipfad
        public Dictionary<DateTime, List<T>> JsonDeserialize(string filepath,string Sensortype)
        {
            Dictionary<DateTime, List<T>> data= new Dictionary<DateTime, List<T>>();
            var serializer = new JsonSerializer();
            if (File.Exists(filepath + Sensortype))
            {
                using (TextReader reader = File.OpenText(filepath + Sensortype))
                {
                    data = (Dictionary<DateTime, List<T>>)serializer.Deserialize(reader, typeof(Dictionary<DateTime, List<T>>));
                }
            }
            return data;
        }
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
          /*  //Als BrokeProfil speichern
            // BrokerProfil Eigenschaften zu Liste konvertieren
            List<string> BP = new List<string>(); 
            BP.Add( data.HostName_IP);
            BP.Add( Convert.ToString(data.Port));
            BP.Add( data.Username);
            BP.Add( data.Password);*/
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
            /*  IDatastorage<T>.BrokerProfile BP = new IDatastorage<T>.BrokerProfile();
              BP.HostName_IP = data[0];
              BP.Port = Convert.ToUInt32(data[1]);
              BP.Username = data[2];
              BP.Password = data[3];

              return BP;*/
            return data;
        }

       
    }
}
