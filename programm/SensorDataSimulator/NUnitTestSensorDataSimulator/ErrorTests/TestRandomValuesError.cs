using NUnit.Framework;
using SensorDataSimulator;
using System;
using System.Collections.Generic;

namespace NUnitTestSensorDataSimulator
{
    public class TestRandomValuesError
    {
        RandomValuesError ErrorGenerator;
        Random Rand = new Random();
        double RandomErrorRatio;
        int RandomErrorLength;
        double RandomMaxError;
        double RandomMinError;

        double RandomAmplitude;
        double RandomPeriod;
        double Phase = 0;
        uint RandomAmmountofValues;
        SensorDataSimulator.HarmonicOscillation DataGenerator;
        List<double> TestData;

        [SetUp]
        public void Setup()
        {
            //Varialblen für FehlerwertErzeugung generieren
            RandomErrorRatio = Rand.NextDouble();
            RandomErrorLength = Rand.Next(0, 10);

            RandomMaxError = Rand.Next() * 1000000;
            // 50% Chance auf negativen Wert
            if (Rand.NextDouble() <= 0.5)
                RandomMaxError *= -1;

            RandomMinError = Rand.Next() * 1000000;
            if (Rand.NextDouble() <= 0.5)
                RandomMinError *= -1;
            //prüfen ob MaxWert tatsächlich größer ist als Minwert, wenn nicht, tauschen
            double temp;
            if(RandomMinError > RandomMaxError)
            {
                // Dreieckstausch
                temp = RandomMaxError;
                RandomMaxError = RandomMinError;
                RandomMinError = temp;
            }

            ErrorGenerator = new SensorDataSimulator.RandomValuesError(RandomErrorRatio, RandomErrorLength, RandomMaxError, RandomMinError);

            //Werteerzeugung für Zufällige Messwerte
            RandomAmplitude = Rand.NextDouble() * 1000000;
            RandomPeriod = Rand.Next(1, 100);
            RandomAmmountofValues = (uint)Rand.Next(1, 10000);
            DataGenerator = new SensorDataSimulator.HarmonicOscillation(RandomAmplitude, RandomPeriod, Phase, RandomAmmountofValues);
            TestData = DataGenerator.GetSimulatorValues();
        }
        
            [Test]

        public void RandomError_NegativErrorRatio_Throws()
        {
            //Negative ErrorRatio
            ErrorGenerator = new SensorDataSimulator.RandomValuesError(RandomErrorRatio * -1, RandomErrorLength, RandomMaxError, RandomMinError);

            //Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => ErrorGenerator.GetSensorDataWithErrors(TestData));

        }
        [Test]
        public void RandomError_ErrorRatioAboveOne_Throws()
        {
            //Negative ErrorRatio
            ErrorGenerator = new SensorDataSimulator.RandomValuesError(RandomErrorRatio +1.1, RandomErrorLength, RandomMaxError, RandomMinError);

            //Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => ErrorGenerator.GetSensorDataWithErrors(TestData));

        }
    }
}