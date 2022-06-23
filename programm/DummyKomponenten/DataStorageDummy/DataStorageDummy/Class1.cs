﻿using System;
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

 

        public void SaveSensorgroup(Dictionary<string, List<string>> SensorListe, string Base, string Filepath)
        {
            throw new NotImplementedException();
        }

        void IDatastorage<double>.JsonSerialize(ISensor<double> data, string filepath, string Sensortype)
        {
            throw new NotImplementedException();
        }

        ISensor<double> IDatastorage<double>.JsonDeserialize(string filepath, string Sensortype)
        {
            throw new NotImplementedException();
        }

        IBrokerProfile IDatastorage<double>.LoadBrokerProfile(string filepath)
        {
            throw new NotImplementedException();
        }

        void IDatastorage<double>.SavebrokerProfile(IBrokerProfile data, string filepath)
        {
            throw new NotImplementedException();
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
                    value = value.Replace(" ", "");

                    if (uint.TryParse(value.Replace(".", ""), out uint output))
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
                    if ((value >= 1000) && (value <= 9999))
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

        }
    }
}
