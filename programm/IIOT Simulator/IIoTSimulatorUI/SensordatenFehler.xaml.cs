﻿using System;
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
    /// Interaktionslogik für SensordatenFehler.xaml
    /// </summary>
    public partial class SensordatenFehler : Window
    {
        public SensordatenFehler()
        {
            InitializeComponent();
        }

        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Fehlermethode(object sender, RoutedEventArgs e)
        {
            FehlerZufallswerte objectFehlerZufall = new FehlerZufallswerte();
            this.Visibility = Visibility.Hidden;
            objectFehlerZufall.Show();
        }
    }
}
