using CommonInterfaces;
using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class DataSimulator : ISensorDataSimulator
    {
        public double Mittelwert => throw new NotImplementedException();

        public double Standardabweichung => throw new NotImplementedException();

        public int Werteanzahl => throw new NotImplementedException();

        public double Fehlerhäufigkeit => throw new NotImplementedException();

        public double Fehlerdauer => throw new NotImplementedException();

        public double MaximalwertFehler => throw new NotImplementedException();

        public double MinimalwertFehler => throw new NotImplementedException();


        public List<double> Exponential(double Basis, double Exponent, int Werteanzahl)
        {
            throw new NotImplementedException();
        }

        public List<double> Linear(double Steigung, double VerschiebungXAchse, int Werteanzahl)
        {
            throw new NotImplementedException();
        }

        public List<double> Normalverteilung(double Mittelwert, double Standardabweichung, int Werteanzahl)
        {
            throw new NotImplementedException();
        }

        public bool SetzeSignalfehler(double Fehlerhäufigkeit, int Fehlerdauer, double MaximalwertFehler, double MinimalwertFehler)
        {
            throw new NotImplementedException();
        }

        public bool SignalfehlerZurücksetzen()
        {
            throw new NotImplementedException();
        }

        public List<bool> Zufallsbool(double Wechselwarscheinlichkeit, int Werteanzahl)
        {
            throw new NotImplementedException();
        }
    }
}
