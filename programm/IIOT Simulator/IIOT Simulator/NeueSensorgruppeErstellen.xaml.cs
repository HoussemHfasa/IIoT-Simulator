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

namespace IIOT_Simulator
{
    /// <summary>
    /// Interaktionslogik für NeueSensorgruppeErstellen.xaml
    /// </summary>
    public partial class NeueSensorgruppeErstellen : Window
    {
        public NeueSensorgruppeErstellen()
        {
            InitializeComponent();
        }

        private void AllesEntfernen(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Alles wurde entfernt.");
        }

        private void StammHinzufuegen(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Stamm wurde hinzugefügt");
        }

        
    }
}
