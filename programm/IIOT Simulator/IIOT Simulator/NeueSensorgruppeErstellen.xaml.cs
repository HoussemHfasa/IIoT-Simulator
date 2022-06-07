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

namespace IIOT_Simulator
{
    /// <summary>
    /// Interaktionslogik für NeueSensorgruppeErstellen.xaml
    /// </summary>
    public partial class NeueSensorgruppeErstellen : Window
    {
        public NeueSensorgruppeErstellen()
        {
            InitializeComponent();
        }


        //Button um alles zu entfernen
        private void AllesEntfernen(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Alles wurde entfernt.");
        }


        //Button um den Stamm hinzuzufügen
        public void StammHinzufuegen(object sender, RoutedEventArgs e)
        {

            string stamm = textBoxStamm.Text; //Die Benutzereingabe in einem String speichern

            TreeViewItem StammItem = new TreeViewItem();
           
            StammItem.Header = stamm;
            TreeView1.Items.Add(StammItem);
            textBoxStamm.Clear();//Funktioniert
        }

        //button zum Unterordner hinzufügen
        private void UnterordnerHinzufügen(object sender, RoutedEventArgs e)
        {

            string unterordner = textBoxStamm.Text; //Die Benutzereingabe in einem String speichern

            TreeViewItem UnterordnerItem = new TreeViewItem();
            UnterordnerItem.Header = unterordner;
            
            
            textBoxStamm.Clear();//Funktioniert noch nicht, einen Weg finden auf den davor festgelegten Stamm zuzugreifen
        }

        //Button um die Sensorgruppe zu speichern
        private void SensorgruppeSpeichern(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sensorgruppe wurde gespeichert.");
        }



        //Button zum Sensor hinzufügen
        private void SensorHinzufügen(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sensor wurde hinzugefügt.");
        }

        //Button zum Abbrechen der wieder zur Startseite führt
        private void AbbrechenButton(object sender, RoutedEventArgs e)
        {
            Startseite objectStartseite3 = new Startseite();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectStartseite3.Show();
        }
    }
}
