using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class DampedOscillation : SensorDataSimualtor<double>
    {
        //Klasse zur Erzeugung einer gedämpften Schwingung

        // Für die Datenerzeugung benötigte Variablen/Felder
        private double Amplitude;
        private double Dampingratio;
        private double Period;
        private double Phase;

        // Dem Konstruktor werden alle zur Berechnung benötigten Werte übergeben
        public DampedOscillation(double Amplitude, double Dampingratio, double Period, double Phase, uint AmmountofValues)
        {
            this.Amplitude = Amplitude;
            this.Dampingratio = Dampingratio;
            this.Period = Period;
            this.Phase = Phase;
            this.AmmountofValues = AmmountofValues;
        }

        // Gibt Liste von errechneten Werten zurück
        public override List<double> GetSimulatorValues()
        {
            List<double> Result = new List<double>();

            // Überprüfung, Dämpfungsrate im erlaubten Bereich liegt. Falls nicht -> Exception
            if (Dampingratio < 0.0)
                throw new ArgumentOutOfRangeException("Dämpfungsrate darf nicht negativ sein!");

            // Als Basis die harmonische Schwingung erzeugen
            HarmonicOscillation HarmonicPortion = new HarmonicOscillation(Amplitude, Period, Phase, AmmountofValues);
            List<double> HarmonicPart = HarmonicPortion.GetSimulatorValues();

            // Mit dämpfenden Teil multiplizieren
            for (int i = 0; i < AmmountofValues; i++)
            {
                Result.Add(HarmonicPart[i] * Math.Exp(-1.0 * Dampingratio * i));
            }

            // Ergebnis zurückgeben
            return Result;
        }
    }
}
