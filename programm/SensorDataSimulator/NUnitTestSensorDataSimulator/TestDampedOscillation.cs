using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SensorDataSimulator;
using System.Linq;

namespace NUnitTestSensorDataSimulator
{
    public class TestDampedOscillation
    {
        private SensorDataSimulator.DampedOscillation TestSimulator;

        // Idee: Test mit zufällig erzeugten Werten, in Setup() mit Werten versehen
        Random rand = new Random();
        double RandomDampingRatio;
        double RandomAmplitude;
        double RandomPeriod;
        double RandomPhase;
        uint RandomValueCount;
        List<double> TestList;

        [SetUp]
        public void Setup()
        {
            //ARRANGE

            RandomDampingRatio = rand.NextDouble() * 0.5;
            RandomAmplitude = rand.NextDouble() * 2500;
            RandomPeriod = rand.Next(1, 8);
            RandomPhase = 0.0;
            RandomValueCount = Convert.ToUInt32(rand.Next(8, 5000));
            TestSimulator = new SensorDataSimulator.DampedOscillation(RandomDampingRatio, RandomAmplitude, RandomPeriod, RandomPhase, RandomValueCount);

        }

        [Test]
        // 1. Test: Maximale Amplitude nicht größer als angegebene Amplitude
        public void GetDampedOscillation_Amplitudetest()
        {
            TestList = TestSimulator.GetSimulatorValues();
            Console.WriteLine("Amplitude sollte  kleiner oder gleich {0} sein. Amplitude ist {1}.", RandomAmplitude, TestList.Max());
            Assert.LessOrEqual(TestList.Max(), RandomAmplitude);
        }

        [Test]
        // 2. Test: Werteanzahl korrekt?
        public void GetDampedOscillation_Counttest()
        {
            TestList = TestSimulator.GetSimulatorValues();
            Console.WriteLine("Methode sollte {0} Werte liefern, Methode hat {1} Werte geliefert", RandomValueCount, TestList.Count());
            Assert.AreEqual(RandomValueCount, TestList.Count());

        }


        [Test]
        // 3. Test: 1. Schwingung > z.b. 3 Schwingung
        public void GetDampedOscillation_Dampes_Amplitude()
        {
            //Exception, wenn zu wenig Werte geliefert sind
            if (RandomPeriod * 2 > RandomValueCount)
            {
                Assert.Fail("Die Werteanzahl ist zu kleine für eine Überprüfung");
            }


            // Wenn mehr als 2 vollständige Schwingungen vorhanden sind


            TestList = TestSimulator.GetSimulatorValues();
            for (int i = 0; i < RandomPeriod; i++)
            {
                if (Math.Abs(TestList[i]) < Math.Abs(TestList[i + (int)RandomPeriod]))
                {
                    // Keine Dämpfung vorhanden
                    Assert.Fail("2. Schwingung ist größer als die 1. Schwingung");
                }
            }

            //Dämpfung vorhanden
            Assert.Pass("Die Schwingung wird gedämpft");
        }



        [Test]
        public void GetDampedOscillation_negativeAmplitude_Throws()
        {
            // Negative Amplitude
            RandomAmplitude *= -1.0;
            TestSimulator = new DampedOscillation(RandomAmplitude, RandomDampingRatio, RandomPeriod, RandomPhase, RandomValueCount);
            // Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetSimulatorValues());

        }


        [Test]

        public void GetDampedOscillation_negativeDampingratio_Throws()
        {
            // Negative Dämpfung 
            RandomDampingRatio *= -1.0;
            TestSimulator = new DampedOscillation(RandomAmplitude, RandomDampingRatio, RandomPeriod, RandomPhase, RandomValueCount);
            // Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetSimulatorValues());

        }

        [Test]

        public void GetDampedOscillation_negativePeriod_Throws()
        {
            // Negative Periodendauer
            RandomPeriod *= -1.0;
            TestSimulator = new DampedOscillation(RandomAmplitude, RandomDampingRatio, RandomPeriod, RandomPhase, RandomValueCount);
            // Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetSimulatorValues());

        }




        [Test]
        // 5. Werte eingeben und mit vorher berechnetem Ergebnis vergleichen
        public void GetDampedOscillation_ResultTest()
        {

            //Arrange
            double TestAmplitude = 250.0;
            double TestDampingRatio = 0.05;
            double TestPeriod = 10.0;
            uint TestAmmountofVaulues = 250;
            List<double> Expected = new List<double>
                                    { 0, 139.78, 215.14, 204.65, 120.31, 0, -108.86, -167.55, -159.38, -93.70, 0 };
            TestSimulator = new DampedOscillation(TestAmplitude, TestDampingRatio, TestPeriod, 0.0, TestAmmountofVaulues);
            // Act
            List<double> DampedTest = TestSimulator.GetSimulatorValues();


            // Assert

            Console.WriteLine("Methode sollte \n "
                   + String.Join(", ", Expected)
                   + "\x0A erzeugen. Es wurde \x0A "
                   + String.Join(", ", DampedTest.Take(11))
                   + "\n erzeugt");

            Assert.True(AreListsEqual(Expected, DampedTest));


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