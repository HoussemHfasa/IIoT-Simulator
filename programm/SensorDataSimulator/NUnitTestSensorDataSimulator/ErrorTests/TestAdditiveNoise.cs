using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NUnitTestSensorDataSimulator
{
    class TestAdditiveNoise
    {
        // Felder für Testdaten ohne Fehler
        double RandomAmplitude;
        double RandomPeriod;
        double Phase = 0;
        uint RandomAmmountofValues;
        SensorDataSimulator.HarmonicOscillation DataGenerator;
        List<double> TestData;
        List<double> TestData2;
        Random Rand = new Random();

        SensorDataSimulator.AdditiveNoise ErrorGenerator;

        [SetUp]
        public void Setup()
        {
            RandomAmplitude = Rand.NextDouble() * 1000000;
            RandomPeriod = Rand.Next(1, 100);
            RandomAmmountofValues = (uint)Rand.Next(10, 10000);
            DataGenerator = new SensorDataSimulator.HarmonicOscillation(RandomAmplitude, RandomPeriod, Phase, RandomAmmountofValues);
            TestData = DataGenerator.GetSimulatorValues();
            
        }

        [Test]
        public void AdditiveNoise_AddingZeroes_keeps_Data_unchanged()
        {
            // Mit 0en addieren soll die Daten nicht ändern
            List<double> Result;
            TestData2 = new List<double> { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };
            ErrorGenerator = new SensorDataSimulator.AdditiveNoise(TestData2);
            Result = ErrorGenerator.GetSensorDataWithErrors(TestData);

            //Sind die Werte der beiden Listen identisch?
            Assert.IsTrue(Enumerable.SequenceEqual(Result, TestData));
        }

        
        [Test]
        // Test ob die Werte der Listen addiert werden
        public void AdditiveNoise_Adds_Values()
        {
            List<double> Result;
            TestData = new List<double> { 9, 2 };
            TestData2 = new List<double> { 15, -7 };
            List<double> Expected = new List<double> { 24, -5 };
            ErrorGenerator = new SensorDataSimulator.AdditiveNoise(TestData);
            Result = ErrorGenerator.GetSensorDataWithErrors(TestData2);
            Console.WriteLine("Liste sollte " + Expected[0] + ", " + Expected[1] + "enthalten, Liste  enthält " +
                Result[0] + ", " + Result[1]);
            Assert.IsTrue(Enumerable.SequenceEqual(Expected, Result));
        }



    }
}
