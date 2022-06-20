using System;
using System.Collections.Generic;
using System.Text;
using CommonInterfaces;

namespace MQTTCommunicator
{
    public class BrokerProfile:IBrokerProfile
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
