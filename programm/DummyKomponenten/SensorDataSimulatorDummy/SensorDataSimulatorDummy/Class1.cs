using System;
using System.Collections.Generic;
using CommonInterfaces;


namespace SensorDataSimulatorDummy
{
    public class SensorDataSimulator : ISensorDataSimulator
    {
        public int AmmountofValues { get { return this.AmmountofValues; } set { this.AmmountofValues = 5; } }

        public List<double> GetDampedOscillation(double Amplitude, double Dampingratio, double Frequency, double Phase, int AmmountofValues)
        {
            return new List<double> { 1.1, 2.2, 3.3, 4.4, 5 };
        }

        public List<double> GetExponentialValues(double Basis, double Exponent, int AmountofValues)
        {
            return new List<double> { 1.1, 2.2, 3 };
        }

        public List<double> GetHarmonicOscillation(double Amplitude, double Frequency, double Phase, int AmmountofValues)
        {
            return new List<double> { 1.1, 2.2, 3.3, 4.4, 5 };
        }

        public List<double> GetLinearValues(double Slope, double XShift, int AmmountofValues)
        {
            return new List<double> { 1.1, 2.2, 3.3, 4.4, 5 };,
        }

        public List<bool> GetRandomBoolValues(double Wechselwarscheinlichkeit, int AmountofValues)
        {
            return new List<bool> { true, false, true };
        }

        public List<double> GetStandardDeviationValues(double Mean, double StandardDeviation, int AmmountofValues)
        {
            return new List<double> { 1.1, 2.2, 3.3, 4.4, 5 };
        }

        public List<double> GetSuperposition(List<double> Oscillation1, List<double> Oscillation2)
        {
            return new List<double> { 1.1, 2.2, 3.3, 4.4, 5 };
        }
    }
}
