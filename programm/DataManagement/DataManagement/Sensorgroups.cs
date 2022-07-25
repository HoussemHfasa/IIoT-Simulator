using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CommonInterfaces;


namespace SensorAndSensorgroup
{
    
        public class Sensorgroups : ISensorGroups
        {
           
        
            //DataStorage<string> store = new DataStorage<string>();
            //allgemeine Adresse für die Sensoren ,die in der Liste sind
            public string Base { get; set; }
            // Unterordner Name
            public string Node { get; set; }
            //Ordnerpfad von die Sensorgruppen gespeichert
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"SensorGroups\");

            // Name der Sensorgruppe
            public string Name { get; set; }

            //List von Sensoren und ihren Node
            public Dictionary<string, List<string>> SensorIds
            {
                get
                {
                    return this.SensorIds;
                }
                set
                {
               //     this.SensorIds = store.LoadSensorgroup(Base, folderPath);
                }
            }


            //Methode Ordner erstellen
            public void Create_File(string folderpath, string Basename)
            {
                if (!Directory.Exists(Path.Combine(folderpath, Basename)))
                {
                    TextWriter tw = new StreamWriter(folderpath + Basename);
                    tw.Close();

                }
            }

            // allgemeine Adresse für die Sensoren hinzufügen
            public void AddBase(string BaseName)
            {
            List<string> BasenameListe = new List<string>();
                // BasenameListe = store.BasenameDeserialize(folderPath );
                 if (!File.Exists(folderPath + "List of Basenames"))
                 {
                     Create_File(folderPath, "List of Basenames");
                 }
                   if (!File.Exists(folderPath+BaseName))
                 {
                     Create_File(folderPath, BaseName);
                 }
                   if(!BasenameListe.Contains(BaseName))
                 {
                     BasenameListe.Add(BaseName);
                 }

              //   store.BasenamSerialize(BasenameListe,folderPath);


            }
            // allgemeine Adresse für die Sensoren loeschen
            public void DeleteBase(string BaseName)
            {
                List<string> List_of_Basename = new List<string>();
              //  List_of_Basename = store.BasenameDeserialize(folderPath);
                if (File.Exists(folderPath + BaseName))
                {
                    File.Delete(folderPath + BaseName);
                    List_of_Basename.Remove(BaseName);
                }
               // store.BasenamSerialize(List_of_Basename, folderPath);
            }
            // Unterordner hinzufügen unter die Adresse
            public void AddNode(string NodeName, string Basename)
            {
            Dictionary<string, List<string>> Ids = new Dictionary<string, List<string>>();

                 

                 if (File.Exists(Path.Combine(folderPath, Basename)))
                 {
                  //   if(!(store.LoadSensorgroup(Basename, folderPath)==null))
                     {
                   //      Ids = store.LoadSensorgroup(Basename, folderPath);
                     }

                     if (!Ids.ContainsKey(NodeName))
                     {
                         Ids.Add(NodeName, new List<string> { });
                    //     store.SaveSensorgroup(Ids, Basename, folderPath);
                     }
                 }
            }

