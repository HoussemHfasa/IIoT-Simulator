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
		public object Sensordaten;
		public List<TreeNode> child;
		public TreeNode(object sensor)
		{
			this.Sensordaten = sensor;
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

		public void addsensor(object Sensor)
		{
			var t = new TreeNode(Sensor);
			this.child.Add(t);
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
}
