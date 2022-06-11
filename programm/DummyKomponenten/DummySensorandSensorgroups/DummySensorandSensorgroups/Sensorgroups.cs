using System;
using System.Collections.Generic;
using CommonInterfaces;
namespace SensorAndSensorgroups
{
    public class SensorGroups : ISensorGroups
    {
        public string Base { get { return this.Base; } set { this.Base = "Hause 1"; } }
        public Dictionary<string, List<string>> SensorIds
        {
            get { return this.SensorIds; }
            set
            {
                Dictionary<string, List<string>> test = new Dictionary<string, List<string>>();
                test.Add("Zimmer", new List<string> { "296" });
                this.SensorIds = test;

            }
        }

        public string Node { get {return this.Node; } set { this.Node = "Zimmer1"; } }


        

        public void AddBase(string BaseName)
        {
            Base = BaseName;
        }

        public void AddNode(string NodeName, string Basename)
        {
            Node = NodeName;
        }

        public void DeleteNodeBase(string NodeName, string Basename)
        {
            throw new NotImplementedException();
        }

        



        public void Sensorhinzufuegen(string sensorid, string NodeName, string Basename)
        {
            SensorIds[NodeName].Add(sensorid);
        }
  

        public void Sensorloeschen(string sensorid, string NodeName, string Basename)
        {
            throw new NotImplementedException();
        }
    }
}

