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
    public class Client : Communicator
    {
        private String id;
        private List<String> subsriptions { get; }
        private List<String> publishing { get; }
        private string _clients { get; set; }

        List<string> clients = new List<string>();

        public Client(string ClientId)
        {
            this.id = ClientId;
            this.subsriptions = new List<String>();
            this.publishing = new List<String>();
        }

        public void addSubscription(String topic)
        {
            this.subsriptions.Add(topic);
        }

        public void addPublishing(String topic)
        {
            this.publishing.Add(topic);
        }

        public void unsubscribe(String topic)
        {
            if (this.subsriptions.Contains(topic))
            {
                this.subsriptions.Remove(topic);
            }
            else
            {
                Console.WriteLine("Trying to remove not subscribed topic");

            }
        }

        public void unpublish(String topic)
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

        public bool isSubscribed(String topic){
            return this.subsriptions.Contains(topic);
        }

        public bool isPublishing(String topic)
        {
            return this.publishing.Contains(topic);
        }
    }

}
