using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class RandomBool : SensorDataSimualtor<bool>
    {
        private Random Rand = new Random();
        private double Toggleprobability;
        public RandomBool(double Toggleprobability, uint AmmountofValues)
        {
            //

            this.Toggleprobability = Toggleprobability;
            this.AmmountofValues = AmmountofValues;
        }
        public override List<bool> GetSimulatorValues()
        {
            List<bool> Result = new List<bool>();

            // Wechselwarscheinlichkeit zwischen 0.0 und 1.0, daher Exception Handling?
            if (Toggleprobability > 1.0 || Toggleprobability < 0.0)
            {
                throw new ArgumentOutOfRangeException("Wechselwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
            }

            // Erzeugung erster Wert
            Result.Add(Convert.ToBoolean(Rand.Next(0, 2)));

            //Werteerzeugung mit Wechselwarscheinlichkeit, i = 1, da bereits ein Wert erzeugt wurde
            for (int i = 1; i < AmmountofValues; i++)
            {

                // Zufalls Double kleiner als Wechselwarscheinlichkeit -> Wert wechselt
                if (Rand.NextDouble() <= Toggleprobability)
                {
                    Result.Add(!Result[i - 1]);
                }

                // Sonst: gleicher Wert
                else
                {
                    Result.Add(Result[i - 1]);
                }
            }


            return Result;
        }
    }
}
