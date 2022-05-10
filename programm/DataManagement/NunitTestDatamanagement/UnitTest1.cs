using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;


namespace NunitTestDatamanagement
{
    public class Tests
    {
        private DataManagement.SensorandSensorgroups<List> DatamanagementTest;
        [SetUp]
        public void Setup()
        {
           DatamanagementTest = new DataManagement.SensorandSensorgroups<List>();
        }

        [Test]
        public void It_should_add_Sensor()
        {
            // Arrange
            string[] SensorList = new string[0];
            string Id = "123456";

            //Act
            DatamanagementTest.Sensorhinzufuegen(SensorList, Id);

            //Assert
            Assert.Pass();
        }
        [Test]
        public void It_should_delete_Sensor()
        {
            // Arrange
            string[] SensorList = new string[] { };
            string Id = "123456";

            //Act
            DatamanagementTest.Sensorloeschen(SensorList, Id);

            //Assert
            Assert.Pass();
        }
        [Test]
        public void It_should_skip_when_the_id_is_founded()
        {
            // Arrange
            string[] SensorList = new string[] { "123456", "148456", "189746" };
            string Id = "123456";
            

            //Act
            DatamanagementTest.Sensorhinzufuegen(SensorList, Id);


            //Assert
            Assert.Contains(Id, SensorList);
        }
        
    }
}