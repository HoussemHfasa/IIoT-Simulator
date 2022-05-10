using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;

namespace DataManagement
{
    public class SensorandSensorgroups<T> : ISenor<T> , ISensorGroups
    {
        public string Sensor_id { get; set; }
        public string[,] Id_Adresse { get; set; }
        public string Sensortype { get; set; }
        public string Einheit { get; set; }

        public DateTime CreationDate { get; }

        public TimeSpan CreationTime { get; }

        public int Werteanzahl { get; }

        public int Timeinterval { get; }


        public T[] Getvalues()
        {
            throw new NotImplementedException();
        }


        public void SetParameter(System.Collections.Generic.List<T> Values)
        {
            throw new NotImplementedException();
        }
        //allgemeine Adresse für die Sensoren ,die in der Liste sind
        public string Adresse { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //List von Sensoren
        public string[] SensorIds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }


        public void Sensorhinzufuegen(string[] sensorids,string sensorid)
        {
            sensorids.Append(sensorid);
        }

        public void Sensorloeschen(string[] sensorids, string sensorid)
        {
            if (sensorids.Contains(sensorid))
            {
                
            }
        }

    
    }
}
