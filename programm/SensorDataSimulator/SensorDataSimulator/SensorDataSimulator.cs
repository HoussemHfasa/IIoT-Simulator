using CommonInterfaces;
using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{



    public class SensorDataSimulator : ISensorDataSimulator
    {
        public int AmmountofValues => throw new NotImplementedException();

        public List<double> GetDampedOscillation(double Amplitude, double Dampingratio, double Frequency, double Phase, int AmmountofValues)
        {
            throw new NotImplementedException();
        }

        public List<double> GetExponentialValues(double Basis, double Exponent, int AmountofValues)
        {
            throw new NotImplementedException();
        }

        public List<double> GetHarmonicOscillation(double Amplitude, double Frequency, double Phase, int AmmountofValues)
        {
            throw new NotImplementedException();
        }

        public List<double> GetLinearValues(double Slope, double XShift, int AmmountofValues)
        {
            throw new NotImplementedException();
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

    public abstract class SensorDataErrors : ISensorDataErrors<double>
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
    public class RandomValuesError : SensorDataErrors, CommonInterfaces.ISensorDataErrors<double>
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

    public class RandomZeroesError : SensorDataErrors, CommonInterfaces.ISensorDataErrors<double>
    {

        public RandomZeroesError()
        {

        }


        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            throw new NotImplementedException();
        }
    }

    public class AdditiveNoise : SensorDataErrors, CommonInterfaces.ISensorDataErrors<double>
    {
        public AdditiveNoise(List<double> Noise)
        {

        }

        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            throw new NotImplementedException();
        }
    }

    public class BurstNoise : SensorDataErrors, CommonInterfaces.ISensorDataErrors<double>
    {
        public BurstNoise(double Burstvalue, int Burstduration)
        {

        }

        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            throw new NotImplementedException();
        }
    }


    public class TransientNoise : SensorDataErrors, CommonInterfaces.ISensorDataErrors<double>
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
