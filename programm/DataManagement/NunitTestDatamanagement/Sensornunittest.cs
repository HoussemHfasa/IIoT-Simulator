using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using DataManagement;


namespace NunitTestDatamanagement
{
    public class Sensornunittest
    {

        private DataManagement.Sensor<List> SensorTest;
  
        [SetUp]
        public void Setup()
        {

            SensorTest = new DataManagement.Sensor<List>();
            
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