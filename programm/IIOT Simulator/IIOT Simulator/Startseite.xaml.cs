using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IIOT_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrokerSettingsClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hier kommt man zu den Broker Einstellungen");
        }

        private void NewSensorGroupClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hier kann man eine neue Sensorgruppe erstellen");
        }

        private void PresentSensorGroupClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hier kann man bestehende Sensorgruppen laden");
        }

        private void OpenWindow(object sender, RoutedEventArgs e)
        {
            BrokerEinstellungen objectBrokerEinstellungen = new BrokerEinstellungen();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectBrokerEinstellungen.Show();
        }
    }
}
