using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;
using SensorAndSensorgroup;





namespace NunitTestDatamanagement
{//die Methode Basename/Nodename/Sensor nicht geeignet für unsere Programm ,da der Nutzer mehrere Unterordner erstellen kann
 //deswegen haben wir eine andere Lösung implimentiert

    /*   class BasenamelistTest
       {
         // private SensorAndSensorgroup.Sensorgroups Basenametest = new Sensorgroups();
         //  DataStorage<string> store = new DataStorage<string>();
           string Basename1;
           string Basename2;
           string Basename3;

           List<string> listname = new List<string>();

           string Filepath;
           [SetUp]
           public void Setup()
           {

               //Arrange
               Basename1 = "Haus1";
               Basename2 = "Haus2";
               Basename3 = "Haus3";
               Basename4 = "Haus4";
               if(!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory+ "SensorGroups"))
                 {
                   System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "SensorGroups");
                }
               Filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SensorGroups\");
           }

           [Test]
           //Prüfung für die Speicherung des Basenameliste
           public void It_should_save_the_BasenameList()
           {
               //Act
              // Basenametest.AddBase(Basename1);
            //   listname = store.BasenameDeserialize(Filepath);

               //Assert
               Assert.That((File.Exists(Filepath + "List of Basenames"))&&(listname.Contains(Basename1)));
           }
           [Test]
           //Prüfung ob Basename nur einmal gespeichert ist
           public void It_should_save_an_existing_Basename()
           {
               //Act
            //   Basenametest.AddBase(Basename4);
           //    Basenametest.AddBase(Basename4);
            //   listname = store.BasenameDeserialize(Filepath);
               //Assert
               Assert.That(listname.Where(s => s != null && s.StartsWith("Haus4")).Count()==1);
           }
           [Test]
           //Prüfung ob die Basename geloecht wird
           public void It_should_delete_basename_from_the_BasenameList()
           {
               //Act
           //    Basenametest.AddBase(Basename2);
           //    Basenametest.AddBase(Basename3);
           //    Basenametest.DeleteBase(Basename3);
             //  listname = store.BasenameDeserialize(Filepath);
               //Assert
               Assert.That((!listname.Contains(Basename3)) && (listname.Contains(Basename2)));
           }
           [Test]
           //prüfen ob gibt es Bugs wenn mann loescht ein nicht existierendes Basename
           public void It_should_delete_not_existing_basename_from_the_BasenameList()
           {
               //Act
               //Basenametest.DeleteBase(Basename3);
             //  Basenametest.DeleteBase(Basename3);
             //  listname = store.BasenameDeserialize(Filepath);
               //Assert
               Assert.That((!listname.Contains(Basename3)));
           }

       }*/
}
