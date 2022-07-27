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
        //Datengenerator harmonische Schwingung
        HarmonicOscillation DataGeneratorOsc;

        //Datengenerator überlagerte Schwingung
        Superposition DataGenerator;

        // Sensor der simuliert wird und Liste mit Daten
        Sensor<double> DoubleSensor = null;
        List<double> Datenliste;
        bool Boolconstructor;
       
        // Für Linechart
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private ChartValues<double> values;

        // Konstruktor bekommt die Referenz des neu erstellten Double Sensors übergeben
        public UeberlagerteSchwingung(ref Sensor<double> NewSensor)
        {
            //Marker, welcher Konstruktor verwendet wurde
            Boolconstructor = true;

            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }

        //Konstruktor der bei Button Aktualisieren aufgerufen wird. Es wird zusätzlich noch das Berechnungsergebnis übergeben
        public UeberlagerteSchwingung(ref SensorAndSensorgroup.Sensor<double> NewSensor, List<double> Sensordaten)
        {
            //Marker, welcher Konstruktor verwendet wurde
            Boolconstructor = false;

            // Parameter abspeichern
            this.DoubleSensor = NewSensor;
            this.Datenliste = Sensordaten;

            values = new ChartValues<double>();
            int[] Labelsint = null;

            //Seite inizialisieren
            InitializeComponent();

            // Neue Collection mit LineSeries
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

            // neue LineSeries hinzufügen
            SeriesCollection.Add(new LineSeries
            {
                Title = DoubleSensor.Sensor_id,
                Values = values,
                LineSmoothness = 0,
                PointForeground = Brushes.Gray
            });
            DataContext = this;
        }

        // Butten Fehler Hinzufügen
        private void FehlerHinzufuegen(object sender, RoutedEventArgs e)
        {
            // try-catch auf ungültiges Format in der Eingabe
            try
            { 
            // Überprüfung Nutzereingaben.
            if (Convert.ToDouble(textBoxAmplitude1.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer1.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl1.Text) < 0 || Convert.ToDouble(textBoxAmplitude2.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer2.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl2.Text) < 0)
            {
                MessageBox.Show("Amplitude, Periodendauer und Werteanzahl dürfen nicht negativ sein.");
            }
            else
            {
                    //Wenn keine vorheringen Daten vorhanden
                    if (Boolconstructor == true)
                    {
                        // Variablen für beide Schwingungen
                        List<double> Oscillation1;
                        List<double> Oscillation2;

                        // Erzeugung der 1. Harmonischen Schwingungen
                        DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude1.Text), Convert.ToDouble(textBoxPeriodendauer1.Text), Convert.ToDouble(textBoxPhasenverschiebung1.Text), Convert.ToUInt32(textBoxWerteanzahl1.Text));
                        Oscillation1 = DataGeneratorOsc.GetSimulatorValues();

                        // Erzeugung der 2. Harmonischen Schwingung
                        DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude2.Text), Convert.ToDouble(textBoxPeriodendauer2.Text), Convert.ToDouble(textBoxPhasenverschiebung2.Text), Convert.ToUInt32(textBoxWerteanzahl2.Text));
                        Oscillation2 = DataGeneratorOsc.GetSimulatorValues();

                        //Erzeugung der Überlagerten Schwingung und abspeichern in Sensor
                        DataGenerator = new Superposition(Oscillation1, Oscillation2);
                        DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
                    }
                    else  //Daten vorhanden (Anzeige in Linechart)
                    {
                        // vorhandenen Daten in Sensor abspeichern
                        DoubleSensor.SetValues(Datenliste);
                    }
                    

                    // Aufufen des Folgefensters;
                    SensordatenFehler objectFehler = new SensordatenFehler(ref DoubleSensor);
                    Close();
                    objectFehler.Show();
            }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }
        }

        // Sensordaten speichern Button
        private void SensordatenSpeichern(object sender, RoutedEventArgs e)
        {
            //try-catch auf ungültiges Format
            try
            { 
            // Überprüfung Nutzereingaben.
            if (Convert.ToDouble(textBoxAmplitude1.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer1.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl1.Text) < 0 || Convert.ToDouble(textBoxAmplitude2.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer2.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl2.Text) < 0)
            {
                MessageBox.Show("Amplitude, Periodendauer und Werteanzahl dürfen nicht negativ sein.");
            }
            else
            {

                    if (Boolconstructor == true)
                    {
                        // Variablen für beide Schwingungen
                        List<double> Oscillation1;
                        List<double> Oscillation2;

                        // Erzeugung der 1. Harmonischen Schwingungen
                        DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude1.Text), Convert.ToDouble(textBoxPeriodendauer1.Text), Convert.ToDouble(textBoxPhasenverschiebung1.Text), Convert.ToUInt32(textBoxWerteanzahl1.Text));
                        Oscillation1 = DataGeneratorOsc.GetSimulatorValues();

                        // Erzeugung der 2. Harmonischen Schwingung
                        DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude2.Text), Convert.ToDouble(textBoxPeriodendauer2.Text), Convert.ToDouble(textBoxPhasenverschiebung2.Text), Convert.ToUInt32(textBoxWerteanzahl2.Text));
                        Oscillation2 = DataGeneratorOsc.GetSimulatorValues();

                        //Erzeugung der Überlagerten Schwingung und abspeichern in Sensor
                        DataGenerator = new Superposition(Oscillation1, Oscillation2);
                        DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());

                        //Erzeugung der Überlagerten Schwingung und abspeichern in Sensor
                        DataGenerator = new Superposition(Oscillation1, Oscillation2);
                        DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
                    }
                    else // Daten aus angezeigten Daten speichern
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

        //Seite schließen Button
        private void ProgrammSchlievßenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Aktualisieren Button erzeugt Linechart
        private void Aktualisieren(object sender, RoutedEventArgs e)
        {
            //try-catch für ungültige Nutzereingaben
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

                    //Erzeugung der Überlagerten Schwingung und abspeichern
                    DataGenerator = new Superposition(Oscillation1, Oscillation2);
                    Datenliste = DataGenerator.GetSimulatorValues();

                    // Seite neu aufrufen mit bestehender Datenliste -> Linechart baut sich auf
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
