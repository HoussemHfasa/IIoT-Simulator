﻿using System;
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
            MessageBox.Show("Geben Sie Ihren Nutzernamen und Passwort ein.");

            //Wenn der Broker-Name und der Port zur Anmeldung reichen wird gleich mit dem Broker verbunden
            //Sollte noch der Nutzername und das Passwort benötigt werden, 
            //ändern diese Elemente beim Verbinden ihre Farbe um auf die Eingabe hinzuweißen
            var bc = new BrushConverter();
            
            PortText.Background = (Brush)bc.ConvertFrom("#ff1f4663");
            
            PassswortBox.Background = (Brush)bc.ConvertFrom("#ff1f4663");

            NutzernameLabel.Foreground = System.Windows.Media.Brushes.White;

            PasswortLabel.Foreground = System.Windows.Media.Brushes.White;

            HakenPort.Foreground = System.Windows.Media.Brushes.Green;

            HakenBrokerName.Foreground = System.Windows.Media.Brushes.Green;
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
    }

}
