using System;
using System.Collections.Generic;
using CommonInterfaces;

namespace DataStorageDummy
{
    public class DataStorage : IDatastorage<double>
    {



        Dictionary<DateTime, List<double>> Data { get { return Data; } set { Data.Add(DateTime.Today, new List<double> { 251, 14, 25, 48, 2.41 }); } }



        public Dictionary<DateTime, List<double>> JsonDeserialize(string filepath, string Sensortype)
        {
            return Data;
        }

        public void JsonSerialize(Dictionary<DateTime, List<double>> data, string filepath,string Sensortype)
        {
            data.Add(DateTime.Today, new List<double> { 251, 14, 25, 48, 2.41 });
            filepath = "funktioniert";
            Console.WriteLine(data.ToString(), filepath);
        }


        Dictionary<string, List<string>> IDatastorage<double>.LoadSensorgroup(string Base, string Filepath)
        {
            Dictionary<string, List<string>> Liste = new Dictionary<string, List<string>>();
            Liste.Add("Zimmer1", new List<string> { "184879", "1849165" });
            return Liste;
        }

       /* public IBrokerProfile LoadBrokerProfile(string filepath)
        {
            IBrokerProfile Beispiel = new IBrokerProfile();
            
            Beispiel.HostName_IP = "125.48.564";
            Beispiel.Port = Convert.ToUInt32("1484");
            Beispiel.Username = "name1";
            Beispiel.Password = "passwort1";
            return Beispiel;

        }*/

        public void SaveSensorgroup(Dictionary<string, List<string>> SensorListe, string Base, string Filepath)
        {
            throw new NotImplementedException();
        }

        public void SavebrokerProfile(IBrokerProfile data, string filepath)
        {
            throw new NotImplementedException();
        }

        public List<string> BasenameDeserialize(string filepath)
        {
            return (new List<string> { "Haus", "Garage" });
        }

        public void BasenamSerialize(List<string> data, string filepath)
        {
            throw new NotImplementedException();
        }

        IBrokerProfile IDatastorage<double>.LoadBrokerProfile(string filepath)
        {
            throw new NotImplementedException();
        }
    }
    }

