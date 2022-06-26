using NUnit.Framework;
using SensorDataSimulator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestSensorDataSimulator
{
    public class TestRandomValuesError
    {
        // Benötigte Variablen/Felder
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
            //ErrorRatio über 1
            ErrorGenerator = new SensorDataSimulator.RandomValuesError(RandomErrorRatio +1.1, RandomErrorLength, RandomMaxError, RandomMinError);

            //Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => ErrorGenerator.GetSensorDataWithErrors(TestData));

        }

        [Test]
        // Debugtest zum Anzeigen des Ergebnisses
        public void RandomError_Print_Result_Test()
        {
            List<double> TestResult;
            DataGenerator.AmmountofValues = 25;
            TestData = DataGenerator.GetSimulatorValues();
            Console.WriteLine("Die Methode hat folgende Werte erhalten: \n "

                   + String.Join(", ", TestData)
                   + "\n"
                   );
            SensorDataSimulator.RandomValuesError ErrorsGenerator = new SensorDataSimulator.RandomValuesError(0.33, RandomErrorLength, RandomMaxError, RandomMinError);
            TestResult = ErrorGenerator.GetSensorDataWithErrors(TestData);
            Console.WriteLine("Die Methode hat folgende Werte erhalten: \n "

                   + String.Join(", ", TestData)
                   + "\n Die Methode hat daraus folgende Werte geliefert: \n"
                   + String.Join(", ", TestResult)
                   + "\x0A erzeugt");
        }

        


        
        [Test]
        public void RandomError_Negative_MinMax_Create_Negtive_Errors()
        {
            double TestMinError = -500.0;
            double TestMaxError = -20.0;
            List<double> TestResult;
            DataGenerator.AmmountofValues = 20;
            TestData = DataGenerator.GetSimulatorValues();
            SensorDataSimulator.RandomValuesError EGenerator = new SensorDataSimulator.RandomValuesError(0.99, RandomErrorLength, TestMaxError, TestMinError);
            
            TestResult = EGenerator.GetSensorDataWithErrors(TestData);
            Console.WriteLine("Die Fehlerwerte sollten zwischen  \n "
                   + TestMaxError
                   + "\n und "
                   +TestMinError
                   + "\n liegen."

                   
                   + "\n Die Methode folgende Fehler erzeugt: \n"
                   + String.Join(", ", TestResult)
                   + "\x0A erzeugt");

            Assert.Less(TestResult.Max(), TestMaxError);
        }


    }

}