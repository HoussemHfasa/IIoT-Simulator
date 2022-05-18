using CommonInterfaces;
using System;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace SensorDataSimulator
{



    public class SensorDataSimulator : ISensorDataSimulator
    {

        private Random Rand;

        // interne Variablen zur Berechnung Standardabweichung
        private double Mean;
        private double StandardDeviation;
        // Maximale WerteÄnderung
        private double MaxValueChange;
        // Tatsächliche WertÄnderung
        private double Prefix;


        private double CurrentValue;

        public int AmmountofValues
        {
            get;
            set;
        }


        public SensorDataSimulator()
        {
            Rand = new Random();
        }

        public List<double> GetHarmonicOscillation(double Amplitude, double Period, double Phase, int AmmountofValues)
        {
            List<double> Result = new List<double>();
            double CatchNegativZero;

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
                // Gleichung für harmonische Schwingung. Hier entsteht durch Rundung teilweise -0 als Wert!
                CatchNegativZero = Math.Round(Amplitude * Math.Sin((2 * Math.PI / Period) * i + Phase), 2);

                // Daher diesen Fall abfangen
                if (CatchNegativZero == -0d)
                {
                    CatchNegativZero = 0d;
                }
                Result.Add(CatchNegativZero);
            }
            return Result;
        }

        public List<double> GetDampedOscillation(double Amplitude, double Dampingratio, double Period, double Phase, int AmmountofValues)
        {
            List<double> Result = new List<double>();

            // Überprüfung, Dämpfungsrate im erlaubten Bereich liegt. Falls nicht -> Exception
            if (Dampingratio < 0.0)
                throw new ArgumentOutOfRangeException("Dämpfungsrate darf nicht negativ sein!");

            // Achtung, hier wird die Harmonische nur auf 2 Kommastellen genau geliefert...
            // evtl harmonische in extra Funktion errechnen, GetHarmonicOscillation müsste dann noch runden.
            List<double> HarmonicPart = GetHarmonicOscillation(Amplitude, Period, Phase, AmmountofValues);
            for (int i = 0; i < AmmountofValues; i++)
            {
                Result.Add(Math.Round(HarmonicPart[i] * Math.Exp(-1.0 * Dampingratio * i), 2));
            }

            return Result;
        }



        public List<bool> GetRandomBoolValues(double Toggleprobability, int AmountofValues)
        {
            List<bool> Result = new List<bool>();

            // Wechselwarscheinlichkeit zwischen 0.0 und 1.0, daher Exception Handling?
            if (Toggleprobability > 1.0 ^ Toggleprobability < 0.0)
            {
                throw new ArgumentOutOfRangeException("Wechselwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
            }

            // Erzeugung erster Wert
            Result.Add(Convert.ToBoolean(Rand.Next(0, 2)));

            //Werteerzeugung mit Wechselwarscheinlichkeit, i = 1, da bereits ein Wert erzeugt wurde
            for (int i = 1; i < AmountofValues; i++)
            {

                // Zufalls Double kleiner als Wechselwarscheinlichkeit -> Wert wechselt
                if (Rand.NextDouble() <= Toggleprobability)
                {
                    Result.Add(!Result[i - 1]);
                }

                // Sonst: gleicher Wert
                else
                {
                    Result.Add(Result[i - 1]);
                }
            }


            return Result;
        }

        public List<double> GetStandardDeviationValues(double MeanInput, double StandardDeviationInput, int AmmountofValues)
        {
            // Standardabweichung darf nicht negativ sein..?!
            // Info: Standardabweichung kann nur mit genügender Menge Werte erreicht werden

            // Ergebnis A
            List<double> Result = new List<double>();

            // Intern abspeichern damit NextValue() darauf zugreifen kann
            Mean = MeanInput;
            StandardDeviation = StandardDeviationInput;
            //Maximale Werteänderung zum Vorhergehenden Wert, 15 ist willkürlich gewählt
            MaxValueChange = StandardDeviation / 15;
            return Result = (List<double>)Normal.Samples(Mean, StandardDeviation);
            // Ersten Wert setzen.. vlt nicht immer Mean nehmen?
            CurrentValue = Mean;
            Result.Add(CurrentValue);

            // Restliche Werte erzeugen
            for (int i = 1; i < AmmountofValues; i++)
            {
                Result.Add(NextValue());
            }
            
            return Result;
        }

        public List<double> GetSuperposition(List<double> Oscillation1, List<double> Oscillation2)
        {
            List<double> Result = new List<double>();
            List<double> ShortList;
            List<double> LongList;

            // Herausfinden, welche die kürzere Liste ist
            if (Oscillation1.Count <= Oscillation2.Count)
            {
                ShortList = Oscillation1;
                LongList = Oscillation2;
            }
            else
            {

                ShortList = Oscillation2;
                LongList = Oscillation1;
            }

            for (int i = 0; i < LongList.Count; i++)
            {
                // Die kurze Liste hat an der Stelle keine Werte mehr
                if (i >= ShortList.Count)
                {
                    Result.Add(LongList[i]);
                }
                else
                {
                    Result.Add(ShortList[i] + LongList[i]);
                }


            }
            return Result;
        }

        private double NextValue()
        {
            // Um wieviel soll sich Wert ändern
            double ActualValueChange = MaxValueChange * Rand.NextDouble();

            // Soll Änderung addiert oder subtrahiert werden?
            Prefix = PlusorMinusOne();


            CurrentValue = CurrentValue + ActualValueChange * Prefix;

            return CurrentValue;
        }

        private double PlusorMinusOne()
        {
            //double DistanceToDev = Math.Abs(CurrentValue - StandardDeviation - Mean);
            double DistanceToMean = Math.Abs(CurrentValue - Mean);

            // Wenn die DistancetoDev größer ist als die Standardabweichung, soll der Wert tendenziell näher zur Standardabweichung
            // Bei Standardabweichung = DistanceToDev 50/50 Warscheinlichkeit.

            // Noch unsicher ob hier gewünschte Normalverteilung entsteht...HIER IST DENKFEHLER
            //double Propability = StandardDeviation / (DistanceToDev * 2);
            double Propability = ((StandardDeviation - DistanceToMean/10) / 2);
           

            // Stanarddeviation entspricht 100%. 0.0 = 0%, jetzt zufälligen Wert normiert auf StandardDeviation erstellen
            double CompareValue = Rand.NextDouble() * StandardDeviation;
            
            //Wird hier der Wert verglichen oder die Adresse?!
            if(Prefix == 1)
            {
                if (Propability > CompareValue)
                    return 1.0;
                else
                    return -1.0;
            }
            else
            {
                if (Propability > CompareValue)
                    return -1.0;
                else
                    return 1.0;
            }

           
        }
    }

    public abstract class SensorDataErrors : ISensorDataErrors
    {
        public double ErrorRatio => throw new NotImplementedException();

        public int ErrorLength => throw new NotImplementedException();

        public double ErrorMax => throw new NotImplementedException();

        public double ErrorMin => throw new NotImplementedException();

        public abstract List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs);

        public bool ResetSensorErrors()
        {
            throw new NotImplementedException();
        }

        public bool SetSensorErrors(double ErrorRatio, int ErrorLength, double ErrorMax, double ErrorMin)
        {
            throw new NotImplementedException();
        }

    }
    public class RandomValuesError : SensorDataErrors, CommonInterfaces.ISensorDataErrors
    {
        double MaxError;
        double MinError;

        public RandomValuesError(double MaxError, double MinError)
        {
            this.MaxError = MaxError;
            this.MinError = MinError;
        }
        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            throw new NotImplementedException();
        }
    }

    public class RandomZeroesError : SensorDataErrors, CommonInterfaces.ISensorDataErrors
    {

        public RandomZeroesError()
        {

        }


        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            throw new NotImplementedException();
        }
    }

    public class AdditiveNoise : SensorDataErrors, CommonInterfaces.ISensorDataErrors
    {
        public AdditiveNoise(List<double> Noise)
        {

        }

        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            throw new NotImplementedException();
        }
    }

    public class BurstNoise : SensorDataErrors, CommonInterfaces.ISensorDataErrors
    {
        public BurstNoise(double Burstvalue, int Burstduration)
        {

        }

        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            throw new NotImplementedException();
        }
    }


    public class TransientNoise : SensorDataErrors, CommonInterfaces.ISensorDataErrors
    {
        public TransientNoise(List<double> Puls, int DecayTime)
        {

        }

        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            throw new NotImplementedException();
        }
    }
}
