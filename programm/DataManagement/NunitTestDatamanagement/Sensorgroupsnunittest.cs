using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using SensorAndSensorgroup;





namespace NunitTestDatamanagement
{
    public class Sensorgroupsnunittest
    {
        private SensorAndSensorgroup.Sensorgroups SensorgroupsTests = new Sensorgroups();
        DataStorage.DataStorage store = new DataStorage.DataStorage();
        string FolderPath;
        string Base;
        string Node;
        string Node1;
        string Node3;
        string Node2;
        string Id;
        string Id2;
        Dictionary<string, List<string>> Ids = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> Ids2 = new Dictionary<string, List<string>>();


        [SetUp]
        public void Setup()
        {
            FolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SensorGroups\");
            Base = "Hometest2";
            Node = "Zimmer5";
            Node2 = "Wohnzimmer";
            Node1 = "zimmer1";
            Node3 = "zimmer4";
            List<string> Sensoren = new List<string> { "18485146", "1784961", "47849525" };
            Ids.TryAdd(Node,Sensoren);
            Id = "8198419";
            Id2 = "46984156";
            if (System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "SensorGroups"))
            {
                System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "SensorGroups");
            }
        }
        [Test]
        public void It_should_Addbase()
        {


            //Act
           SensorgroupsTests.AddBase(Base);
           
            //Assert

            Assert.That(File.Exists(FolderPath+Base));

           
        }
        [Test]
        public void It_should_Addnode()
        {

            //Act
            SensorgroupsTests.AddNode(Node, Base);
            
            Ids2 = store.LoadSensorgroup(Base, FolderPath);
            

            //Assert
            Assert.That(Ids2.Keys.Contains(Node));
           
        }
        
        
        [Test]
        public void It_should_add_Sensor()
        {

            //Act

           SensorgroupsTests.AddNode(Node2, Base);
           SensorgroupsTests.Sensorhinzufuegen(Id,Node2,Base);
            
            Ids2 = store.LoadSensorgroup(Base, FolderPath);

            //Assert
            Assert.That(Ids2[Node2].Contains(Id));
        }
        [Test]
        public void It_should_delete_node()
        {


            //Act
            SensorgroupsTests.DeleteNode(Node3, Base);
            Ids2 = store.LoadSensorgroup(Base, FolderPath);

            //Assert
            Assert.That(!Ids2.ContainsKey(Node3));
        }
        [Test]
        public void It_should_delete_Sensor()
        {


            //Act
            SensorgroupsTests.Sensorhinzufuegen(Id2, Node, Base);
            SensorgroupsTests.Sensorloeschen(Id2, Node, Base);
           Ids2 = store.LoadSensorgroup(Base, FolderPath);

            //Assert
            Assert.That(!Ids2[Node].Contains(Id2));
        }
        [Test]
        public void It_should_skip_when_the_id_is_founded()
        {

            //Act
            SensorgroupsTests.Sensorhinzufuegen(Id, Node, Base);
            SensorgroupsTests.Sensorhinzufuegen(Id, Node, Base);

            Ids2 = store.LoadSensorgroup(Base, FolderPath);

            //Assert
            Assert.That(Ids2[Node].Where(s => s != null && s.StartsWith(Id)).Count() <= 1);
        }
    }
}