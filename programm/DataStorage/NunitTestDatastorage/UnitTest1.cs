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
        [SetUp]
        public void Setup()
        {
            Storagetest = new DataStorage.DataStorage<double>();
        }

        [Test]
        public void It_should_serialize()
        {
            //Arrange
            string filename = @"teststorage.txt";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + filename;
            Dictionary<DateTime, List<double>> data = new Dictionary<DateTime, List<double>>();
            data.Add(DateTime.Parse("01 02 1875"), new List<double> { 1, 2, 4, 8, 96, 1457 });

            //Act
            Storagetest.JsonSerialize(data, filePath);
            Console.WriteLine(filePath);

            //Assert
            Assert.Pass();
        }
        [Test]
        public void It_should_serialize_then_deserialize()
        {
            //Arrange
            double[] data = { 1, 2, 3, 6, 5, 47 };

            //Act
            var json = JsonConvert.SerializeObject(data);
            var deserializeddata = JsonConvert.DeserializeObject<double[]>(json);


            //Assert
            Assert.That(deserializeddata, Is.EqualTo(data));
        }
        [Test]
        public void It_should_deserialize()
        {
            //Arrange
           
            string filename = @"teststorage.txt";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + filename;
            Dictionary<DateTime, List<double>> data = new Dictionary<DateTime, List<double>>();
            data.Add(DateTime.Parse("01 02 1875"), new List<double> { 1, 2, 4, 8, 96, 1457 });

            //Act
            Dictionary<DateTime, List<double>> data1 = Storagetest.JsonDeserialize(filePath);

            //Assert
            Assert.That(data1.Values, Is.EqualTo(data.Values));
            Assert.That(data1.Keys, Is.EqualTo(data.Keys));
            
        }
    }
}