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
    /// Interaktionslogik für FehlerAbklingendesRauschen.xaml
    /// </summary>
    public partial class FehlerAbklingendesRauschen : Window
    {
        Sensor<double> DoubleSensor;
        public FehlerAbklingendesRauschen(ref Sensor<double> NewSensor)
        {
            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }

        private void Hinzufügen(object sender, RoutedEventArgs e)
        {
            // TODO Nutzereingaben überprüfen

            // Rauschen erzeugen, Fehlerdatengenerator erzeugen, Sensordaten mit Fehlern versehen
            StandardDeviation DataGeneratorStdDev = new StandardDeviation(Convert.ToDouble(textBoxMittelwert.Text), Convert.ToDouble(textBoxStandardabweichung.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
            TransientNoise DataGenerator = new TransientNoise(DataGeneratorStdDev.GetSimulatorValues(), Convert.ToInt32(textBoxAbklingzeit.Text));
            DoubleSensor.SetValues(DataGenerator.GetSensorDataWithErrors(DoubleSensor.GetValues()));
            
            Close();

        }

        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {

        }

        private void Aktualisieren(object sender, RoutedEventArgs e)
        {

        }
    }
}
