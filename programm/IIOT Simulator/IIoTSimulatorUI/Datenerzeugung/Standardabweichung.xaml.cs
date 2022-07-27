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
    /// Interaktionslogik für Standardabweichung.xaml
    /// </summary>
    /// 

   
    public partial class Standardabweichung : Window
    {
        //Datengenerator Standardabweichung
        StandardDeviation DataGenerator;

        // Sensor der simuliert wird
        Sensor<double> DoubleSensor = null;

        // Für Linechart
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private ChartValues<double> values;

        //Liste enhält die generierten Werte
        List<double> Datenliste;
        bool Boolconstructor;

        // Konstruktor bekommt Referenz des neu erstellen Double Sensors übergeben
        public Standardabweichung(ref Sensor<double> NewSensor)
        {
            //Marker, welcher Konstruktor verwendet wurde
            Boolconstructor = true;

            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }


        //Konstruktor der bei Button Aktualisieren aufgerufen wird. Es wird zusätzlich noch das Berechnungsergebnis übergeben
        public Standardabweichung(ref SensorAndSensorgroup.Sensor<double> NewSensor, List<double> Sensordaten)
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
            //try-catch auf ungültiges Format der Eingabe
            try 
            { 
            //Überprüfung Nutzereingaben. 
                if (Convert.ToInt16(textBoxWerteanzahl.Text)<0)
                 {
                    MessageBox.Show("Werteanzahl darf nicht negativ sein.");
                 }
                else
                 {
                    //Wenn keine vorheringen Daten vorhanden
                    if (Boolconstructor == true)
                    {
                        // Objekt der Datenerzeugungsmethode erstellen
                        DataGenerator = new StandardDeviation(Convert.ToDouble(textBoxMittelwert.Text), Convert.ToDouble(textBoxStandartabweichung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                        DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
                    }
                    else //Daten vorhanden (Anzeige in Linechart)
                    {
                        // vorhandenen Daten in Sensor abspeichern
                        DoubleSensor.SetValues(Datenliste);
                    }

                    //Folgefenster erstellen
                    SensordatenFehler objectFehler = new SensordatenFehler(ref DoubleSensor);

                    Close();

                    // Folgefenster öffnen
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
            //Überprüfung Nutzereingaben. 
            if (Convert.ToInt16(textBoxWerteanzahl.Text) < 0)
            {
                MessageBox.Show("Werteanzahl darf nicht negativ sein.");
            }
            else
                { 
                    if (Boolconstructor == true)
                    {
                        // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in DoubleSensor abspeichern
                        DataGenerator = new StandardDeviation(Convert.ToDouble(textBoxMittelwert.Text), Convert.ToDouble(textBoxStandartabweichung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                        DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
                    }
                    else// Daten aus angezeigten Daten speichern
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

        //Seite schließen Button
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Aktualisieren Button erzeugt Linechart
        private void Aktualisieren(object sender, RoutedEventArgs e)
        {
            //try-catch für ungültige Nutzereingaben
            try
            {
                //Überprüfung Nutzereingaben. 
                if (Convert.ToInt16(textBoxWerteanzahl.Text) < 0)
                {
                    MessageBox.Show("Werteanzahl darf nicht negativ sein.");
                }
                else
                {
                    // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in DoubleSensor abspeichern
                    DataGenerator = new StandardDeviation(Convert.ToDouble(textBoxMittelwert.Text), Convert.ToDouble(textBoxStandartabweichung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                    Datenliste = DataGenerator.GetSimulatorValues();

                    // Seite neu aufrufen mit bestehender Datenliste -> Linechart baut sich auf
                    Standardabweichung Aktualisirung = new Standardabweichung(ref DoubleSensor, Datenliste);
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
