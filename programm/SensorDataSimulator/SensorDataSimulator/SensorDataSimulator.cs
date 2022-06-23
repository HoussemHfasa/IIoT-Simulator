using CommonInterfaces;
using System;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace SensorDataSimulator
{

    public abstract class SensorDataSimualtor<T> : ISensorDataSimulator<T>
    {
        // Werteanzahl, die erzeugt werden soll
        public uint AmmountofValues { get; set; }
        // in abgeleiteten Klasse werden hiermit die Werte erzeugt
        public abstract List<T> GetSimulatorValues();
        
    }

    public class HarmonicOscillation : SensorDataSimualtor<double>
    {
        //Absprache nötig: Werden diese Daten in der Anzeige später gebraucht?
        private double Amplitude;
        private double Period;
        private double Phase;

        //Konstruktor
        public HarmonicOscillation(double Amplitude, double Period, double Phase, uint AmmountofValues )
        {
            // Parameter intern abspeichern
            this.Amplitude = Amplitude;
            this.Period = Period;
            this.Phase = Phase;
            this.AmmountofValues = AmmountofValues;
        }


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

    public class RandomBool : SensorDataSimualtor<bool>
    {
        private Random Rand = new Random();
        private double Toggleprobability;
        public RandomBool(double Toggleprobability, uint AmmountofValues)
        {
            //

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
        public Superposition(List<double> Oscillation1, List<double> Oscillation2)
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
            //Eingaben auf Exceptions überprüfen
            //Fehlerrate zwischen 0 und 1?
            if (ErrorRatio > 1.0 || ErrorRatio < 0.0)
            {
                throw new ArgumentOutOfRangeException("Fehlerwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
            }

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
                    // Hier entstand Index out of Range Fehler, Absprache Rodner
                    
                    for (int z = 0; z < ErrorLength && z+i < TempList.Count; z++)
                    {
                        //potentieller Corner Case bei maximal und minimal möglichem doublewert entsteht Überlauf?
                        TempList[i+z] = Rand.NextDouble() * (ErrorMax-ErrorMin) + ErrorMin;
                        
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
