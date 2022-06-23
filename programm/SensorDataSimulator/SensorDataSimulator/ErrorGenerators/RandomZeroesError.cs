using CommonInterfaces;
using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class RandomZeroesError : SensorDataErrors, ISensorDataErrors
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
                        if(i+z < TempList.Count)
                        TempList[i+z] = 0.0;
                    }

                    //äußere Zählschleife soll gesetzte Fehler nicht überschreiben
                    i += ErrorLength - 1;
                }
            }
            return TempList;
        }
    }
}
