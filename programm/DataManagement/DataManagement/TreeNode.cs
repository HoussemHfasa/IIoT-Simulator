using System;
using System.Collections.Generic;
using System.Text;

namespace SensorAndSensorgroup
{
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
}