            // Unterordner Löschen
            public void DeleteNode(string NodeName, string Basename)
            {
                Dictionary<string, List<string>> Sensorids = new Dictionary<string, List<string>>();
             //   if (!(store.LoadSensorgroup(Basename, folderPath) == null))
                {
             //       Sensorids = store.LoadSensorgroup(Basename, folderPath);
                }
                if (File.Exists(folderPath + Basename))
                {
                    if (Sensorids.ContainsKey(NodeName))
                    {
                        Sensorids.Remove(NodeName);
                    }

              //      store.SaveSensorgroup(Sensorids, Basename, folderPath);
                }

            }
            // Sensor in eimen bestimmten Adresse und Unterordner hinzufügen
            public void Sensorhinzufuegen(string sensorid, string NodeName, string Basename)
            {
                Dictionary<string, List<string>> Sensorids = new Dictionary<string, List<string>>();
              //  if (!(store.LoadSensorgroup(Basename, folderPath) == null))
                {
               //     Sensorids = store.LoadSensorgroup(Basename, folderPath);
                }
                if ((File.Exists(folderPath + Basename)) && (Sensorids.Keys.Contains(NodeName)))
                {
                    if (!Sensorids[NodeName].Contains(sensorid))
                    {
                        Sensorids[NodeName].Add(sensorid);
                    }

                  //  store.SaveSensorgroup(Sensorids, Basename, folderPath);
                }
            }
            // Sensor von eimen bestimmten Adresse und Unterordner löschen
            public void Sensorloeschen(string sensorid, string NodeName, string Basename)
            {
                Dictionary<string, List<string>> Sensorids = new Dictionary<string, List<string>>();
            //    if (!(store.LoadSensorgroup(Basename, folderPath) == null))
                {
              //      Sensorids = store.LoadSensorgroup(Basename, folderPath);
                }
                if ((File.Exists(folderPath + Basename)) && (Sensorids.ContainsKey(NodeName)))
                {

                    if (Sensorids[NodeName].Contains(sensorid))
                    {
                        Sensorids[NodeName].Remove(sensorid);
                    }
                 //   store.SaveSensorgroup(Sensorids, Basename, folderPath);
                }

            }
        // Änderungen im Gruppenmeeting

        //die Methode Basename/Nodename/Sensor nicht geeignet für unsere Programm ,da der Nutzer mehrere Unterordner erstellen kann
        public Dictionary<string, TreeNode> allchildren = new Dictionary<string, TreeNode>();
        public Dictionary<string, int> basenames_children = new Dictionary<string, int>();
        public List<string> basenames = new List<string>();
        public string Sensorgroupname;
        public void Add_new_Base(string basename)
        {
            var Tree = new NAryTree();
            Tree.root = new TreeNode(basename);
            basenames.Add(basename);
            Tree.root.path.Add(basenames.IndexOf(basename));
            basenames_children.Add(basename, 0);
        }
        public void Add_new_Node(string Mother, string Node)
        {

            if (basenames.Contains(Mother))
            {
                TreeNode ch1 = new TreeNode(Node);
                ch1.path.Add(basenames.IndexOf(Mother));
                ch1.path.Add(basenames_children[Mother]);
                basenames_children[Mother] += 1;
                allchildren.Add(Node, ch1);

            }
            else
            {
                TreeNode ch = new TreeNode(Node);
                allchildren.Add(Node, ch);
                allchildren[Node].path = new List<int>();
                allchildren[Node].path.AddRange(allchildren[Mother].path);
                allchildren[Node].path.Add(allchildren[Mother].childnumber);
                allchildren[Mother].childnumber += 1;
               
            }
        }
        public void  Add_new_Sensor(string Mother, dynamic new_Sensor)
        {
            if (basenames.Contains(Mother))
            {
                TreeNode ch = new TreeNode(new_Sensor);
                ch.name = new_Sensor.Sensor_id;
                ch.path.Add(basenames.IndexOf(Mother));
                ch.path.Add(basenames_children[Mother]);
                basenames_children[Mother] += 1;
                allchildren.Add(new_Sensor.Sensor_id, ch);
              
            }
            else
            {
                TreeNode ch = new TreeNode(new_Sensor);
                ch.name = new_Sensor.Sensor_id;
                allchildren.Add(new_Sensor.Sensor_id, ch);
                List<int> path = allchildren[Mother].path;
                int i = allchildren[Mother].path.Count;
                allchildren[new_Sensor.Sensor_id].path = new List<int>();
                allchildren[new_Sensor.Sensor_id].path.AddRange(allchildren[Mother].path);
                ch.path.Add(allchildren[Mother].childnumber);
                allchildren[Mother].childnumber += 1;
                
            }
        }
        }


    }
    



