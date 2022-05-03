using System;
using System.Collections.Generic;

namespace CommonInterfaces
{
    public interface IDatamanager
    {


    }

    public interface IMQTTCommunicator
    {
        public void registerClient()
        {

        }
        public void SubscribeTopic(string name)
        {

        }
        public void CreateTopic(string name)
        {

        }
        public void PublishToTopic(string name)
        {

        }

        //public void SetQoS(int ServiceLevel)
        //

    }

    public interface IDatastorage
    {


    }

  
    public interface ISensorDataSimulator

    {

        //Dokumentation vervollständigen
        /// <summary>
        /// Die Methode erzeugt eine Liste mit double Werten. Im Normalfall sind die Werte stetig und normalverteilt. Es können Fehler eingebaut werden
        /// </summary>
        /// <param name="Mittelwert"> </param>
        /// <returns>Liste zufälliger, normalverteilter double Werte</returns>
        List<double> Normalverteilung(double Mittelwert, double Standardabweichung, int Werteanzahl, double Fehlerhäufigkeit,
                                int Fehlerdauer, double MaximalwertFehler, double MinimalwertFehler);

        List<bool> Zufallsbool(double Wechselwarscheinlichkeit, int Werteanzahl);


        List<double> Exponential(double Basis, double Exponent, int Werteanzahl, double Fehlerhäufigkeit,
                                int Fehlerdauer, double MaximalwertFehler, double MinimalwertFehler);

        List<double> Linear(double Steigung, double VerschiebungXAchse, int Werteanzahl, double Fehlerhäufigkeit,
                                int Fehlerdauer, double MaximalwertFehler, double MinimalwertFehler);

        


        // Idee:
        // Über Methode void/bool SetFehlerParameter die Fehlerparameter entgegennehmen und intern abspeichern.
        // bool SetFehlerParameter( double Fehlerhäufigkeit,int Fehlerdauer, double MaximalwertFehler, double MinimalwertFehler)
        // Fehlerparameter-Variablen nur mit public getter. Müssten die Variablen hier mit aufgeführt werden?


        // Dadurch werden Methodenaufrufe übersichtlicher/vereinfacht
        // Problem: Wenn die Fehlerparameter gesetzt wurden und andere Funktion aufgerufen wird, sind die Parameter weiterhin hinterlegt. Das kann 
        // zu ungewolltem Output mit Fehlern führen.
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

