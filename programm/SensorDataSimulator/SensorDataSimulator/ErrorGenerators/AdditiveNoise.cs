﻿using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class AdditiveNoise : SensorDataErrors, CommonInterfaces.ISensorDataErrors
    {
        // Klasse zur Erzeugung von Fehlern Aufgrund von Rauschen(Noise)
        List<double> Noise;
       
        //Konstruktor erhält Noise/Rauschen Werte
        public AdditiveNoise(List<double> Noise)
        {
            this.Noise = Noise;
        }

        //Fehlererzeugungsmethode
        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            // Achtung, wenn Noise Daten weniger sind als Sensordaten..
            List<double> Result = new List<double>();
            List<double> ShorterList;
            List<double> LongerList;

            // Herausfinden, welche die kürzere Liste ist
            if (SensorDataWithoutErorrs.Count <= Noise.Count)
            {
                ShorterList = SensorDataWithoutErorrs;
                LongerList = Noise;
            }
            else
            {

                ShorterList = Noise;
                LongerList = SensorDataWithoutErorrs;
            }

            // Für jeden Wert der längeren Liste :
            for (int i = 0; i < SensorDataWithoutErorrs.Count; i++)
            {
                // Die kurze Liste hat an der Stelle keine Werte mehr: Wert der langen Liste nehmen
                if (i >= ShorterList.Count)
                {
                    Result.Add(LongerList[i]);
                }
                else
                {
                    Result.Add(ShorterList[i] + LongerList[i]);
                }


            }
            return Result;
        }
    }
}
