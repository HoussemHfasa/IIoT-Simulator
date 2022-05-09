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
        /// <summary>
        /// Setzt die Parameter für Fehlerwerte
        /// </summary>
        /// <param name="ErrorRatio"> Fehlerhäufigkeit. Wert muss zwischen 0.0 und 1.0 liegen </param>
        /// <param name="ErrorLength"> Länge des Fehlers </param>
        /// <param name="ErrorMax"> Maximalwert des Fehlers</param>
        /// <param name="ErrorMin"> Minimalwert des Fehlers</param>
        bool SetSensorErrors(double ErrorRatio, int ErrorLength, double ErrorMax, double ErrorMin);

       
        /// <summary>
        /// Setzt die Parameter für Fehlerwerte zurück auf 0
        /// </summary>
        bool ResetSensorErrors();

        /// <summary>
        /// Nimmt eine Liste von Messwerten mit double Werten, baut Fehler/Rauschen ein und gibt die editierte Liste zurück. Fehlererzeugungart siehe KlassenDokumentation
        /// </summary>
        /// <param name="SensorDataWithoutErorrs"> ursprüngliche Sensordaten/Messwerte </param>
       
        List<T> GetSensorDataWithErrors(List<T> SensorDataWithoutErorrs);



    }
    public interface ISensorDataSimulator

    {

        public int AmmountofValues { get; }


        //Funktionen zur Erzeugung von Messwerten

        /// <summary>
        /// Erzeugt eine Liste mit stetigen, normalverteilten double Werten der Anzahl AmmountofValues. 
        /// </summary>
        /// <param name="Mean"> Mittelwert </param>
        /// <param name="StandardDeviation"> Standardabweichung </param>
        /// <returns>Liste zufälliger, normalverteilter double Werte</returns>
        List<double> GetStandardDeviationValues(double Mean, double StandardDeviation, int AmmountofValues);

        /// <summary>
        /// Erzeugt eine Liste mit zufällig erzeugten bool Werten 
        /// </summary>
        /// <param name="Toggleprobability"> Wert zw. 0-1. Umschaltwarscheinlichkeit 0 -> 1 bzw. 1 -> 0 </param>
        List<bool> GetRandomBoolValues(double Toggleprobability, int AmountofValues);

        /// <summary>
        /// Erzeugt Mithilfe einer Exponentialfunktion eine Liste an double Werten. 
        /// </summary>
        List<double> GetExponentialValues(double Basis, double Exponent, int AmountofValues);

        /// <summary>
        /// Erzeugt Mithilfe einer Linearfunktion eine Liste an double Werten
        /// </summary>
        /// <param name="Slope"> Steigung </param>
        List<double> GetLinearValues(double Slope, double XShift, int AmmountofValues);

        /// <summary>
        /// Erzeugt Mithilfe einer harmonischen Schwingungsgleichung eine Liste an double Werten
        /// </summary>
        /// <param name="Amplitude"> Amplitude </param>
        /// <param name="Frequency"> Frequenz </param>
        /// <param name="Phase"> Phasenverschiebung </param>
        List<double> GetHarmonicOscillation(double Amplitude, double Frequency, double Phase, int AmmountofValues);

        /// <summary>
        /// Erzeugt Mithilfe einer gedämüften harmonischen Schwingungsgleichung eine Liste an double Werten
        /// </summary>
        /// <param name="Amplitude"> Amplitude </param>
        /// <param name="Frequency"> Frequenz </param>
        /// <param name="Dampingratio"> Dämpfungsrate </param>
        /// <param name="Phase"> Phasenverschiebung </param>
        List<double> GetDampedOscillation(double Amplitude, double Dampingratio, double Frequency, double Phase, int AmmountofValues);


        /// <summary>
        /// Überlagerung zweier Schwingungen/Messwertreihen 
        /// </summary>
        /// <param name="Oscillation1"> Liste mit Sensordaten, bevorzugt schwingende Messwerte </param>
        List<double> GetSuperposition(List<double> Oscillation1, List<double> Oscillation2);

        
        }
        
        
       
    }

