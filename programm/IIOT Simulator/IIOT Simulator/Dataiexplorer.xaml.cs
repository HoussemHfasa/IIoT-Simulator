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
using CommonInterfaces;
using SensorAndSensorgroups;

namespace IIOT_Simulator
{
    /// <summary>
    /// Interaktionslogik für Dataiexplorer.xaml
    /// </summary>
    public partial class Dataiexplorer : Window
    {
        public Dataiexplorer()
        {
            InitializeComponent();


            //Objekt der Klasse SensorGroups erstellen für die ListView:
            SensorGroups sensorgroups = new SensorGroups();

            //Eine Liste der Sensorgruppen:
            List<SensorGroups> sensorgruppenListe = new List<SensorGroups>();

            //ListBox erstellen:
            ListBox DateiexplorerListe = new ListBox();

            //Der ListBox die SensorgruppenListe übergeben
            DateiExplorerListe.DataContext = sensorgruppenListe;
        }

        private void AbbrechenButton(object sender, RoutedEventArgs e)
        {
            Startseite objectStartseite3 = new Startseite();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectStartseite3.Show();
        }
    }
}
