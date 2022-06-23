using NUnit.Framework;
using SensorDataSimulator;
using System;
using System.Collections.Generic;

namespace NUnitTestSensorDataSimulator
{
    public class TestRandomBool
    {
        double RandomToggle;
        uint RandomValueCount;
        Random Rand = new Random();
        RandomBool TestSimulator;

        [SetUp]
        public void Setup()
        {
            RandomToggle = Rand.NextDouble();
            RandomValueCount = (uint)Rand.Next(1,10000);
            
        }

        [Test]

        public void RandomBool_negative_TogglePorpability_Throws()
        {
            TestSimulator = new RandomBool(RandomToggle * -1, RandomValueCount);

            // Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetSimulatorValues());
        }

        [Test]

        public void RandomBool_TogglePropability_MoreThanOne_Throws()
        {
            TestSimulator = new RandomBool(RandomToggle + 1.6, RandomValueCount);
            // Löst Exception aus?
            Assert.Throws<ArgumentOutOfRangeException>(() => TestSimulator.GetSimulatorValues());

        }

        [Test]
        public void RandomBool_ValueCount_Test()
        {
            TestSimulator = new RandomBool(RandomToggle, RandomValueCount);
            List<bool> TestList = TestSimulator.GetSimulatorValues();
            
            Console.WriteLine("Methode sollte {0} Werte liefern, Methode hat {1} Werte geliefert", RandomValueCount, TestList.Count);
            Assert.AreEqual(RandomValueCount, TestList.Count);

        }

        [Test]
        public void RandomBool_Print_Result_Test()
        {
            TestSimulator = new RandomBool(RandomToggle, 20);
            List<bool> TestList = TestSimulator.GetSimulatorValues();
            Console.WriteLine("Methode hat mit Wechselwarscheinlichkeit von \n "
                   + RandomToggle*100
                   + "% \n" 
                   + String.Join(", ", TestList)
                   + "\x0A erzeugt");
        }

        [Test]
        public void RandomBool_ToggleProbability_Matches_Result()
        {
            int AmmountofToggles = 0;
            double ActualToggleRate;
            uint TestAmmountofValues = 1000000;
            TestSimulator = new RandomBool(RandomToggle, TestAmmountofValues);
            List<bool> TestList = TestSimulator.GetSimulatorValues();
            for(int i = 0; i+1 < TestList.Count; i++)
            {
                if (TestList[i] != TestList[i + 1])
                    AmmountofToggles += 1;
            }

            ActualToggleRate = (double)AmmountofToggles / (double)TestAmmountofValues;

            Console.WriteLine("Die erwartete Wechselwarscheinlichkeit ist "
                                    + RandomToggle * 100
                                    + "% \n"
                                    + "Die tatsächliche Wechselwarscheinlichkeit ist "
                                    + ActualToggleRate * 100
                                    + "% \n");
            //Ist die Abweichung größer als 0.01%?
            if (Math.Abs(ActualToggleRate - RandomToggle) > 0.002)
            {
                Assert.Fail("Die Wechselwarscheinlichkeit weicht maßgeblich vom erwarteten Wert ab");
            }
            else
            {
                Assert.Pass("Die Wechselwarscheinlichkeit entspricht dem erwarteten Wert");
            }
        }
    }
}