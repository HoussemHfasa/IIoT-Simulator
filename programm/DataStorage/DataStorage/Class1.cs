using System;
using CommonInterfaces;
namespace DataStorage
{
    public class DataStorage : IDatastorage
    {
        public object Data { get; set; }
        public string filepath { get; set; }

        public object JsonDeserialize(string filepath)
        {
            throw new NotImplementedException();
        }

        public void JsonSerialize(object data, string filepath)
        {
            throw new NotImplementedException();
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
