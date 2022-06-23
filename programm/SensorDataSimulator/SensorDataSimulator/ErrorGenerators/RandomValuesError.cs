using System;
using System.Collections.Generic;

namespace SensorDataSimulator
{
    public class RandomValuesError : SensorDataErrors, CommonInterfaces.ISensorDataErrors
    {

        private List<double> TempList;
        private Random Rand = new Random();

        public RandomValuesError(double ErrorRatio, int ErrorLength, double MaxError, double MinError)
        {
            // Errorlenght auf Uint ändern



            // Wenn Min und Max verwechselt wurde, entsprechend tauschen

            if(MaxError >= MinError)
            {
                this.ErrorMax = MaxError;
                this.ErrorMin = MinError;
            }
            else
            {
                this.ErrorMin = MaxError;
                this.ErrorMax = MinError;
            }

            this.ErrorRatio = ErrorRatio;
            this.ErrorLength = ErrorLength;
        }
        public override List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs)
        {
            //Eingaben auf Exceptions überprüfen
            //Fehlerrate zwischen 0 und 1?
            if (ErrorRatio > 1.0 || ErrorRatio < 0.0)
            {
                throw new ArgumentOutOfRangeException("Fehlerwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
            }

            TempList = SensorDataWithoutErorrs;

            // Bei Fehlerlänge 0 wird Liste zurückgegeben
            if (ErrorLength == 0)
                return TempList;

            //per Zufall mit Warscheinlichkeit entscheiden, ob Fehler 
            for(int i = 0; i < TempList.Count; i++)
            {
                if(Rand.NextDouble() <= ErrorRatio)
                {
                    //Fehler einbauen mit angegebener Fehlerlänge  
                    // Hier entstand Index out of Range Fehler, Absprache Rodner
                    
                    for (int z = 0; z < ErrorLength && z+i < TempList.Count; z++)
                    {
                        //potentieller Corner Case bei maximal und minimal möglichem doublewert entsteht Überlauf?
                        TempList[i+z] = Rand.NextDouble() * (ErrorMax-ErrorMin) + ErrorMin;
                        
                    }
                    // die äußere Zählschleife soll die eingebauten Fehlerwerte überspringen
                    i += ErrorLength - 1;
                    

                }
            }
            return TempList;
        }
    }
}
