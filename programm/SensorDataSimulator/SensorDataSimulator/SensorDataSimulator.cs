using CommonInterfaces;
using System;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace SensorDataSimulator
{

    public abstract class SensorDataSimualtor<T> : ISensorDataSimulator<T>
    {
        public uint AmmountofValues { get; set; }

        public abstract List<T> GetSimulatorValues();
        
    }

    public class HarmonicOscillation : SensorDataSimualtor<double>
    {
        private double Amplitude;
        private double Period;
        private double Phase;
        public HarmonicOscillation(double Amplitude, double Period, double Phase, uint AmmountofValues )
        {
            this.Amplitude = Amplitude;
            this.Period = Period;
            this.Phase = Phase;
            this.AmmountofValues = AmmountofValues;
        }

        public override List<double> GetSimulatorValues()
        {
            //Ohne Rundungen
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
            return Result;
        }
    }

    public class DampedOscillation : SensorDataSimualtor<double>
    {
        private double Amplitude;
        private double Dampingratio;
        private double Period;
        private double Phase;
        public DampedOscillation(double Amplitude, double Dampingratio, double Period, double Phase, uint AmmountofValues)
        {
            this.Amplitude = Amplitude;
            this.Dampingratio = Dampingratio;
            this.Period = Period;
            this.Phase = Phase;
            this.AmmountofValues = AmmountofValues;
        }
        public override List<double> GetSimulatorValues()
        {
            List<double> Result = new List<double>();

            // Überprüfung, Dämpfungsrate im erlaubten Bereich liegt. Falls nicht -> Exception
            if (Dampingratio < 0.0)
                throw new ArgumentOutOfRangeException("Dämpfungsrate darf nicht negativ sein!");

            // Achtung, hier wird die Harmonische nur auf 2 Kommastellen genau geliefert...
            // evtl harmonische in extra Funktion errechnen, GetHarmonicOscillation müsste dann noch runden.
            HarmonicOscillation HarmonicPortion = new HarmonicOscillation(Amplitude, Period, Phase, AmmountofValues);
            List<double> HarmonicPart = HarmonicPortion.GetSimulatorValues();
            for (int i = 0; i < AmmountofValues; i++)
            {
                Result.Add(HarmonicPart[i] * Math.Exp(-1.0 * Dampingratio * i));
            }

            return Result;
        }
    }

    public class RandomBool : SensorDataSimualtor<bool>
    {
        private Random Rand = new Random();
        private double Toggleprobability;
        public RandomBool(double Toggleprobability, uint AmmountofValues)
        {
            this.Toggleprobability = Toggleprobability;
            this.AmmountofValues = AmmountofValues;
        }
        public override List<bool> GetSimulatorValues()
        {
            List<bool> Result = new List<bool>();

            // Wechselwarscheinlichkeit zwischen 0.0 und 1.0, daher Exception Handling?
            if (Toggleprobability > 1.0 || Toggleprobability < 0.0)
            {
                throw new ArgumentOutOfRangeException("Wechselwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
            }

            // Erzeugung erster Wert
            Result.Add(Convert.ToBoolean(Rand.Next(0, 2)));

            //Werteerzeugung mit Wechselwarscheinlichkeit, i = 1, da bereits ein Wert erzeugt wurde
            for (int i = 1; i < AmmountofValues; i++)
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
    }

    public class Superposition : SensorDataSimualtor<double>
    {
        List<double> Oscillation1;
        List<double> Oscillation2;
        public Superposition(List<double> Oscillation1,List<double> Oscillation2)
        {
            this.Oscillation1 = Oscillation1;
            this.Oscillation2 = Oscillation2;
        }
        public override List<double> GetSimulatorValues()
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
    }

    public class StandardDeviation : SensorDataSimualtor<double>
    {
        public override List<double> GetSimulatorValues()
        {
            throw new NotImplementedException();
        }
    }
    /*
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

            public uint AmmountofValues
            {
                get;
                set;
            }


            public SensorDataSimulator( uint ValueCount)
            {
                Rand = new Random();
                AmmountofValues = ValueCount;
            }

            public List<double> GetHarmonicOscillation(double Amplitude, double Period, double Phase)
            {

                //Ohne Rundungen
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

            public List<double> GetDampedOscillation(double Amplitude, double Dampingratio, double Period, double Phase)
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



            public List<bool> GetRandomBoolValues(double Toggleprobability)
            {
                List<bool> Result = new List<bool>();

                // Wechselwarscheinlichkeit zwischen 0.0 und 1.0, daher Exception Handling?
                if (Toggleprobability > 1.0 || Toggleprobability < 0.0)
                {
                    throw new ArgumentOutOfRangeException("Wechselwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
                }

                // Erzeugung erster Wert
                Result.Add(Convert.ToBoolean(Rand.Next(0, 2)));

                //Werteerzeugung mit Wechselwarscheinlichkeit, i = 1, da bereits ein Wert erzeugt wurde
                for (int i = 1; i < AmmountofValues; i++)
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

            public List<double> GetStandardDeviationValues(double MeanInput, double StandardDeviationInput)
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
    */
    public abstract class SensorDataErrors : ISensorDataErrors
    {

        // kein public set?
        public double ErrorRatio {
            get; set;
        }

        public int ErrorLength { get; set; }

        public double ErrorMax { get; set; }

        public double ErrorMin { get; set; }

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

        private List<double> TempList;
        private Random Rand = new Random();

        public RandomValuesError(double ErrorRatio, int ErrorLength, double MaxError, double MinError)
        {
            // Errorlenght auf Uint ändern

            //Eingaben auf Exceptions überprüfen
            //Fehlerrate zwischen 0 und 1?
            if (ErrorRatio > 1.0 || ErrorRatio < 0.0)
            {
                throw new ArgumentOutOfRangeException("Fehlerwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
            }

            // Wenn Min und Max verwechselt wurde, entsprechend tauschen

            if(MaxError >= MinError)
            {
                this.ErrorMax = MaxError;
                this.ErrorMin = MinError;
            }
            else
            {
                this.ErrorMin = MaxError;
                this.ErrorMax = MinError;
            }

            this.ErrorRatio = ErrorRatio;
            this.ErrorLength = ErrorLength;
        }
        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            
            TempList = SensorDataWithoutErorrs;

            // Bei Fehlerlänge 0 wird Liste zurückgegeben
            if (ErrorLength == 0)
                return TempList;

            //per Zufall mit Warscheinlichkeit entscheiden, ob Fehler 
            for(int i = 0; i < TempList.Count; i++)
            {
                if(Rand.NextDouble() <= ErrorRatio)
                {
                    //Fehler einbauen mit angegebener Fehlerlänge
                    for (int z = 0; z < ErrorLength; z++)
                    {
                        TempList[i+z] = Rand.NextDouble() * ErrorMax + ErrorMin;
                        
                    }
                    // die äußere Zählschleife soll die eingebauten Fehlerwerte überspringen
                    i += ErrorLength - 1;
                    

                }
            }
            return TempList;
        }
    }

    public class RandomZeroesError : SensorDataErrors, CommonInterfaces.ISensorDataErrors
    {
        private List<double> TempList;
        private Random Rand = new Random();

        public RandomZeroesError(double ErrorRatio, int ErrorLength)
        {
            if (ErrorRatio > 1.0 || ErrorRatio < 0.0)
            {
                throw new ArgumentOutOfRangeException("Fehlerwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
            }

            this.ErrorRatio = ErrorRatio;
            this.ErrorLength = ErrorLength;
        }


        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            TempList = SensorDataWithoutErorrs;

            if (ErrorLength == 0)
                return TempList;

            
            for(int i = 0; i < TempList.Count; i++)
            {
                if(Rand.NextDouble() <= ErrorRatio)
                {
                    // entsprechend der Fehlerlänge Nullen setzen
                    for (int z = 0; z < ErrorLength; z++)
                    {
                        TempList[i+z] = 0.0;
                    }

                    //äußere Zählschleife soll gesetzte Fehler nicht überschreiben
                    i += ErrorLength - 1;
                }
            }
            return TempList;
        }
    }

    public class AdditiveNoise : SensorDataErrors, CommonInterfaces.ISensorDataErrors
    {
        List<double> Noise;
        public AdditiveNoise(List<double> Noise)
        {
            this.Noise = Noise;
        }

        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            // Achtung, wenn Noise Daten weniger sind als Sensordaten..
            List<double> Result = new List<double>();
            List<double> ShorterList;
            List<double> LongerList;

            // Herausfinden, welche die kürzere Liste ist
            if (SensorDataWithoutErorrs.Count <= Noise.Count)
            {
                ShorterList = SensorDataWithoutErorrs;
                LongerList = Noise;
            }
            else
            {

                ShorterList = Noise;
                LongerList = SensorDataWithoutErorrs;
            }

            for (int i = 0; i < SensorDataWithoutErorrs.Count; i++)
            {
                // Die kurze Liste hat an der Stelle keine Werte mehr
                if (i >= ShorterList.Count)
                {
                    Result.Add(LongerList[i]);
                }
                else
                {
                    Result.Add(ShorterList[i] + LongerList[i]);
                }


            }
            return Result;
        }
    }

    public class BurstNoise : SensorDataErrors, CommonInterfaces.ISensorDataErrors
    {
        double BurstValue;

        List<double> TempList;
        Random Rand = new Random();
        public BurstNoise(double Burstvalue, int Burstduration, double BurstRatio)
        {
            //Exception Handling BurstRatio
            if (BurstRatio > 1.0 || BurstRatio < 0.0)
            {
                throw new ArgumentOutOfRangeException("Burst-Warscheinlichkeit darf nicht unter 0 oder über 1 liegen");
            }

            // Werte intern abspeichern
            this.BurstValue = Burstvalue;
            this.ErrorLength = Burstduration;
            this.ErrorRatio = BurstRatio;
        }

        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            TempList = SensorDataWithoutErorrs;

            for(int i = 0; i < SensorDataWithoutErorrs.Count; i++)
            {
                if(Rand.NextDouble() <= ErrorRatio)
                {
                    for (int z = 0; z < ErrorLength; z++)
                    {
                        TempList[i + z] = BurstValue;
                    }
                    i += ErrorLength - 1;
                }
            }

            return TempList;
        }


    }


    public class TransientNoise : SensorDataErrors, CommonInterfaces.ISensorDataErrors
    {
        List<double> Result;
        List<double> Puls;
        double DampingRatio;
        public TransientNoise(List<double> Puls, int DecayTime)
        {
            this.Puls = Puls;
            // DecayTime in Uint?
            //DecayTime in Dämpfung umrechnen. Gedämpfte Schwingung e-Funktion wird nie 0,
            //daher soll die Pulsamplitude auf mindestens 1 Hunderttausenstel bis zur Decyatime gedämpft werden.
            DampingRatio = Math.Log(0.00001) / (DecayTime * -1);
        }

        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            for (int i = 0; i < SensorDataWithoutErorrs.Count; i++)
            {
                Result.Add(SensorDataWithoutErorrs[i] + Puls[i] * Math.Exp(-1.0 * DampingRatio * i));
            }

            return Result;
        }
    }
}
