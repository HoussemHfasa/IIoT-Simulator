using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SensorDataSimulator;
using System.Linq;

namespace NUnitTestSensorDataSimulator
{
    class TestSensorDataSimulator
    {
        private SensorDataSimulator.SensorDataSimulator TestSimulator;

        [SetUp]
        public void Setup()
        {
            //ARRANGE
            TestSimulator = new SensorDataSimulator.SensorDataSimulator();
            
        }

        [Test]

        public void Testing_GetHarmonicOscillation()
        {


            // Idee: Zufallswerte?
            double Testamplitude = 25.0;
            double TestPeriod = 5.0;
            double Testphase = 0.0;
            int TestValueCount = 2500; 
            List<double> Testlist = TestSimulator.GetHarmonicOscillation(Testamplitude, TestPeriod, Testphase, TestValueCount);


            // 1. Amplitude abgleichen
            Console.WriteLine("Amplitude sollte {0} sein, Amplitude ist {1}.", Testamplitude, Testlist.Max());
            Assert.AreEqual(Testamplitude, Testlist.Max());
            


            // 2. Werteanzahl korrekt?
            Assert.AreEqual(TestValueCount, Testlist.Count());
            Console.WriteLine("Methode sollte {0} Werte liefern, Methode hat {1} Werte geliefert", TestValueCount, Testlist.Count());

            // 3. bestimmte Werte eingeben, mit vorher berechnetem Ergebnis abgleichen. Hier könnten Probleme auftreten durch falsche Rundungen
            List<double> Testlist2 = TestSimulator.GetHarmonicOscillation(2.0, 8.0, 0.0, 8);
            List<double> Ergebnis = new List<Double> { 0, 1.41, 2, 1.41, 0, 1.41, -2, -1.41, 0 };
            bool TestPassed = true;
            for (int i = 0; i < Ergebnis.Count; i++)
            {
                if (Testlist[i] != Ergebnis[i])
                    TestPassed = false;
            }
            Assert.True(TestPassed);

            Console.WriteLine("Methode sollte "
                               + String.Join(", ", Ergebnis)
                               + "erzeugen. Es wurde "
                               + String.Join(", ", Testlist2)
                               + "erzeugt");

        }



        [Test]

        public void Testing_GetDampedOscillation()
        {
            TestSimulator.GetDampedOscillation(15.0, 0.6, 10.0, 0, 250);

            // 1. Test: Maximale Amplitude nicht größer als angegebene Amplitude

            // 2. Test: Werteanzahl korrekt?

            // 3. Test: 1. Schwingung > z.b. 3 Schwingung

            // 4. Negative Amplitude, Dämpfung, Frequenz, Wertezahl erzeugt Out of Bounce Exception

            // 5. Werte eingeben und mit vorher berechnetem Ergebnis vergleichen




        }


        [Test]

        public void Testing_GetRandomBoolValues()
        {
            Assert.Pass(" Test erfolgreich");
            Assert.Pass();
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
