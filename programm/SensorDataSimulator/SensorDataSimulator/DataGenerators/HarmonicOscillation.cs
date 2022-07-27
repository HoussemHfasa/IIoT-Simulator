using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class HarmonicOscillation : SensorDataSimualtor<double>
    {
        //Klasse zur Erzeugung einer harmonischen Schwingung

        //Absprache nötig: Werden diese Daten in der Anzeige später gebraucht?
        private double Amplitude;
        private double Period;
        private double Phase;

        //Konstruktor erhält alle benötigten Parameter
        public HarmonicOscillation(double Amplitude, double Period, double Phase, uint AmmountofValues )
        {
            // Parameter intern abspeichern
            this.Amplitude = Amplitude;
            this.Period = Period;
            this.Phase = Phase;
            this.AmmountofValues = AmmountofValues;
        }

        // Methode gibt Simulatordaten als Liste zurück
        public override List<double> GetSimulatorValues()
        {
           
            List<double> Result = new List<double>();
            

            // Überprüfung, ob Parameter in korrektem Wertebereich liegen. Falls nicht -> Exception
            if (Amplitude < 0.0)
                throw new ArgumentOutOfRangeException("Amplitude darf nicht negativ sein!");
            if (Period < 0.0)
                throw new ArgumentOutOfRangeException("Periodendauer darf nicht negativ sein");
            // oder über uint..
            if (AmmountofValues < 0)
                throw new ArgumentOutOfRangeException("Werteanzahl darf nicht negativ sein");

            //Werteerzeugung
            for (int i = 0; i < AmmountofValues; i++)
            {
                Result.Add(Amplitude * Math.Sin((2 * Math.PI / Period) * i + Phase));
            }

            // Ergebnis zurückgeben
            return Result;
        }
    }
}
