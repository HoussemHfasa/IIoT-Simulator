using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class StandardDeviation : SensorDataSimualtor<double>
    {
        // Der Algorithmus zur Berechnung CalculateNextValue und DecideIncreaseOrDecrease
        // wurde inspiriert/entlehnt von Joshua Hercher, "Simulation von Sensordaten".
        // Zum besseren Verständnis wurden von mir z.B. Variablennamen angepasst
        // Mittelwert wird mit genügender Wertezahl gut erreicht. Die erzeugte Standardabweichung
        // ist in der Regel etwas unterschritten, meist innerhalb von 20% Abweichung/Unterschreitung.

        //Mittelwert
        private double Mean;
        //Standardabweichung
        private double StandardDev;
        //Maximale Werteänderung zwischen zwei Werten
        private double MaxStepSize;
        private double CurrentValue;
        private Random Rand;

        public StandardDeviation(double MeanInput, double StandardDeviationInput, uint AmmountofValues)
        {

            this.Mean = MeanInput;
            this.StandardDev = Math.Abs(StandardDeviationInput);
            this.AmmountofValues = AmmountofValues;
            MaxStepSize = StandardDev / 10;
            //Startwert der Funktion
            CurrentValue = MeanInput;
            Rand = new Random();

        }
        public override List<double> GetSimulatorValues()
        {
            List<double> Result = new List<double> { };
            
            for(int i =0; i < AmmountofValues; i++)
            {
                Result.Add(CalculateNextValue());
            }
            return Result;
        }
        // Funktion zur Berechnung des Folgewerts
        private double CalculateNextValue()
        {
            // 1. Um wieviel ändert sich der Wert
            double ChangeValue = Rand.NextDouble() * MaxStepSize;

            // 2. Entscheidung ob + oder -
            int Multiplier = DecideIncreaseOrDecrease();

            CurrentValue += ChangeValue * Multiplier;
            return CurrentValue;
        }

        // Funktion, die entscheidet,ob der nächste Wert
        // von CalculateNextValue kleiner oder größer als aktueller Wert wird
        private int DecideIncreaseOrDecrease()
        {
            // Abstand zum Mittelwert
            double DistanceToMean;
            // Wird diese Variable gewählt, entfernt sich der Wert vom Mittelwert
            int GoAwayFromMean;
            // Wird diese Variable gewählt, nähert sich der Wert dem Mittelwert an
            int GoTowardsMean;

            // Bestimmen, ob wir uns unterhalb oder oberhalb des Mittelwerts befinden
            //Faktoren -1/+1 entsprechend tauschen

            if(CurrentValue > Mean)
            {
                DistanceToMean = CurrentValue - Mean;
                GoAwayFromMean = 1;
                GoTowardsMean = -1;
            }
            else
            {
                DistanceToMean = Mean - CurrentValue;
                GoAwayFromMean = -1;
                GoTowardsMean = 1;
            }

            /* Zur Entscheidung ob GoAwayFromMean oder GoTowardsMean zurückgegeben wird:
            Die StardardDev wird als 100% gesetzt. Per Zufallsgenerator wird ein Wert zw.
            0-100% (bzw. (0.0 bis 1)* StandardDev) geliefert. 
            Als Vergleich dient = 50% (StandardDev/2) - Betrag der Abhänhig von DistanceToMean ist 
            credit Joshua Hercher, "Simulation von Sensordaten"
            */
             // DistanceToMean/ 50 wurde von Joshua Hercher empirisch ermittelt
            double Chance = (StandardDev / 2) - (DistanceToMean / 50);
            double RandomValue = Rand.NextDouble() * StandardDev;

            // Wenn der Zufallswert kleiner als die Chance ist, wird GoAwayFromMean zurückgegeben
            if (RandomValue < Chance)
                return GoAwayFromMean;
            else
                return GoTowardsMean;

        }
    }
}
