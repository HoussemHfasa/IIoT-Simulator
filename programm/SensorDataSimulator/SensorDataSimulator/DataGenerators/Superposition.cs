using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class Superposition : SensorDataSimualtor<double>
    {
        // Klasse zur Erzeugung von z.B. überlagerten Schwingungen

        List<double> Oscillation1;
        List<double> Oscillation2;

        // Konstruktor erhält 2 Listen vom Typ Double
        public Superposition(List<double> Oscillation1, List<double> Oscillation2)
        {
            // Parameter intern abspeichern
            this.Oscillation1 = Oscillation1;
            this.Oscillation2 = Oscillation2;
        }

        // Werteerzeugung
        public override List<double> GetSimulatorValues()
        {
            List<double> Result = new List<double>();
            List<double> ShortList;
            List<double> LongList;

            // Herausfinden, welche die kürzere Liste ist
            if (Oscillation1.Count <= Oscillation2.Count)
            {
                ShortList = Oscillation1;
                LongList = Oscillation2;
            }
            else
            {

                ShortList = Oscillation2;
                LongList = Oscillation1;
            }

            // Für jedes Element der längeren Liste..
            for (int i = 0; i < LongList.Count; i++)
            {
                // Wenn die kürzere Liste an der Stelle keine Werte mehr hat: Wert der langen Liste nehmen
                if (i >= ShortList.Count)
                {
                    Result.Add(LongList[i]);
                }
                //Ansonsten: Beide Listenwerte addieren
                else
                {
                    Result.Add(ShortList[i] + LongList[i]);
                }


            }
            return Result;

        }
    }
}
