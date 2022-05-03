using System;
using System.Collections.Generic;

namespace CommonInterfaces
{
    public interface IDatamanager
    {


    }

    public interface IBrokerComm
    {


    }

    public interface IDatastorage
    {


    }

    public interface ISensorDataSimulator

    {
 


        List<double> Normalverteilung(double Mittelwert, double Standardabweichung, int Werteanzahl, double Fehlerhäufigkeit,
                                int Fehlerdauer, double MaximalwertFehler, double MinimalwertFehler);

        List<bool> Zufallsbool(double Wechselwarscheinlichkeit);

        List<double> Exponential(double Basis, double Exponent, double Faktor);

        List<double> Linear(double Steigung, double VerschiebungXAchse);




        // Idee:
        // Über Methode void/bool SetFehlerParameter die Fehlerparameter entgegennehmen und intern abspeichern.
        // bool SetFehlerParameter( double Fehlerhäufigkeit,int Fehlerdauer, double MaximalwertFehler, double MinimalwertFehler)
        // Fehlerparameter-Variablen nur mit public getter. Müssten die Variablen hier mit aufgeführt werden?


        // Dadurch werden Methodenaufrufe übersichtlicher/vereinfacht
        // Aktuell favorisierte Variante!
        // 




        // ALTERNATIVE:
        // Zur Vereinfachung der Nutzung und Lesbarkeit oben genannter Methoden werden die Fehlerparameter in eigener Klasse zusammengefasst
        // Dann würde für die Fehlergenerierung ein Objekt der Klasse Fehlerparameter genügen. 
        /*
          class FehlerParameter
        {
            double Fehlerhäufigkeit { get; }

            int Fehlerdauer { get; }
            double MaximalwertFehler { get; }
            double MinimalwertFehler { get; }

            bool IsFehler { get; }

            //Wenn Fehler vorhanden, dieser Konstruktor
            public FehlerParameter(double Fehlerhäufigkeit,
                                int Fehlerdauer, double MaximalwertFehler, double MinimalwertFehler)
            {
                this.Fehlerhäufigkeit = Fehlerhäufigkeit;
                this.Fehlerdauer = Fehlerdauer;
                this.MaximalwertFehler = MaximalwertFehler;
                this.MinimalwertFehler = MinimalwertFehler;
                this.IsFehler = true;
            }

            //Wenn kein Fehler vorhanden, dieser Konstruktor
            public FehlerParameter()
            {
                this.IsFehler = false;
            }


        */
        }
        
        
       
    }
}
