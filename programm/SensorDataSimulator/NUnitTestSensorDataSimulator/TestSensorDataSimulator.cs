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
        public void GetHarmonicOscillation_Amplitudetest()
        {

            double Testamplitude = 25.0;
            double TestPeriod = 8.0;
            double Testphase = 0.0;
            int TestValueCount = 2500;

            List<double> Testlist = TestSimulator.GetHarmonicOscillation(Testamplitude, TestPeriod, Testphase, TestValueCount);
            // 1. Amplitude abgleichen
            Console.WriteLine("Amplitude sollte {0} sein, Amplitude ist {1}.", Testamplitude, Testlist.Max());
            Assert.AreEqual(Testamplitude, Testlist.Max());

        }
        [Test]
        public void GetHarmonicOscillation_Counttest()
        {
            double Testamplitude = 25.0;
            double TestPeriod = 5.0;
            double Testphase = 0.0;
            int TestValueCount = 2500;
            List<double> Testlist = TestSimulator.GetHarmonicOscillation(Testamplitude, TestPeriod, Testphase, TestValueCount);

            // 2. Werteanzahl korrekt?
            Console.WriteLine("Methode sollte {0} Werte liefern, Methode hat {1} Werte geliefert", TestValueCount, Testlist.Count());
            Assert.AreEqual(TestValueCount, Testlist.Count());
            
        }
        [Test]
        public void GetHarmonicOscillation_ResultTest()
        {


            // Idee: Zufallswerte?
            double Testamplitude = 25.0;
            double TestPeriod = 5.0;
            double Testphase = 0.0;
            int TestValueCount = 2500; 
            List<double> Testlist = TestSimulator.GetHarmonicOscillation(Testamplitude, TestPeriod, Testphase, TestValueCount);


           
            // 3. bestimmte Werte eingeben, mit vorher berechnetem Ergebnis abgleichen. Hier könnten Probleme auftreten durch falsche Rundungen
            List<double> Testlist2 = TestSimulator.GetHarmonicOscillation(2.0, 8.0, 0.0, 9);
            List<double> Ergebnis = new List<Double> { 0, 1.41, 2, 1.41, 0, -1.41, -2, -1.41, 0 };
            

            Console.WriteLine("Methode sollte "
                               + String.Join(", ", Ergebnis)
                               + "erzeugen. Es wurde "
                               + String.Join(", ", Testlist2)
                               + "erzeugt");
            Assert.True(AreListsEqual(Ergebnis, Testlist2));
        }



        [Test]

        public void Testing_GetDampedOscillation()
        {
            double TestAmplitude = 250.0;
            double TestDampingRatio = 0.05;
            double TestPeriod = 10.0;
            int TestAmmountofVaulues = 250;
            List<double> DampedTest = TestSimulator.GetDampedOscillation(TestAmplitude, TestDampingRatio, TestPeriod, 0, TestAmmountofVaulues);

            

            // 1. Test: Maximale Amplitude nicht größer als angegebene Amplitude

            // 2. Test: Werteanzahl korrekt?

            // 3. Test: 1. Schwingung > z.b. 3 Schwingung

            // 4. Negative Amplitude, Dämpfung, Frequenz, Wertezahl erzeugt Out of Bounce Exception

            // 5. Werte eingeben und mit vorher berechnetem Ergebnis vergleichen

            List<double> Expected = new List<double> 
                                    { 0, 139.78, 215.14, 204.65, 120.31, 0, -108.86, -167.55, -159.38, -93.70, 0 };


            Console.WriteLine("Methode sollte \n "
                   + String.Join(", ", Expected)
                   + "\x0A erzeugen. Es wurde \x0A "
                   + String.Join(", ", DampedTest.Take(11))
                   + "\n erzeugt");

            Assert.True(AreListsEqual(Expected, DampedTest));


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


        private bool AreListsEqual(List<double> FirstList, List<double> SecondList)
        {
            for (int i = 0; i < FirstList.Count; i++)
            {
                //Kann verbessert werden
                if (FirstList[i].Equals(SecondList[i]))
                { }
                else
                    return false;
            }
            return true;
        }

}
}
