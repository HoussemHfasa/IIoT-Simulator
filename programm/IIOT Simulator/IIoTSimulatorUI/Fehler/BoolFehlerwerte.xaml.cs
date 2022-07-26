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
    /// Interaktionslogik für BoolFehlerwerte.xaml
    /// </summary>
    public partial class BoolFehlerwerte : Window
    {
        Sensor<bool> BoolSensor;
        public BoolFehlerwerte(ref Sensor<bool> NewSensor)
        {
            this.BoolSensor = NewSensor;
            InitializeComponent();
        }

        private void SensordatenSpeichern(object sender, RoutedEventArgs e)
        {
            // TODO Nutzereingaben überprüfen

            // Fehlerdatengenerator erzeugen, Sensordaten mit Fehlern versehen
            RandomZeroesError DataGenerator = new RandomZeroesError(Convert.ToDouble(textBoxFehlerrate.Text), Convert.ToInt32(textBoxFehlerlänge.Text));
            BoolSensor.SetValues(DataGenerator.GetSensorDataWithBoolErrors(BoolSensor.GetValues()));

            Close();
        }

        private void ProgrammSchlievßenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Aktualisieren(object sender, RoutedEventArgs e)
        {

        }
    }
}
