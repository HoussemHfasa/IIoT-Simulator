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
    public static class TestVariables
    {
        public static double Pfad;
    }
    /// <summary>
    /// Interaktionslogik für PopUpSensoren.xaml
    /// </summary>
    public partial class PopUpSensoren : Window
    {
        Sensor<double> DoubleSensor = null;
        Sensor<bool> BoolSensor = null;
        // Konstruktor, der einen double Sensor übergeben bekommt
        public PopUpSensoren(ref Sensor<double> newSensor)
        {
            DoubleSensor = newSensor;
            InitializeComponent();
        }
        // Konstruktor, der einen bool Sensor übergeben bekommt
        public PopUpSensoren(ref Sensor<bool> newSensor)
        {
            BoolSensor = newSensor;
            InitializeComponent();
        }
        /*public PopUpSensoren()
        {
            InitializeComponent();
        } */


        private void Schwingung(object sender, RoutedEventArgs e)
        {
            
           
            /* if (!DoubleSensor.Equals(null))
            {
                TestVariables.Pfad = 1;
                objectSchwingung = new HarmonischeSchwingung(ref DoubleSensor);
            }
            else if(!BoolSensor.Equals(null))
            {
                TestVariables.Pfad = 2;
                objectSchwingung = new HarmonischeSchwingung(ref BoolSensor);
            }
            else
            {
                TestVariables.Pfad = 3;
                // in diesem else wäre eine Fehlererkennung nötig. Hier darf das Programm nicht hin
                objectSchwingung = new HarmonischeSchwingung(ref DoubleSensor);
            }
            */

            //Weiterer Pfad zu den Fenstern je nach Auswahl - Harmonische Schwingung...
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
            else
            {
                MessageBox.Show("Wählen Sie eine Datenerzeugungsmethode aus.");
            }
        }

        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
