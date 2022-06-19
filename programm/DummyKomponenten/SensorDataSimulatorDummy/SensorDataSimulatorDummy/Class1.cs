using System;
using System.Collections.Generic;
using CommonInterfaces;


namespace SensorDataSimulatorDummy
{
    public class SensorDataSimulator : ISensorDataSimulator<double>
    {
        public SensorDataSimulator (uint ValueCount)
        {
            AmmountofValues = ValueCount;
        }
        public uint AmmountofValues { get { return this.AmmountofValues; } set { this.AmmountofValues = 5; } }

        public List<double> GetDampedOscillation(double Amplitude, double Dampingratio, double Frequency, double Phase)
        {
            return new List<double> { 1.1, 2.2, 3.3, 4.4, 5 };
        }

        public List<double> GetExponentialValues(double Basis, double Exponent)
        {
            return new List<double> { 1.1, 2.2, 3 };
        }

        public List<double> GetHarmonicOscillation(double Amplitude, double Frequency, double Phase)
        {
            return new List<double> { 1.1, 2.2, 3.3, 4.4, 5 };
        }

        public List<double> GetLinearValues(double Slope, double XShift)
        {
            return new List<double> { 1.1, 2.2, 3.3, 4.4, 5 };
        }

        public List<bool> GetRandomBoolValues(double Wechselwarscheinlichkeit)
        {
            return new List<bool> { true, false, true };
        }

        public List<double> GetStandardDeviationValues(double Mean, double StandardDeviation)
        {
            return new List<double> { 1.1, 2.2, 3.3, 4.4, 5 };
        }

        public List<double> GetSuperposition(List<double> Oscillation1, List<double> Oscillation2)
        {
            return new List<double> { 1.1, 2.2, 3.3, 4.4, 5 };
        }

        List<double> ISensorDataSimulator<double>.GetSimulatorValues()
        {
            throw new NotImplementedException();
        }
    }
}
