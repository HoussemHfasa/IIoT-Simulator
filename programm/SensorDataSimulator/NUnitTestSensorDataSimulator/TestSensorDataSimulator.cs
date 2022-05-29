using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using SensorDataSimulator;
using System.Linq;

namespace NUnitTestSensorDataSimulator
{
    [TestFixture]
    class TestSensorDataSimulator
    {
        private SensorDataSimulator.SensorDataSimulator TestSimulator;
        
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

            RandomDampingRatio = rand.NextDouble()*0.5;
            RandomAmplitude = rand.NextDouble() * 2500;
            RandomPeriod = rand.Next(1, 8);
            RandomPhase = 0.0;
            RandomValueCount = rand.Next(8,5000);
            TestSimulator = new SensorDataSimulator.SensorDataSimulator(RandomValueCount);

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
            // Wenn Amplitude zwischen zwei Werten liegt, würde der Test fehlschlagen.. anders überprüfen
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

        public void GetHarmonicOscillation_negativeAmplitude_Throws()
        {
            // Negative Amplitude
            RandomAmplitude = RandomAmplitude * -1.0;

            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetHarmonicOscillation(RandomAmplitude, RandomPeriod, RandomPhase, RandomValueCount));

        }

        [Test]

        public void GetHarmonicOscillation_negativePeriod_Throws()
        {
            // Negative Periodendauer
            RandomPeriod = RandomPeriod * -1.0;

            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetHarmonicOscillation(RandomAmplitude, RandomPeriod, RandomPhase, RandomValueCount));

        }

        [Test]
        public void GetHarmonicOscillation_negativeValueCount_Throws()
        {
            // Negative Werteanzahl
            RandomValueCount = RandomValueCount * -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetHarmonicOscillation(RandomAmplitude, RandomPeriod, RandomPhase, RandomValueCount));


        }






        
        [Test]
        // 1. Test: Maximale Amplitude nicht größer als angegebene Amplitude
        public void GetDampedOscillation_Amplitudetest()
        {
            TestList = TestSimulator.GetDampedOscillation(RandomAmplitude, RandomDampingRatio, RandomPeriod, RandomPhase, RandomValueCount);
            Console.WriteLine("Amplitude sollte  kleiner oder gleich {0} sein. Amplitude ist {1}.", RandomAmplitude, TestList.Max());
            Assert.LessOrEqual(TestList.Max(),RandomAmplitude);
        }

        [Test]
        // 2. Test: Werteanzahl korrekt?
        public void GetDampedOscillation_Counttest()
        {
            TestList = TestSimulator.GetDampedOscillation(RandomAmplitude, RandomDampingRatio, RandomPeriod, RandomPhase, RandomValueCount);
            Console.WriteLine("Methode sollte {0} Werte liefern, Methode hat {1} Werte geliefert", RandomValueCount, TestList.Count());
            Assert.AreEqual(RandomValueCount, TestList.Count());

        }


        [Test]
        // 3. Test: 1. Schwingung > z.b. 3 Schwingung
        public void GetDampedOscillation_Dampes_Amplitude()
        {
            //Exception, wenn zu wenig Werte geliefert sind
            if (RandomPeriod*2 > RandomValueCount)
            {
                Assert.Fail("Die Werteanzahl ist zu kleine für eine Überprüfung");
            }
                
            
            // Wenn mehr als 2 vollständige Schwingungen vorhanden sind
            

            TestList = TestSimulator.GetDampedOscillation(RandomAmplitude, RandomDampingRatio, RandomPeriod, RandomPhase, RandomValueCount);
            for(int i = 0; i<RandomPeriod; i++)
            {
                if(Math.Abs(TestList[i]) < Math.Abs(TestList[i+(int)RandomPeriod]))
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
            RandomAmplitude = RandomAmplitude * -1.0;

            // Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetDampedOscillation(RandomAmplitude,RandomDampingRatio ,RandomPeriod, RandomPhase, RandomValueCount));

        }


        [Test] 

        public void GetDampedOscillation_negativeDampingratio_Throws()
        {
            // Negative Dämpfung 
            RandomDampingRatio = RandomDampingRatio * -1.0;

            // Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetDampedOscillation(RandomAmplitude, RandomDampingRatio, RandomPeriod, RandomPhase, RandomValueCount));

        }

        [Test]

        public void GetDampedOscillation_negativePeriod_Throws()
        {
            // Negative Periodendauer
            RandomPeriod = RandomPeriod * -1.0;
            // Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetDampedOscillation(RandomAmplitude,RandomDampingRatio, RandomPeriod, RandomPhase, RandomValueCount));

        }

        [Test]
        public void GetDampedOscillation_negativeValueCount_Throws()
        {
            // Negative Werteanzahl
            RandomValueCount = RandomValueCount * -1;
            // Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetDampedOscillation(RandomAmplitude,RandomDampingRatio, RandomPeriod, RandomPhase, RandomValueCount));


        }


        [Test]
        // 5. Werte eingeben und mit vorher berechnetem Ergebnis vergleichen
        public void GetDampedOscillation_ResultTest()
        {

            //Arrange
            double TestAmplitude = 250.0;
            double TestDampingRatio = 0.05;
            double TestPeriod = 10.0;
            int TestAmmountofVaulues = 250;
            List<double> Expected = new List<double>
                                    { 0, 139.78, 215.14, 204.65, 120.31, 0, -108.86, -167.55, -159.38, -93.70, 0 };

            // Act
            List<double> DampedTest = TestSimulator.GetDampedOscillation(TestAmplitude, TestDampingRatio, TestPeriod, 0, TestAmmountofVaulues);

            
            // Assert

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
            List<double> Testresult;


            // ACT
            Testresult = TestSimulator.GetStandardDeviationValues(TestMean, TestDeviation, TestCount);

            // ASSERT
            Console.WriteLine(String.Join(", ", Testresult)); 
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
