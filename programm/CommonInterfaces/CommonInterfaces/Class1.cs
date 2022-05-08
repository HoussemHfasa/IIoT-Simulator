using System;
using System.Collections.Generic;

namespace CommonInterfaces
{
    public interface IDatamanager
    {
        // Idee:
        //string AdressBaum{get; set;}
        //string[] SensorIds{set; set;}
        //void sensorhinzufuegen(){}
        //void sensorloeschen(){}


        // Methodennamen mit Großbuchstaben beginnen /Paul
        /* Interfaces enthalten normalerweise keine Klassen!
         * Vorschlag: Unterteilung des Interface IDatamanager in ISensorGroup und ISensor.
         * dort jeweils die benötigten Methoden und Eigenschaften deklarieren (nicht implementieren!) /Paul
         */

        class Sensorgruppen
        {
            string Adresse { get; set; }
            string[] SensorIds { get; set; }
            public Sensorgruppen(string adresse, string[] sensorids )
            {
                Adresse = adresse;
                for(int i=0; i<sensorids.Length; i++)
                {
                    SensorIds[i] = sensorids[i];
                }
            }
        
        public void Sensorhinzufuegen()
        { }

        public void Sensorloeschen()
        { }
        }
         public abstract class  Sensor<T> 
        {
            public string[,] Id_Adresse { get; set; }
            public string Sensortype { get; set; }
            public string Einheit { get; set; }
            public DateTime CreationDate { get; }
            public TimeSpan CreationTime { get; }
            public int Werteanzahl { get; }
            public int Timeinterval { get; }

            public abstract T[] getvalues(); 
            public abstract void setvalues();
            //Übergabeparameter (Liste der Werte) fehlt?  
        }

        class TemperatureSensor : IDatamanager.Sensor<double>
        {
            public string Sensor_id { get; set; }
            private double[] values;
            public override double[] getvalues()
            {
                return new double[] { } ;
            }
            public override void setvalues()
            { }
        }
        class Feuchtigkeitssensor : IDatamanager.Sensor<double>
        {
            public string Sensor_id { get; set; }
            private double[] values;
          public override double[] getvalues()
            {
                return new double[] { };
            }
            public override void setvalues()
            { }
        }
        class Feuersensor : IDatamanager.Sensor<bool>
        {
            public string Sensor_id { get; set; }
            private double[] values;
          public override bool[] getvalues()
            {
                return new bool[] { };
            }
            public override void setvalues()
            { }
        }
        class Dehnungssensor : IDatamanager.Sensor<int>
        {
            public string Sensor_id { get; set; }
            private double[] values;
            public override int[] getvalues()
            {
                return new int[] { };
            }
            public override void setvalues()
            { }
        }
         class Lichtsensor : IDatamanager.Sensor<int>
        {
            public string Sensor_id { get; set; }
            private double[] values;
            public override int[] getvalues()
            {
                return new int[] { };
            }
            public override void setvalues()
            { }
        }
        class DrehmomentSensor : IDatamanager.Sensor<double>
        {
            public string Sensor_id { get; set; }
            private double[] values;
            public override double[] getvalues()
            {
                return new double[] { };
            }
            public override void setvalues()
            { }
        }
        class FuellstandsSensor : IDatamanager.Sensor<double>
        {
            public string Sensor_id { get; set; }
            private double[] Gasvalues;

            public override double[] getvalues()
            {
                return new double[] { };
            }
            public override void setvalues()
            { }
        }

    }

    public interface IMQTTCommunicator
    {
        //Welche Informationen werden zur Registrierung des Clients benoetigt? /Paul
        public void registerClient()
        {

        }

        //  "name" in diesem Kontext nicht selbsterklärend /Paul
        public void SubscribeTopic(string name)
        {

        }

        // "name" nicht selbsterklärend
        public void CreateTopic(string name)
        {

        }

        // wird "name" gepublished?
        public void PublishToTopic(string name)
        {

        }

        //public void SetQoS(int ServiceLevel)
        //

        // die geschweiften Klammern entfernen, nur Methoden deklarieren /Paul
    }

    public interface IDatastorage
    {


    }

  
    public interface ISensorDataSimulator

    {
        // Nutzereingaben zur Berechnung der Messwerte, setzen nur über Methoden möglich
        public double Mittelwert { get; }
        public double Standardabweichung { get; }
        public int Werteanzahl { get; }
        public double Fehlerhäufigkeit { get; }
        public double Fehlerdauer { get; }
        public double MaximalwertFehler { get; }
        public double MinimalwertFehler { get; }

        //Einstellung des Messfehlers/Signalfehlers
        bool SetzeSignalfehler(double Fehlerhäufigkeit, int Fehlerdauer, double MaximalwertFehler, double MinimalwertFehler);
        bool SignalfehlerZurücksetzen();

      

        //Funktionen zur Erzeugung von Messwerten

        /// <summary>
        /// Erzeugt eine Liste mit stetigen, normalverteilten double Werten. Sollte SetzeSignalfehler eingestellt sein, werden nichtstetige Fehler eingefügt
        /// </summary>
        /// <param name="Mittelwert"> </param>
        /// <returns>Liste zufälliger, normalverteilter double Werte</returns>
        List<double> Normalverteilung(double Mittelwert, double Standardabweichung, int Werteanzahl);


        List<bool> Zufallsbool(double Wechselwarscheinlichkeit, int Werteanzahl);


        List<double> Exponential(double Basis, double Exponent, int Werteanzahl);

        List<double> Linear(double Steigung, double VerschiebungXAchse, int Werteanzahl);

        


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

