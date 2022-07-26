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
using SensorAndSensorgroup;
using SensorDataSimulator;


namespace IIoTSimulatorUI
{
    /// <summary>
    /// Interaktionslogik für UeberlagerteSchwingung.xaml
    /// </summary>
    public partial class UeberlagerteSchwingung : Window
    {
        HarmonicOscillation DataGeneratorOsc;
        Superposition DataGenerator;
        Sensor<double> DoubleSensor = null;

        public UeberlagerteSchwingung(ref Sensor<double> NewSensor)
        {
            this.DoubleSensor = NewSensor;
            InitializeComponent();
        }

        private void FehlerHinzufuegen(object sender, RoutedEventArgs e)
        {
            //TODO: Es fehlt hier noch die Überprüfung Nutzereingaben. Bitte noch spezifizieren welche Überprüfungen stattfinden müssen

            List<double> Oscillation1;
            List<double> Oscillation2;
            
            // Erzeugung der Harmonischen Schwingungen
            DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude1.Text), Convert.ToDouble(textBoxPeriodendauer1.Text), Convert.ToDouble(textBoxPhasenverschiebung1.Text), Convert.ToUInt32(textBoxWerteanzahl1.Text));
            Oscillation1 = DataGenerator.GetSimulatorValues();

            DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude2.Text), Convert.ToDouble(textBoxPeriodendauer2.Text), Convert.ToDouble(textBoxPhasenverschiebung2.Text), Convert.ToUInt32(textBoxWerteanzahl2.Text));
            Oscillation2 = DataGenerator.GetSimulatorValues();

            //Erzeugung der Überlagerten Schwingung und abspeichern in Sensor
            DataGenerator = new Superposition(Oscillation1, Oscillation2);
            DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());

            // Aufufen des Folgefensters; TODO: Hier wieder close?
            SensordatenFehler objectFehler = new SensordatenFehler(ref DoubleSensor);
            this.Visibility = Visibility.Hidden;
            objectFehler.Show();
        }

        private void SensordatenSpeichern(object sender, RoutedEventArgs e)
        {
            //TODO: Es fehlt hier noch die Überprüfung Nutzereingaben. Bitte noch spezifizieren welche Überprüfungen stattfinden müssen

            List<double> Oscillation1;
            List<double> Oscillation2;

            // Erzeugung der Harmonischen Schwingungen
            DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude1.Text), Convert.ToDouble(textBoxPeriodendauer1.Text), Convert.ToDouble(textBoxPhasenverschiebung1.Text), Convert.ToUInt32(textBoxWerteanzahl1.Text));
            Oscillation1 = DataGeneratorOsc.GetSimulatorValues();

            DataGeneratorOsc = new HarmonicOscillation(Convert.ToDouble(textBoxAmplitude2.Text), Convert.ToDouble(textBoxPeriodendauer2.Text), Convert.ToDouble(textBoxPhasenverschiebung2.Text), Convert.ToUInt32(textBoxWerteanzahl2.Text));
            Oscillation2 = DataGeneratorOsc.GetSimulatorValues();

            //Erzeugung der Überlagerten Schwingung und abspeichern in Sensor
            DataGenerator = new Superposition(Oscillation1, Oscillation2);
            DoubleSensor.SetValues(DataGenerator.GetSimulatorValues());
            Close();
        }

        private void ProgrammSchlievßenClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
