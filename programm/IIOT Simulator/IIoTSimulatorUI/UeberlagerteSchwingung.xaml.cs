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

namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für UeberlagerteSchwingung.xaml
    /// </summary>
    public partial class UeberlagerteSchwingung : Window
    {
        public UeberlagerteSchwingung()
        {
            InitializeComponent();
        }

        private void FehlerHinzufuegen(object sender, RoutedEventArgs e)
        {
            SensordatenFehler objectFehler = new SensordatenFehler();
            this.Visibility = Visibility.Hidden;
            objectFehler.Show();
        }

        private void SensordatenSpeichern(object sender, RoutedEventArgs e)
        {
            NeueSensorgruppeUI objectNeueSensorgruppe = new NeueSensorgruppeUI();
            this.Visibility = Visibility.Hidden;
            objectNeueSensorgruppe.Show();
        }

        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
