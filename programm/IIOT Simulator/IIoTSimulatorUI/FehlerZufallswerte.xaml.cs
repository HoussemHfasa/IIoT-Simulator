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
    /// Interaktionslogik für FehlerZufallswerte.xaml
    /// </summary>
    public partial class FehlerZufallswerte : Window
    {

        public FehlerZufallswerte()
        {
            InitializeComponent();
        }

        private void Hinzufügen(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
