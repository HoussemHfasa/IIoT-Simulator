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

namespace IIOT_Simulator
{
    /// <summary>
    /// Interaktionslogik für BrokerEinstellungen.xaml
    /// </summary>
    public partial class BrokerEinstellungen : Window
    {
        public BrokerEinstellungen()
        {
            InitializeComponent();
        }

        //Methode für den Button 'Verbinden'
        //Hat die Verbindung mit dem Broker funktioniert wird hier über eine MessageBox die Verbindung bestätigt
        private void Verbinden(object sender, RoutedEventArgs e)
        {
           MessageBox.Show("Erfolgreich mit dem Broker verbunden");
            
        }


        //Methode für den Button 'Abbrechen'
        //Man gelangt zurück zur Startseite
        private void Abbrechen(object sender, RoutedEventArgs e)
        {
            Startseite objectStartseite2 = new Startseite();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectStartseite2.Show();
        }

        private void StartseiteButton(object sender, RoutedEventArgs e)
        {
            Startseite objectStartseite2 = new Startseite();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectStartseite2.Show();
        }

        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    
}
