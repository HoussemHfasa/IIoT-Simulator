using CommonInterfaces;
using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{



    public class SensorDataSimulator : ISensorDataSimulator
    {
        public int AmmountofValues => throw new NotImplementedException();

        

      
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
            for( int i = 0; i < AmmountofValues; i++)
            {
                // Gleichung für harmonische Schwingung. Hier entsteht durch Rundung teilweise -0 als Wert!
                CatchNegativZero = Math.Round(Amplitude * Math.Sin((2 * Math.PI / Period) * i + Phase), 2);

                // Daher diesen Fall abfangen
                if(CatchNegativZero == -0d)
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



        public List<bool> GetRandomBoolValues(double Wechselwarscheinlichkeit, int AmountofValues)
        {
            throw new NotImplementedException();
        }

        public List<double> GetStandardDeviationValues(double Mean, double StandardDeviation, int AmmountofValues)
        {
            throw new NotImplementedException();
        }

        public List<double> GetSuperposition(List<double> Oscillation1, List<double> Oscillation2)
        {
            throw new NotImplementedException();
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
