using System;
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
using System.Threading.Tasks;


namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für SimulationUI.xaml
    /// </summary>
    public partial class SimulationUI : Window
    {
        Sensorgroups Sensorgroup;

        private Line xAxisLine, yAxisLine;
        private double xAxisStart = 30, yAxisStart = 30, interval = 40;
        private Polyline chartPolyline;
        private Point origin;
        private List<Holder> holders;
        private List<Value> values;
        
        private Dictionary<string,List<Value>> SensorValues;

        public List<Value> EinzelneSensorDaten(TreeNode Sensor)
        {
            
            double intervall = 0;
           // Sensor.Sensordaten.Values.Add(0);
            values = new List<Value>();      
            
            if (Convert.ToString(Sensor.Sensordaten.Sensortype) == "Rauchmelder")
            {
                for (int i = 0; i < Convert.ToInt16(Sensor.Sensordaten.AmmountofValues); i++)
                {
                    if(Sensor.Sensordaten.Values[i]=="true")
                    {
                        values.Add(new Value(intervall, true));
                    }
                    else
                    {
                        values.Add(new Value(intervall, false));
                    }
                    
                    intervall += 1;
                }

            }
            else
            {
                
                for (int i = 0; i < Convert.ToInt16(Sensor.Sensordaten.AmmountofValues); i++)
                {
                    values.Add(new Value(intervall, Convert.ToDouble(Sensor.Sensordaten.Values[i])));
                    intervall += 1;
                }
            }
                return values;
            
        }
    
        public SimulationUI(ref Sensorgroups ExistingSensorgroup)
        {
            
            this.Sensorgroup = ExistingSensorgroup;
            InitializeComponent();
            SensorValues = new Dictionary<string,List<Value>>();
            foreach(string Sensor in Sensorgroup.allchildren.Keys)
            {
                if (!(Sensorgroup.allchildren[Sensor].Sensordaten==null))
                {
                   SensorValues.Add(Sensor,EinzelneSensorDaten(Sensorgroup.allchildren[Sensor]));
                }
                
            }
            holders = new List<Holder>();
           /* values = new List<Value>()
            {
                //new Value(0,0),
                //new Value(100,100),
                //new Value(200,200),
                //new Value(300,300),
                //new Value(400,200),
                //new Value(500,500),
                //new Value(600,500),
                //new Value(700,500),
                //new Value(800,500),
                //new Value(900,600),
                //new Value(1000,200),
                //new Value(1100,100),
                //new Value(1200,400),

                //new Value(0,0),
                //new Value(100,200),
                //new Value(200,100),
                //new Value(300,200),
                //new Value(400,300),
                //new Value(500,400),
                //new Value(600,500),
                //new Value(700,400),
                //new Value(800,500),
                //new Value(900,600),
                //new Value(1000,300),
                //new Value(1100,100),
                //new Value(1200,400),

                new Value(0,0),
                new Value(100,100),
                new Value(200,400),
                new Value(300,200),
                new Value(400,400),
                new Value(500,300),
                new Value(600,100),
                new Value(700,700),
                new Value(800,200),
                new Value(900,600),
                new Value(1000,600),
                new Value(1100,0),
                new Value(1200,100),
                new Value(1300,100),
            };*/

            Paint();

            this.StateChanged += (sender, e) => Paint();
            this.SizeChanged += (sender, e) => Paint();
            
        }

        public void Paint()
        {
            try
            {
                if (this.ActualWidth > 0 && this.ActualHeight > 0)
                {
                    chartCanvas.Children.Clear();
                    holders.Clear();

                    // axis lines
                    xAxisLine = new Line()
                    {
                        X1 = xAxisStart ,
                        Y1 = this.ActualHeight - yAxisStart,
                        X2 = this.ActualWidth - xAxisStart,
                        Y2 = this.ActualHeight - yAxisStart,
                        Stroke = Brushes.LightGray,
                        StrokeThickness = 0.5,
                    };
                    yAxisLine = new Line()
                    {
                        X1 = xAxisStart,
                        Y1 = yAxisStart ,
                        X2 = xAxisStart,
                        Y2 = this.ActualHeight - yAxisStart,
                        Stroke = Brushes.LightGray,
                        StrokeThickness = 0.5,
                    };

                    chartCanvas.Children.Add(xAxisLine);
                    chartCanvas.Children.Add(yAxisLine);

                    origin = new Point(xAxisLine.X1, yAxisLine.Y2);

                    var xTextBlock0 = new TextBlock() { Text = $"{0}" };
                    chartCanvas.Children.Add(xTextBlock0);
                    Canvas.SetLeft(xTextBlock0, origin.X);
                    Canvas.SetTop(xTextBlock0, origin.Y + 5);

                    // y axis lines
                    var xValue = xAxisStart;
                    double xPoint = origin.X + interval;
                    while (xPoint < xAxisLine.X2)
                    {
                        var line = new Line()
                        {
                            X1 = xPoint,
                            Y1 = yAxisStart,
                            X2 = xPoint,
                            Y2 = this.ActualHeight - yAxisStart,
                            Stroke = Brushes.LightGray,
                            StrokeThickness = 2,
                            Opacity = 1,
                        };

                        chartCanvas.Children.Add(line);

                        var textBlock = new TextBlock { Text = $"{xValue}", };

                        chartCanvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, xPoint - 12.5);
                        Canvas.SetTop(textBlock, line.Y2 + 5);

                        xPoint += interval;
                        xValue += interval;
                    }


                    var yTextBlock0 = new TextBlock() { Text = $"{0}" };
                    chartCanvas.Children.Add(yTextBlock0);
                    Canvas.SetLeft(yTextBlock0, origin.X - 20);
                    Canvas.SetTop(yTextBlock0, origin.Y - 10);

                    // x axis lines
                    var yValue = yAxisStart;
                    double yPoint = origin.Y - interval;
                    while (yPoint > yAxisLine.Y1)
                    {
                        var line = new Line()
                        {
                            X1 = xAxisStart,
                            Y1 = yPoint,
                            X2 = this.ActualWidth - xAxisStart,
                            Y2 = yPoint,
                            Stroke = Brushes.LightGray,
                            StrokeThickness = 2,
                            Opacity = 0,
                        };

                        chartCanvas.Children.Add(line);

                        var textBlock = new TextBlock() { Text = $"{yValue}" };
                        chartCanvas.Children.Add(textBlock);
                        Canvas.SetLeft(textBlock, line.X1 - 30);
                        Canvas.SetTop(textBlock, yPoint - 10);

                        yPoint -= interval;
                        yValue += interval;
                    }

                    // connections
                    double x = 0, y = 0;
                    xPoint = origin.X;
                    yPoint = origin.Y;
                    while (xPoint < xAxisLine.X2)
                    {
                        while (yPoint > yAxisLine.Y1)
                        {
                            var holder = new Holder()
                            {
                                X = x,
                                Y = y,
                                Point = new Point(xPoint, yPoint),
                            };

                            holders.Add(holder);

                            yPoint -= interval;
                            y += interval;
                        }

                        xPoint += interval;
                        yPoint = origin.Y;
                        x += 100;
                        y = 0;
                    }

                    // polyline
                    chartPolyline = new Polyline()
                    {
                        Stroke = new SolidColorBrush(Color.FromRgb(68, 114, 196)),
                        StrokeThickness = 10,
                    };
                    chartCanvas.Children.Add(chartPolyline);

                    // showing where are the connections points
                    foreach (var holder in holders)
                    {
                        Ellipse oEllipse = new Ellipse()
                        {
                            Fill = Brushes.Red,
                            Width = 10,
                            Height = 10,
                            Opacity = 0,
                        };

                        chartCanvas.Children.Add(oEllipse);
                        Canvas.SetLeft(oEllipse, holder.Point.X - 5);
                        Canvas.SetTop(oEllipse, holder.Point.Y - 5);
                    }

                    // add connection points to polyline
                    foreach (var value in values)
                    {
                        var holder = holders.FirstOrDefault(h => h.X == value.X && h.Y == value.Y);
                        if (holder != null)
                            chartPolyline.Points.Add(holder.Point);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

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


        }
    
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

        private void DatenSenden(object sender, RoutedEventArgs e)
        {
            
            string k = "Die Sensordaten wurden an den Broker gesendet.";
            foreach(string sensor in SensorValues.Keys)
            {
                // Logbuch, welche Daten gesendet wurden
                k += $"\nDer Sensor {sensor} hat {SensorValues[sensor].Count} Wert an den Broker gesendet";
                for (int i = 0; i < SensorValues[sensor].Count; i++)
                {
                    ScrollTextBlock.Text += $"\n Der Sensor { sensor} hat" + (SensorValues[sensor][i].Y) + "Wert an den Broker gesendet";
                }
                // Hier müssten Daten an Broker gesendet werden
            }

            MessageBox.Show(k);
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

        private void Sensorauswaehlen(object sender, RoutedEventArgs e)
        {
            
        }
    }
    public class Holder
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Point Point { get; set; }

        public Holder()
        {
        }
    }

    public class Value
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool Z { get; set; }

        public Value(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Value(double x, bool z)
        {
            X = x;
            Z=z;
        }
    }
}
