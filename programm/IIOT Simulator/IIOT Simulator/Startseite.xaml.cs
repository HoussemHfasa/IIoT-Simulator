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
    /// Interaction logic for Startseite.xaml
    /// </summary>
    public partial class Startseite : Window
    {
        public Startseite()
        {
            InitializeComponent();
        }


        //Methode für den Button 'Broker Einstellungen'
        //Öffnet das Fenster BrokerEinstellungen und schließt die Startseite
        private void BrokerSettingsClick(object sender, RoutedEventArgs e)
        {
            BrokerEinstellungen objectBrokerEinstellungen = new BrokerEinstellungen();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectBrokerEinstellungen.Show();
         }

        //Methode für den Button 'Neue Sensorgruppe erstellen'
        //Öffnet das Fenster NeueSensorgruppeErstellen und schließt die Startseite
        private void NewSensorGroupClick(object sender, RoutedEventArgs e)
        {
            NeueSensorgruppeErstellen objectNeueSensorGruppe = new NeueSensorgruppeErstellen();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectNeueSensorGruppe.Show();
        }

        //Methode für den Button 'Bestehende Sensorgruppe laden'
        private void PresentSensorGroupClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hier kann man bestehende Sensorgruppen laden");
        }

    }
}
