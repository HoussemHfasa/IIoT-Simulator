using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{

	//NAryTree für die Datenspeicherung
	//die Methode Basename/Nodename/Sensor nicht geeignet für unsere Programm ,da der Nutzer mehrere Unterordner erstellen kann
	
	public class TreeNode
	{
		public string name;
		public List<int> path = new List<int> { };
		public int childnumber = 0;
		public dynamic Sensordaten=null;
		public List<TreeNode> child;
		[JsonConstructor]
		public TreeNode(dynamic sensor)
		{
			this.Sensordaten = sensor;
			this.child = new List<TreeNode>();
		}
		public TreeNode(string Nodename)
		{
			this.name = Nodename;
			this.child = new List<TreeNode>();
		}

		public void addChild(TreeNode nodename)
		{

			this.child.Add(nodename);
		}

	}
	public class NAryTree
	{
		public TreeNode root;
		public NAryTree()
		{
			// Set initial tree root to null
			this.root = null;
		}
	}

    //NAryTree für die Datenspeicherung
    //die Methode Basename/Nodename/Sensor nicht geeignjet für unsere Programm ,da der Nutzer mehrere Unterordner erstellen kann
    public class TreeNode<T>
    {
        public object key;
        public List<TreeNode<T>> child;
        public TreeNode(object key)
        {
            this.key = key;
            this.child = new List<TreeNode<T>>();
        }

        public void addChild(string key)
        {
            var t = new TreeNode<T>(key);
            this.child.Add(t);
        }

        public class NAryTree
        {
            public TreeNode<T> root;
            public NAryTree()
            {
                // Set initial tree root to null
                this.root = null;
            }
            
        }
    }
}
