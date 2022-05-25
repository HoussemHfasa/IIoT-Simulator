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


        private SensorAndSensorgroup.Sensorgroups SensorgroupsTest;
        [SetUp]
        public void Setup()
        {

            SensorgroupsTest = new Sensorgroups();
        }

        [Test]
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
        public void It_should_Addbase()
        {
            // Arrange
            string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\";
            string NewBase = "Wohnung X";


            //Act
            SensorgroupsTest.AddBase(NewBase);


            //Assert
            Assert.IsTrue(Directory.Exists(folderPath));
        }
        [Test]
        public void It_should_Addnode()
        {
            // Arrange
            string folderPath = @"C:\Users\houss\Documents\gitlab\programm\DataManagement\Tests\";
            string NewBase = "Wohnung X";
            string NewNode = "Zimmer Y";


            //Act
            SensorgroupsTest.AddNode(NewNode, NewBase);


            //Assert
            Assert.IsTrue(Directory.Exists(folderPath +NewBase));
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
       /* public void It_should_delete_Base()
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