﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
<<<<<<< HEAD
	//NAryTree für die Datenspeicherung
	//die Methode Basename/Nodename/Sensor nicht geeignet für unsere Programm ,da der Nutzer mehrere Unterordner erstellen kann
	public class Node
	{
		public string Mothername;
		public string name;
		public List<int> path = new List<int> { };
		public int childnumber = 0;
		public Node(string node)
		{
			this.name = node;
		}
	}
	public class TreeNode
	{
		public string nodename;
		public object Sensordaten;
		public List<TreeNode> child;
		public TreeNode(object sensor)
		{
			this.Sensordaten = sensor;
			this.child = new List<TreeNode>();

		}
		public TreeNode(string Nodename)
		{
			this.nodename = Nodename;
			this.child = new List<TreeNode>();
		}

		public void addChild(Node nodename)
		{
			var t = new TreeNode(nodename);
			this.child.Add(t);
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
=======
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
            public void printPreorder(TreeNode<T> node)
            {
                if (node == null)
                {
                    return;
                }
                var i = 0;
                TreeNode<T> temp = null;
                Console.Write("  " + node.key.ToString());
                // iterating the child of given node
                while (i < node.child.Count)
                {
                    temp = node.child[i];
                    this.printPreorder(temp);
                    i++;
                }
            }
        }
    }
>>>>>>> parent of 4e757bc (Gruppen Meeting TreeStructure bearbeitet)
}
