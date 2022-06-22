using System;
using System.Collections.Generic;

namespace CommonInterfaces
{
    public interface ISensor<T>
    {
        // Eindeutige ID des Sensors
        public string Sensor_id { get; set; }

        // MQTT Topic, auf die der Sensor Daten sendet
        public string Topic { get; set; }

        // Sensortyp
        public string Sensortype { get; }
        // pysikalische Einhei
        public string Unit { get; set; }

       /* //Startzeit erster Wert?
        public DateTime CreationDate { get; }
        //Zeitspanne zwischen zwei Werten?
        public TimeSpan CreationTime { get; } */
        public int AmmountofValues { get; }
        public int Timeinterval { get; set; }

        
        //Gibt eine Liste mit Sensordaten zurück
        public abstract List<T> GetValues();
        
        //Zum Abspeichern der Sensordaten im Objekt
        public void SetValues(List<T> Values);
        public abstract void JsonSerialize(ISensor<T> data, string filepath);
        public abstract ISensor<T> JsonDeserialize(string filepath, string Sensor_id);
    }
    public interface ISensorGroups
    {
        
        // algemeine Adresse für die Sensorgruppe
        string Base { get; set; }

       
        // Unterordner Name
        public string Node { get; set; }

        //List von Sensoren und ihren Node
        public Dictionary<string, List<string>> SensorIds { get; }



        /// <summary>
        /// Ein Sensor_Id in der SensorIds Liste hinzufügen
        /// </summary>
        /// <param name="sensorids"> die Liste von Sensorids </param>
        /// <param name="sensorid"> das id zu hinzufugen zur Id_liste </param>
        public void Sensorhinzufuegen(string sensorid, string NodeName, string Basename);

        /// <summary>
        /// Ein Sensor_Id von der SensorIds Liste loeschen
        /// </summary>
        /// <param name="sensorids"> die Liste von Sensorids </param>
        /// <param name="sensorid"> das id zu loeschen von der Liste </param>
        public void Sensorloeschen(string sensorid, string NodeName, string Basename);

        //Stamm hinzufügen
        public void AddBase(string BaseName);

        // Unterordner hinzufügen
        public void AddNode(string NodeName, string Basename);

        //Löschen von Stamm/Unterordner
        public void DeleteNode(string NodeName, string Basename);
        public void DeleteBase(string BaseName);


    }
   
    public interface IMQTTCommunicator
    {
        
        // Überlegung ob Rückgabewerte benötigt wird für Rückmeldung von Erfolg/Nichterfolg
        //Welche Informationen werden zur Registrierung des Clients benoetigt? /Paul
        /// <summary>
        /// Erstellung einer Verbindung zum Broker
        /// </summary>
        /// <param name="Host">Domainname oder IP-Adresse des Brokers</param>
        /// <param name="Port">Port des Brokers</param>
        public void ConnectToBroker(string Host, int Port);
        
        public void RegisterClient(string clientId, bool isGroup);

        public void SubscribeTopic(string clientId, string topicName);

        public void CreateTopic(string clientId, string topicName);

        public void PublishToTopic(string clientId, string topicName, string messagePayload);

        public void SetNewBroker(string Host, int Port);

