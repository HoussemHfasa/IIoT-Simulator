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
        public Dictionary<DateTime,List<T>> Data { get; set; }
       // public string filepath { get; set; }


        // Ladung der Daten in der Dateipfad
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
        // Speicherung der Daten in der Dateipfad
        public void JsonSerialize(Dictionary<DateTime, List<T>> data, string filepath)
        {
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(filepath))
            {
                serializer.Serialize(writer, data);
            }
        
        
    }
       
        public object LoadBrockerProfile(string filepath)
        {
            throw new NotImplementedException();
        }

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

        public void SavebrockerProfile(object data, string filepath)
        {
            throw new NotImplementedException();
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
    }
}
