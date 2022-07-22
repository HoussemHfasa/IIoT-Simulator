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
using SensorAndSensorgroup;




namespace NunitTestDatamanagement
{
    /// <summary>
    /// //Der gleiche Test für alle Sensorentype
    /// </summary>
    class TreenodeUnittest
    {
       
        private SensorAndSensorgroup.Sensorgroups sensorgroupstest = new Sensorgroups();
        private DataStorage.DataStorage Datastoragetest = new DataStorage.DataStorage();
        
        SensorAndSensorgroup.Sensor<int> Sensor1 = new BrightnessSensor();
        SensorAndSensorgroup.Sensor<double> Sensor2 = new TemperatureSensor();
        public Dictionary<string, NAryTree> allTree = new Dictionary<string, NAryTree>();
        public Dictionary<string, TreeNode> allchildren = new Dictionary<string, TreeNode>();
        public Dictionary<string, dynamic> allsensor = new Dictionary<string, dynamic>();
        public Dictionary<string, int> basenames_children = new Dictionary<string, int>();
        public List<string> basenames = new List<string>();
        [SetUp]
        public void Setup()
        {

            //Arrange
            Sensor1.Sensor_id = "1561561";
            Sensor1.Topic = "Haus//Zimmer1";
            Sensor1.Timeinterval = 10;
            Sensor1.SetValues(new List<int> { 185, 48, 5, 16, 49, 6 });
            Sensor2.Sensor_id = "561151";
            Sensor2.Topic = "Haus//Zimmer2";
            Sensor2.Timeinterval = 10;
            Sensor2.SetValues(new List<double> { 90, 48, 5, 16, 49, 6 });
        }

        [Test]
        public void It_should_save_the_Tree()
        {
            //Act
            sensorgroupstest.Add_new_Base("Home");
            sensorgroupstest.Add_new_Base("Home2");
            sensorgroupstest.Add_new_Node("Home","Zimmer1"); 
            sensorgroupstest.Add_new_Node("Home", "Wohnzimmer");
            sensorgroupstest.Add_new_sensor("Zimmer1",Sensor1);
            sensorgroupstest.Add_new_sensor("Whonzimmer", Sensor2);
            Datastoragetest.Save(sensorgroupstest.allTree,sensorgroupstest.allchildren,sensorgroupstest.allsensor,sensorgroupstest.basenames,sensorgroupstest.basenames_children);

            //Assert
            Assert.That(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "alltree"));
        }
        [Test]
        public void It_should_load_the_Tree()
        {
            //Act
            allTree = Datastoragetest.Load_alltree();
            allsensor = Datastoragetest.Load_allsensor();
            allchildren = Datastoragetest.Load_allchildren();
            basenames = Datastoragetest.Load_Basenames();
            basenames_children = Datastoragetest.Load_Basenames_children();

            //Assert
           // Assert.AreEqual(allTree,sensorgroupstest.allTree);
            //Assert.That(allTree.Equals(sensorgroupstest.allTree));
       //     Assert.AreEqual(allchildren, sensorgroupstest.allchildren);
         //   Assert.AreEqual(allsensor, sensorgroupstest.allsensor);
          //  Assert.AreEqual(basenames, sensorgroupstest.basenames);
            Assert.AreEqual(basenames_children, sensorgroupstest.basenames_children);
        }
      

    }
}

