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
using DummySensorandSensorgroups;

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
        public void SensorgruppeErstellen(object sender, RoutedEventArgs e)
        {

            string stammText = textBoxEingabe.Text;
            string unterordnerText = textBoxEingabe2.Text;                                  

            TreeViewItem StammItem = new TreeViewItem();

            StammItem.Header = stammText;
            TreeView1.Items.Add(StammItem); 

            StammItem.Items.Add(new TreeViewItem() { Header = unterordnerText });

            textBoxEingabe.Clear();
            textBoxEingabe2.Clear();//Funktioniert*/
                
        }

   

        //Button um die Sensorgruppe zu speichern
        private void SensorgruppeSpeichern(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sensorgruppe wurde gespeichert.");
        }


        //Button zum Abbrechen der wieder zur Startseite führt
        private void AbbrechenButton(object sender, RoutedEventArgs e)
        {
            Startseite objectStartseite3 = new Startseite();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectStartseite3.Show();
        }


        //Menü Leiste:

        //Button um zu den Broker Einstellungen zu gelangen
        private void BrokerSettingsClick(object sender, RoutedEventArgs e)
        {
            BrokerEinstellungen objectBrokerEinstellungen = new BrokerEinstellungen();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectBrokerEinstellungen.Show();
        }


        //Button um zurück zur Startseite zu gelangen
        private void StartseiteButton(object sender, RoutedEventArgs e)
        {
            Startseite objectStartseite2 = new Startseite();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectStartseite2.Show();
        }


        //Sensorgruppe erstellen:

        //Button um den Stamm hinzuzufügen
        private void StammHinzufuegen(object sender, RoutedEventArgs e)
        {
            string stamm = textBoxEingabe.Text; //Benutzereingabe in einem String speichern
            string folderPath = @"C:\stamm"; //Den Pfad zu diesem Ordner festlegen
            if (!Directory.Exists(folderPath)) //Prüfen ob der Ordner bereits existiert
            {
                Directory.CreateDirectory(folderPath); //Wenn nicht, erstelle den Ordnerpfad
            }
            else //Ansonsten lass eine MassageBox erscheinen die darum bittet einen neuen Namen einzugeben 
            {
                MessageBox.Show("Der Name existiert bereits. Bitte geben Sie einen neuen Namen ein.");
            }
            textBoxEingabe.Clear();//In jedem Fall wird die TextBox geleert


        }

        //Button um den Unterordner hinzuzufügen
        private void UnterordnerHinzufuegenClick(object sender, RoutedEventArgs e)
        {
            string unterordner = textBoxEingabe2.Text;
            
        }

        //Button um den Sensor hinzuzufügen
        private void SensorHinzufuegenClick(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
