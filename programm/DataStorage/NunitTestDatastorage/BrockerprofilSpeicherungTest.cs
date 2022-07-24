using NUnit.Framework;
using DataStorage;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using Newtonsoft.Json.Linq;
using MQTTCommunicator;



namespace NunitTestDatastorage
{
    public class BrockerprofilSpeicherungTest
    {
        private DataStorage.DataStorage Storagetest = new DataStorage.DataStorage();
        
        IBrokerProfile Beispiel1 = new MQTTCommunicator.BrokerProfile();
        IBrokerProfile Beispiel2 = new MQTTCommunicator.BrokerProfile();
        IBrokerProfile Beispiel3 = new MQTTCommunicator.BrokerProfile();
        string filePath;
        [SetUp]
        public void Setup()
        {
            //Arrange
            if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Tests"))
            {
                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Tests");
            }
            Storagetest = new DataStorage.DataStorage();
             filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"Tests\");
            Beispiel1.HostName_IP = "124.145.12.326";
            Beispiel1.Password = "12345";
           Beispiel1.Port = Convert.ToUInt32("1423");
            Beispiel1.Username = "name1";
            Beispiel2.HostName_IP = "193.145";
            Beispiel2.Password = "ho5475";
            Beispiel2.Port = Convert.ToUInt32("8710");
            Beispiel2.Username = "name2";
           
        }

        [Test]
        //Prüfung für die Speicherung des Brockerprofiles
        public void it_should_save_thebroker_details()
        {
            
            //Act
            Storagetest.SavebrokerProfile(Beispiel1, filePath);
       
            //Assert
            Assert.That(File.Exists(filePath+ "BrokerProfileTest"));
        }
        [Test]
        //Prüfung für die Ladung des Brockerprofiles
        public void it_should_load_thebroker_details()
        {
            //Act
            Storagetest.SavebrokerProfile(Beispiel1, filePath);
            Beispiel3 = Storagetest.LoadBrokerProfile(filePath);
            //Assert
            Assert.AreEqual(Beispiel1.HostName_IP, Beispiel3.HostName_IP);
            Assert.AreEqual(Beispiel1.Port, Beispiel3.Port);
            Assert.AreEqual(Beispiel1.Username, Beispiel3.Username);
            Assert.AreEqual(Beispiel1.Password, Beispiel3.Password);

        }
        [Test]
        //Prüfung ob nur das letzte Brokerprofile gespeichert wird
        public void it_should_save_only_the_last_broker_details()
        {
            //Act
            Storagetest.SavebrokerProfile(Beispiel1, filePath);
            Storagetest.SavebrokerProfile(Beispiel2, filePath);
            Beispiel3 = Storagetest.LoadBrokerProfile(filePath);
            //Assert
            Assert.AreEqual(Beispiel2.HostName_IP, Beispiel3.HostName_IP);
            Assert.AreEqual(Beispiel2.Username, Beispiel3.Username);
            Assert.AreEqual(Beispiel2.Password, Beispiel3.Password);
            Assert.AreEqual(Beispiel2.Port, Beispiel3.Port);
        }

    }
}
     