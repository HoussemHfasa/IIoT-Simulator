using System;
using System.Collections.Generic;
using System.Text;
using MQTTnet;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using MQTTnet.Client;
using MQTTnet.Server;
using MQTTnet.Client.Options;

namespace MQTTCommunicator
{
    public class Client
    {
        private String id;
      //  private List<String> subscriptions { get; }
        private List<String> publishing { get; }
        private string _clients { get; set; }

        public Client(string ClientId)
        {
            this.id = ClientId;
           // this.subscriptions = new List<String>();
            this.publishing = new List<String>();
        }


        public void AddPublishing(String topic)
        {
            this.publishing.Add(topic);
        }

        public void Unpublish(String topic)
        {
            if (this.publishing.Contains(topic))
            {
                this.publishing.Remove(topic);
            }
            else
            {
                Console.WriteLine("Trying to remove not subscribed topic");

            }
        }

        public bool IsPublishing(String topic)
        {
            return this.publishing.Contains(topic);
        }
    }

}
