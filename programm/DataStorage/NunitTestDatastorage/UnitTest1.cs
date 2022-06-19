using NUnit.Framework;
using DataStorage;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using Newtonsoft.Json.Linq;



namespace NunitTestDatastorage
{
    public class Tests
    {
        private DataStorage.DataStorage<double> Storagetest=new DataStorage<double>();
        string Sensortype;
        string filePath ;
        Dictionary<DateTime, List<double>> data = new Dictionary<DateTime, List<double>>();
        Dictionary<DateTime, List<double>> data2 = new Dictionary<DateTime, List<double>>();
        Dictionary<DateTime, List<double>> data3 = new Dictionary<DateTime, List<double>>();

        [SetUp]
        public void Setup()
        {
            //Arrange
            Sensortype = "Temperaturesensor";
            Storagetest = new DataStorage.DataStorage<double>();
         filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Tests\");
        data.TryAdd(DateTime.Parse("01 01 1285"), new List<double> { 1, 2, 4, 8, 946, 1457 });
            data3.TryAdd(DateTime.Parse("01 01 1285"), new List<double> { 1, 5152, 4, 8, 946, 1457 });
        }

        [Test]
        public void It_should_serialize()
        {
            //Act
            Storagetest.JsonSerialize(data, filePath,Sensortype);
            Storagetest.JsonSerialize(data3, filePath, Sensortype);
            //Assert
            Assert.That(File.Exists(filePath+Sensortype));
        }
        
        [Test]
        public void It_should_deserialize()
        {
            //Act
            data2 = Storagetest.JsonDeserialize(filePath,Sensortype);

            //Assert
            Assert.AreEqual(data2,data);
           
        }
    }
}