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
using System.IO;
using MQTTCommunicator;

namespace IIoTSimulatorUI
{
    // statische Klasse die ein Communicator Objekt enhält, damit Simulation UI auf das Objekt zugreifen kann
    public static class MQTT
    {
        public static Communicator BrokerCom = new Communicator();
    }
    

    /// <summary>
    /// Interaktionslogik für BrokerEinstellungenUI.xaml
    /// </summary>
    public partial class BrokerEinstellungenUI : Window
    {
        BrokerProfile Brokerdaten = new BrokerProfile();
        DataStorage.DataStorage Datasave = new DataStorage.DataStorage(); 
        bool button1WasClicked = false;
        bool stillconnected=false;
        public BrokerEinstellungenUI()
        {
             stillconnected = false;
            InitializeComponent();
            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory+@"\BrokerProfileTest"))
            {
               Brokerdaten= Datasave.LoadBrokerProfile(AppDomain.CurrentDomain.BaseDirectory);
                 BrokerNameText.Text= Brokerdaten.HostName_IP;
                 PortText.Text=Convert.ToString(Brokerdaten.Port);
                 NutzernameText.Text= Brokerdaten.Username;
                 PassswortBox.Password=Brokerdaten.Password;
            }
        }

        string brokerNameEingabe;
        int portEingabe;
        string nutzernameEingabe;
        string passwortEingabe;
        //Methode für den Button 'Verbinden'
        //Hat die Verbindung mit dem Broker funktioniert wird hier über eine MessageBox
        //aufgefordert den Nutzernamen und das Passwort einzugeben.
        private void Verbinden(object sender, RoutedEventArgs e)
        {
            if(Brokerdaten.HostName_IP== BrokerNameText.Text.Replace(" ", "")&& Brokerdaten.Port== Int32.Parse(PortText.Text.Replace(" ", ""))&&stillconnected==true)
            {
                MessageBox.Show("Sie sind schon mit dem Broker verbunden");
            }
            else
            {
                stillconnected = false;
                try
                {
                    brokerNameEingabe = BrokerNameText.Text.Replace(" ", "");
                    portEingabe = Int32.Parse(PortText.Text.Replace(" ", ""));
                    nutzernameEingabe = NutzernameText.Text;
                    passwortEingabe = PassswortBox.Password.ToString();
                    if (button1WasClicked == false)//Hier wird die Verbindung hergestellt nur mit Broker-Namen und dem Port
                    {
                        // Verbindung mit Broker herstellen
                        string verbunden = MQTT.BrokerCom.ConnectToBroker(brokerNameEingabe, portEingabe);
                        if (verbunden.Equals("-Connected\n-"))
                        {
                            stillconnected = true;
                            Brokerdaten.HostName_IP = BrokerNameText.Text.Replace(" ", "");
                            Brokerdaten.Port = uint.Parse(PortText.Text.Replace(" ", ""));
                            Brokerdaten.Username = NutzernameText.Text;
                            Brokerdaten.Password = PassswortBox.Password.ToString();
                            Datasave.SavebrokerProfile(Brokerdaten, AppDomain.CurrentDomain.BaseDirectory);
                            MessageBox.Show("Erfolgreiche Broker-Verbindung");
                        }
                        else
                        {
                            MessageBox.Show("Verbindung fehlgeschlagen: " + verbunden);
                            stillconnected = false;
                        }
                    }
                    else//Sollte der Haken gesetzt werden, wird mit dem Broker Namen, Port, Nutzernamen und dem Passwort eine Verbindung hergestellt
                    {
                        // Verbindung mit dem Broker herstellen
                        string verbunden2 = MQTT.BrokerCom.ConnectToBroker(brokerNameEingabe, portEingabe, nutzernameEingabe, passwortEingabe);

                        if (verbunden2.Equals("-Connected\n-"))
                        {
                            stillconnected = true;
                            MessageBox.Show("Erfolgreiche Broker-Verbindung");
                        }
                        else
                        {
                            stillconnected = false;
                            MessageBox.Show("Verbindung fehlgeschlagen: " + verbunden2);
                        }
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Invalid Host and Port");
                }
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

        // Der Haken in der UI wurde gesetzt -> Nutzername und Passwort werden hell
        private void HakenSetzen(object sender, RoutedEventArgs e)
        {
            //Sollte noch der Nutzername und das Passwort benötigt werden, 
            //werden diese beim Haken setzen aufhellen
            var bc = new BrushConverter();

            (sender as Button).Foreground = new SolidColorBrush(Colors.White);

            (sender as Button).Background = new SolidColorBrush(Colors.Green);

            NutzernameText.Background = (Brush)bc.ConvertFrom("#ff1f4663");

            PassswortBox.Background = (Brush)bc.ConvertFrom("#ff1f4663");

            NutzernameLabel.Foreground = System.Windows.Media.Brushes.White;

            PasswortLabel.Foreground = System.Windows.Media.Brushes.White;

            Foreground = (Brush)bc.ConvertFrom("#ffffff");


            button1WasClicked = true;
        }
    }

}

