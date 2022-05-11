using System;
using System.Collections.Generic;

namespace CommonInterfaces
{
    public interface ISenor<T>
    {
        public string Sensor_id { get; set; }

        // 2 Dimensionales Array
        public string[,] Id_Adresse { get; set; }
        public string Sensortype { get; }
        public string Einheit { get; set; }
        public DateTime CreationDate { get; }
        public TimeSpan CreationTime { get; }
        public int Werteanzahl { get; }
        public int Timeinterval { get; }
        //bekommt die Daten von der Sensoren
        public abstract List<T> Getvalues();
 
    }
    public interface ISensorGroups<T> where T : ISenor<T> 
    {
        // algemeine Adresse für die Sensorgruppe
        string Adresse { get; set; }

        // Das Verzeichnis, welche Sensoren sich wo befinden. (vorläufig)
        List<List<object>> GroupDirectory { get; set; }
        
        // alle Ids die in diesem Gruppe sind, evtl nicht mehr benötigt
        string[] SensorIds { get; set; }

        /// <summary>
        /// Ein Sensor_Id in der SensorIds Liste hinzufügen
        /// </summary>
        /// <param name="sensorids"> die Liste von Sensorids </param>
        /// <param name="sensorid"> das id zu hinzufugen zur Id_liste </param>
        public void Sensorhinzufuegen( T Sensor);

        /// <summary>
        /// Ein Sensor_Id von der SensorIds Liste loeschen
        /// </summary>
        /// <param name="sensorids"> die Liste von Sensorids </param>
        /// <param name="sensorid"> das id zu loeschen von der Liste </param>
        public void Sensorloeschen(string sensorid);

        //Stamm hinzufügen
        public void AddBase(string BaseName);

        // Unterordner hinzufügen
        public void AddNode(string NodeName, string[] NodeAdress);

        //Löschen von Stamm/Unterordner
        public void DeleteNodeBase(string[] Adress);



    }

    public interface IMQTTCommunicator
    {
        // Überlegung ob Rückgabewerte benötigt wird für Rückmeldung von Erfolg/Nichterfolg
        //Welche Informationen werden zur Registrierung des Clients benoetigt? /Paul
        public void ConnectToBroker(dynamic Host, int Port);
        
        public void RegisterClient(string clientId, bool isGroup);

        public void SubscribeTopic(string clientId, string topicName);

        public void CreateTopic(string clientId, string topicName);

        public void PublishToTopic(string clientId, string topicName, dynamic value);

        public void SetNewBroker(dynamic Host, int Port);

        public List<string> GetTopics();
        public List<string> GetClients();

    }

    public interface IDatastorage
    {
        // die Daten die von der Sensoren kommt
        public object Data { get; }
        //Dateipfad der Datei
        public string filepath { get; }
        /// <summary>
        /// serialise die Daten zu Textdatei
        /// </summary>
        /// <param name="data"> die Daten zu speichern </param>
        /// <param name="filepath"> Dateipfad, wo die Daten werden gespeichert </param>
        public void JsonSerialize(object data, string filepath);
        /// <summary>
        /// deserialise Textdatei zu Json datei ,um die gespeicherte Datei zu laden
        /// </summary>
        /// <param name="filepath"> Dateipfad, wo die Daten sind gespeichert </param>
        public Object JsonDeserialize(string filepath);
        /// <summary>
        /// deserialise Textdatei zu Json datei ,um die gespeicherte sensorgruppe zu laden
        /// </summary>
        /// <param name="filepath"> Dateipfad, wo die Daten sind gespeichert </param>
        public object LoadSensorgroup(string filepath);
        /// <summary>
        /// deserialise Textdatei zu Json datei ,um die gespeicherte BrockerProfile zu laden
        /// </summary>
        /// <param name="filepath"> Dateipfad, wo die Daten sind gespeichert </param>
        public object LoadBrockerProfile(string filepath);
        /// <summary>
        /// speichern die Sensorgruppe
        /// </summary>
        ///  <param name="data"> die liste mit der Sensor_ids des gruppes </param>
        /// <param name="filepath"> Dateipfad, wo die Sensorgroup werden gespeichert </param>
        public void SaveSensorgroup(object data, string filepath);
        /// <summary>
        /// speichern die BrockerProfile
        /// </summary>
        ///  <param name="data"> die BrockerProfileDaten </param>
        /// <param name="filepath"> Dateipfad, wo die BrockerProfileDaten werden gespeichert </param>
        public void SavebrockerProfile(object data, string filepath);
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
        /// Erzeugt Mithilfe einer harmonischen Schwingungsgleichung eine Liste an double Werten
        /// </summary>
        /// <param name="Amplitude"> Amplitude </param>
        /// <param name="Period"> Periodendauer </param>
        /// <param name="Phase"> Phasenverschiebung </param>
        List<double> GetHarmonicOscillation(double Amplitude, double Period, double Phase, int AmmountofValues);

        /// <summary>
        /// Erzeugt Mithilfe einer gedämüften harmonischen Schwingungsgleichung eine Liste an double Werten
        /// </summary>
        /// <param name="Amplitude"> Amplitude </param>
        /// <param name="Period"> Periodendauer </param>
        /// <param name="Dampingratio"> Dämpfungsrate </param>
        /// <param name="Phase"> Phasenverschiebung </param>
        List<double> GetDampedOscillation(double Amplitude, double Dampingratio, double Period, double Phase, int AmmountofValues);


        /// <summary>
        /// Überlagerung zweier Schwingungen/Messwertreihen 
        /// </summary>
        /// <param name="Oscillation1"> Liste mit Sensordaten, bevorzugt schwingende Messwerte </param>
        List<double> GetSuperposition(List<double> Oscillation1, List<double> Oscillation2);

        
        }
        
        
       
    }

