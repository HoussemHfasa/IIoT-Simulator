using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;

namespace DataManagement
{
    public class Sensor<T> : ISenor<T>
    {
        public string Sensor_id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string[,] Id_Adresse { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Sensortype { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Einheit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DateTime CreationDate => throw new NotImplementedException();

        public TimeSpan CreationTime => throw new NotImplementedException();

        public int Werteanzahl => throw new NotImplementedException();

        public int Timeinterval => throw new NotImplementedException();

        public T[] Getvalues()
        {
            throw new NotImplementedException();
        }
    }
}

