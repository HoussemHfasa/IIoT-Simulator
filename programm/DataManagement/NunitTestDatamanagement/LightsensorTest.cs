using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using SensorAndSensorgroup;
//using DataStorage;




namespace NunitTestDatamanagement
{
    /// <summary>
    /// //Der gleiche Test für alle Sensorentype
    /// </summary>
    class LightsensorTest
    {
        private SensorAndSensorgroup.Sensor<int> Lightsensortest = new SensorAndSensorgroup.BrightnessSensor();

        SensorAndSensorgroup.Sensor<int> Sensor1 = new BrightnessSensor();
        SensorAndSensorgroup.Sensor<int> Sensor2 = new BrightnessSensor();
        ISensor<int> Sensor3 = new BrightnessSensor();
        string Filepath;
        [SetUp]
        public void Setup()
        {

            //Arrange
            Sensor1.Sensor_id = "1561561";
            Sensor1.Topic = "Haus//Zimmer1";
            Sensor1.Timeinterval = 10;
            Sensor1.SetValues(new List<int> { 185, 48, 5, 16, 49, 6 });
            Filepath = AppDomain.CurrentDomain.BaseDirectory;
        }

         [Test]
           public void It_should_save_the_sensor()
           {
            //Act
            Lightsensortest.JsonSerialize(Sensor1, Filepath);

            //Assert
            Assert.That(File.Exists(Filepath + Sensor1.Sensor_id));
           }
        [Test]
        public void It_should_load_the_sensor()
        {
            //Act
           Sensor3= Lightsensortest.JsonDeserialize(Filepath,Sensor1.Sensor_id);

            //Assert
            Assert.Pass();
        }


    }
}

