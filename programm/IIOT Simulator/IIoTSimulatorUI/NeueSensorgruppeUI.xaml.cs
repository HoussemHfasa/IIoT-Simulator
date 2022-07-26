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
using DataStorage;
using Microsoft.Win32;

namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für NeueSensorgruppeUI.xaml
    /// </summary>

    public partial class NeueSensorgruppeUI : Window
    {
        Sensorgroups Sensorgroup;
        DataStorage.DataStorage Datasave = new DataStorage.DataStorage();
        SensorAndSensorgroup.Sensor<double> DoubleSensor;
        SensorAndSensorgroup.Sensor<bool> BoolSensor;
        string Oberordner;
        TreeViewItem selectedTVI;
        TreeViewItem unterordner;
        string unterordnerText;
        public void Unterordnerladen( int level,string Oberordner,string Unterordner, TreeViewItem selectedTVI,TreeViewItem unterordner, string unterordnerText)
        {
            selectedTVI = (TreeViewItem)TreeView1.Items[Sensorgroup.allchildren[Unterordner].path[0]] as TreeViewItem;
            if(!(Sensorgroup.allchildren[Unterordner].Sensordaten==null))
            {
                Sensorgroup.allchildren[Unterordner].Sensordaten.Topic += selectedTVI.Name;
            }
            for (int i=1;i<level-1;i++)
            {
                selectedTVI = (TreeViewItem)selectedTVI.Items[Sensorgroup.allchildren[Unterordner].path[i]] as TreeViewItem;
                if (!(Sensorgroup.allchildren[Unterordner].Sensordaten == null))
                {
                  Sensorgroup.allchildren[Unterordner].Sensordaten.Topic += "/"+selectedTVI.Name;
                }
            }
            Oberordner = Sensorgroup.basenames[Sensorgroup.allchildren[Unterordner].path[0]];//Das ausgewählte Objekt in String speichern
            selectedTVI.IsSelected = true;
            unterordner = new TreeViewItem(); //Ein TreeViewItem vom Unterordner erstellen
            unterordnerText = Unterordner; //Benutzereingabe in einem string speichern
            textBoxEingabe2.Clear(); //TextBox Eingabe wieder löschen
            unterordner.Header = unterordnerText;
            selectedTVI.Items.Add(unterordner);// Dem ausgewählten Item den Unterordner hinzufügen
            if (!(Sensorgroup.allchildren[Unterordner].Sensordaten == null))
            {
                Sensorgroup.allchildren[Unterordner].Sensordaten.Topic += "/" + Sensorgroup.allchildren[Unterordner].name;
            }
        }
        public NeueSensorgruppeUI(ref Sensorgroups NewSensorgroup)
        {
            InitializeComponent();

            this.Sensorgroup = NewSensorgroup;
            //Stamme laden
            foreach (string Base in Sensorgroup.basenames)
            {
                string stammText = Base; //Benutzer Eingabe in einem string speichern
                TreeViewItem stamm = new TreeViewItem(); //Neues Treeviewtem für den Stamm erstellen
                stamm.IsExpanded = true;
                stamm.Header = stammText; //Dem TreeViewItem den Header übergeben
                stamm.Name = stammText;
                TreeView1.Items.Add(stamm); //Der TreeView den Stamm hinzufügen
                
            }
            //UnterOrdner laden
            
            foreach (string Unterordner in Sensorgroup.allchildren.Keys)
            {
                Unterordnerladen(Sensorgroup.allchildren[Unterordner].path.Count, Oberordner, Unterordner, selectedTVI, unterordner, unterordnerText);
                                
            }
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
             MainWindow objectStartseite2 = new MainWindow(Sensorgroup);
             this.Visibility = Visibility.Hidden; 
             objectStartseite2.Show(); 
            
        }


        //Sensorgruppe erstellen:

        //Button um den Stamm hinzuzufügen
        private void StammHinzufuegen(object sender, RoutedEventArgs e)
        {
            if ((textBoxEingabe.Text).Equals(""))
            {
                // Kein Name eingetragen
                MessageBox.Show("Tragen Sie einen Namen für den Stamm ein");
            }
            else if (Sensorgroup.basenames.Contains(textBoxEingabe.Text) || (Sensorgroup.allchildren.ContainsKey(textBoxEingabe.Text)))
            {
                // Stamm doppelte Name
                MessageBox.Show("Den Name ist schon ausgewählt");
            }
            else
            {
                string stammText = textBoxEingabe.Text; //Benutzer Eingabe in einem string speichern

                TreeViewItem stamm = new TreeViewItem(); //Neues Treeviewtem für den Stamm erstellen

                stamm.IsExpanded = true;

                stamm.Header = stammText; //Dem TreeViewItem den Header übergeben

                TreeView1.Items.Add(stamm); //Der TreeView den Stamm hinzufügen

                textBoxEingabe.Clear(); //TextBox Eingabe wieder löschen

                HakenStamm.Foreground = System.Windows.Media.Brushes.Green; //Grünen Haken erscheinen lassen

                LabelInfo.Foreground = System.Windows.Media.Brushes.White; //Info ausblenden

                Sensorgroup.Add_new_Base(stammText);
            }

        }

        //Button um den Unterordner hinzuzufügen
        private void UnterordnerHinzufuegenClick(object sender, RoutedEventArgs e)
        {
            //Falls keine Name eingetragen wurde MessageBox aufrufen
            if ((textBoxEingabe2.Text).Equals(""))
            {
                // Kein Name eingetragen
                MessageBox.Show("Tragen Sie einen Namen für den Unterordner ein");
            }
            else if (Sensorgroup.basenames.Contains(textBoxEingabe2.Text) ||(Sensorgroup.allchildren.ContainsKey(textBoxEingabe2.Text)))
            {
                // Unterordner doppelte Name
                MessageBox.Show("Den Name ist schon ausgewählt");
            }
            else
            {

                // try catch für NullReferenceException (Kein Ordner ausgewählt
                try
                {
                    TreeViewItem selectedTVI = (TreeViewItem)TreeView1.SelectedItem as TreeViewItem; //Ein TreeViewItem vom ausgewählten Item erstellen


                    string oberordner = selectedTVI.Header.ToString();//Das ausgewählte Objekt in String speichern

                    TreeViewItem unterordner = new TreeViewItem(); //Ein TreeViewItem vom Unterordner erstellen

                    Style test = new Style();
                    unterordner.IsExpanded = true;


                    string unterordnerText = textBoxEingabe2.Text; //Benutzereingabe in einem string speichern
                    
                    textBoxEingabe2.Clear(); //TextBox Eingabe wieder löschen

                    unterordner.Header = unterordnerText;

                    selectedTVI.Items.Add(unterordner);// Dem ausgewählten Item den Unterordner hinzufügen

                    //sensorgroupsObject.AddNode(unterordnerText, oberordner);

                    Sensorgroup.Add_new_Node(oberordner, unterordnerText);
                }
                catch (System.NullReferenceException)
                {
                    MessageBox.Show("Wählen Sie zuerst einen übergeordnet Ordner für den neuen Ordner aus.");

                }
            }

        }



        //Speichert die Sensorgruppe als Topic(Soll)
        private void SensorgruppeSpeichernClick(object sender, RoutedEventArgs e)
        {

            // Windows SaveFileDialog aufrufen  , evtl noch InitialDirectory setzen
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Sensorgroup File (*)|*";
            if (saveDialog.ShowDialog() == true)
            {
                string Filename = saveDialog.FileName;

                // TODO: Die Methode sollte nur Sensorgroup und Path entgegennehmen, Name der Sensorgruppe soll in Sensorgroup intern abgespeichert sein
                Datasave.SaveTree(Sensorgroup,  Filename);

                MessageBox.Show("Sensorgruppe wurde gespeichert");
                
                MainWindow objectStartseite2 = new MainWindow(Sensorgroup);
                this.Visibility = Visibility.Hidden;
                objectStartseite2.Show();
            }

            

          // LabelTopic.Content = sensor.Topic;

            
        }


        //Schließt das Progranm
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }


        //Löscht die ganze Sensorgruppe
        private void SensorgruppeLoeschen(object sender, RoutedEventArgs e)
        {
            // Sensorgruppe und Treeview löschen/überschreiben
            Sensorgroup = new Sensorgroups();
            TreeView1.Items.Clear();
        }

        //Button um zu den Sensordaten-Einstellungen zu gelangen
        private void Sensordaten(object sender, RoutedEventArgs e)
        {
            string textSensorname = textBoxEingabeSensor.Text; //Benutzereingabe in einem string speichern
            string SensortypAuswahl = SensortypBox.Text;

            //Nutzer hat keinen Namen eingegeben
            if (textSensorname.Equals(""))
            {
                MessageBox.Show("Geben Sie einen Namen für den Sensor ein");
            }
            else if(SensortypAuswahl.Equals(""))
            {
                MessageBox.Show("Wählen Sie einen Sensortyp aus.");
            }
            else if (Sensorgroup.basenames.Contains(textSensorname) || (Sensorgroup.allchildren.ContainsKey(textSensorname)))
            {
                // Sensor doppelte Name
                MessageBox.Show("Den Name ist schon ausgewählt");
            }
            //Nutzer hat Namen und einen Sensortypen ausgewählt eingegeben
            else
            {
                try
                {

                
                TreeViewItem selectedTVI = (TreeViewItem)TreeView1.SelectedItem as TreeViewItem; //Ein TreeViewItem vom ausgewählten Item erstellen


                string oberordner = selectedTVI.Header.ToString();//Das ausgewählte Objekt in String speichern

                TreeViewItem sensorname = new TreeViewItem(); //Ein TreeViewItem vom Sensornamen erstellen            

                sensorname.Header = textSensorname;
                textBoxEingabeSensor.Clear(); //TextBox Eingabe wieder löschen

                // Sensoren mit anderer Farbe
                SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                sensorname.Foreground = brush;
                

                selectedTVI.Items.Add(sensorname);

                // Auswahl für die verschiedenen Sensoren, die der Nutzer genommen hat
                // und Hinzufügen des Sensors in unsere Sensorgroup
                

                // Funktioniert noch nicht wie gewollt

                if (SensortypAuswahl == "Temperatursensor")
                {
                    DoubleSensor = new TemperatureSensor();
                    DoubleSensor.Sensor_id = textSensorname;
                    Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);

                    PopUpSensoren objectPopupSensoren;
                    objectPopupSensoren = new PopUpSensoren(ref DoubleSensor);
                    objectPopupSensoren.Show();
                }
                else if (SensortypAuswahl == "Helligkeitssensor")
                {
                    DoubleSensor = new BrightnessSensor();
                    DoubleSensor.Sensor_id = textSensorname;
                    Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);

                    PopUpSensoren objectPopupSensoren;
                    objectPopupSensoren = new PopUpSensoren(ref DoubleSensor);
                    objectPopupSensoren.Show();
                }
                else if (SensortypAuswahl == "Stromsensor")
                {
                    DoubleSensor = new CurrentSensor();
                    DoubleSensor.Sensor_id = textSensorname;
                    Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);

                    PopUpSensoren objectPopupSensoren;
                    objectPopupSensoren = new PopUpSensoren(ref DoubleSensor);
                    objectPopupSensoren.Show();
                }
                else if (SensortypAuswahl == "Rauchmelder")
                {
                    BoolSensor = new firedetector();
                    BoolSensor.Sensor_id = textSensorname;
                    Sensorgroup.Add_new_Sensor(oberordner, BoolSensor);

                    ZufallsBool ObjectZufallsBool;
                    ObjectZufallsBool = new ZufallsBool(ref BoolSensor);
                        ObjectZufallsBool.Show();
                }
                else if (SensortypAuswahl == "Feuchtigkeitssensor")
                {
                    DoubleSensor = new HumiditySensor();
                    DoubleSensor.Sensor_id = textSensorname;
                    Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);

                    PopUpSensoren objectPopupSensoren;
                    objectPopupSensoren = new PopUpSensoren(ref DoubleSensor);
                    objectPopupSensoren.Show();
                }
                else if (SensortypAuswahl == "Füllstandsensor")
                {
                    DoubleSensor = new LevelSensor();
                    DoubleSensor.Sensor_id = textSensorname;
                    Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);

                    PopUpSensoren objectPopupSensoren;
                    objectPopupSensoren = new PopUpSensoren(ref DoubleSensor);
                    objectPopupSensoren.Show();
                }
                else if (SensortypAuswahl == "Dehnungssensor")
                {
                    DoubleSensor = new StrainSensor();
                    DoubleSensor.Sensor_id = textSensorname;
                    Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);

                    PopUpSensoren objectPopupSensoren;
                    objectPopupSensoren = new PopUpSensoren(ref DoubleSensor);
                    objectPopupSensoren.Show();
                }
                else if (SensortypAuswahl == "Drehmomentsensor")
                {
                    DoubleSensor = new TorqueSensor();
                    DoubleSensor.Sensor_id = textSensorname;
                    Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);

                    PopUpSensoren objectPopupSensoren;
                    objectPopupSensoren = new PopUpSensoren(ref DoubleSensor);
                    objectPopupSensoren.Show();
                }
                else if (SensortypAuswahl == "Spannungssensor")
                {
                    DoubleSensor = new VoltageSensor();
                    DoubleSensor.Sensor_id = textSensorname;
                    Sensorgroup.Add_new_Sensor(oberordner, DoubleSensor);

                    PopUpSensoren objectPopupSensoren;
                    objectPopupSensoren = new PopUpSensoren(ref DoubleSensor);
                    objectPopupSensoren.Show();
                }

                }
                catch (System.NullReferenceException f)
                {
                    MessageBox.Show("Wählen Sie zuerst einen Ordner aus, in dem der Sensor erstellt werden soll. ");
                }

            }

        }
    }
}
