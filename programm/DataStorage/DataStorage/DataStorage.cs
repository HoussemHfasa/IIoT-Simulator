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
    public class DataStorage : IDatastorage
    {
        public object Data { get; set; }
        public string filepath { get; set; }
   


        public JObject JsonDeserialize(string filepath)
        {
            JObject obj = null;
            var serializer = new JsonSerializer();
            using (TextReader reader = File.OpenText(filepath))
            {
                obj = serializer.Deserialize(reader,typeof(object)) as JObject;
            }
            
            return obj;
        }

        public void JsonSerialize(object data, string filepath)
        {
            var serializer = new JsonSerializer();
            using (TextWriter writer = File.CreateText(filepath))
            {
                serializer.Serialize(writer, data);
            }
           /* JsonSerializer jsonSerializer= new JsonSerializer();
            if (File.Exists(filepath)) File.Delete(filepath);
            StreamWriter sw = new StreamWriter(filepath);
            JsonWriter jsonWriter = new JsonTextWriter(sw);

            jsonSerializer.Serialize(jsonWriter, data);

            jsonWriter.Close();
            sw.Close();*/
        }

        public object LoadBrockerProfile(string filepath)
        {
            throw new NotImplementedException();
        }

        public object LoadSensorgroup(string filepath)
        {
            throw new NotImplementedException();
        }

        public void SavebrockerProfile(object data, string filepath)
        {
            throw new NotImplementedException();
        }

        public void SaveSensorgroup(object data, string filepath)
        {
            throw new NotImplementedException();
        }
    }
}
