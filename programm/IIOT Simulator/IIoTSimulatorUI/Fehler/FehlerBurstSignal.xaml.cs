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
    /// Interaktionslogik für FehlerBurstSignal.xaml
    /// </summary>
    public partial class FehlerBurstSignal : Window
    {
        Sensor<double> DoubleSensor;
        public FehlerBurstSignal(ref Sensor<double> NewSensor)
        {
            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }

        private void Hinzufügen(object sender, RoutedEventArgs e)
        {
            // TODO Nutzereingaben überprüfen

            // Fehlerdatengenerator erzeugen, Sensordaten mit Fehlern versehen
            BurstNoise DataGenerator = new BurstNoise(Convert.ToDouble(textBoxBurstvalue.Text), Convert.ToInt32(textBoxBurstduration.Text), Convert.ToDouble(textBoxErrorRate.Text));
            DoubleSensor.SetValues(DataGenerator.GetSensorDataWithErrors(DoubleSensor.GetValues()));

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
