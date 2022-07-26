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
        //private Dictionary<String, Client> clients;
        private List<String> Topic { get; set; }
        private List<String> TopicMessage { get; set; }
        string host;
        int port;
        string username;
        string password;
        /// <summary>
        /// Konstruktor des Communicators
        /// </summary>
        public Communicator()
        {
            mqttClient = new MqttFactory().CreateMqttClient();

            this.Topic = new List<string>();

            this.TopicMessage = new List<string>();

            

        }
        /// <summary>
        ///  Verbindung zum Broker mit und ohne Authentifizierung. Wenn man keine login und password eingibt, wird eine
        ///  ungesicherte Verbindung zum Port 1883 aufgebaut. Anderfalls wird eine Verbindung zum Port 1884 aufgebaut.
        ///  Falls die Verbindung abbricht, wird es nochmal versucht eine einzustellen.
        /// </summary>
        /// <param name="Host">Erwuenschte Brokername bzw. IP-Adresse des Brokers</param>
        /// <param name="Port">Ein gültiger Port eingeben</param>
        /// <param name="Username">Eingestellte Username</param>
        /// <param name="Password">Dazugehörige Kennwort</param>
        public string ConnectToBroker(string Host, int Port, string Username = null, string Password = null)
        {
            this.host = Host;
            this.port = Port;
            this.username = Username;
            this.password = Password;

            string message = "";
            
            //Console.WriteLine("Connecting to " + Host + " : " + Port);
            try
            {
                if (Port > 65535 || Port < 0)
                {
                    message += "Invalid Port\n";
                }
                if (string.IsNullOrWhiteSpace(Host))
                {
                    message += "Invalid Host\n";
                }

                if ( string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) )
                {
                    var options = new MqttClientOptionsBuilder()
                        .WithClientId("TestClient")
                        .WithTcpServer(Host, Port)
                        .WithCleanSession()
                        .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V311)
                        .Build();
                    mqttClient.UseConnectedHandler(e =>
                    {
                        message += "-Connected\n-";
                    });
                    Task t = Task.Run(() => mqttClient.ConnectAsync(options));
                    Task.WaitAll(t);


                }
                else
                {
                    var options = new MqttClientOptionsBuilder()
                        .WithClientId("TestClient")
                        .WithTcpServer(Host, Port)
                        .WithCleanSession()
                        .WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V311)
                        .WithCredentials(Username, Password)
                        .Build();
                    mqttClient.UseConnectedHandler(e =>
                    {
                        message += "-Connected\n-";
                        Task t = Task.Run(() => mqttClient.ConnectAsync(options));
                    Task.WaitAll(t);

                    });
                }
            }
            catch
            {
                message += "-Connection failed\n-";
            }

            return message;
        }

        public void CreateTopic(string topicName)
        {
            Console.ReadKey();
        }
        /// <summary>
        /// Erstmal nicht gebraucht
        /// </summary>
        /// <returns></returns>
        public List<string> GetClients()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Erstmal nicht gebraucht
        /// </summary>
        /// <returns></returns>
        public List<string> GetTopics()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Versendug von Messages an den Broker mit Voreinstellungen. Wird ausführbar, erst dann wenn der eine Verbindung zum
        /// Broker besteht. Nachrichten werden mit QoS = 2 gesendet.
        /// </summary>
        /// <param name="topicName">Als TopicName wird Pfad von einem Sensor genommen</param>
        /// <param name="messagePayload">Ubersendete Nachrichten an den Broker</param>
        public async void PublishToTopic(string topicName, String messagePayload)
        {
            /*
             if(mqttClient.IsConnected)
             {                                      
                     try
                     {
                          var message = new MqttApplicationMessageBuilder()
                          .WithTopic(topicName)
                          .WithPayload(messagePayload)
                          .WithExactlyOnceQoS()
                          .WithRetainFlag(true)
                          .Build();
                             AddTopic(topicName);
                             AddTopicMessage(messagePayload);
                             Console.WriteLine("Publishing message -" + messagePayload + "- from: " + "TestClient" + " to Topic " + topicName);
                    Task t = Task.Run(() => mqttClient.PublishAsync(message));
                    Task.WaitAll(t);                                      
                } 
                     catch (Exception e)
                     {
                          Console.WriteLine("Something went wrong: " + e.Message);
                          //Console.ReadKey();
                     }
             }
             else if(mqttClient.IsConnected == false)
            {
                ConnectToBroker(host, port, username, password);
            }*/

            if (mqttClient.IsConnected == false)
            {
                ConnectToBroker(host, port, username, password);
            }
            try
            {
                var message = new MqttApplicationMessageBuilder()
                .WithTopic(topicName)
                .WithPayload(messagePayload)
                .WithExactlyOnceQoS()
                .WithRetainFlag(true)
                .Build();
                AddTopic(topicName);
                AddTopicMessage(messagePayload);
                Console.WriteLine("Publishing message -" + messagePayload + "- from: " + "TestClient" + " to Topic " + topicName);
                Task t = Task.Run(() => mqttClient.PublishAsync(message));
                Task.WaitAll(t);
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong: " + e.Message);
                //Console.ReadKey();
            }
        }

    
        /// <summary>
        /// Falls Benutzer eine neue Verbindung braucht, wird bestehende abgebrochen. Dann nochmal die Methode
        /// ConnectToBroker ausgeführt.
        /// </summary>
        /// <param name="Host">Erwuenschte Brokername bzw. IP-Adresse des Brokers</param>
        /// <param name="Port">Ein gültiger Port eingeben</param>
        /// <param name="Username">Eingestellte Username</param>
        /// <param name="Password">Dazugehörige Kennwort</param>
        public void SetNewBroker(string Host, int Port, string Username = null, string Password = null)
        {
            mqttClient.DisconnectAsync();
            Console.WriteLine("Disconnected");
            ConnectToBroker(Host, Port,  Username = null, Password = null);
        }

        public void AddTopic(String Topic)
        {
            this.Topic.Add(Topic);
        }
        public void AddTopicMessage(String TopicMessage)
        {
            this.TopicMessage.Add(TopicMessage);
        }
        

    }
}
