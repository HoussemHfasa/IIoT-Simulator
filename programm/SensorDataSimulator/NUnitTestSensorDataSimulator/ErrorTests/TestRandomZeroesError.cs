using System;
using NUnit.Framework;
using System.Collections.Generic;
using SensorDataSimulator;
using System.Text;

namespace NUnitTestSensorDataSimulator
{
    class TestRandomZeroesError
    {
        RandomZeroesError TestGenerator;
        Random Rand = new Random();
        double TestErrorRatio;
        int TestErrorLength;

        // Felder für Testdaten ohne Fehler
        double RandomAmplitude;
        double RandomPeriod;
        double Phase = 0;
        uint RandomAmmountofValues;
        SensorDataSimulator.HarmonicOscillation DataGenerator;
        List<double> TestData;

        [SetUp]
        public void Setup()
        {
            // Werte für Fehlererzeugung
            TestErrorRatio = Rand.NextDouble();
            TestErrorLength = Rand.Next(0, 10);
            TestGenerator = new RandomZeroesError(TestErrorRatio, TestErrorLength);

            // Messwerterzeugung ohne Fehler
            RandomAmplitude = Rand.NextDouble() * 1000000;
            RandomPeriod = Rand.Next(1, 100);
            RandomAmmountofValues = (uint)Rand.Next(1, 10000);
            HarmonicOscillation DataGenerator = new SensorDataSimulator.HarmonicOscillation(RandomAmplitude, RandomPeriod, Phase, RandomAmmountofValues);
            TestData = DataGenerator.GetSimulatorValues();
        }

        [Test]
        public void RandomZeroesError_NegativErrorRatio_Throws()
        {
            
            //Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => new RandomZeroesError(TestErrorRatio * -1, TestErrorLength));
        }

        [Test]
        public void RandomZeroesError_ErrorRatio_AboveOne_Throws()
        {
            //Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => new SensorDataSimulator.RandomZeroesError(TestErrorRatio + 1.1, TestErrorLength));
        }

        [Test]
        public void RandomZeroesError_ErrorRatio_One_Returns_AllZeroes()
        {
            TestGenerator = new RandomZeroesError(1.0, TestErrorLength);
            List<double> Result = TestGenerator.GetSensorDataWithErrors(TestData);
            for(int i = 1; i < Result.Count; i++)
            {
                if (Result[i] != 0.0)
                    Assert.Fail("Die generierte Liste enthälte nicht nur O'en");
            }
        }
    }
}
