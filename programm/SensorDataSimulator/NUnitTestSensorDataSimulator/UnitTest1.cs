using NUnit.Framework;
using SensorDataSimulator;

namespace NUnitTestSensorDataSimulator
{
    public class Tests
    {
        private SensorDataSimulator.SensorDataSimulator TestSimulator;
        [SetUp]
        public void Setup()
        {
            //ARRANGE
            TestSimulator = new SensorDataSimulator.SensorDataSimulator();
        }

        [Test]
        public void Using_GetStandardDeviation_Updates_properties()
        {
            // ARRANGE
            double TestMean = 15;
            double TestDeviation = 7;
            int TestCount = 100;
            

            // ACT
            TestSimulator.GetStandardDeviationValues(TestMean, TestDeviation, TestCount);

            // ASSERT

            Assert.AreEqual(TestSimulator.AmmountofValues, TestCount);


        }



        [Test]
        public void Using_SetSensorErrors_ErrorRatio_OutofBounds_NotSetting_Errors()
        {




        }
    }
}