        public List<string> GetTopics();
        public List<string> GetClients();
    }
    public interface IBrokerProfile
    {
        public string HostName_IP { get; set; }
        public uint Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    public interface IDatastorage<T> 
    {

        /// <summary>
        /// serialise die Daten zu Textdatei
        /// </summary>
        /// <param name="data"> die Daten zu speichern </param>
        /// <param name="filepath"> Dateipfad, wo die Daten werden gespeichert </param>
     //   public virtual void JsonSerialize(ISensor<T> data, string filepath);
        /// <summary>
        /// deserialise Textdatei zu Json datei ,um die gespeicherte Datei zu laden
        /// </summary>
        /// <param name="filepath"> Dateipfad, wo die Daten sind gespeichert </param>
      //  public virtual ISensor<T> JsonDeserialize(string filepath, string Sensortype);
        /// <summary>
        /// deserialise Textdatei zu Json datei ,um die gespeicherte sensorgruppe zu laden
        /// </summary>
        /// <param name="filepath"> Dateipfad, wo die Daten sind gespeichert </param>
        public Dictionary<string, List<string>> LoadSensorgroup(string Base, string Filepath);
        /// <summary>
        /// deserialise Textdatei zu Json datei ,um die gespeicherte BrockerProfile zu laden
        /// </summary>
        /// <param name="filepath"> Dateipfad, wo die Daten sind gespeichert </param>
        public IBrokerProfile LoadBrokerProfile(string filepath);
        /// <summary>
        /// speichern die Sensorgruppe
        /// </summary>
        ///  <param name="data"> die liste mit der Sensor_ids des gruppes </param>
        /// <param name="filepath"> Dateipfad, wo die Sensorgroup werden gespeichert </param>
        public void SaveSensorgroup(Dictionary<string, List<string>> SensorListe, string Base, string Filepath);
        /// <summary>
        /// speichern die BrockerProfile
        /// </summary>
        ///  <param name="data"> die BrockerProfileDaten </param>
        /// <param name="filepath"> Dateipfad, wo die BrockerProfileDaten werden gespeichert </param>
        public void SavebrokerProfile(IBrokerProfile data, string filepath);
        //Ladung der Liste von Basenamen
        public List<string> BasenameDeserialize(string filepath);
        //Speicherung der Liste von Basenamen
        public void BasenamSerialize(List<string> data, string filepath);
    }

  
    public interface ISensorDataErrors  
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
       
        List<double> GetSensorDataWithErrors(List<double> SensorDataWithoutErorrs);



    }
    public interface ISensorDataSimulator<T>

    {

        public uint AmmountofValues { get; set; }

        public List<T> GetSimulatorValues();
        //Funktionen zur Erzeugung von Messwerten

        /// <summary>
        /// Erzeugt eine Liste mit stetigen, normalverteilten double Werten der Anzahl AmmountofValues. 
        /// </summary>
        /// <param name="Mean"> Mittelwert </param>
        /// <param name="StandardDeviation"> Standardabweichung </param>
        /// <returns>Liste zufälliger, normalverteilter double Werte</returns>
        //List<double> GetStandardDeviationValues(double Mean, double StandardDeviation);

        /// <summary>
        /// Erzeugt eine Liste mit zufällig erzeugten bool Werten 
        /// </summary>
        /// <param name="Toggleprobability"> Wert zw. 0-1. Umschaltwarscheinlichkeit 0 -> 1 bzw. 1 -> 0 </param>
        //List<bool> GetRandomBoolValues(double Toggleprobability);

       

        /// <summary>
        /// Erzeugt Mithilfe einer harmonischen Schwingungsgleichung eine Liste an double Werten
        /// </summary>
        /// <param name="Amplitude"> Amplitude </param>
        /// <param name="Period"> Periodendauer </param>
        /// <param name="Phase"> Phasenverschiebung </param>
        //List<double> GetHarmonicOscillation(double Amplitude, double Period, double Phase);

        /// <summary>
        /// Erzeugt Mithilfe einer gedämüften harmonischen Schwingungsgleichung eine Liste an double Werten
        /// </summary>
        /// <param name="Amplitude"> Amplitude </param>
        /// <param name="Period"> Periodendauer </param>
        /// <param name="Dampingratio"> Dämpfungsrate </param>
        /// <param name="Phase"> Phasenverschiebung </param>
        //List<double> GetDampedOscillation(double Amplitude, double Dampingratio, double Period, double Phase);


        /// <summary>
        /// Überlagerung zweier Schwingungen/Messwertreihen 
        /// </summary>
        /// <param name="Oscillation1"> Liste mit Sensordaten, bevorzugt schwingende Messwerte </param>
        //List<double> GetSuperposition(List<double> Oscillation1, List<double> Oscillation2);

        
    }
        
        
       
}

