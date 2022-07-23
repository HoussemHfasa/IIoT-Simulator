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
    /// Interaktionslogik für SensordatenFehler.xaml
    /// </summary>
    public partial class SensordatenFehler : Window
    {
        Sensor<bool> BoolSensor;
        Sensor<double> DoubleSensor;

        //Konstruktor für bool Sensoren
        public SensordatenFehler(ref Sensor<bool> newSensor)
        {
            this.BoolSensor = newSensor; 
            InitializeComponent();
        }
        //Konstruktor für double Sensoren
        public SensordatenFehler(ref Sensor<double> newSensor)
        {
            this.DoubleSensor = newSensor;
            InitializeComponent();
        }

        // TODO: Dieser Konstruktor sollte nicht verwendet werden
        public SensordatenFehler()
        {
            
            InitializeComponent();
        }


        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Fehlermethode(object sender, RoutedEventArgs e)
        {
            //TODO: Fallunterscheidung welches Combobox Contentitem ausgewählt wurde

            // Für Zufallswerte
            FehlerZufallswerte objectFehlerZufall = new FehlerZufallswerte(ref DoubleSensor);
            this.Visibility = Visibility.Hidden;
            objectFehlerZufall.Show();
        }
    }
}
