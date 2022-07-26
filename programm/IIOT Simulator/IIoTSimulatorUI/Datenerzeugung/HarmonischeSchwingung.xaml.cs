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
using SensorAndSensorgroup;
using SensorDataSimulator;

namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für HarmonischeSchwingung.xaml
    /// </summary>
    public partial class HarmonischeSchwingung : Window
    {
        HarmonicOscillation DataGenerator;
        Sensor<double> DoubleSensor = null;
        Sensor<bool> BoolSensor = null;
        public HarmonischeSchwingung(ref SensorAndSensorgroup.Sensor<double> NewSensor)
        {
            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }

        //TODO Harmonische sollte keinen Konstruktor mit bool haben, kann entfernt werden, vorher überprüfen ob dann noch alles funktioniert
      /*  public HarmonischeSchwingung(ref SensorAndSensorgroup.Sensor<bool> NewSensor)
        {
            this.BoolSensor = NewSensor;
            InitializeComponent();
        }*/

        private void FehlerHinzufuegen(object sender, RoutedEventArgs e)
        {
            try
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
                    DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
                    SensordatenFehler objectFehler = new SensordatenFehler(ref DoubleSensor);

                    //TODO: Hier wieder close?
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
            try { 
            //die Überprüfung Nutzereingaben. 
            if (Convert.ToDouble(textBoxAmplitude.Text) < 0.0 || Convert.ToDouble(textBoxPeriodendauer.Text) < 0.0 || Convert.ToInt32(textBoxWerteanzahl.Text) < 0)
            {
                MessageBox.Show("Amplitude,Dämpfungsrate, Periodendauer und Werteanzahl dürfen nicht negativ sein.");
            }
            else
            {
                // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in DoubleSensor abspeichern
                DataGenerator = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude.Text), Convert.ToDouble(textBoxPeriodendauer.Text), Convert.ToDouble(textBoxPhasenverschiebung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
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

        }
    }
}
