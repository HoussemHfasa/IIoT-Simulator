using System;
using System.Collections.Generic;
using System.Text;
using CommonInterfaces;
using Newtonsoft.Json;

namespace MQTTCommunicator
{
    public class BrokerProfile:IBrokerProfile
    {
        string hostName_IP;
        uint port;
        string username;
        string password;    
        public string HostName_IP
        {
            get
            {
                return this.hostName_IP;
            }
            set
            {
                //Der IP enthält nur Zahlen und Punkten
                value = value.Replace(" ", "");

                if (ulong.TryParse(value.Replace(".", ""), out ulong output))
                {
                    this.hostName_IP = value;
                }
                else
                {
                    throw new Exception("Ungültige Eingabe000");
                }
            }
        }
        public uint Port
        {
            get
            {
                return this.port;
            }
            set
            {
                //der Proxy muss zwischen 1000 und 9999 sein
                if ((value >= 1000) && (value <= 9999))
                {
                    this.port = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("der Proxy muss zwischen 1000 und 9999 sein");
                }
            }
        }
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }
        public string Password {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        //Konsructor für die Dateneingabe
        public BrokerProfile(string hostname_IP, uint port, string username, string password)
        {
            HostName_IP = hostname_IP;
            Port = port;
            Username = username;
            Password = password;
        }


        //Konstructor, wenn der Nutzname und den Passwort nicht nötig sind
        public BrokerProfile(string hostname_IP, uint port)
        {
            HostName_IP = hostname_IP;
            Port = port;
        }
        public BrokerProfile()
        {
        }
    }
}
