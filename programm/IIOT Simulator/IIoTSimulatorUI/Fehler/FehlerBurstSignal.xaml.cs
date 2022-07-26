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
            

            //Nutzereingabe ErrorRate überprüfen
            if((Convert.ToDouble(textBoxErrorRate.Text) < 0.0) || (Convert.ToDouble(textBoxErrorRate.Text) > 1))
            {
                MessageBox.Show("Die Fehlerrate muss zwischen 0,0 und 1,0 liegen.");
            }
            else if(Convert.ToInt32(textBoxBurstduration.Text) < 0)
            {
                MessageBox.Show("Die Fehlerdauer darf nicht kleiner als 0 sein.");
            }
            else
            {

            
            // Fehlerdatengenerator erzeugen, Sensordaten mit Fehlern versehen
            BurstNoise DataGenerator = new BurstNoise(Convert.ToDouble(textBoxBurstvalue.Text), Convert.ToInt32(textBoxBurstduration.Text), Convert.ToDouble(textBoxErrorRate.Text));
            DoubleSensor.SetValues(DataGenerator.GetSensorDataWithErrors(DoubleSensor.GetValues()));

            Close();
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
