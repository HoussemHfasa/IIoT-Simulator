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
     
    }
   
    public interface ISensorGroups
    {
        //die Methode Basename/Nodename/Sensor nicht geeignet für unsere Programm ,da der Nutzer mehrere Unterordner erstellen kann
        //deswegen haben wir eine andere Lösung implimentiert
       
        //Dictionary enthalt die Unterordner- und Sensorenname und ihre Treenode
       // public Dictionary<string, TreeNode> allchildren { get; set; }
        //Dictionary enthalt die Basenames und ihre Unterordnername
      /*  public Dictionary<string, int> basenames_children { get; set; }
        //Liste von Basenames
        public List<string> basenames { get; set; }
        //sensorgroupname wichtig für die Speicherung und die Ladung der Sensorgruppe
        public string Sensorgroupname { get; set; }
        //Methode erstellt ein Basenames*/
        public void Add_new_Base(string basename);
        //Methode, die ein neues Unterordner hinzufügt
        public void Add_new_Node(string Mother, string Node);
        //Methode, die ein neues Sensor hinzufügt
        public void Add_new_Sensor(string Mother, dynamic new_Sensor);

    }

    public interface IMQTTCommunicator
    {
        
        // Überlegung ob Rückgabewerte benötigt wird für Rückmeldung von Erfolg/Nichterfolg
        //Welche Informationen werden zur Registrierung des Clients benoetigt? /Paul
        /// <summary>
        /// Erstellung einer einfachen Verbindung zum Broker, welche nicht gesichert ist
        /// </summary>
        /// <param name="Host">Domainname oder IP-Adresse des Brokers</param>
        /// <param name="Port">Port des Brokers</param>
        public string ConnectToBroker(string Host, int Port, string Username, string Password);

        public void CreateTopic(string topicName);


        /// <summary>
        /// Versendug von Messages an den Broker mit Voreinstellungen. Wird ausführbar, erst dann wenn der eine Verbindung zum
        /// Broker besteht. Nachrichten werden mit QoS = 2 gesendet.
        /// </summary>
        /// <param name="topicName">Als TopicName wird Pfad von einem Sensor genommen</param>
        /// <param name="messagePayload">Ubersendete Nachrichten an den Broker</param>
        public void PublishToTopic(string topicName, string messagePayload);

        /// <summary>
        /// Falls Benutzer eine neue Verbindung braucht, wird bestehende abgebrochen. Dann nochmal die Methode
        /// ConnectToBroker ausgeführt.
        /// </summary>
        /// <param name="Host">Erwuenschte Brokername bzw. IP-Adresse des Brokers</param>
        /// <param name="Port">Ein gültiger Port eingeben</param>
        /// <param name="Username">Eingestellte Username</param>
        /// <param name="Password">Dazugehörige Kennwort</param>
        public void SetNewBroker(string Host, int Port, string Username, string Password);

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
    
    public interface IDatastorage
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
        /// <summary>
        /// speichern die Sensorgruppe
        /// </summary>
        ///  <param name="data"> die liste mit der Sensor_ids des gruppes </param>
        /// <param name="filepath"> Dateipfad, wo die Sensorgroup werden gespeichert </param>
        public void SavebrokerProfile(IBrokerProfile data, string filepath);
        /// <summary>
        /// Ladung der Liste von Basenamen
        /// </summary>
        /// <param name="filepath"> Dateipfad, wo die SensorListe wird gespeichert </param>
        //Das Tree speichern
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

