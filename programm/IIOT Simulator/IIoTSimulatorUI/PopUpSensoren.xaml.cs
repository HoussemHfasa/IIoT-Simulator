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
        public PopUpSensoren(Sensor<double> newSensor)
        { }

        public PopUpSensoren(Sensor<bool> newSensor)
        { }
        public PopUpSensoren()
        {
            InitializeComponent();
        }

        private void Schwingung(object sender, RoutedEventArgs e)
        {
            HarmonischeSchwingung objectSchwingung = new HarmonischeSchwingung();
            this.Visibility = Visibility.Hidden;
            objectSchwingung.Show();
        }

        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
