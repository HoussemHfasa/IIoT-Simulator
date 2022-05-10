using System;
using CommonInterfaces;

namespace DataStorageDummy
{
    public class DataStorage : IDatastorage
    {
        public object Data { get { return this.Data; } set { this.Data = 5; } }
        public string filepath { get { return this.filepath} set { this.filepath = "funktioniert"; } }

        public object JsonDeserialize(string filepath)
        {
            object beispiel = 5;
            return beispiel;
        }

        public void JsonSerialize(object data, string filepath)
        {
            data = 5;
            filepath = "funktioniert";
            Console.WriteLine((string)data,filepath);
        }

        public object LoadBrockerProfile(string filepath)
        {
            object beispiel = 5;
            return beispiel;
        }

        public object LoadSensorgroup(string filepath)
        {
            object beispiel = 5;
            return beispiel;
        }

        public void SavebrockerProfile(object data, string filepath)
        {
            data = 5;
            filepath = "funktioniert";
            Console.WriteLine((string)data,filepath);
        }

        public void SaveSensorgroup(object data, string filepath)
        {
            data = 5;
            filepath = "funktioniert";
            Console.WriteLine((string)data, filepath);
        }
    }
}
