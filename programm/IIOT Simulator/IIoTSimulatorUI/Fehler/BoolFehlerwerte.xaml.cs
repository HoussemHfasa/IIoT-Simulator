using System;
using System.Collections.Generic;
using System.Text;
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

namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für BoolFehlerwerte.xaml
    /// </summary>
    public partial class BoolFehlerwerte : Window
    {
        // Sensor der simuliert wird
        Sensor<bool> BoolSensor;

        //Liste enhält die generierten Werte
        List<bool> Datenliste;
        bool Boolconstructor;

        // Für Linechart
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private ChartValues<double> values;

        // Konstruktor bekommt die Referenz des neu erstellten bool Sensors übergeben
        public BoolFehlerwerte(ref Sensor<bool> NewSensor)
        {
            //Marker, welcher Konstruktor verwendet wurde
            Boolconstructor = true;
            this.BoolSensor = NewSensor;
            InitializeComponent();
        }

        //Konstruktor der bei Button Aktualisieren aufgerufen wird. Es wird zusätzlich noch das Berechnungsergebnis übergeben
        public BoolFehlerwerte(ref SensorAndSensorgroup.Sensor<bool> NewSensor, List<bool> Sensordaten)
        {
            Boolconstructor = false;
            this.BoolSensor = NewSensor;
            this.Datenliste = Sensordaten;
            values = new ChartValues<double>();
            int[] Labelsint = null;
            InitializeComponent();
            SeriesCollection = new SeriesCollection
            {
            };
            Labelsint = new int[Datenliste.Count];
            Labels = new string[Datenliste.Count];
            for (int i = 0; i < Datenliste.Count; i++)
            {
                Labelsint[i] = i;
            }
            if (!(Labelsint == null))
            {
                Labels = Array.ConvertAll(Labelsint, x => x.ToString());
            }
            values = new ChartValues<double>();
            for (int i = 0; i < Datenliste.Count; i++)
            {
                values.Add(Datenliste[i] ? 1 : 0);
            }

            // Beschriftung der Werte YAchse
            YFormatter = value => value.ToString("");
            //modifying the series collection will animate and update the chart

            SeriesCollection.Add(new LineSeries
            {
                Title = BoolSensor.Sensor_id,
                Values = values,
                LineSmoothness = 0,
                PointForeground = Brushes.Gray
            });
            DataContext = this;
        }

        private void SensordatenSpeichern(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Boolconstructor == true)
                {
                    // Nutzereingaben überprüfen
                    if (Convert.ToDouble(textBoxFehlerrate.Text) < 0.0 || Convert.ToDouble(textBoxFehlerrate.Text) > 1.0)
                    {
                        MessageBox.Show("Fehlerrate darf nicht unter 0 oder über 1 liegen.");
                    }
                    else
                    {
                        // Fehlerdatengenerator erzeugen, Sensordaten mit Fehlern versehen
                        RandomZeroesError DataGenerator = new RandomZeroesError(Convert.ToDouble(textBoxFehlerrate.Text), Convert.ToInt32(textBoxFehlerlänge.Text));
                        BoolSensor.SetValues(DataGenerator.GetSensorDataWithBoolErrors(BoolSensor.GetValues()));
                    }
                }
                else
                {
                    BoolSensor.SetValues(Datenliste);

                }
                
                Close();
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }
        }

        private void ProgrammSchlievßenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Aktualisieren(object sender, RoutedEventArgs e)
        {
            try
            {
                // Nutzereingaben überprüfen
                if (Convert.ToDouble(textBoxFehlerrate.Text) < 0.0 || Convert.ToDouble(textBoxFehlerrate.Text) > 1.0)
                {
                    MessageBox.Show("Fehlerrate darf nicht unter 0 oder über 1 liegen.");
                }
                else
                {
                    // Fehlerdatengenerator erzeugen, Sensordaten mit Fehlern versehen
                    RandomZeroesError DataGenerator = new RandomZeroesError(Convert.ToDouble(textBoxFehlerrate.Text), Convert.ToInt32(textBoxFehlerlänge.Text));
                    Datenliste = DataGenerator.GetSensorDataWithBoolErrors(BoolSensor.GetValues());
                    BoolFehlerwerte Aktualisirung = new BoolFehlerwerte(ref BoolSensor, Datenliste);
                    this.Visibility = Visibility.Hidden;
                    Aktualisirung.Show();
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }
        }
    }
}
