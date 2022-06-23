using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class Superposition : SensorDataSimualtor<double>
    {
        List<double> Oscillation1;
        List<double> Oscillation2;
        public Superposition(List<double> Oscillation1, List<double> Oscillation2)
        {
            this.Oscillation1 = Oscillation1;
            this.Oscillation2 = Oscillation2;
        }
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

            for (int i = 0; i < LongList.Count; i++)
            {
                // Die kurze Liste hat an der Stelle keine Werte mehr
                if (i >= ShortList.Count)
                {
                    Result.Add(LongList[i]);
                }
                else
                {
                    Result.Add(ShortList[i] + LongList[i]);
                }


            }
            return Result;

        }
    }
}
