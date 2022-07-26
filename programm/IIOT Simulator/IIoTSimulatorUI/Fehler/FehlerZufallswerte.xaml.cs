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
    /// Interaktionslogik für FehlerZufallswerte.xaml
    /// </summary>
    public partial class FehlerZufallswerte : Window
    {
        Sensor<double> DoubleSensor;
        List<double> Datenliste;
        // Für neue Linechart

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private ChartValues<double> values;

        public FehlerZufallswerte(ref Sensor<double> NewSensor)
        {
            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }
        public FehlerZufallswerte(ref SensorAndSensorgroup.Sensor<double> NewSensor, List<double> Sensordaten)
        {
            this.DoubleSensor = NewSensor;
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
            values.AddRange(Datenliste);

            // Beschriftung der Werte YAchse
            YFormatter = value => value.ToString("");
            //modifying the series collection will animate and update the chart

            SeriesCollection.Add(new LineSeries
            {
                Title = DoubleSensor.Sensor_id,
                Values = values,
                LineSmoothness = 0,
                PointForeground = Brushes.Gray
            });
            DataContext = this;
        }

        private void Hinzufügen(object sender, RoutedEventArgs e)
        {
            try
            { 
            //Nutzereingabe überprüfen
            if(Convert.ToDouble(textBoxErrorRate.Text) < 0.0 || Convert.ToDouble(textBoxErrorRate.Text) > 1.0)
            {
                MessageBox.Show("Die Fehlerrate muss zwischen 0,0 und 1,0 liegen.");
            }
            else
            { 
            // Fehlerdatengenerator erzeugen, Sensordaten mit Fehlern versehen
            RandomValuesError DataGenerator = new RandomValuesError(Convert.ToDouble(textBoxErrorRate.Text), Convert.ToInt32(textBoxErrorLength.Text), Convert.ToDouble(textBoxMaxError.Text), Convert.ToDouble(textBoxMinError.Text));
            DoubleSensor.SetValues(DataGenerator.GetSensorDataWithErrors(DoubleSensor.GetValues()));

            Close();
            }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }
        }

        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Aktualisieren(object sender, RoutedEventArgs e)
        {
            try
            {
                //Nutzereingabe überprüfen
                if (Convert.ToDouble(textBoxErrorRate.Text) < 0.0 || Convert.ToDouble(textBoxErrorRate.Text) > 1.0)
                {
                    MessageBox.Show("Die Fehlerrate muss zwischen 0,0 und 1,0 liegen.");
                }
                else
                {
                    // Fehlerdatengenerator erzeugen, Sensordaten mit Fehlern versehen
                    RandomValuesError DataGenerator = new RandomValuesError(Convert.ToDouble(textBoxErrorRate.Text), Convert.ToInt32(textBoxErrorLength.Text), Convert.ToDouble(textBoxMaxError.Text), Convert.ToDouble(textBoxMinError.Text));
                    Datenliste= DataGenerator.GetSensorDataWithErrors(DoubleSensor.GetValues());
                    /*textBoxErrorRate.Clear();
                    textBoxErrorLength.Clear();
                    textBoxMaxError.Clear();
                    textBoxMinError.Clear();*/
                    FehlerZufallswerte Aktualisirung = new FehlerZufallswerte(ref DoubleSensor, Datenliste);
                    this.Visibility = Visibility.Hidden;
                    Aktualisirung.Show();
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }
            /* try
             {
                 //die Überprüfung Nutzereingaben. 
                 if (Convert.ToDouble(textBoxAmplitude.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl.Text) < 0)
                 {
                     MessageBox.Show("Amplitude,Dämpfungsrate, Periodendauer und Werteanzahl dürfen nicht negativ sein.");
                 }
                 else
                 {
                     // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in DoubleSensor abspeichern
                     DataGenerator = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude.Text), Convert.ToDouble(textBoxPeriodendauer.Text), Convert.ToDouble(textBoxPhasenverschiebung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                     Datenliste = DataGenerator.GetSimulatorValues();
                     textBoxAmplitude.Clear();
                     textBoxPeriodendauer.Clear();
                     textBoxPhasenverschiebung.Clear();
                     textBoxWerteanzahl.Clear();
                     HarmonischeSchwingung Aktualisirung = new HarmonischeSchwingung(ref DoubleSensor, Datenliste);
                     this.Visibility = Visibility.Hidden;
                     Aktualisirung.Show();
                 }
             }
             catch (System.FormatException)
             {
                 MessageBox.Show("Ungültige Eingabe");
             }*/
        }
    }
}
