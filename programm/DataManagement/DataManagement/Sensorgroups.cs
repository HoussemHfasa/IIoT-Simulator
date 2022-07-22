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
        public Dictionary<string, NAryTree> allTree = new Dictionary<string, NAryTree>();
        public Dictionary<string, TreeNode> allchildren = new Dictionary<string, TreeNode>();
        public Dictionary<string, dynamic> allsensor = new Dictionary<string, dynamic>();
        public Dictionary<string, int> basenames_children = new Dictionary<string, int>();
        public List<string> basenames = new List<string>();
        public void Add_new_Base(string basename)
        {
            var Tree = new NAryTree();
            Tree.root = new TreeNode(basename);
            allTree.Add(basename, Tree);
            basenames.Add(basename);
            basenames_children.Add(basename, 0);
        }

        public void Add_new_Node(string Mother, string Node)
        {
            if (allTree.ContainsKey(Mother))
            {
                TreeNode ch1 = new TreeNode(Node);
                allTree[Mother].root.addChild(ch1);
                ch1.path.Add(basenames.IndexOf(Mother));
                ch1.path.Add(basenames_children[Mother]);
                basenames_children[Mother] += 1;
                allchildren.Add(Node, ch1);

            }
            else
            {
                TreeNode ch = new TreeNode(Node);
                allchildren.Add(Node, ch);
                if (allchildren.ContainsKey(Mother))
                {
                    List<int> path = allchildren[Mother].path;
                    int i = allchildren[Mother].path.Count;
                    switch (i)
                    {
                        case 2:
                            allchildren[Node].path = new List<int>();
                            foreach (int k in allchildren[Mother].path)
                            {
                                allchildren[Node].path.Add(k);
                            }
                            allchildren[Node].path.Add(allchildren[Mother].childnumber);
                            allchildren[Mother].childnumber += 1;

                            allTree[basenames[path[0]]].root.child[path[1]].addChild(ch);

                            break;
                        case 3:
                            allchildren[Node].path = new List<int>();
                            foreach (int k in allchildren[Mother].path)
                            {
                                allchildren[Node].path.Add(k);
                            }
                            ch.path.Add(allchildren[Mother].childnumber);
                            allchildren[Mother].childnumber += 1;

                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].addChild(ch);
                            break;
                        case 4:
                            allchildren[Node].path = new List<int>();
                            foreach (int k in allchildren[Mother].path)
                            {
                                allchildren[Node].path.Add(k);
                            }
                            ch.path.Add(allchildren[Mother].childnumber);
                            allchildren[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].addChild(ch);
                            break;
                        case 5:
                            allchildren[Node].path = new List<int>();
                            foreach (int k in allchildren[Mother].path)
                            {
                                allchildren[Node].path.Add(k);
                            }
                            ch.path.Add(allchildren[Mother].childnumber);
                            allchildren[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].addChild(ch);
                            break;
                        case 6:
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].child[path[5]].addChild(ch);
                            break;
                        case 7:
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].child[path[5]].child[path[6]].addChild(ch);
                            break;

                    }
                }
                else if (allsensor.ContainsKey(Mother))
                {
                    List<int> path = allsensor[Mother].path;
                    int i = allsensor[Mother].path.Count;
                    switch (i)
                    {
                        case 2:
                            allchildren[Node].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allchildren[Node].path.Add(k);
                            }
                            ch.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].addChild(ch);
                            break;
                        case 3:
                            allchildren[Node].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allchildren[Node].path.Add(k);
                            }
                            ch.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].addChild(ch);
                            break;
                        case 4:
                            allchildren[Node].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allchildren[Node].path.Add(k);
                            }
                            ch.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].addChild(ch);
                            break;
                        case 5:
                            allchildren[Node].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allchildren[Node].path.Add(k);
                            }
                            ch.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].addChild(ch);
                            break;
                        case 6:
                            allchildren[Node].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allchildren[Node].path.Add(k);
                            }
                            ch.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].child[path[5]].addChild(ch);
                            break;
                        case 7:
                            allchildren[Node].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allchildren[Node].path.Add(k);
                            }
                            ch.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].child[path[5]].child[path[6]].addChild(ch);
                            break;

                    }
                }
            }
        }
        public void Add_new_sensor(string Mother, dynamic new_Sensor)
        {
            if (allTree.ContainsKey(Mother))
            {


                new_Sensor.path.Add(basenames.IndexOf(Mother));
                new_Sensor.path.Add(basenames_children[Mother]);
                basenames_children[Mother] += 1;
                allsensor.Add(new_Sensor.Sensor_id, new_Sensor);

                allTree[Mother].root.addsensor(new_Sensor);

            }
            else
            {
                if (allchildren.ContainsKey(Mother))
                {
                    allsensor.Add(new_Sensor.Sensor_id, new_Sensor);
                    List<int> path = allchildren[Mother].path;
                    int i = allchildren[Mother].path.Count;
                    switch (i)
                    {
                        case 2:

                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allchildren[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allchildren[Mother].childnumber);
                            allchildren[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].addsensor(new_Sensor);
                            break;
                        case 3:
                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allchildren[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allchildren[Mother].childnumber);
                            allchildren[Mother].childnumber += 1;
                            //allsensor.Add(Node, new_Sensor);
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].addsensor(new_Sensor);
                            break;
                        case 4:
                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allchildren[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allchildren[Mother].childnumber);
                            allchildren[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].addsensor(new_Sensor);
                            break;
                        case 5:
                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allchildren[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allchildren[Mother].childnumber);
                            allchildren[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].addsensor(new_Sensor);
                            break;
                        case 6:
                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allchildren[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allchildren[Mother].childnumber);
                            allchildren[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].child[path[5]].addsensor(new_Sensor);
                            break;
                        case 7:
                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allchildren[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allchildren[Mother].childnumber);
                            allchildren[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].child[path[5]].child[path[6]].addsensor(new_Sensor);
                            break;

                    }
                }
                else if ((allsensor.ContainsKey(Mother)))
                {
                    allsensor.Add(new_Sensor.Sensor_id, new_Sensor);
                    List<int> path = allsensor[Mother].path;
                    int i = allsensor[Mother].path.Count;
                    switch (i)
                    {
                        case 2:
                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;

                            allTree[basenames[path[0]]].root.child[path[1]].addsensor(new_Sensor);
                            break;
                        case 3:
                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;
                            //allsensor.Add(Node, new_Sensor);
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].addsensor(new_Sensor);
                            break;
                        case 4:
                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].addsensor(new_Sensor);
                            break;
                        case 5:
                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].addsensor(new_Sensor);
                            break;
                        case 6:
                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].child[path[5]].addsensor(new_Sensor);
                            break;
                        case 7:
                            allsensor[new_Sensor.Sensor_id].path = new List<int>();
                            foreach (int k in allsensor[Mother].path)
                            {
                                allsensor[new_Sensor.Sensor_id].path.Add(k);
                            }
                            new_Sensor.path.Add(allsensor[Mother].childnumber);
                            allsensor[Mother].childnumber += 1;
                            allTree[basenames[path[0]]].root.child[path[1]].child[path[2]].child[path[3]].child[path[4]].child[path[5]].child[path[6]].addsensor(new_Sensor);

                            break;
                    }
                }
            }
        }

    }

}


