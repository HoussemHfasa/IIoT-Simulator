using System;
using System.Collections.Generic;
using CommonInterfaces;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MQTTCommunicator
{
    public class Communicator : IMQTTCommunicator
    {
        private IMqttClient mqttClient;
        private Dictionary<String, Client> clients;


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
            Console.WriteLine("Connecting to " + Host + " : " + Port);
            try
            {
                var options = new MqttClientOptionsBuilder()
                    .WithClientId("TestClient")
                    .WithTcpServer(Host, Port)
                    .WithCleanSession()
                    //.WithTls(tlsOptions)
                    .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V311)
                    .Build();
                
                mqttClient.UseConnectedHandler(e =>
                {
                    Console.WriteLine("Connected to the broker");
                });

                await mqttClient.ConnectAsync(options);
                
            }
            catch
            {
                    Console.WriteLine("Connection failed");
                    Console.ReadKey();
            }
            if (mqttClient.IsConnected)
            {
                var message = new MqttApplicationMessageBuilder()
                    .WithTopic("test/123")
                    .WithPayload("test")
                    .WithExactlyOnceQoS()
                    .WithRetainFlag()
                    .Build();

                //Task t = Task.Run(() => mqttClient.PublishAsync(message));
                //Task.WaitAll(t);
                await mqttClient.PublishAsync(message);
            }
            else
            {
                Console.WriteLine("Connection lost");
            }
         

        }


        public void CreateTopic(string clientId, string topicName)
        {
            Console.ReadKey();
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
            /* if(mqttClient.IsConnected)
             {           
                 Client client;
                 clients.TryGetValue(clientId, out client);
                 if (client == null || !client.IsPublishing(topicName))
                 {
                     Console.WriteLine("The client " + clientId + " can not write to the topic " + topicName + " message: " + messagePayload);
                     return;
                 }
                 else
                 {                
                     Console.WriteLine("Publishing message -" + messagePayload +"- from: " + clientId + " to " + topicName);
                     try
                     {
                          var message = new MqttApplicationMessageBuilder()
                          .WithTopic(topicName)
                          .WithPayload(messagePayload)
                          .WithExactlyOnceQoS()
                          .WithRetainFlag()
                          .Build();

                      //Task t = Task.Run(() => mqttClient.PublishAsync(message));
                      //Task.WaitAll(t);
                          await mqttClient.PublishAsync(message);
                          Console.ReadKey();
                     } 
                     catch (Exception e)
                     {
                          Console.WriteLine("Something went wrong: " + e.Message);
                          Console.ReadKey();
                     }
                 }
             }*/
            //Console.ReadKey();
        }

        public void RegisterClient(string clientId, bool isGroup)
        {
            clients.Add(clientId, new Client(clientId));
        }

        public void SetNewBroker(string Host, int Port)
        {
            
        }
        /*private void HandleMessageReceived(MqttApplicationMessage applicationMessage)
        {
            Console.WriteLine("### RECEIVED APPLICATION MESSAGE ###");
            Console.WriteLine($"+ Topic = {applicationMessage.Topic}");
            Console.WriteLine($"+ Payload = {Encoding.UTF8.GetString(applicationMessage.Payload)}");
            Console.WriteLine($"+ QoS = {applicationMessage.QualityOfServiceLevel}");
            Console.WriteLine($"+ Retain = {applicationMessage.Retain}");
            Console.WriteLine();
        }*/

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
