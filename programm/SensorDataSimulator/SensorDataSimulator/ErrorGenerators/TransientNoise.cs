using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{
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
