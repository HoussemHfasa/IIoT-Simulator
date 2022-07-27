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
    /// Interaktionslogik für UeberlagerteSchwingung.xaml
    /// </summary>
    public partial class UeberlagerteSchwingung : Window
    {
        HarmonicOscillation DataGeneratorOsc;
        Superposition DataGenerator;
        Sensor<double> DoubleSensor = null;
        List<double> Datenliste;
        bool Boolconstructor;
        // Für neue Linechart

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private ChartValues<double> values;
        public UeberlagerteSchwingung(ref Sensor<double> NewSensor)
        {
            Boolconstructor = true;
            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }
        public UeberlagerteSchwingung(ref SensorAndSensorgroup.Sensor<double> NewSensor, List<double> Sensordaten)
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

        private void FehlerHinzufuegen(object sender, RoutedEventArgs e)
        {
            try { 
            // Überprüfung Nutzereingaben.
            if (Convert.ToDouble(textBoxAmplitude1.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer1.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl1.Text) < 0 || Convert.ToDouble(textBoxAmplitude2.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer2.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl2.Text) < 0)
            {
                MessageBox.Show("Amplitude, Periodendauer und Werteanzahl dürfen nicht negativ sein.");
            }
            else
            {
                List<double> Oscillation1;
                List<double> Oscillation2;

                // Erzeugung der Harmonischen Schwingungen
                DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude1.Text), Convert.ToDouble(textBoxPeriodendauer1.Text), Convert.ToDouble(textBoxPhasenverschiebung1.Text), Convert.ToUInt32(textBoxWerteanzahl1.Text));
                Oscillation1 = DataGeneratorOsc.GetSimulatorValues();

                DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude2.Text), Convert.ToDouble(textBoxPeriodendauer2.Text), Convert.ToDouble(textBoxPhasenverschiebung2.Text), Convert.ToUInt32(textBoxWerteanzahl2.Text));
                Oscillation2 = DataGeneratorOsc.GetSimulatorValues();

                    if (Boolconstructor == true)
                    {
                        //Erzeugung der Überlagerten Schwingung und abspeichern in Sensor
                        DataGenerator = new Superposition(Oscillation1, Oscillation2);
                        DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
                    }
                    else
                    {
                        DoubleSensor.SetValues(Datenliste);
                    }
                    Close();

                    // Aufufen des Folgefensters; TODO: Hier wieder close?
                    SensordatenFehler objectFehler = new SensordatenFehler(ref DoubleSensor);
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
            if (Convert.ToDouble(textBoxAmplitude1.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer1.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl1.Text) < 0 || Convert.ToDouble(textBoxAmplitude2.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer2.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl2.Text) < 0)
            {
                MessageBox.Show("Amplitude, Periodendauer und Werteanzahl dürfen nicht negativ sein.");
            }
            else
            {
                List<double> Oscillation1;
                List<double> Oscillation2;

                // Erzeugung der Harmonischen Schwingungen
                DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude1.Text), Convert.ToDouble(textBoxPeriodendauer1.Text), Convert.ToDouble(textBoxPhasenverschiebung1.Text), Convert.ToUInt32(textBoxWerteanzahl1.Text));
                Oscillation1 = DataGeneratorOsc.GetSimulatorValues();

                DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude2.Text), Convert.ToDouble(textBoxPeriodendauer2.Text), Convert.ToDouble(textBoxPhasenverschiebung2.Text), Convert.ToUInt32(textBoxWerteanzahl2.Text));
                Oscillation2 = DataGeneratorOsc.GetSimulatorValues();

                    if (Boolconstructor == true)
                    {
                        //Erzeugung der Überlagerten Schwingung und abspeichern in Sensor
                        DataGenerator = new Superposition(Oscillation1, Oscillation2);
                        DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
                    }
                    else
                    {
                        DoubleSensor.SetValues(Datenliste);
                    }
                    Close();
                    SensordatenFehler objectFehler = new SensordatenFehler(ref DoubleSensor);
                }
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
                // Überprüfung Nutzereingaben.
                if (Convert.ToDouble(textBoxAmplitude1.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer1.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl1.Text) < 0 || Convert.ToDouble(textBoxAmplitude2.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer2.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl2.Text) < 0)
                {
                    MessageBox.Show("Amplitude, Periodendauer und Werteanzahl dürfen nicht negativ sein.");
                }
                else
                {
                    List<double> Oscillation1;
                    List<double> Oscillation2;

                    // Erzeugung der Harmonischen Schwingungen
                    DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude1.Text), Convert.ToDouble(textBoxPeriodendauer1.Text), Convert.ToDouble(textBoxPhasenverschiebung1.Text), Convert.ToUInt32(textBoxWerteanzahl1.Text));
                    Oscillation1 = DataGeneratorOsc.GetSimulatorValues();
                    DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude2.Text), Convert.ToDouble(textBoxPeriodendauer2.Text), Convert.ToDouble(textBoxPhasenverschiebung2.Text), Convert.ToUInt32(textBoxWerteanzahl2.Text));
                    Oscillation2 = DataGeneratorOsc.GetSimulatorValues();
                    //Erzeugung der Überlagerten Schwingung und abspeichern in Sensor
                    DataGenerator = new Superposition(Oscillation1, Oscillation2);
                    DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
                    Datenliste = DataGenerator.GetSimulatorValues();
                    UeberlagerteSchwingung Aktualisirung = new UeberlagerteSchwingung(ref DoubleSensor, Datenliste);                   
                    this.Visibility = Visibility.Hidden;
                    Aktualisirung.Show();
                    
                    Close();
                }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }
           
        }

    }
}
