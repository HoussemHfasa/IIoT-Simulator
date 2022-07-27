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
    /// Interaktionslogik für PopUpSensoren.xaml
    /// </summary>
    public partial class PopUpSensoren : Window
    {
        // Sensor aus Konstruktor
        Sensor<double> DoubleSensor = null;
       
        // Konstruktor, der einen double Sensor übergeben bekommt
        public PopUpSensoren(ref Sensor<double> newSensor)
        {
            DoubleSensor = newSensor;
            InitializeComponent();
        }


        // Auswählen Button, Nutzer hat in Combobox eine Datenerzeugungsmethode ausgewählt
        private void Schwingung(object sender, RoutedEventArgs e)
        {
            

            //Weiterer Pfad zu den Fenstern je nach Auswahl, übergabe der Referenz des Sensors
            string DatenerzeugungAuswahl = DatenerzeugungBox.Text;
            if (DatenerzeugungAuswahl.Equals("Harmonische Schwingung"))
            {
                HarmonischeSchwingung objectDatenerzeugung;
                objectDatenerzeugung = new HarmonischeSchwingung(ref DoubleSensor);
                this.Visibility = Visibility.Hidden;
                objectDatenerzeugung.Show();
            }
            else if (DatenerzeugungAuswahl.Equals("Gedämpfte Schwingung"))
            {
                GedaempfteSchwingung objectDatenerzeugung;
                objectDatenerzeugung = new GedaempfteSchwingung(ref DoubleSensor);
                this.Visibility = Visibility.Hidden;
                objectDatenerzeugung.Show();
            }
            else if(DatenerzeugungAuswahl.Equals("Standardabweichung"))
            {
                Standardabweichung objectDatenerzeugung;
                objectDatenerzeugung = new Standardabweichung(ref DoubleSensor);
                this.Visibility = Visibility.Hidden;
                objectDatenerzeugung.Show();

            }
            else if(DatenerzeugungAuswahl.Equals("Überlagerte Schwingung"))
            {
                UeberlagerteSchwingung objectDatenerzeugung;
                objectDatenerzeugung = new UeberlagerteSchwingung(ref DoubleSensor);
                this.Visibility = Visibility.Hidden;
                objectDatenerzeugung.Show();
            }
            else  // Keine Datenerzeugungsmethode ausgewählt
            {
                MessageBox.Show("Wählen Sie eine Datenerzeugungsmethode aus.");
            }
        }

        // Schließt das Fenster
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
