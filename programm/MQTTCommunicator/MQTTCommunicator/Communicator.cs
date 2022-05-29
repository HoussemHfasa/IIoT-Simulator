using System;
using System.Collections.Generic;
using CommonInterfaces;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace MQTTCommunicator
{
    public class Communicator : IMQTTCommunicator
    {
        public IMqttClient mqttClient;
        Dictionary<String, Client> clients;

        /*public enum MqttClientConnectionStatus
        {
            Disconnected,
            Disconnecting,
            Connected,
            Connecting
        }*/


        public Communicator()
        {
            mqttClient = new MqttFactory().CreateMqttClient();
            clients = new Dictionary<string, Client>();
        }

        public async void ConnectToBroker(string Host, int Port)
        {
            var mqttClient = new MqttFactory().CreateMqttClient();

            Console.WriteLine("Connecting to " + Host + " : " + Port);

                var options = new MqttClientOptionsBuilder()
                    .WithClientId("client1")
                    .WithTcpServer(Host, Port)
                    .WithCleanSession()
                    //.WithTls(tlsOptions)
                    .Build();
                
                Console.WriteLine("wwwwtttffff");
                
                await mqttClient.ConnectAsync(options);

                mqttClient.UseConnectedHandler(e =>
                {
                    Console.WriteLine("Connected to the broker");
                });




                var message = new MqttApplicationMessageBuilder()
                .WithTopic("test/123")
                .WithPayload("initial message")
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();






            /* mqttClient.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await mqttClient.ConnectAsync(options);
                }
                catch
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            });

            */

        }

        public void CreateTopic(string clientId, string topicName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetClients()
        {
            throw new NotImplementedException();
        }

        public List<string> GetTopics()
        {
            throw new NotImplementedException();
        }

        public async void PublishToTopic(string clientId, string topicName, String messagePayload)
        {
            Client client;
            clients.TryGetValue(clientId, out client);
            if (client == null || !client.isPublishing(topicName)) {
                Console.WriteLine("The client " + clientId + " can not write to the topic " + topicName);
                return;
            }


        Console.WriteLine("Publishing message -" + messagePayload +"- from: " + clientId + " to " + topicName);
            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topicName)
                .WithPayload(messagePayload)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();
            try
            {
                Task t = Task.Run(() => mqttClient.PublishAsync(message));
                Task.WaitAll(t);
            } catch (Exception e){
                Console.WriteLine("Something went wrong: " + e.Message);

            }
            
            /*var message = new MqttApplicationMessageBuilder()
                .WithTopic("test/123")
                .WithPayload("aaaaaaaaaaaaaaaaaaa")
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();
            //await mqttClient.PublishAsync(message);*/
            Console.ReadKey();
           
        }

        public void RegisterClient(string clientId, bool isGroup)
        {
            clients.Add(clientId, new Client(clientId));
        }

        public void SetNewBroker(string Host, int Port)
        {
            
        }
        private void HandleMessageReceived(MqttApplicationMessage applicationMessage)
        {
            Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
            Console.WriteLine($"+ Topic = {applicationMessage.Topic}");
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(applicationMessage.Payload)}");
            Console.WriteLine($"+ QoS = {applicationMessage.QualityOfServiceLevel}");
            Console.WriteLine($"+ Retain = {applicationMessage.Retain}");
            Console.WriteLine();
        }

        public void SubscribeTopic(string clientId, string topicName)
        {
            mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
                Console.WriteLine($"+ Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine($"+ QoS = {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine($"+ Retain = {e.ApplicationMessage.Retain}");
                Console.WriteLine();
            });
        }
    }
}
