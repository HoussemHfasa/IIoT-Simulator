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
    /// Interaktionslogik für GedaempfteSchwingung.xaml
    /// </summary>
    public partial class GedaempfteSchwingung : Window
    {
        
        Sensor<double> DoubleSensor = null;
        DampedOscillation DataGenerator;
        List<double> Datenliste;
        bool Boolconstructor;
        // Für neue Linechart

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private ChartValues<double> values;
        // Konstruktor bekommt die Referenz des neu erstellten Double Sensors übergeben
        public GedaempfteSchwingung(ref Sensor<double> NewSensor)
        {
            Boolconstructor = true;
            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }
        public GedaempfteSchwingung(ref SensorAndSensorgroup.Sensor<double> NewSensor, List<double> Sensordaten)
        {
            Boolconstructor = false;
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
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FehlerHinzufuegen(object sender, RoutedEventArgs e)
        {
            try { 
            // Überprüfung Nutzereingaben
            if(Convert.ToDouble(textBoxAmplitude.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl.Text) < 0|| Convert.ToDouble(textBoxDaempfungsrate.Text)<0.0)
            {
                MessageBox.Show("Amplitude,Dämpfungsrate, Periodendauer und Werteanzahl dürfen nicht negativ sein.");
            }
            else
            {
                    if (Boolconstructor == true)
                    {
                        // Objekt der Datenerzeugungsmethode erstellen und Werte übergeben
                        DataGenerator = new DampedOscillation(Convert.ToDouble(textBoxAmplitude.Text), Convert.ToDouble(textBoxDaempfungsrate.Text), Convert.ToDouble(textBoxPeriodendauer.Text), Convert.ToDouble(textBoxPhasenverschiebung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                        DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
                    }
                    else
                    {
                        DoubleSensor.SetValues(Datenliste);
                    }
                    Close();
                    

            // FolgeFenster erstellen
            SensordatenFehler objectFehler = new SensordatenFehler(ref DoubleSensor);

            // TODO: Hier wieder close?
            this.Visibility = Visibility.Hidden;
            objectFehler.Show();
            }
        }
            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }
}

        private void SensordatenSpeichern(object sender, RoutedEventArgs e)
        {
            try
            { 
            // Überprüfung Nutzereingaben.
            if (Convert.ToDouble(textBoxAmplitude.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl.Text) < 0 || Convert.ToDouble(textBoxDaempfungsrate.Text) < 0.0)
            {
                MessageBox.Show("Amplitude,Dämpfungsrate, Periodendauer und Werteanzahl dürfen nicht negativ sein.");
            }
            else
            {
                    if (Boolconstructor == true)
                    {
                        // Objekt der Datenerzeugungsmethode erstellen und Werte übergeben
                        DataGenerator = new DampedOscillation(Convert.ToDouble(textBoxAmplitude.Text), Convert.ToDouble(textBoxDaempfungsrate.Text), Convert.ToDouble(textBoxPeriodendauer.Text), Convert.ToDouble(textBoxPhasenverschiebung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                        DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
                    }
                    else
                    {
                        DoubleSensor.SetValues(Datenliste);
                    }
                    Close();
                }
        }
            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }
}

        private void Aktualisieren(object sender, RoutedEventArgs e)
        {
            try
            {
                // Überprüfung Nutzereingaben.
                if (Convert.ToDouble(textBoxAmplitude.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl.Text) < 0 || Convert.ToDouble(textBoxDaempfungsrate.Text) < 0.0)
                {
                    MessageBox.Show("Amplitude,Dämpfungsrate, Periodendauer und Werteanzahl dürfen nicht negativ sein.");
                }
                else
                {
                    // Objekt der Datenerzeugungsmethode erstellen und Werte übergeben
                    DataGenerator = new DampedOscillation(Convert.ToDouble(textBoxAmplitude.Text), Convert.ToDouble(textBoxDaempfungsrate.Text), Convert.ToDouble(textBoxPeriodendauer.Text), Convert.ToDouble(textBoxPhasenverschiebung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                    Datenliste = DataGenerator.GetSimulatorValues();
                    GedaempfteSchwingung Aktualisirung = new GedaempfteSchwingung(ref DoubleSensor, Datenliste);
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
