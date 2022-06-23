using NUnit.Framework;
using SensorDataSimulator;
using System;
using System.Collections.Generic;

namespace NUnitTestSensorDataSimulator
{
    public class Superposition
    {
        List<double> HarmonicList1;
        List<double> HarmonicList2;
        List<double> SuperpositionList;
        double Amplitude;
        double Period;
        double Phase;
        uint AmmountofValues;
        SensorDataSimulator.Superposition SuperpositionGenerator;

        Random Rand = new Random();
        [SetUp]
        public void Setup()
        {
            //Werte für die erste Schwingung
            Amplitude = Rand.NextDouble() * 5000;
            Period = Rand.NextDouble() * 20 + 5;
            Phase = 0;
            AmmountofValues = (uint)Rand.Next(5,50000);


            // Schwingung erzeugen
            HarmonicOscillation HarmonicGenerator = new HarmonicOscillation(Amplitude, Period, Phase, AmmountofValues);
            HarmonicList1 = HarmonicGenerator.GetSimulatorValues();

            //Werte für die zweite Schwingung
            Amplitude = Rand.NextDouble() * 5000;
            Period = Rand.NextDouble() * 20 + 5;
            Phase = 0;
            AmmountofValues = (uint)Rand.Next(5,50000);
            // Schwingung 2 erzeugen
            HarmonicGenerator = new HarmonicOscillation(Amplitude, Period, Phase, AmmountofValues);
            HarmonicList2 = HarmonicGenerator.GetSimulatorValues();

            SuperpositionGenerator = new SensorDataSimulator.Superposition(HarmonicList1, HarmonicList2);
            
        }


        [Test]

        public void Superposition_Counttest()
        {
            SuperpositionList = SuperpositionGenerator.GetSimulatorValues();
            if ((SuperpositionList.Count == HarmonicList1.Count) || (SuperpositionList.Count == HarmonicList2.Count))
            {

                Assert.Pass("Die zurückgelieferte Schwingung hat die korrekte Werteanzahl");
            }
        }

        [Test]
        public void Superposition_Resulttest()
        {
            List<double> KnownList1 = new List<double> {1.1, -55.7, 0, 7655.794641, 4684.12121};
            List<double> KnownList2 = new List<double> { 451.12144, -33.12487, 0, -2326.4888, 26222.788, 15, 78.555 };
            List<double> ExpectedList = new List<double> { 452.22144, -88.82487, 0, 5329.305841, 30906.90921, 15, 78.555 };
            SensorDataSimulator.Superposition SuperpositionCalculator = new SensorDataSimulator.Superposition(KnownList1, KnownList2);
            List<double> ResultList = SuperpositionCalculator.GetSimulatorValues();

            // Anzeige des Ergebnis und Erwartungswert in den Testdetails
            Console.WriteLine("Methode sollte "
             + String.Join(", ", ExpectedList)
             + "erzeugen. Es wurde \n "
             + String.Join(", ", ResultList)
             + "erzeugt");

            for (int i = 0; i < ResultList.Count; i++)
            {
                //Auch bei Addition von double können Werteabweichungen entstehen, daher nicht auf exakte Gleichheit überprüfen
                if (Math.Abs(ResultList[i] - ExpectedList[i]) > 1e-12)
                {

                    Console.WriteLine("An der " + i
                        + "ten Stelle Stimmt das Ergebnis \n "
                        + ResultList[i]
                        + " nicht mit dem erwarteten Wert \n "
                        + ExpectedList[i]
                        + " überein.");
                    Assert.Fail("Die Listen stimmen nicht überein");
                }
            }
            Assert.Pass("Die berechnete Liste stimmt mit dem erwartetem Ergebnis überein. \n(Abzüglich Ungenauigkeiten durch Präzision von Wertetyp Double");
        }
    }
}