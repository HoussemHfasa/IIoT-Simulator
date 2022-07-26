using CommonInterfaces;
using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class BurstNoise : SensorDataErrors, ISensorDataErrors
    {
        // Klasse zur Erzeugung von Fehlern Aufgrund von Burst-Signalen 
        double BurstValue;

        List<double> TempList;
        Random Rand = new Random();

        // Konstruktor erhält alle benötigten Werte als Parameter
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
        // Fehlererzeugungsmethode erhält die Liste mit Daten ohne Fehler als Übergabeparameter
        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            TempList = SensorDataWithoutErorrs;
            // Für jedes Element in der Liste:
            for(int i = 0; i < SensorDataWithoutErorrs.Count; i++)
            {
                // Falls Zufallswert kleiner Fehlerrate :
                if(Rand.NextDouble() <= ErrorRatio)
                {
                    // Fehler erzeugen auf gesamter Fehlerlänge
                    for (int z = 0; z < ErrorLength; z++)
                    {
                        try
                        { 
                        TempList[i + z] = BurstValue;
                        }
                        catch(ArgumentOutOfRangeException e)
                        {
                            TempList.Add(BurstValue);
                        }
                    }
                    
                    i += ErrorLength - 1;
                }
            }

            return TempList;
        }


    }
}
