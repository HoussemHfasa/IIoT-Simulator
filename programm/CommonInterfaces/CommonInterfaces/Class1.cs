using System;
using System.Collections.Generic;

namespace CommonInterfaces
{
    public interface ISenor<T>
    {
        public string Sensor_id { get; set; }
        public string[,] Id_Adresse { get; set; }
        public string Sensortype { get; set; }
        public string Einheit { get; set; }
        public DateTime CreationDate { get; }
        public TimeSpan CreationTime { get; }
        public int Werteanzahl { get; }
        public int Timeinterval { get; }

        public abstract T[] Getvalues();
        public abstract void SetParameter(List<T> Values);
        //Übergabeparameter (Liste der Werte) fehlt? 
    }
    public interface ISensorGroups
    {

        string Adresse { get; set; }
        string[] SensorIds { get; set; }

        public void Sensorhinzufuegen();

        public void Sensorloeschen();



    }

    public interface IMQTTCommunicator
    {
        //Welche Informationen werden zur Registrierung des Clients benoetigt? /Paul
        public void RegisterClient(string clientId, bool isGroup);


        //  "name" in diesem Kontext nicht selbsterklärend /Paul
        public void SubscribeTopic(string clientId, string topicName);

        // "name" nicht selbsterklärend
        public void CreateTopic(string clientId, string topicName);

        // wird "name" gepublished?
        public void PublishToTopic(string clientId, string topicName, dynamic value);

        public void SetNewBroker(dynamic Host, int Port);


        //public void SetQoS(int ServiceLevel)
        //
    }

    public interface IDatastorage
    {
        // die Daten die von der Sensoren kommt
        public object Data { get; set; }
        //Dateipfad der Datei
        public string filepath { get; set; }
        public void JsonSerialize(object data, string filepath);
        public object JsonDeserialize(Type dataType, string filepath);

        public object LoadSensorgroup(string filepath);
        public object LoadBrockerProfile(string filepath);
        public void SaveSensorgroup(object data, string filepath);
        public void SavebrockerProfile(object data, string filepath);
    }

  
    public interface ISensorDataErrors<T>  
    {
        //Fehlerrate
        public double ErrorRatio { get; }
        // Fehlerdauer
        public int ErrorLength { get; }
        //FehlerMaximum
        public double ErrorMax { get; }
        //FehlerMinimum
        public double ErrorMin { get; }

         
        //Einstellung des Messfehlers/Signalfehlers, Fehlerwerte werden ebenfalls beim Konstruktor als Parameter übergeben
        bool SetSensorErrors(double ErrorRatio, int ErrorLength, double ErrorMax, double ErrorMin);
        bool ResetSensorErrors();

        List<T> GetSensorDataWithErrors(List<T> SensorDataWithoutErorrs);



    }
    public interface ISensorDataSimulator

    {

        public int AmmountofValues { get; }
                       

        //Funktionen zur Erzeugung von Messwerten

        /// <summary>
        /// Erzeugt eine Liste mit stetigen, normalverteilten double Werten. Sollte SetzeSignalfehler eingestellt sein, werden nichtstetige Fehler eingefügt
        /// </summary>
        /// <param name="Mittelwert"> </param>
        /// <returns>Liste zufälliger, normalverteilter double Werte</returns>
        List<double> GetStandardDeviationValues(double Mean, double StandardDeviation, int AmmountofValues);

        List<bool> GetRandomBoolValues(double Wechselwarscheinlichkeit, int AmountofValues);

        List<double> GetExponentialValues(double Basis, double Exponent, int AmountofValues);

        List<double> GetLinearValues(double Slope, double XShift, int AmmountofValues);

        List<double> GetHarmonicOscillation(double Amplitude, double Frequency, double Phase, int AmmountofValues);

        List<double> GetDampedOscillation(double Amplitude, double Dampingratio, double Frequency, double Phase, int AmmountofValues);

        List<double> GetSuperposition(List<double> Oscillation1, List<double> Oscillation2);

        


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

