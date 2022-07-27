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

        //die Methode Basename/Nodename/Sensor nicht geeignet für unsere Programm ,da der Nutzer mehrere Unterordner erstellen kann
        //deswegen haben wir eine andere Lösung implimentiert
       
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
    



