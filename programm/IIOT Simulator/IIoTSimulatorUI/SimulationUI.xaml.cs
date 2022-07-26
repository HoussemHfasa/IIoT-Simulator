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
using DummySensorandSensorgroups;
using SensorAndSensorgroup;
using SensorDataSimulator;
using System.Threading.Tasks;



namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für SimulationUI.xaml
    /// </summary>
    public partial class SimulationUI : Window
    {
        Sensorgroups Sensorgroup;

      /*  private Line xAxisLine, yAxisLine;
        private double xAxisStart = 30, yAxisStart = 30, interval = 40;
        private Polyline chartPolyline;
        private Point origin;
        private List<Holder> holders;*/
        private ChartValues<double> values;
        
        private Dictionary<string, ChartValues<double>> SensorValues;



        //
        //
        // Für neue Linechart
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        //
        //
        //



        public SimulationUI(ref Sensorgroups ExistingSensorgroup)
        {

            this.Sensorgroup = ExistingSensorgroup;
            InitializeComponent();
            SensorValues = new Dictionary<string, ChartValues<double>>();
            foreach (string Sensor in Sensorgroup.allchildren.Keys)
            {
                if (!(Sensorgroup.allchildren[Sensor].Sensordaten == null))
                {
                    SensorValues.Add(Sensor, EinzelneSensorDaten(Sensorgroup.allchildren[Sensor]));
                }

            }

            SeriesCollection = new SeriesCollection
            {
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");




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
                                    //    PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                                    //    PointGeometrySize = 50,
                PointForeground = Brushes.Gray
            });
            DataContext = this;
                 }
             }
        }
        public ChartValues<double> EinzelneSensorDaten(TreeNode Sensor)
            {
            values = new ChartValues<double>();

                if (Convert.ToString(Sensor.Sensordaten.Sensortype) == "Rauchmelder")
                {
                    for (int i = 0; i < Convert.ToInt16(Sensor.Sensordaten.AmmountofValues); i++)
                    {
                    values.Add(Convert.ToBoolean(Sensor.Sensordaten.Values[i]) ? 1:0);
                }
                }
                else
                {
                    for (int i = 0; i < Convert.ToInt16(Sensor.Sensordaten.AmmountofValues); i++)
                    {
                        values.Add(Convert.ToDouble(Sensor.Sensordaten.Values[i]));
                    }
                }
                return values;
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

        private void Verbinden(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Die Sensordaten wurden an den Broker gesendet.");
        }

        private void DatenSenden(object sender, RoutedEventArgs e)
        {
            
            string k = "Die Sensordaten wurden an den Broker gesendet.";
            foreach(string sensor in SensorValues.Keys)
            {
                // Logbuch, welche Daten gesendet wurden
                k += $"\nDer Sensor {sensor} hat {SensorValues[sensor].Count} Wert an den Broker gesendet";
                for (int i = 0; i < SensorValues[sensor].Count; i++)
                {
                    ScrollTextBlock.Text += $"\n Der Sensor { sensor} hat" + (SensorValues[sensor][i]) + "Wert an den Broker gesendet";
                }
                // Hier müssten Daten an Broker gesendet werden
            }

            MessageBox.Show(k);
        }

        private void Abbrechen(object sender, RoutedEventArgs e)
        {
                MainWindow objectStartseite2 = new MainWindow();
                this.Visibility = Visibility.Hidden; 
                objectStartseite2.Show();
        }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Sensorauswaehlen(object sender, RoutedEventArgs e)
        {
            
        }
    }

}
