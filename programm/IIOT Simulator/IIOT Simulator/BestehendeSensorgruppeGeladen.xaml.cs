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
    /// Interaktionslogik für BestehendeSensorgruppeGeladen.xaml
    /// </summary>
    public partial class BestehendeSensorgruppeGeladen : Window
    {
        public BestehendeSensorgruppeGeladen()
        {
            InitializeComponent();
        }

        private void AbbrechenButton(object sender, RoutedEventArgs e)
        {
            Startseite objectStartseite2 = new Startseite();
            this.Visibility = Visibility.Hidden; //So wird das aktuelle Fenster dann geschlossen
            objectStartseite2.Show();
        }
    }
}
