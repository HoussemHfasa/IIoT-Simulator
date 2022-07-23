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
using DummySensorandSensorgroups;
using SensorAndSensorgroup;

namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für NeueSensorgruppeUI.xaml
    /// </summary>

    public partial class NeueSensorgruppeUI : Window
    {
        //Sensorgroups sensorgroupsObject = new Sensorgroups();

        public NeueSensorgruppeUI()
        {
            InitializeComponent();
        }

        
        //Menü Leiste:

        //Button um zu den Broker Einstellungen zu gelangen
        private void BrokerSettingsClick(object sender, RoutedEventArgs e)
        {
            BrokerEinstellungenUI objectBrokerEinstellungen = new BrokerEinstellungenUI();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectBrokerEinstellungen.Show();
        }


        //Button um zurück zur Startseite zu gelangen
        private void StartseiteButton(object sender, RoutedEventArgs e)
        {
            MainWindow objectStartseite2 = new MainWindow();
            this.Visibility = Visibility.Hidden; 
            objectStartseite2.Show();
        }


        //Sensorgruppe erstellen:

        //Button um den Stamm hinzuzufügen
        private void StammHinzufuegen(object sender, RoutedEventArgs e)
        {
            string stammText = textBoxEingabe.Text; //Benutzer Eingabe in einem string speichern

            TreeViewItem stamm = new TreeViewItem(); //Neues Treeviewtem für den Stamm erstellen

            stamm.Header = stammText; //Dem TreeViewItem den Header übergeben

            TreeView1.Items.Add(stamm); //Der TreeView den Stamm hinzufügen

            textBoxEingabe.Clear(); //TextBox Eingabe wieder löschen

            HakenStamm.Foreground = System.Windows.Media.Brushes.Green; //Grünen Haken erscheinen lassen

            LabelInfo.Foreground = System.Windows.Media.Brushes.White; //Info ausblenden

            //Sensorgroups sensorgroupsObject = new Sensorgroups();

            //sensorgroupsObject.AddBase(stammText);
        }

        //Button um den Unterordner hinzuzufügen
        private void UnterordnerHinzufuegenClick(object sender, RoutedEventArgs e)
        {
            TreeViewItem selectedTVI = (TreeViewItem)TreeView1.SelectedItem as TreeViewItem; //Ein TreeViewItem vom ausgewählten Item erstellen
           
            string oberordner = selectedTVI.Header.ToString();//Das ausgewählte Objekt in String speichern

            TreeViewItem unterordner = new TreeViewItem(); //Ein TreeViewItem vom Unterordner erstellen

            string unterordnerText = textBoxEingabe2.Text; //Benutzereingabe in einem string speichern

            unterordner.Header = unterordnerText;

            selectedTVI.Items.Add(unterordner);// Dem ausgewählten Item den Unterordner hinzufügen

            //sensorgroupsObject.AddNode(unterordnerText, oberordner);


        }



        //Speichert die Sensorgruppe als Topic(Soll)
        private void SensorgruppeSpeichernClick(object sender, RoutedEventArgs e)
        {
            Sensorbeispiel sensor = new Sensorbeispiel();
           LabelTopic.Content = sensor.Topic;

            MessageBox.Show("Sensorgruppe wurde gespeichert");
        }


        //Schließt das Progranm
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        //Löscht die ganze Sensorgruppe
        private void SensorgruppeLoeschen(object sender, RoutedEventArgs e)
        {
            TreeView1.Items.Clear();
        }

        //Button um zu den Sensordaten-Einstellungen zu gelangen
        private void Sensordaten(object sender, RoutedEventArgs e)
        {

            TreeViewItem selectedTVI = (TreeViewItem)TreeView1.SelectedItem as TreeViewItem; //Ein TreeViewItem vom ausgewählten Item erstellen
           
            string oberordner = selectedTVI.Header.ToString();//Das ausgewählte Objekt in String speichern

            TreeViewItem sensorname = new TreeViewItem(); //Ein TreeViewItem vom Sensornamen erstellen

            string textSensorname = textBoxEingabeSensor.Text; //Benutzereingabe in einem string speichern

            sensorname.Header = textSensorname;

            selectedTVI.Items.Add(sensorname);
        }

        private void SensortypHinzufuegen(object sender, RoutedEventArgs e)
        {
            //SensorAndSensorgroup.Sensor<double> newSensor = new SensorAndSensorgroup.Sensor<double>();

            //Das Fenster Sensortyp öffnen
            PopUpSensoren objectPopupSensoren = new PopUpSensoren();
            objectPopupSensoren.Show();
        }
    }
}
