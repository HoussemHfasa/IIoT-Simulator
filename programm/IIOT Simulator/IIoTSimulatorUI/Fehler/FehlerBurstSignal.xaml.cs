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
    /// Interaktionslogik für FehlerBurstSignal.xaml
    /// </summary>
    public partial class FehlerBurstSignal : Window
    {
        // Sensor der simuliert wird
        Sensor<double> DoubleSensor;

        //Liste enhält die generierten Werte
        List<double> Datenliste;
        bool Boolconstructor;

        // Für Linechart
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private ChartValues<double> values;

        // Konstruktor bekommt die Referenz des neu erstellten bool Sensors übergeben
        public FehlerBurstSignal(ref Sensor<double> NewSensor)
        {
            //Marker, welcher Konstruktor verwendet wurde
            Boolconstructor = true;

            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }

        //Konstruktor der bei Button Aktualisieren aufgerufen wird. Es wird zusätzlich noch das Berechnungsergebnis übergeben
        public FehlerBurstSignal(ref SensorAndSensorgroup.Sensor<double> NewSensor, List<double> Sensordaten)
        {
            //Marker, welcher Konstruktor verwendet wurde
            Boolconstructor = false;

            // Parameter abspeichern
            this.DoubleSensor = NewSensor;
            this.Datenliste = Sensordaten;
            values = new ChartValues<double>();
            values.AddRange(Datenliste);

            int[] Labelsint = null;

            //Seite inizialisieren
            InitializeComponent();

            // Neue Collection mit Linien
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

        // Sensordaten speichern/hinzufügen Button
        private void Hinzufügen(object sender, RoutedEventArgs e)
        {
            try//try-catch auf ungültiges Format
            {
                //Daten aus Nutzereingaben speichern
                if (Boolconstructor == true)
                {
                    //Nutzereingabe  überprüfen
                    if ((Convert.ToDouble(textBoxErrorRate.Text) < 0.0) || (Convert.ToDouble(textBoxErrorRate.Text) > 1))
                    {
                        MessageBox.Show("Die Fehlerrate muss zwischen 0,0 und 1,0 liegen.");
                    }
                    else if (Convert.ToInt32(textBoxBurstduration.Text) < 0)
                    {
                        MessageBox.Show("Die Fehlerdauer darf nicht kleiner als 0 sein.");
                    }
                    else
                    {
                        // Fehlerdatengenerator erzeugen, Sensordaten mit Fehlern versehen
                        BurstNoise DataGenerator = new BurstNoise(Convert.ToDouble(textBoxBurstvalue.Text), Convert.ToInt32(textBoxBurstduration.Text), Convert.ToDouble(textBoxErrorRate.Text));
                        DoubleSensor.SetValues(DataGenerator.GetSensorDataWithErrors(DoubleSensor.GetValues()));
                    }
                }
                else
                { 
                    // Daten aus Anzeige speichern
                    DoubleSensor.SetValues(Datenliste);
                }
                Close();
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }

        }

        // Button zum schließen der Seite
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Aktualisieren Button erzeugt Linechart
        private void Aktualisieren(object sender, RoutedEventArgs e)
        {
            try //try-catch für ungültige Nutzereingaben
            {
                //Nutzereingabe ErrorRate überprüfen
                if ((Convert.ToDouble(textBoxErrorRate.Text) < 0.0) || (Convert.ToDouble(textBoxErrorRate.Text) > 1))
                {
                    MessageBox.Show("Die Fehlerrate muss zwischen 0,0 und 1,0 liegen.");
                }
                else if (Convert.ToInt32(textBoxBurstduration.Text) < 0)
                {
                    MessageBox.Show("Die Fehlerdauer darf nicht kleiner als 0 sein.");
                }
                else
                {
                    // Fehlerdatengenerator erzeugen, Sensordaten mit Fehlern versehen
                    BurstNoise DataGenerator = new BurstNoise(Convert.ToDouble(textBoxBurstvalue.Text), Convert.ToInt32(textBoxBurstduration.Text), Convert.ToDouble(textBoxErrorRate.Text));
                    Datenliste= DataGenerator.GetSensorDataWithErrors(DoubleSensor.GetValues());

                    // Seite neu aufrufen mit bestehender Datenliste -> Linechart baut sich auf
                    FehlerBurstSignal Aktualisirung = new FehlerBurstSignal(ref DoubleSensor, Datenliste);
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
