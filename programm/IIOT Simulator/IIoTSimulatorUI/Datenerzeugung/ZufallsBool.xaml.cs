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
        RandomBool DataGenerator;
        Sensor<bool> BoolSensor = null;

        public ZufallsBool(ref Sensor<bool> NewSensor)
        {
            Boolconstructor = true;
            this.BoolSensor = NewSensor;
            InitializeComponent();
        }
        List<bool> Datenliste;
        bool Boolconstructor;
        // Für neue Linechart

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        private ChartValues<double> values;

        public ZufallsBool(ref SensorAndSensorgroup.Sensor<bool> NewSensor, List<bool> Sensordaten)
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

        private void ProgrammSchlievßenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FehlerHinzufuegen(object sender, RoutedEventArgs e)
        {
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
                    if (Boolconstructor == true)
                    {
                        // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in Boolsensor abspeichern
                        DataGenerator = new RandomBool(Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                        BoolSensor.SetValues(DataGenerator.GetSimulatorValues());
                    }
                    else
                    {
                        BoolSensor.SetValues(Datenliste);
                    }
                    BoolFehlerwerte objectFehler = new BoolFehlerwerte(ref BoolSensor);
                    this.Visibility = Visibility.Hidden;
                    objectFehler.Show();
                    Close();
                
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

              
                if (Boolconstructor == true)
                {
                    // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in Boolsensor abspeichern
                    DataGenerator = new RandomBool(Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                    BoolSensor.SetValues(DataGenerator.GetSimulatorValues());
                }
                else
                {
                    BoolSensor.SetValues(Datenliste);
                }
                }
                Close();
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
                    // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in Boolsensor abspeichern
                    DataGenerator = new RandomBool(Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                Datenliste = DataGenerator.GetSimulatorValues();
                ZufallsBool Aktualisirung = new ZufallsBool(ref BoolSensor, Datenliste);
                this.Visibility = Visibility.Hidden;
                Aktualisirung.Show();

            }

            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }
            
        }
    }
}
