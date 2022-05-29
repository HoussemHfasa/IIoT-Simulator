using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SensorDataSimulator;
using System.Linq;

namespace NUnitTestSensorDataSimulator
{
    public class TestHarmonicOscillation
    {
        private SensorDataSimulator.HarmonicOscillation TestSimulator;

        // Idee: Test mit zufällig erzeugten Werten, in Setup() mit Werten versehen
        Random rand = new Random();
        double RandomAmplitude;
        double RandomPeriod;
        double RandomPhase;
        uint RandomValueCount;
        List<double> TestList;
        [SetUp]
        public void Setup()
        {
            //ARRANGE

          
            RandomAmplitude = rand.NextDouble() * 2500;
            RandomPeriod = rand.Next(1, 8);
            RandomPhase = 0.0;
            RandomValueCount = Convert.ToUInt32(rand.Next(8, 5000));
            TestSimulator = new SensorDataSimulator.HarmonicOscillation(RandomAmplitude,RandomPeriod, RandomPhase, RandomValueCount);

        }

        [Test]
        public void GetHarmonicOscillation_Amplitudetest()
        {

            List<double> Testlist = TestSimulator.GetSimulatorValues();
            // 1. Amplitude abgleichen
            Console.WriteLine("Amplitude sollte {0} sein, Amplitude ist {1}.", RandomAmplitude, Testlist.Max());
            // Wenn Amplitude zwischen zwei Werten liegt, würde der Test fehlschlagen.. anders überprüfen
            Assert.LessOrEqual(Testlist.Max(), RandomAmplitude);

        }
        [Test]
        public void GetHarmonicOscillation_Counttest()
        {
     
            
            List<double> Testlist = TestSimulator.GetSimulatorValues();

            // 2. Werteanzahl korrekt?
            Console.WriteLine("Methode sollte {0} Werte liefern, Methode hat {1} Werte geliefert", RandomValueCount, Testlist.Count());
            Assert.AreEqual(RandomValueCount, Testlist.Count());

        }
        [Test]
        public void GetHarmonicOscillation_ResultTest()
        {


            // Idee: Zufallswerte?
            double Testamplitude = 25.0;
            double TestPeriod = 5.0;
            double Testphase = 0.0;
            uint TestValueCount = 2500;
            TestSimulator = new SensorDataSimulator.HarmonicOscillation(Testamplitude, TestPeriod, Testphase, TestValueCount);
            



            // 3. bestimmte Werte eingeben, mit vorher berechnetem Ergebnis abgleichen. Hier könnten Probleme auftreten durch falsche Rundungen
            TestSimulator = new SensorDataSimulator.HarmonicOscillation(2.0, 8.0, 0.0, 9);
            List<double> Testlist2 = TestSimulator.GetSimulatorValues();
            List<double> Ergebnis = new List<Double> { 0, 1.41, 2, 1.41, 0, -1.41, -2, -1.41, 0 };


            Console.WriteLine("Methode sollte "
                               + String.Join(", ", Ergebnis)
                               + "erzeugen. Es wurde "
                               + String.Join(", ", Testlist2)
                               + "erzeugt");
            Assert.True(AreListsEqual(Ergebnis, Testlist2));
            
        }

        [Test]

        public void GetHarmonicOscillation_negativeAmplitude_Throws()
        {
            // Negative Amplitude
            RandomAmplitude *= -1.0;
            TestSimulator = new SensorDataSimulator.HarmonicOscillation(RandomAmplitude, RandomPeriod, RandomPhase, RandomValueCount);
            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetSimulatorValues());

        }

        [Test]

        public void GetHarmonicOscillation_negativePeriod_Throws()
        {
            // Negative Periodendauer
            RandomPeriod *= -1.0;
            TestSimulator = new SensorDataSimulator.HarmonicOscillation(RandomAmplitude, RandomPeriod, RandomPhase, RandomValueCount);
            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetSimulatorValues());

        }

        private bool AreListsEqual(List<double> FirstList, List<double> SecondList)
        {
            for (int i = 0; i < FirstList.Count; i++)
            {
                // Math.Abs(x-y) < 1e-12 , Keine Rundungen
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