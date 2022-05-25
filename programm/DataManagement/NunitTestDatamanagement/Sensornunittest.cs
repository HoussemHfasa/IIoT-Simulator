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

        private SensorAndSensorgroup.Currentsensor SensorTest = new Currentsensor();

        [SetUp]
        public void Setup()
        {

            SensorTest = new Currentsensor();

        }

      /*  [Test]
         public void test()
         {
             // Arrange


             //Act


             //Assert
             Assert.Pass();
         }
   */

    }
}