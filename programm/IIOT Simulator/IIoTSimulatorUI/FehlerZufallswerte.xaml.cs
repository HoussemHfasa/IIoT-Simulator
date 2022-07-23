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
    /// Interaktionslogik für FehlerZufallswerte.xaml
    /// </summary>
    public partial class FehlerZufallswerte : Window
    {
        Sensor<double> DoubleSensor;
        public FehlerZufallswerte(ref Sensor<double> NewSensor)
        {
            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }

        private void Hinzufügen(object sender, RoutedEventArgs e)
        {
            List<double> Testvalues = new List<double>();
            Testvalues.Add(Convert.ToDouble(textBoxErrorRate.Text));
            Testvalues.Add(Convert.ToInt32(textBoxErrorLength.Text));
            Testvalues.Add(Convert.ToDouble(textBoxMaxError.Text));
            Testvalues.Add(Convert.ToDouble(textBoxMinError.Text));
            RandomValuesError DataGenerator = new RandomValuesError(Convert.ToDouble(textBoxErrorRate.Text), Convert.ToInt32(textBoxErrorLength.Text), Convert.ToDouble(textBoxMaxError.Text), Convert.ToDouble(textBoxMinError.Text));
            DoubleSensor.SetValues(DataGenerator.GetSensorDataWithErrors(DoubleSensor.GetValues()));

            Close();
        }

        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
