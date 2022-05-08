using NUnit.Framework;
using SensorDataSimulator;

namespace NUnitTestSensorDataSimulator
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Using_Normalverteilung_Updates_properties()
        {
            // ARRANGE
            double TestMean = 15;
            double TestDeviation = 7;
            int TestCount = 100;
            DataSimulator TestSimulator = new DataSimulator();

            // ACT
            TestSimulator.Normalverteilung(TestMean, TestDeviation, TestCount);

            // ASSERT
            Assert.AreEqual(TestMean, TestSimulator.Standardabweichung);
            Assert.AreEqual(TestSimulator.Standardabweichung, TestDeviation);
            Assert.AreEqual(TestSimulator.Werteanzahl, TestCount);

            
        }
    }
}