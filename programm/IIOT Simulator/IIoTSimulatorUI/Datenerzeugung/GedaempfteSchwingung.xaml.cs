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
    /// Interaktionslogik für GedaempfteSchwingung.xaml
    /// </summary>
    public partial class GedaempfteSchwingung : Window
    {
        
        Sensor<double> DoubleSensor = null;
        DampedOscillation DataGenerator;

        // Konstruktor bekommt die Referenz des neu erstellten Double Sensors übergeben
        public GedaempfteSchwingung(ref Sensor<double> NewSensor)
        {
            this.DoubleSensor = NewSensor;
            InitializeComponent();

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
            // Objekt der Datenerzeugungsmethode erstellen und Werte übergeben
            DataGenerator = new DampedOscillation(Convert.ToDouble(textBoxAmplitude.Text), Convert.ToDouble(textBoxDaempfungsrate.Text), Convert.ToDouble(textBoxPeriodendauer.Text),Convert.ToDouble(textBoxPhasenverschiebung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
            DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());

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
                // Objekt der Datenerzeugungsmethode erstellen und Werte übergeben
                DataGenerator = new DampedOscillation(Convert.ToDouble(textBoxAmplitude.Text), Convert.ToDouble(textBoxDaempfungsrate.Text), Convert.ToDouble(textBoxPeriodendauer.Text), Convert.ToDouble(textBoxPhasenverschiebung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
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
            //Hier werden die Sensordaten nach der Eingabe aktualisiert
        }
    }
}
