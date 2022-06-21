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
    class LightsensorTest
    {
        private SensorAndSensorgroup.Sensor<int> Lightsensortest = new SensorAndSensorgroup.BrightnessSensor();

        SensorAndSensorgroup.Sensor<int> Sensor = new BrightnessSensor();
        string Filepath;
        [SetUp]
        public void Setup()
        {

            //Arrange
            Sensor.Sensor_id = "1561561";
            Sensor.Topic = "Haus//Zimmer1";
            Sensor.Timeinterval = 10;
            Filepath = AppDomain.CurrentDomain.BaseDirectory;
        }

         [Test]
           public void It_should_save_the_sensor()
           {
            //Act
            Lightsensortest.JsonSerialize(Sensor, Filepath);

               //Assert
               Assert.Pass();
           }
     

    }
}

