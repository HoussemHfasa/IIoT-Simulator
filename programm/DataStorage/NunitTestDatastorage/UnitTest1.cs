using NUnit.Framework;
using DataStorage;
using Newtonsoft.Json;

namespace NunitTestDatastorage
{
    public class Tests
    {
        private DataStorage.DataStorage Storagetest;
        [SetUp]
        public void Setup()
        {
            Storagetest = new DataStorage.DataStorage();
        }

        [Test]
        public void It_should_serialize()
        {
            //Arrange
            string filepath = @"C:\Users\houss\Documents\gitlab\programm\DataStorage\Tests\teststorage.txt";
            double[] data = { 1, 2, 3, 6, 5, 47 };

            //Act
            Storagetest.JsonSerialize(data, filepath);
           
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
            string filepath = @"C:\Users\houss\Documents\gitlab\programm\DataStorage\Tests\teststorage.txt";
            double[] data = { 1, 2, 3, 6, 5, 47 };
            
            //Act
            var json = JsonConvert.SerializeObject(Storagetest.JsonDeserialize(filepath)) ;
            var data1 = JsonConvert.DeserializeObject<double[]>(json);

            //Assert
            Assert.That(data1, Is.EqualTo(data));
            
        }
    }
}