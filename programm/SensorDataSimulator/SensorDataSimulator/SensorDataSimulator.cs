using CommonInterfaces;
using System;
using System.Collections.Generic;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.Random;

namespace SensorDataSimulator
{

    public abstract class SensorDataSimualtor<T> : ISensorDataSimulator<T>
    {
        // Mutterklasse aller SensorDataSimulatoren

        // Werteanzahl, die erzeugt werden soll
        public uint AmmountofValues { get; set; }
        // in abgeleiteten Klasse werden hiermit die Werte erzeugt
        public abstract List<T> GetSimulatorValues();
        
    }

    public abstract class SensorDataErrors : ISensorDataErrors
    {
        //Mutterklasse aller SensorDataError-Klassen


        // kein public set nötig? Absprache mit Team nicht vor Abgabe Einzelphase möglich
        public double ErrorRatio {
            get; set;
        }

        public int ErrorLength { get; set; }

        public double ErrorMax { get; set; }

        public double ErrorMin { get; set; }


        // Fehlererzeugungsmethode, muss von jeder abgeleiteten Klasse implementiert werden
        public abstract List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs);

        // void wäre sinnvoller, Absprache mit Team nicht vor Abgabe Einzelphase möglich
        public bool ResetSensorErrors()
        {
            //Alle Werte auf Null zurücksetzen
            ErrorMax = 0;
            ErrorLength = 0;
            ErrorMin = 0;
            ErrorRatio = 0;
            return true;
        }

        // void wäre sinnvoller, Absprache mit Team nicht vor Abgabe Einzelphase möglich
        public bool SetSensorErrors(double ErrorRatio, int ErrorLength, double ErrorMax, double ErrorMin)
        {
            this.ErrorRatio = ErrorRatio;
            this.ErrorLength = ErrorLength;
            this.ErrorMax = ErrorMax;
            this.ErrorMin = ErrorMin;
            return true;
        }

    }
}
