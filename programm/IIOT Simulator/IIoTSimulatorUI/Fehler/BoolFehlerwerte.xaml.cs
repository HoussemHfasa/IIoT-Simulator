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
    /// Interaktionslogik für BoolFehlerwerte.xaml
    /// </summary>
    public partial class BoolFehlerwerte : Window
    {
        public BoolFehlerwerte(ref Sensor<bool> NewSensor)
        {
            InitializeComponent();
        }

        private void SensordatenSpeichern(object sender, RoutedEventArgs e)
        {

        }

        private void ProgrammSchlievßenClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
