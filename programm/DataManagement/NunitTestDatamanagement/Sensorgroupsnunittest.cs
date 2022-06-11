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
        string Base;
        string Node;
        string Id;
        Dictionary<string, List<string>> Ids = new Dictionary<string, List<string>>();


        [SetUp]
        public void Setup()
        {

            Base = "HT";
            Node = "Zimmer2";
            Ids.Add(Node,new List<string> {"18485146","1784961","47849525" });
            Id = "8198419";
        }
        [Test]
        public void It_should_Addbase()
        {


            //Act
            SensorgroupsTests.AddBase(Base);


            //Assert
            Assert.Pass();
        }
        [Test]
        public void It_should_Addnode()
        {


            //Act
            SensorgroupsTests.AddNode(Node, Base);


            //Assert
            Assert.IsTrue(Ids.ContainsKey(Node));
        }
        /*[Test]
        public void It_should_add_Sensor()
        {
            // Arrange
           List<string> SensorList = new List<string> { "123456", "148456", "189746" };
           string Id = "123156";
            //Act
            SensorgroupsTest.Sensorhinzufuegen(SensorList, Id);

            //Assert
            Assert.Pass();
        }
        [Test]
        public void It_should_delete_Sensor()
        {
            // Arrange
            List<string> SensorList = new List<string> { "123456", "148456", "189746" };
            string Id = "123456";

            //Act
            SensorgroupsTest.Sensorloeschen(SensorList, Id);

            //Assert
            Assert.Pass();
        }
        [Test]
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
        
        
        [Test]
        public void It_should_delete_node()
        {
            // Arrange
            string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\";
            string NewBase = "Wohnung X";
            string NewNode = "Zimmer Y";



            //Act
            SensorgroupsTest.DeleteNodeBase(NewNode,NewBase);


            //Assert
            Assert.IsFalse(Directory.Exists(folderPath + $"\\,{NewBase}"));
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