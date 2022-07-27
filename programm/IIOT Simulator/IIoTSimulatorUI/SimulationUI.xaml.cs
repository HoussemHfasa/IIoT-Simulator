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

        
        //
        //
        //

        // Variable zum Abspeichern des aktuellen Sensorwertes
        int CurrentValueNumber = 0;

        // Konstruktor für Simulationsseite, Sensorgruppe die angezeigt werden soll wird dem KOnstruktor übergeben
        public SimulationUI( Sensorgroups ExistingSensorgroup)
        {
            int AmmountofValuesMax=0;
            int[] Labelsint=null;
            this.Sensorgroup = ExistingSensorgroup;

            //Seite initialisieren
            InitializeComponent();

            

            // foreach(var in Sensorliste)
            /* {
             * 
             * - du kannst hier die Liste direkt mit ItemsSource übergeben -
                SensortypBox.ItemsSource = new List<string> { "Item1", "Item2", "Item3"};

             }*/

            SensorValues = new Dictionary<string, ChartValues<double>>();

            // TODO : Mit Houssem: Kommentar, was hier passiert und die beiden foreach Schleifen zusammenfassen
            //Für jedes Element in Allchildren
            foreach (string Sensor in Sensorgroup.allchildren.Keys)
            {
                // Wenn Element ein Sensor ist: 
                if (!(Sensorgroup.allchildren[Sensor].Sensordaten == null))
                {
                    SensorValues.Add(Sensor, EinzelneSensorDaten(Sensorgroup.allchildren[Sensor]));
                    if(Convert.ToInt16(Sensorgroup.allchildren[Sensor].Sensordaten.AmmountofValues)>AmmountofValuesMax)
                    {
                        AmmountofValuesMax = Convert.ToInt16(Sensorgroup.allchildren[Sensor].Sensordaten.AmmountofValues);
                        Labelsint = new int[AmmountofValuesMax];
                        for(int i=0;i<AmmountofValuesMax;i++)
                        {
                            Labelsint[i] = i;
                        }
                    }
                }

            }

            SeriesCollection = new SeriesCollection
            {
            };
             
            Labels = new string[AmmountofValuesMax];
            if (!(Labelsint==null))
            {
                Labels= Array.ConvertAll(Labelsint, x => x.ToString());
            }

            // Beschriftung der Werte Y-Achse
            YFormatter = value => value.ToString("");





             SensorValues = new Dictionary<string, ChartValues<double>>();
             //modifying the series collection will animate and update the chart
             foreach (string Sensor in Sensorgroup.allchildren.Keys)
             {
                 if (!(Sensorgroup.allchildren[Sensor].Sensordaten == null))
                 {
                    SensorValues.Add(Sensor, EinzelneSensorDaten(Sensorgroup.allchildren[Sensor]));

             SeriesCollection.Add(new LineSeries
            {
                Title = Sensor,
                Values = SensorValues[Sensor],
                LineSmoothness = 0, //0: straight lines, 1: really smooth lines

                PointForeground = Brushes.Gray
            });
            DataContext = this;
                 }
             }
        }



        
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void StartseiteButton(object sender, RoutedEventArgs e)
        {
            MainWindow objectStartseite2 = new MainWindow();
            this.Visibility = Visibility.Hidden; 
            objectStartseite2.Show();
        }

        private void BrokerSettingsClick(object sender, RoutedEventArgs e)
        {
            BrokerEinstellungenUI objectBrokerEinstellungen = new BrokerEinstellungenUI();
            this.Visibility = Visibility.Hidden; 
            objectBrokerEinstellungen.Show();
        }



        private void DatenSenden(object sender, RoutedEventArgs e)
        {

                string k = "Die Sensordaten wurden an den Broker gesendet.";
                foreach (string sensor in SensorValues.Keys)
                {
                    // Logbuch, welche Daten gesendet wurden

                    k += $"\nDer Sensor {sensor} hat {SensorValues[sensor].Count} Wert an den Broker gesendet und den Topic ist {Convert.ToString(Sensorgroup.allchildren[sensor].Sensordaten.Topic)}";

                    //ArgumentoutofRange mit iF Abfrage abfangen
                    if (CurrentValueNumber < SensorValues[sensor].Count)
                    {
                    if (MQTT.BrokerCom.IsConnected())
                    {
                        // 1 Datenpaket über Broker senden und Sensorwert in Scrollbox schreiben
                        ScrollTextBlock.Text += $"\n Der Sensor { sensor} hat den Wert " + (SensorValues[sensor][CurrentValueNumber]) + " an den Broker gesendet";
                        MQTT.BrokerCom.PublishToTopic(Convert.ToString(Sensorgroup.allchildren[sensor].Sensordaten.Topic), Convert.ToString(SensorValues[sensor][CurrentValueNumber]));
                        CurrentValueNumber += 1;
                    }
                    else
                    {
                        ScrollTextBlock.Text += $"\n Der Sensor { sensor} konnte den Wert " + (SensorValues[sensor][CurrentValueNumber]) + " nicht an den Broker senden- Keine Verbindung zum Broker";
                    }
                    }


                    /* Für alle Daten gleichzeitig
                    for (int i = 0; i < SensorValues[sensor].Count; i++)
                    {
                        ScrollTextBlock.Text += $"\n Der Sensor { sensor} hat das Wert " + (SensorValues[sensor][i]) + " an den Broker gesendet";
                        MQTT.BrokerCom.PublishToTopic(Convert.ToString(Sensorgroup.allchildren[sensor].Sensordaten.Topic),Convert.ToString(SensorValues[sensor][i]));
                    }
                    */
                    // Hier müssten Daten an Broker gesendet werden
                }
        }

            

      
            
        

        private void Abbrechen(object sender, RoutedEventArgs e)
        {
                MainWindow objectStartseite2 = new MainWindow();
                this.Visibility = Visibility.Hidden; 
                objectStartseite2.Show();
        }



        private void Sensorauswaehlen(object sender, RoutedEventArgs e)
        {
            
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

        private void DatenSendenZeit(object sender, RoutedEventArgs e)
        {
            // Die TextBox dazu heißt "TextBoxZeit" um die Eingabe zu übernehmen
        }
    }

}
