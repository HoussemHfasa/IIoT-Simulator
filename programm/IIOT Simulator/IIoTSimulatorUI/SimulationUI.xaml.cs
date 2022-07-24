﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using DummySensorandSensorgroups;
using SensorAndSensorgroup;
using SensorDataSimulator;


namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für SimulationUI.xaml
    /// </summary>
    public partial class SimulationUI : Window 
    {
        Sensorgroups Sensorgroup;
        
        public SimulationUI(Sensorgroups ExistingSensorgroup)
        {
            this.Sensorgroup = ExistingSensorgroup;
            InitializeComponent();

            //
            /*
             * Alle Sensoren in eine Liste übertragen
             * Sensorgroup.allchildren.Values.ToList();
             * oder:
             * foreach(Sensor<double> item in Sensorgroup.allchildren.Values)
             * {
             *      item.GetValues()
             * }
             * foreach Sensor<double> in            
             * 
             */


            //Die geladene Sensorgreupe
            Sensorbeispiel sensor = new Sensorbeispiel();

             LabelTopicSimulation.Text = sensor.Topic;

            //Label für das Topic in der Leiste
             LabelTopic.Content = sensor.Topic;


            //Visualisierung der Sensordaten, bisher noch kein Binding
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Series 1",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                },
                new LineSeries
                {
                    Title = "Series 2",
                    Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
                    PointGeometry = null
                },
                new LineSeries
                {
                    Title = "Series 3",
                    Values = new ChartValues<double> { 4,2,7,2,7 },
                    PointGeometry = DefaultGeometries.Square,
                    PointGeometrySize = 15
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");

            SeriesCollection.Add(new LineSeries
            {
                Title = "Series 4",
                Values = new ChartValues<double> { 5, 3, 2, 4 },
                
                LineSmoothness = 0,
                PointGeometry = Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                PointGeometrySize = 50,
                PointForeground = Brushes.Gray
            });

            SeriesCollection[3].Values.Add(5d);

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
    
    
        private void ProgrammSchließenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void StartseiteButton(object sender, RoutedEventArgs e)
        {
            MainWindow objectStartseite2 = new MainWindow();
            this.Visibility = Visibility.Hidden; 
            objectStartseite2.Show();
        }

        private void BrokerSettingsClick(object sender, RoutedEventArgs e)
        {
            BrokerEinstellungenUI objectBrokerEinstellungen = new BrokerEinstellungenUI();
            this.Visibility = Visibility.Hidden; 
            objectBrokerEinstellungen.Show();
        }

        private void Verbinden(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Die Sensordaten wurden an den Broker gesendet.");
        }

        private void Abbrechen(object sender, RoutedEventArgs e)
        {
                MainWindow objectStartseite2 = new MainWindow();
                this.Visibility = Visibility.Hidden; 
                objectStartseite2.Show();
        }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
