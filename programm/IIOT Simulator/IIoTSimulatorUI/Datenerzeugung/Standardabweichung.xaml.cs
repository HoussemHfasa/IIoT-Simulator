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
    /// Interaktionslogik für Standardabweichung.xaml
    /// </summary>
    /// 

   
    public partial class Standardabweichung : Window
    {
        StandardDeviation DataGenerator;
        Sensor<double> DoubleSensor = null;

        // Konstruktor bekommt Referenz des neu erstellen Double Sensors übergeben
        public Standardabweichung(ref Sensor<double> NewSensor)
        {
            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }

        private void FehlerHinzufuegen(object sender, RoutedEventArgs e)
        {
            //TODO: Es fehlt hier noch die Überprüfung Nutzereingaben. Bitte noch spezifizieren welche Überprüfungen stattfinden müssen

            // Objekt der Datenerzeugungsmethode erstellen
            DataGenerator = new StandardDeviation(Convert.ToDouble(textBoxMittelwert.Text), Convert.ToDouble(textBoxStandartabweichung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
            DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());

            SensordatenFehler objectFehler = new SensordatenFehler(ref DoubleSensor);

            // TODO Hier wieder close?
            this.Visibility = Visibility.Hidden;
            objectFehler.Show();
        }

        private void SensordatenSpeichern(object sender, RoutedEventArgs e)
        {
            DataGenerator = new StandardDeviation(Convert.ToDouble(textBoxMittelwert.Text), Convert.ToDouble(textBoxStandartabweichung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
            DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
            Close();
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
