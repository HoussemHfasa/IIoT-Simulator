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
    public class SensorGroupsSpeicherungTest
    {
        private DataStorage.DataStorage<double> GroupsTest = new DataStorage<double>();
        String Base;
        String Folderpath;
        Dictionary<string, List<string>> Sensorliste = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> Sensorliste2 = new Dictionary<string, List<string>>();
        [SetUp]
        public void Setup()
        {
            //Arrange
            Base = "HH1";
            Folderpath = AppDomain.CurrentDomain.BaseDirectory;
            Sensorliste.Add("Zimmer1",new List<string> { "151546","18546","84984"});

        }

        [Test]
        public void it_should_save_the_sensor_groups()
        {

            //Act
            GroupsTest.SaveSensorgroup(Sensorliste,Base,Folderpath);

            //Assert
            //  Assert.IsTrue(Directory.Exists(Path.Combine(Folderpath, Base)));
            Assert.Pass();
        }
        [Test]
        public void it_should_save_only_the_last_broker_details()
        {
            //Act
            
           Sensorliste2= GroupsTest.LoadSensorgroup(Base,Folderpath);

            //Assert
            //  Assert.IsTrue(Directory.Exists(Path.Combine(Folderpath, Base)));
            Assert.That(Sensorliste, Is.EqualTo(Sensorliste2));
        }

    }
}
