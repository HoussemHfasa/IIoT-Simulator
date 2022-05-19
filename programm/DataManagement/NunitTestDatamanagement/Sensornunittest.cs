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
    public class Sensornunittest
    {

        private SensorAndSensorgroup.Sensor<List> SensorTest;
  
        [SetUp]
        public void Setup()
        {

           // SensorTest = new SensorAndSensorgroup.Sensor<List>();
            
        }

        [Test]
        public void test()
        {
            // Arrange
          

            //Act
            

            //Assert
            Assert.Pass();
        }
  

    }
}