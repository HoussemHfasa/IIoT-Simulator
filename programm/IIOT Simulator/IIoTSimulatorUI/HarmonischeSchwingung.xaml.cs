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
    /// Interaktionslogik für HarmonischeSchwingung.xaml
    /// </summary>
    public partial class HarmonischeSchwingung : Window
    {
        public HarmonischeSchwingung()
        {
            InitializeComponent();
        }

        private void FehlerHinzufuegen(object sender, RoutedEventArgs e)
        {

        }

        private void SensordatenSpeichern(object sender, RoutedEventArgs e)
        {

        }

        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
