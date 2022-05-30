using System;
using MQTTCommunicatorDummy;
using CommonInterfaces;
using MQTTnet;
using MQTTCommunicator;

namespace MQTTConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
              int menuNumber;
              Console.WriteLine("Test menu:");
              Console.WriteLine("1. for CreateTopic");
              Console.WriteLine("2. for PublishToTopic");
              Console.WriteLine("3. for RegisterClient");
              Console.WriteLine("4. for SetNewBroker");
              Console.WriteLine("5. for SubscribeTopic");
              Console.WriteLine("6. for GetClients");
              Console.WriteLine("7. for GetTopics");



              menuNumber = Int32.Parse(Console.ReadLine());

              switch (menuNumber) 
              {
                  case 1:
                      comm1.CreateTopic("dummy","dummy");
                      break;
                  case 2:
                      comm1.PublishToTopic("dummy", "dummy", 5);
                      break;
                  case 3:
                      comm1.RegisterClient("dummy", true);
                      break;
                  case 4:
                      comm1.SetNewBroker("192.168.1.1", 5555);
                      break;
                  case 5:
                      comm1.SubscribeTopic("dummy", "dummy");
                      break;
                  case 6:
                      comm1.GetClients();
                      break;
                  case 7:
                      comm1.GetTopics();
                      break;
                  default:
                      break;
              }*/

            Console.WriteLine("Projekt started");
            Communicator communicator = new Communicator();
            communicator.ConnectToBroker("localhost", 1883);
            communicator.PublishToTopic("client1", "test/124", "5");
            /*communicator.PublishToTopic("client1", "test/123", "8");
            communicator.PublishToTopic("client1", "test/124", "5");*/
        }
    }
}
