using NUnit.Framework;
using DataStorage;

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
        public void Test1()
        {
            Assert.Pass();
        }
    }
}