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
        // Speicherung der SensorDaten in der Dateipfad
        public void JsonSerialize(Dictionary<DateTime, List<T>> data, string filepath)
        {
            //Ladung der vorhandenen Daten
            Dictionary<DateTime, List<T>> already_data = new Dictionary<DateTime, List<T>>();
            already_data = JsonDeserialize(filepath);

            Verfizierung(already_data, data);
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(filepath))
            {
                serializer.Serialize(writer, already_data);
            }

        }
        // Ladung der SensorDaten von der Dateipfad
        public Dictionary<DateTime, List<T>> JsonDeserialize(string filepath)
        {
            Dictionary<DateTime, List<T>> data;
            var serializer = new JsonSerializer();
            using (TextReader reader = File.OpenText(filepath))
            {
                data = (Dictionary<DateTime, List<T>>)serializer.Deserialize(reader, typeof(Dictionary<DateTime, List<T>>));
            }
            return data;
        }
        
       
        
        // Ladung der Sensorgroups Daten(Ids)
        public List<string> LoadSensorgroup(string Base, string Node)
        {
            string Filepath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\" + Base + "\\" + Node;
            List<string> data;
            var serializer = new JsonSerializer();
            using (TextReader reader = File.OpenText(Filepath))
            {
                data = (List<string>)serializer.Deserialize(reader, typeof(List<string>));

            }

            return data;
        }
        
        //Speicherung der Brokerdaten
        public void SavebrokerProfile(IDatastorage<T>.BrokerProfile data, string filepath)
        {

            // BrokerProfil Eigenschaften zu Liste konvertieren
            List<string> BP = new List<string>(); 
            BP.Add( data.HostName_IP);
            BP.Add( Convert.ToString(data.Port));
            BP.Add( data.Username);
            BP.Add( data.Password);
            

            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(filepath))
            {
                serializer.Serialize(writer, BP);
            }
        }
        //Ladung der Brokerdaten
        public IDatastorage<T>.BrokerProfile LoadBrokerProfile(string filepath)
        {
            
            List<string> data = new List<string>();
            var serializer = new JsonSerializer();
            using (TextReader reader = File.OpenText(filepath))
            {
               data  = (List<string>)serializer.Deserialize(reader, typeof(List<string>));
            }
            IDatastorage<T>.BrokerProfile BP = new IDatastorage<T>.BrokerProfile();
            BP.HostName_IP = data[0];
            BP.Port = Convert.ToUInt32(data[1]);
            BP.Username = data[2];
            BP.Password = data[3];

            return BP;
        }

        public void SaveSensorgroup(List<string> SensorListe,string Base, string Node)
        {
            string Filepath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\" + Base + "\\" + Node;
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(Filepath))
            {
                serializer.Serialize(writer, SensorListe);
            }

        }

       

        //BrockerProfileEigenschaften
        public class BrokerProfile
        {
            public string HostName_IP
            {
                get
                {
                    return this.HostName_IP;
                }
                set
                {
                    //Der IP enthält nur Zahlen und Punkten
                     value = value.Replace(" ","");
                    
                    if(uint.TryParse(value.Replace(".",""),out uint output))
                    {
                        this.HostName_IP = value;
                    }
                    else
                    {
                        throw new Exception("Ungültige Eingabe");
                    }
                }
            }
            public uint Port
            { 
                 get
                {
                    return this.Port;
                }
                set
                {
                    //der Proxy muss zwischen 1000 und 9999 sein
                    if ((value>=1000)&&(value<=9999))
                    {
                        this.Port = value;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("der Proxy muss zwischen 1000 und 9999 sein");
                    }
                }
                }
            public string Username { get; set; }
            public string Password { get; set; }

            //Konsructor für die Dateneingabe
            public BrokerProfile(string hostname_IP, uint port, string username, string password)
            {
                HostName_IP = hostname_IP;
                Port = port;
                Username = username;
                Password = password;
            }
            

            //Konstructor, wenn der Nutzname und den Passwort nicht nötig sind
            public BrokerProfile(string hostname_IP, uint port)
            {
                HostName_IP = hostname_IP;
                Port = port;
            }
        }
    }
}
