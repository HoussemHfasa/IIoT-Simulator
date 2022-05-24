using System;
using System.Collections.Generic;
using CommonInterfaces;

namespace DataStorageDummy
{
    public class DataStorage : IDatastorage<double> 
    {
        
        public string filepath { get { return this.filepath; } set { this.filepath = "funktioniert"; } }

        Dictionary<DateTime, List<double>> Data { get { return Data; } set { Data.Add(DateTime.Today, new List<double> { 251, 14, 25, 48, 2.41 }); } }

        Dictionary<DateTime, List<double>> IDatastorage<double>.Data { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Dictionary<DateTime, List<double>> IDatastorage<double>.JsonDeserialize(string filepath)
        {
            Dictionary<DateTime, List<double>> beispiel = new Dictionary<DateTime, List<double>> { };
            beispiel.Add(DateTime.Today, new List<double> { 251, 14, 25, 48, 2.41 });
           
            return beispiel;
        }

        public void JsonSerialize(Dictionary<DateTime, List<double>> data, string filepath)
        {
            data.Add(DateTime.Today,new List<double> {251,14,25,48,2.41 });
            filepath = "funktioniert";
            Console.WriteLine(data.ToString(), filepath);
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

        public List<string> LoadSensorgroup(string Base, string Node)
        {
            List<string> beispiel = new List<string> { "fdv51654","dfbfdbfd651","f+ä+#214" };
            return beispiel;
        }

        public void SavebrockerProfile(object data, string filepath)
        {
            data = 5;
            filepath = "funktioniert";
            Console.WriteLine((string)data, filepath);
        }

       

        public void SaveSensorgroup(List<string> SensorListe, string Base, string Node)
        {
            SensorListe= new List<string> { "fdv51654","dfbfdbfd651","f+ä+#214"} ;
            Base = "test1";
            Node = "test2";
            Console.WriteLine(SensorListe.ToString(), filepath);
        }

       

       
    }
}
