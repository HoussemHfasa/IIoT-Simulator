using System;
using System.Collections.Generic;

namespace CommonInterfaces
{
    public interface IDatamanager
    {


    }

    public interface IBrokerComm
    {


    }

    public interface IDatastorage
    {


    }

    public interface ISensorDataSimulator

    {
        // Alle Klassen, die diese Schnittstelle implementieren, besitzen einen Konstruktor,
        // der alle für diesen speziellen Simulator benötigten Nutzereingaben aufnimmt,
        // eine Liste mit Werten und eine Funktion "SimulateValues". 


        bool SimulateValues(int Amount);


        //würde nur für double Werte funktionieren.. wie kann man Int Werte und bools zurückgeben
        //List<double> getdoubleValues();

        //List<int> getIntValues();

        //List<bool> getBoolValues();

        //oder über die den Standard-getter?

        //Idee: Eine große Simulatorklasse mit einzelnen Funktionen für Normalverteilung, Linear, exponential, bool..!!


       
    }
}
