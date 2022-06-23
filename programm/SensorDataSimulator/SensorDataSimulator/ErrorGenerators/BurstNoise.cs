using CommonInterfaces;
using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class BurstNoise : SensorDataErrors, ISensorDataErrors
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
}
