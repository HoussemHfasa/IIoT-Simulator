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
    class BasenamelistTest
    {
        private SensorAndSensorgroup.Sensorgroups Basenametest = new Sensorgroups();
        DataStorage<string> store = new DataStorage<string>();
        string Basename1;
        string Basename2;
        string Basename3;
        string Basename4;
        List<string> listname = new List<string>();

        string Filepath;
        [SetUp]
        public void Setup()
        {

            //Arrange
            Basename1 = "Haus1";
            Basename2 = "Haus2";
            Basename3 = "Haus3";
            Basename4 = "Haus4";
            Filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SensorGroups\");
        }

        [Test]
        public void It_should_save_the_BasenameList()
        {
            //Act
            Basenametest.AddBase(Basename1);
            listname = store.BasenameDeserialize(Filepath);

            //Assert
            Assert.That((File.Exists(Filepath + "List of Basenames"))&&(listname.Contains(Basename1)));
        }
        [Test]
        public void It_should_save_an_existing_Basename()
        {
            //Act
            Basenametest.AddBase(Basename4);
            Basenametest.AddBase(Basename4);
            listname = store.BasenameDeserialize(Filepath);
            //Assert
            Assert.That(listname.Where(s => s != null && s.StartsWith("Haus4")).Count()==1);
        }
        [Test]
        public void It_should_delete_basename_from_the_BasenameList()
        {
            //Act
            Basenametest.AddBase(Basename2);
            Basenametest.AddBase(Basename3);
            Basenametest.DeleteBase(Basename3);
            listname = store.BasenameDeserialize(Filepath);
            //Assert
            Assert.That((!listname.Contains(Basename3)) && (listname.Contains(Basename2)));
        }
        [Test]
        public void It_should_delete_not_existing_basename_from_the_BasenameList()
        {
            //Act
            Basenametest.DeleteBase(Basename3);
            Basenametest.DeleteBase(Basename3);
            listname = store.BasenameDeserialize(Filepath);
            //Assert
            Assert.That((!listname.Contains(Basename3)));
        }

    }
}
