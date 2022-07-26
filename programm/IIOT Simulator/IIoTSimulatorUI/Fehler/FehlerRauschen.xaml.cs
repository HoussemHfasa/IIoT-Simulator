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
    /// Interaktionslogik für FehlerRauschen.xaml
    /// </summary>
    public partial class FehlerRauschen : Window
    {
        Sensor<double> DoubleSensor;
        public FehlerRauschen(ref Sensor<double> NewSensor)
        {
            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }

        private void Hinzufügen(object sender, RoutedEventArgs e)
        {
            try
            { 
            // Nutzereingaben überprüfen
            if (Convert.ToInt16(textBoxWerteanzahl.Text) < 0)
            {
                MessageBox.Show("Werteanzahl darf nicht negativ sein.");
            }
            else
            {
                // Rauschen erzeugen, Fehlerdatengenerator erzeugen, Sensordaten mit Fehlern versehen
                StandardDeviation DataGeneratorStdDev = new StandardDeviation(Convert.ToDouble(textBoxMittelwert.Text), Convert.ToDouble(textBoxStandardabweichung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                AdditiveNoise DataGenerator = new AdditiveNoise(DataGeneratorStdDev.GetSimulatorValues());
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

        }
    }
}
