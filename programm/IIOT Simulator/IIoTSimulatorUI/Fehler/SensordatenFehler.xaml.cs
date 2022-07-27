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

        // Deklaration Sensor<double um ref Sensor aus Konstruktor zu speichern
        Sensor<double> DoubleSensor;


        //Konstruktor für double Sensoren
        public SensordatenFehler(ref Sensor<double> newSensor)
        {
            this.DoubleSensor = newSensor;
            InitializeComponent();
        }

        // Button zum schließen der Seite
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        // Button zum Auswahl der Fehlermethode
        private void Fehlermethode(object sender, RoutedEventArgs e)
        {
           
            // Vom Nuter ausgewählten Text speichern
            string FehlerAuswahl = FehlerBox.Text;

            // Fallunterscheidung für die verschiendenen Fehlererzeugungsmethoden öffnet die jeweilige Fehlererzeugungsseite
            if (FehlerAuswahl.Equals("Zufallswerte"))
            {                
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
            else // Wenn keine Methode ausgewählt wurde
            {
                MessageBox.Show("Wählen Sie eine Fehlermethode aus.");
            }
        }
    }
}
