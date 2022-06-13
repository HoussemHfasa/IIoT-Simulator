using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using SensorAndSensorgroup;
using DataStorage;




namespace NunitTestDatamanagement
{
    public class Sensorgroupsnunittest
    {
        private SensorAndSensorgroup.Sensorgroups SensorgroupsTests = new Sensorgroups();
        DataStorage<string> store = new DataStorage<string>();
        string FolderPath;
        string Base;
        string Node;
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
            List<string> Sensoren = new List<string> { "18485146", "1784961", "47849525" };
            Ids.TryAdd(Node,Sensoren);
            Id = "8198419";
            Id2 = "46984156";
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
            SensorgroupsTests.AddNode(Node2, Base);
            Ids2 = store.LoadSensorgroup(Base, FolderPath);
            

            //Assert
            Assert.That(Ids2.ContainsKey(Node));
           
        }
        
        
        [Test]
        public void It_should_add_Sensor()
        {

            //Act
            SensorgroupsTests.Sensorhinzufuegen(Id,Node,Base);
            SensorgroupsTests.Sensorhinzufuegen(Id2, Node, Base);
            Ids2 = store.LoadSensorgroup(Base, FolderPath);

            //Assert
            Assert.That(Ids2[Node].Contains(Id));
        }
        [Test]
        public void It_should_delete_node()
        {


            //Act
            SensorgroupsTests.DeleteNodeBase(Node2, Base);
            Ids2 = store.LoadSensorgroup(Base, FolderPath);

            //Assert
            Assert.That(!Ids2.ContainsKey(Node2));
        }
        [Test]
        public void It_should_delete_Sensor()
        {


            //Act
            SensorgroupsTests.Sensorloeschen(Id,Node,Base);
            SensorgroupsTests.Sensorloeschen(Id2, Node, Base);
            Ids2 = store.LoadSensorgroup(Base, FolderPath);
            //Assert
            Assert.That(!Ids2[Node].Contains(Id2));
        }
        /* [Test]
         public void It_should_skip_when_the_id_is_founded()
         {
             // Arrange
             List<string> SensorList = new List<string> { "123456", "148456", "189746" };
             string Id = "123456";


             //Act
             SensorgroupsTest.Sensorhinzufuegen(SensorList, Id);


             //Assert
             Assert.Contains(Id, SensorList);
         }


         
         public void It_should_delete_Base()
         {
             // Arrange
             string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\";
             string NewBase = "Wohnung X";
             string NewNode = "";



             //Act
             SensorgroupsTest.DeleteNodeBase(NewNode, NewBase);


             //Assert
             Assert.IsFalse(Directory.Exists(folderPath + $"\\,{NewBase}"));
         }*/
    }
}