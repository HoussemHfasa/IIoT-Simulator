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
using SensorDataSimulator;

namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für ZufallsBool.xaml
    /// </summary>
    public partial class ZufallsBool : Window
    {
        RandomBool DataGenerator;
        Sensor<bool> BoolSensor = null;

        public ZufallsBool(ref Sensor<bool> NewSensor)
        {
            this.BoolSensor = NewSensor;
            InitializeComponent();
        }

        private void ProgrammSchlievßenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FehlerHinzufuegen(object sender, RoutedEventArgs e)
        {
            try
            {
            //Überprüfung Nutzereingaben
            if (Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text)>1.0|| Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text)<0.0)
            {
                MessageBox.Show("Wechselwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
            }
            else if ( Convert.ToInt16(textBoxWerteanzahl.Text)<0)
            {
                MessageBox.Show("Werteanzahl darf nicht negativ sein.");
            }
            else
            {
                // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in Boolsensor abspeichern
                DataGenerator = new RandomBool(Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
                BoolSensor.SetValues(DataGenerator.GetSimulatorValues());

                BoolFehlerwerte objectFehler = new BoolFehlerwerte(ref BoolSensor);

                //TODO: Hier wieder close?
                this.Visibility = Visibility.Hidden;
                objectFehler.Show();
            }
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }
        }

        private void SensordatenSpeichern(object sender, RoutedEventArgs e)
        {
            try
            {
                //Überprüfung Nutzereingaben
                if (Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text) > 1.0 || Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text) < 0.0)
                {
                    MessageBox.Show("Wechselwarscheinlichkeit darf nicht unter 0 oder über 1 liegen");
                }
                else if (Convert.ToInt16(textBoxWerteanzahl.Text) < 0)
                {
                    MessageBox.Show("Werteanzahl darf nicht negativ sein.");
                }
                else
                    // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in Boolsensor abspeichern
                    DataGenerator = new RandomBool(Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
            BoolSensor.SetValues(DataGenerator.GetSimulatorValues());

            Close();
        }
    
            catch (System.FormatException)
            {
                MessageBox.Show("Ungültige Eingabe");
            }
        }
        

        private void Aktualisieren(object sender, RoutedEventArgs e)
        {

        }
    }
}
