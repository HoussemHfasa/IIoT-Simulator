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



namespace NunitTestDatastorage
{
    public class BrockerprofilSpeicherungTest
    {
        private DataStorage.DataStorage<double> Storagetest = new DataStorage<double>();
        IDatastorage<double>.BrokerProfile Beispiel1 = new IDatastorage<double>.BrokerProfile();
        IDatastorage<double>.BrokerProfile Beispiel2 = new IDatastorage<double>.BrokerProfile();
        string filePath;
        string filename;
        [SetUp]
        public void Setup()
        {
            //Arrange
            Storagetest = new DataStorage.DataStorage<double>();
            filename = @"\BrokerProfileTest.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            Beispiel1.HostName_IP = "124.145.12.326";
            Beispiel1.Password = "12345";
            Beispiel1.Port = Convert.ToUInt32("15423");
            Beispiel1.Username = "name1";
            Beispiel2.HostName_IP = "193.145.12.326";
            Beispiel2.Password = "ho5475";
            Beispiel2.Port = Convert.ToUInt32("8710");
            Beispiel2.Username = "name2";

        }

        [Test]
        public void it_should_save_thebroker_details()
        {
            //Arrange
            Storagetest = new DataStorage.DataStorage<double>();
            filename = @"BrokerProfileTest.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
            
            //Act
            Storagetest.SavebrokerProfile(Beispiel1, filePath);

            //Assert
            Assert.That(Beispiel1.ToString(), Is.EqualTo(Storagetest.LoadBrokerProfile(filePath).ToString()));
        }
        [Test]
        public void it_should_save_only_the_last_broker_details()
        {
            //Arrange
            Storagetest = new DataStorage.DataStorage<double>();
            filename = @"BrokerProfileTest.txt";
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);

            //Act
            Storagetest.SavebrokerProfile(Beispiel1, filePath);
            Storagetest.SavebrokerProfile(Beispiel2, filePath);

            //Assert
            Assert.That(Beispiel2.ToString(), Is.EqualTo(Storagetest.LoadBrokerProfile(filePath).ToString()));
        }

    }
}
     