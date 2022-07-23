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
        Sensorgroups Sensorgroup = new Sensorgroups();
        SensorAndSensorgroup.Sensor<double> DoubleSensor;
        SensorAndSensorgroup.Sensor<bool> BoolSensor;
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

            //
            Sensorgroup.Add_new_Base(stammText);

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

            Sensorgroup.Add_new_Node(oberordner, unterordnerText);
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
            DoubleSensor.GetValues();
            TreeView1.Items.Clear();
        }

        //Button um zu den Sensordaten-Einstellungen zu gelangen
        private void Sensordaten(object sender, RoutedEventArgs e)
        {
            // TODO: Für diesen Button muss auch die Sensortypbox ausgewählt sein. Dann kann hier bereits parallel zum Treeview 
            // unser Sensor erzeugt und zur Sensorgroup hinzugefügt werden


            TreeViewItem selectedTVI = (TreeViewItem)TreeView1.SelectedItem as TreeViewItem; //Ein TreeViewItem vom ausgewählten Item erstellen
           
            
            string oberordner = selectedTVI.Header.ToString();//Das ausgewählte Objekt in String speichern

            TreeViewItem sensorname = new TreeViewItem(); //Ein TreeViewItem vom Sensornamen erstellen

            string textSensorname = textBoxEingabeSensor.Text; //Benutzereingabe in einem string speichern

            sensorname.Header = textSensorname;

            selectedTVI.Items.Add(sensorname);

            // Auswahl für die verschiedenen Sensoren, die der Nutzer genommen hat
            // und Hinzufügen des Sensors in unsere Sensorgroup
            string SensortypAuswahl = SensortypBox.Text;
            if (SensortypAuswahl == "Temperatursensor")
            {
                DoubleSensor = new TemperatureSensor();
                DoubleSensor.Sensor_id = textSensorname;
                Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);
            }
            else if (SensortypAuswahl == "Helligkeitssensor")
            {
                DoubleSensor = new BrightnessSensor();
                DoubleSensor.Sensor_id = textSensorname;
                Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);
            }
            else if (SensortypAuswahl == "Stromsensor")
            {
                DoubleSensor = new CurrentSensor();
                DoubleSensor.Sensor_id = textSensorname;
                Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);
            }
            else if (SensortypAuswahl == "Rauchmelder")
            {
                BoolSensor = new firedetector();
                BoolSensor.Sensor_id = textSensorname;
                Sensorgroup.Add_new_Sensor(oberordner, BoolSensor);
            }
            else if (SensortypAuswahl == "Feuchtigkeitssensor")
            {
                DoubleSensor = new HumiditySensor();
                DoubleSensor.Sensor_id = textSensorname;
                Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);
            }
            else if (SensortypAuswahl == "Füllstandsensor")
            {
                DoubleSensor = new LevelSensor();
                DoubleSensor.Sensor_id = textSensorname;
                Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);
            }
            else if (SensortypAuswahl == "Dehnungssensor")
            {
                DoubleSensor = new StrainSensor();
                DoubleSensor.Sensor_id = textSensorname;
                Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);
            }
            else if (SensortypAuswahl == "Drehmomentsensor")
            {
                DoubleSensor = new TorqueSensor();
                DoubleSensor.Sensor_id = textSensorname;
                Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);
            }
            else if (SensortypAuswahl == "Spannungssensor")
            {
                DoubleSensor = new VoltageSensor();
                DoubleSensor.Sensor_id = textSensorname;
                Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);
            }
            else
            {
                MessageBox.Show("Wählen Sie einen Sensortyp aus.");
            }



        }

        private void SensortypHinzufuegen(object sender, RoutedEventArgs e)
        {
            // TODO: Dieser Button soll zu einem Button "Sensordaten erzeugen" o.ä. werden. Der Nutzer kann
            // damit die Daten des Sensors bestimmen 


            //SensorAndSensorgroup.Sensor<double> DoubleSensor;

            //SensorAndSensorgroup.Sensor<double> newSensor = new SensorAndSensorgroup.Sensor<double>();
            PopUpSensoren objectPopupSensoren;
            //TODO Das Fenster Sensortyp öffnen ; IF Abfrage für Double oder Bool Sensor
            string SensortypAuswahl = SensortypBox.Text;
            if (SensortypAuswahl == "Rauchmelder")
            {
                //Bool
                objectPopupSensoren = new PopUpSensoren(ref BoolSensor);               
            }
            else
            {
                objectPopupSensoren = new PopUpSensoren(ref DoubleSensor);               
            }
            objectPopupSensoren.Show();




        }
    }
}
