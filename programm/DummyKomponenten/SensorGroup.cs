using System;
using CommonInterfaces;
namespace SensorAndSensorgroups
{
    public class SensorGroups : ISensorGroups
    {
        public string Adresse { get { return this.Adresse; } set { this.Adresse = "funktioniert"; } }
        public string[] SensorIds
        {
            get { return this.SensorIds; }
            set
            {
                string[] test = { "funktioniert" };
                this.SensorIds = test;

            }
        }
                public void Sensorhinzufuegen()
                {
                    throw new NotImplementedException();
                }

                public void Sensorloeschen()
                {
                    throw new NotImplementedException();
                }
            }
    }

