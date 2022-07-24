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
using Microsoft.Win32;
using SensorAndSensorgroup;

namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Sensorgroups Sensorgroup;
        public MainWindow()
        {
            this.Sensorgroup = new Sensorgroups();
            InitializeComponent();
        }

        public MainWindow(ref Sensorgroups ExistingSensorgroup)
        {
            this.Sensorgroup = ExistingSensorgroup;
            InitializeComponent();
        }

        //Methode für den Button 'Broker Einstellungen'
        //Öffnet das Fenster BrokerEinstellungen und schließt die Startseite
        private void BrokerSettingsClick(object sender, RoutedEventArgs e)
        {
            BrokerEinstellungenUI objectBrokerEinstellungen = new BrokerEinstellungenUI();
            this.Visibility = Visibility.Hidden;
            objectBrokerEinstellungen.Show();
        }

        //Methode für den Button 'Neue Sensorgruppe erstellen'
        //Öffnet das Fenster NeueSensorgruppeErstellen und schließt die Startseite
        private void NewSensorGroupClick(object sender, RoutedEventArgs e)
        {
            NeueSensorgruppeUI objectNeueSensorGruppe = new NeueSensorgruppeUI(ref Sensorgroup);
            this.Visibility = Visibility.Hidden;
            objectNeueSensorGruppe.Show();
        }

        //Methode für den Button 'Bestehende Sensorgruppe laden'
        //Öffnet den Dataiexplorer und setzt den Topic auf die geladene Sensorgruppe
        private void PresentSensorGroupClick(object sender, RoutedEventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            Nullable<bool> dialogOK = fileDialog.ShowDialog();
            //Es muss noch programmiert werden was mit der geöffneten Datei passiert
        }

        //Button um auf die Seite der Simulation zu gelangen
        private void StartSimulationClick(object sender, RoutedEventArgs e)
        {
            SimulationUI objectSimulation = new SimulationUI();
            this.Visibility = Visibility.Hidden;
            objectSimulation.Show();
        }


        //Button um das Programm zu schließen
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

