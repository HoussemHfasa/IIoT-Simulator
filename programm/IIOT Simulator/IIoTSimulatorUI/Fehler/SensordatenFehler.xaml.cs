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
namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für SensordatenFehler.xaml
    /// </summary>
    public partial class SensordatenFehler : Window
    {
        Sensor<bool> BoolSensor;
        Sensor<double> DoubleSensor;

        //Konstruktor für bool Sensoren
        public SensordatenFehler(ref Sensor<bool> newSensor)
        {
            this.BoolSensor = newSensor; 
            InitializeComponent();
        }
        //Konstruktor für double Sensoren
        public SensordatenFehler(ref Sensor<double> newSensor)
        {
            this.DoubleSensor = newSensor;
            InitializeComponent();
        }

        // TODO: Dieser Konstruktor sollte nicht verwendet werden
        public SensordatenFehler()
        {
            InitializeComponent();
        }


        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Fehlermethode(object sender, RoutedEventArgs e)
        {
            //TODO: Fallunterscheidung welches Combobox Contentitem ausgewählt wurde

            string FehlerAuswahl = FehlerBox.Text;

            if (FehlerAuswahl.Equals("Zufallswerte"))
            {
                // Für Zufallswerte
                FehlerZufallswerte objectFehlerZufall = new FehlerZufallswerte(ref DoubleSensor);
                this.Visibility = Visibility.Hidden;
                objectFehlerZufall.Show();
            }
            else if (FehlerAuswahl.Equals("Burst-Signal"))
            {
                FehlerBurstSignal objectBurstSignal = new FehlerBurstSignal(ref DoubleSensor);
                this.Visibility = Visibility.Hidden;
                objectBurstSignal.Show();
            }
            else if (FehlerAuswahl.Equals("Rauschen"))
            {
                FehlerRauschen objectRauschen = new FehlerRauschen(ref DoubleSensor);
                this.Visibility = Visibility.Hidden;
                objectRauschen.Show();
            }
            else if (FehlerAuswahl.Equals("Abklingendes Rauschen"))
            {
                FehlerAbklingendesRauschen objectAbklingend = new FehlerAbklingendesRauschen(ref DoubleSensor);
                this.Visibility = Visibility.Hidden;
                objectAbklingend.Show();
            }
            else
            {
                MessageBox.Show("Wählen Sie eine Fehlermethode aus.");
            }
        }
    }
}
