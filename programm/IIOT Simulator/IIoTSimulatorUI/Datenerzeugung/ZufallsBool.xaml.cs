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
    /// Interaktionslogik für ZufallsBool.xaml
    /// </summary>
    public partial class ZufallsBool : Window
    {
        //Datengenerator harmonische Schwingung
        RandomBool DataGenerator;
        
        // Sensor der simuliert wird
        Sensor<bool> BoolSensor = null;

        //Liste enhält die generierten Werte
        List<bool> Datenliste;
        bool Boolconstructor;

        // Für Linechart
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private ChartValues<double> values;

        // Konstruktor bekommt die Referenz des neu erstellten bool Sensors übergeben
        public ZufallsBool(ref Sensor<bool> NewSensor)
        {
            //Marker, welcher Konstruktor verwendet wurde
            Boolconstructor = true;

            this.BoolSensor = NewSensor;
            InitializeComponent();
        }


        //Konstruktor der bei Button Aktualisieren aufgerufen wird. Es wird zusätzlich noch das Berechnungsergebnis übergeben
        public ZufallsBool(ref SensorAndSensorgroup.Sensor<bool> NewSensor, List<bool> Sensordaten)
        {
            //Marker, welcher Konstruktor verwendet wurde
            Boolconstructor = false;

            // Parameter abspeichern
            this.BoolSensor = NewSensor;
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

            // von Bool in double /0 oder 1 wandeln.
            for (int i = 0; i < Datenliste.Count; i++)
            {
                values.Add(Datenliste[i] ? 1 : 0);
            }

            // Beschriftung der Werte YAchse
            YFormatter = value => value.ToString("");

            // neue LineSeries hinzufügen
            SeriesCollection.Add(new LineSeries
            {
                Title = BoolSensor.Sensor_id,
                Values = values,
                LineSmoothness = 0,
                PointForeground = Brushes.Gray
            });
            DataContext = this;
        }

        //Button Seite schließen
        private void ProgrammSchlievßenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Butten Fehler Hinzufügen
        private void FehlerHinzufuegen(object sender, RoutedEventArgs e)
        {
            // try-catch auf ungültiges Format in der Eingabe
            try
            {
                
                if (Boolconstructor == true)
                {
                    //Überprüfung Nutzereingaben
                    if (Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text) > 1.0 || Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text) < 0.0)
                    {
                        MessageBox.Show("Wechselwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
                    }
                    else if (Convert.ToInt16(textBoxWerteanzahl.Text) < 0)
                    {
                        MessageBox.Show("Werteanzahl darf nicht negativ sein.");
                    }

                    else
                    {
                        // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in Boolsensor abspeichern
                        DataGenerator = new RandomBool(Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                        BoolSensor.SetValues(DataGenerator.GetSimulatorValues());
                    }
                }
                else //Daten vorhanden (Anzeige in Linechart)
                {
                    // vorhandenen Daten in Sensor abspeichern
                    BoolSensor.SetValues(Datenliste);

                }
                BoolFehlerwerte objectFehler = new BoolFehlerwerte(ref BoolSensor);
                Close();
                //Folgefenster öffnen
                objectFehler.Show();
                
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

                if (Boolconstructor == true)
                {
                    //Überprüfung Nutzereingaben
                    if (Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text) > 1.0 || Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text) < 0.0)
                    {
                        MessageBox.Show("Wechselwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
                    }
                    else if (Convert.ToInt16(textBoxWerteanzahl.Text) < 0)
                    {
                        MessageBox.Show("Werteanzahl darf nicht negativ sein.");
                    }

                    else
                    {
                        // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in Boolsensor abspeichern
                        DataGenerator = new RandomBool(Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                        BoolSensor.SetValues(DataGenerator.GetSimulatorValues());
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

        // Aktualisieren Button erzeugt Linechart
        private void Aktualisieren(object sender, RoutedEventArgs e)
        {
            //try-catch für ungültige Nutzereingaben
            try
            {
                //Überprüfung Nutzereingaben
                if (Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text) > 1.0 || Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text) < 0.0)
                {
                    MessageBox.Show("Wechselwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
                }
                else if (Convert.ToInt16(textBoxWerteanzahl.Text) < 0)
                {
                    MessageBox.Show("Werteanzahl darf nicht negativ sein.");
                }
                else
                {
                    // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in Boolsensor abspeichern
                    DataGenerator = new RandomBool(Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                    Datenliste = DataGenerator.GetSimulatorValues();

                    // Seite neu aufrufen mit bestehender Datenliste -> Linechart baut sich auf
                    ZufallsBool Aktualisirung = new ZufallsBool(ref BoolSensor, Datenliste);
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
