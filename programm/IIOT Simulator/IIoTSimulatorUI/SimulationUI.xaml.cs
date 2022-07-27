using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using SensorAndSensorgroup;
using SensorDataSimulator;
using System.Threading.Tasks;
using MQTTnet.Exceptions;
using System.Timers;
using System.Threading;
using System.Windows.Threading;

//using System.Threading;

namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für SimulationUI.xaml
    /// </summary>
    public partial class SimulationUI : Window
    {
        // Object der Sensorgruppe
        Sensorgroups Sensorgroup;

        // Für neue Linechart, SeriesCollection beinhaltet alle "Linien"     
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private ChartValues<double> values;
        // Sensordaten als Dictionary für Hinzufügen zur Lineseriens
        private Dictionary<string, ChartValues<double>> SensorValues;

        // Dispatcher Timer für Timerevent und Daten in Zeitintervallen an Broker senden
        DispatcherTimer timer;

        // Variable zum Abspeichern des aktuellen Sensorwertes
        int CurrentValueNumber = 0;

        // Konstruktor für Simulationsseite, Sensorgruppe die angezeigt werden soll wird dem KOnstruktor übergeben
        public SimulationUI(Sensorgroups ExistingSensorgroup)
        {
            // Timerobjekt erstellen
            timer = new DispatcherTimer();

            int AmmountofValuesMax = 0;
            int[] Labelsint = null;
            this.Sensorgroup = ExistingSensorgroup;

            //Seite initialisieren
            InitializeComponent();


            SensorValues = new Dictionary<string, ChartValues<double>>();

            // Sammlung von Linien
            SeriesCollection = new SeriesCollection
            {
            };

           
            //Für jedes Element in Allchildren
            foreach (string Sensor in Sensorgroup.allchildren.Keys)
            {
                // Wenn Element ein Sensor ist: 
                if (!(Sensorgroup.allchildren[Sensor].Sensordaten == null))
                {
                    // Sensordaten in Linechart Kompatibles Format abspeichern
                    SensorValues.Add(Sensor, EinzelneSensorDaten(Sensorgroup.allchildren[Sensor]));

                    // neue Lineseries erstellen
                    SeriesCollection.Add(new LineSeries
                    {
                        Title = Sensor,
                        Values = SensorValues[Sensor],
                        LineSmoothness = 0, //0: straight lines, 1: really smooth lines

                        PointForeground = Brushes.Gray
                    });

                    //Label entsprechend der Höchsten Werteanzahl in der Sensorgruppe abspeichern
                    if (Convert.ToInt16(Sensorgroup.allchildren[Sensor].Sensordaten.AmmountofValues) > AmmountofValuesMax)
                    {
                        AmmountofValuesMax = Convert.ToInt16(Sensorgroup.allchildren[Sensor].Sensordaten.AmmountofValues);
                        Labelsint = new int[AmmountofValuesMax];
                        for (int i = 0; i < AmmountofValuesMax; i++)
                        {
                            Labelsint[i] = i;
                        }
                    }
                }

            }


            // Int werte des Labelsint in String umwandeln
            Labels = new string[AmmountofValuesMax];
            if (!(Labelsint == null))
            {
                Labels = Array.ConvertAll(Labelsint, x => x.ToString());
            }

            // Beschriftung der Werte Y-Achse
            YFormatter = value => value.ToString("");

            DataContext = this;

        }




        // Button Programm schließen 
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Startseite Button
        private void StartseiteButton(object sender, RoutedEventArgs e)
        {
            MainWindow objectStartseite2 = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objectStartseite2.Show();
        }

        // BrokerSettings Button
        private void BrokerSettingsClick(object sender, RoutedEventArgs e)
        {
            BrokerEinstellungenUI objectBrokerEinstellungen = new BrokerEinstellungenUI();
            this.Visibility = Visibility.Hidden;
            objectBrokerEinstellungen.Show();
        }


        // Daten einzeln Senden Button
        private void DatenSenden(object sender, RoutedEventArgs e)
        {
            // Für jeden Schlüssel(Sensornamen) in SensorValues:
            foreach (string sensor in SensorValues.Keys)
            {

                //ArgumentoutofRange mit iF Abfrage abfangen
                if (CurrentValueNumber < SensorValues[sensor].Count)
                {
                    // Wenn Broker connected ist
                    if (MQTT.BrokerCom.IsConnected())
                    {
                        // Logbuch, welche Daten gesendet wurden
                        ScrollTextBlock.Text += $"\n Der Sensor { sensor} hat den Wert " + (SensorValues[sensor][CurrentValueNumber]) + " an den Broker gesendet";

                        // An Broker senden
                        MQTT.BrokerCom.PublishToTopic(Convert.ToString(Sensorgroup.allchildren[sensor].Sensordaten.Topic), Convert.ToString(SensorValues[sensor][CurrentValueNumber]));
                        CurrentValueNumber += 1;
                    }
                    else  // Broker nicht verbunden
                    {
                        ScrollTextBlock.Text += $"\n Der Sensor { sensor} konnte den Wert " + (SensorValues[sensor][CurrentValueNumber]) + " nicht an den Broker senden- Keine Verbindung zum Broker";
                        CurrentValueNumber += 1;
                    }
                }
            }
        }




        // Button abbrechen geht zu Hauptseite
        private void Abbrechen(object sender, RoutedEventArgs e)
        {
            MainWindow objectStartseite2 = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objectStartseite2.Show();
        }


        // Funktion, die einen Sensor als Parameter erhält und seine Daten als ChartValues<double> zurückgiebt. Boolwerte werden als 0 und 1 dargestellt.
        public ChartValues<double> EinzelneSensorDaten(TreeNode Sensor)
        {
            values = new ChartValues<double>();

            // Wenn Sensortyp mit Bool Werten arbeitet, Boolwerte in double umwandeln und Chartvalues abspeichern
            if (Convert.ToString(Sensor.Sensordaten.Sensortype) == "Rauchmelder")
            {
                for (int i = 0; i < Convert.ToInt16(Sensor.Sensordaten.AmmountofValues); i++)
                {
                    values.Add(Convert.ToBoolean(Sensor.Sensordaten.Values[i]) ? 1 : 0);
                }
            }
            // Sonst Double Werte in ChartValues abspeichern
            else
            {
                for (int i = 0; i < Convert.ToInt16(Sensor.Sensordaten.AmmountofValues); i++)
                {
                    values.Add(Convert.ToDouble(Sensor.Sensordaten.Values[i]));
                }
            }
            return values;
        }
        
        // Button für in Zeitintervall senden startet den Timer
        private void DatenSendenZeit(object sender, RoutedEventArgs e)
        {
            
            timer.Interval = new TimeSpan(0, 0, Convert.ToInt32(TextBoxZeit.Text));
            timer.Tick += new EventHandler( timer_Tick);
            timer.Start();

            // Für jeden Schlüssel(Sensornamen) in SensorValues:


        }
        // Das Timer Tick ereignis
        void timer_Tick(object sender, EventArgs e)
        {

            // Für jeden Schlüssel(Sensornamen) in SensorValues:
            foreach (string sensor in SensorValues.Keys)
            {

                //ArgumentoutofRange mit iF Abfrage abfangen
                if (CurrentValueNumber < SensorValues[sensor].Count)
                {
                    // Wenn Broker connected ist
                    if (MQTT.BrokerCom.IsConnected())
                    {
                        // Logbuch, welche Daten gesendet wurden
                        ScrollTextBlock.Text += $"\n Der Sensor { sensor} hat den Wert " + (SensorValues[sensor][CurrentValueNumber]) + " an den Broker gesendet";

                        // An Broker senden
                        MQTT.BrokerCom.PublishToTopic(Convert.ToString(Sensorgroup.allchildren[sensor].Sensordaten.Topic), Convert.ToString(SensorValues[sensor][CurrentValueNumber]));
                        CurrentValueNumber += 1;
                    }
                    else  // Broker nicht verbunden
                    {
                        ScrollTextBlock.Text += $"\n Der Sensor { sensor} konnte den Wert " + (SensorValues[sensor][CurrentValueNumber]) + " nicht an den Broker senden- Keine Verbindung zum Broker";
                        CurrentValueNumber += 1;
                    }
                }
            }

        }
       
    }

}
