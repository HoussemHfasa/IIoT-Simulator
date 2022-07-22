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
using DataStorageDummy;
using DummySensorandSensorgroups;
using MQTTCommunicator;

namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für BrokerEinstellungenUI.xaml
    /// </summary>
    public partial class BrokerEinstellungenUI : Window
    {
        
        public BrokerEinstellungenUI()
        {
            InitializeComponent();
        }

        //Methode für den Button 'Verbinden'
        //Hat die Verbindung mit dem Broker funktioniert wird hier über eine MessageBox
        //aufgefordert den Nutzernamen und das Passwort einzugeben.
        private void Verbinden(object sender, RoutedEventArgs e)
        {
            string brokerNameEingabe = BrokerNameText.Text;
            int portEingabe = Int32.Parse(PortText.Text);
            string nutzernameEingabe = NutzernameText.Text;
            string passwortEingabe = PassswortBox.Password.ToString();

            //bool hakenGesetzt = HakenSetzen.Click -hier ncoh eine If-Abfrage die überprüft ob der Haken gesetzt wurde

            Communicator communicatorObject = new Communicator();

            string verbunden = communicatorObject.ConnectToBroker(brokerNameEingabe, portEingabe);
            
            string verbunden2 = communicatorObject.ConnectToBroker(brokerNameEingabe, portEingabe, nutzernameEingabe, passwortEingabe);

            if (verbunden.Equals("-Connected\n-"))
            {
                MessageBox.Show("Erfolgreiche Broker-Verbindung" );
            }
            else
            {
                MessageBox.Show("Verbindung fehlgeschlagen: " + verbunden);
            }
            

        }


        //Methode für den Button 'Abbrechen'
        //Man gelangt zurück zur Startseite
        private void Abbrechen(object sender, RoutedEventArgs e)
        {
            MainWindow objectStartseite2 = new MainWindow();
            this.Visibility = Visibility.Hidden; 
            objectStartseite2.Show();
        }


        //Button um zurück zur Startseite zu gelangen
        private void StartseiteButton(object sender, RoutedEventArgs e)
        {
            MainWindow objectStartseite2 = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objectStartseite2.Show();
        }

        //Schließt das Programm
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void HakenSetzen(object sender, RoutedEventArgs e)
        {
            //Sollte noch der Nutzername und das Passwort benötigt werden, 
            //werden diese beim Haken setzen aufhellen
            var bc = new BrushConverter();

            PortText.Background = (Brush)bc.ConvertFrom("#ff1f4663");

            PassswortBox.Background = (Brush)bc.ConvertFrom("#ff1f4663");

            NutzernameLabel.Foreground = System.Windows.Media.Brushes.White;

            PasswortLabel.Foreground = System.Windows.Media.Brushes.White;

            Foreground = (Brush)bc.ConvertFrom("#ffffff");
        }
    }

}

