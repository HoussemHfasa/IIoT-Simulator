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
            //TODO: Es fehlt hier noch die Überprüfung Nutzereingaben. Bitte noch spezifizieren welche Überprüfungen stattfinden müssen

            // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in Boolsensor abspeichern
            DataGenerator = new RandomBool(Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
            BoolSensor.SetValues(DataGenerator.GetSimulatorValues());

            BoolFehlerwerte objectFehler = new BoolFehlerwerte(ref BoolSensor);

            //TODO: Hier wieder close?
            this.Visibility = Visibility.Hidden;
            objectFehler.Show();
        }

        private void SensordatenSpeichern(object sender, RoutedEventArgs e)
        {
            // Objekt der Datenerzeugungsmethode erstellen, Daten erzeugen und in Boolsensor abspeichern
            DataGenerator = new RandomBool(Convert.ToDouble(textBoxWechselwarscheinlichkeit.Text), Convert.ToUInt32(textBoxWerteanzahl.Text));
            BoolSensor.SetValues(DataGenerator.GetSimulatorValues());

            Close();

        }
    }
}
