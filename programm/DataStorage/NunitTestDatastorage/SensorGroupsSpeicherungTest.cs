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
        String Base2;
        String Folderpath;
        Dictionary<string, List<string>> Sensorliste = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> Sensorliste2 = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> Sensorliste3 = new Dictionary<string, List<string>>();
        [SetUp]
        public void Setup()
        {
            //Arrange
            Base = "HG1";
            Base2 = "HG2";
            Folderpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Tests\");
            List<string> Sensoren = new List<string> { "151546", "18546", "84984" };
            Sensorliste.TryAdd("Zimmer1",Sensoren);
            if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Tests"))
            {
                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "Tests");
            }

        }

        [Test]
        //Prüfung für die Speicherung des Sensorgruppen
        public void it_should_save_the_sensor_groups()
        {

            //Act
            GroupsTest.SaveSensorgroup(Sensorliste,Base,Folderpath);

            //Assert
             Assert.That(File.Exists(Folderpath+Base));
           
        }
        [Test]
        //Prüfung für die Ladung des Sensorgruppen
        public void it_should_load_the_sensor_groups()
        {
            //Act         
           Sensorliste2= GroupsTest.LoadSensorgroup(Base,Folderpath);
            //Assert
            
            Assert.That(Sensorliste, Is.EqualTo(Sensorliste2));
        }
        [Test]
        //prüfen ob gibt es Bugs wenn mann laedt ein nicht existierendes File
        public void it_should_not_load_the_not_found_file()
        {
            //Act         

            Sensorliste3 = GroupsTest.LoadSensorgroup(Base2,Folderpath);
            //Assert

            Assert.AreEqual(Sensorliste3,new Dictionary<string, List<string>> { });
        }

    }
}
