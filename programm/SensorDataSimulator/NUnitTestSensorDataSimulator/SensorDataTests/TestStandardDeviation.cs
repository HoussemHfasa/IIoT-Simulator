using NUnit.Framework;
using SensorDataSimulator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnitTestSensorDataSimulator
{
    public class TestStandardDeviation
    {
        double TestMean = 250.0;
        double TestStandardDev = 15.0;
        uint TestAmmountofValues = 20;
        StandardDeviation TestSimulator;
        List<double> Result;

        [SetUp]
        public void Setup()
        {
            List<double> Result = new List<double> { };
            TestSimulator = new StandardDeviation(TestMean, TestStandardDev, TestAmmountofValues);
        }
            [Test]
        // gibt korrekte Werteanzahl zurück
        public void StandardDeviation_Returns_CorrectCount()
        {
            Result = TestSimulator.GetSimulatorValues();
            Assert.AreEqual(Result.Count, TestAmmountofValues);
        }

        [Test]
        public void StandardDeviation_Matches_ExpectedMean ()
        {
            
            TestMean = 250;
            TestStandardDev = 15;
            TestAmmountofValues = 30000;
            TestSimulator = new StandardDeviation(TestMean, TestStandardDev, TestAmmountofValues);
            Result = TestSimulator.GetSimulatorValues();
            
            // Wenn Abweichung unter 5% liegt -> True, ansonsten Exception
            if (Math.Abs(TestMean - Result.Average()) / TestMean < 0.05)
            {
                Console.WriteLine("Mittelwert sollte {0} sein, Mittelwert ist {1}. \n " +
                      "Abweichung liegt im Erwartungsbereich.", TestMean, Result.Average());
                Assert.True(true);
            }
            else
                Assert.True(false, "Abweichung über 5%");
        }

        [Test]
        public void StandardDeviation_Matches_ExpectedDeviation()
        {
            TestMean = 2500;
            TestStandardDev = 150;
            TestAmmountofValues = 100000;
            TestSimulator = new StandardDeviation(TestMean, TestStandardDev, TestAmmountofValues);
            Result = TestSimulator.GetSimulatorValues();

            // Berechnung der Standardabweichung
            double CalculateStandardDev(List<double> Input)
            {
                double Avg = Input.Average();
                return Math.Sqrt(Input.Average(v => Math.Pow(v - Avg, 2)));
            }

            // Wenn erzeugte Standardabweichung in Bereich um 20% des Erwartungswert liegt -> True
            if (Math.Abs((TestStandardDev-CalculateStandardDev(Result)) / TestStandardDev) < 0.2)
            {
                Console.WriteLine("Standardabweichung sollte {0} sein, Standardabweichung ist {1}. \n " +
                      "Abweichung liegt im Erwartungsbereich.", TestStandardDev, CalculateStandardDev(Result));
                Assert.True(true);
            }
            else
            {
                Console.WriteLine("Standardabweichung sollte {0} sein, Standardabweichung ist {1}. \n " +
                      "Abweichung nicht im Erwartungsbereich.", TestStandardDev, CalculateStandardDev(Result));
                Assert.True(false, "Abweichung über 20%");
            }
        }

    }
}