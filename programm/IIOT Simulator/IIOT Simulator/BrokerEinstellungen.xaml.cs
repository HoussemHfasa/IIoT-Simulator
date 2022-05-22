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
    /// Interaktionslogik für BrokerEinstellungen.xaml
    /// </summary>
    public partial class BrokerEinstellungen : Window
    {
        public BrokerEinstellungen()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           MessageBox.Show("Erfolgreich mit dem Broker verbunden");
        }
    }
    
}
