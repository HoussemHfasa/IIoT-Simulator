using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IIOT_Simulator
{
    public class TreeViewKlasse<T> : IEnumerable<TreeViewKlasse<T>>
    {
        public T Data { get; set; }
        public TreeViewKlasse<T> Parent { get; set; }
        public ICollection<TreeViewKlasse<T>> Children { get; set; }
        public TreeViewKlasse(T data)
        {
            this.Data = data;
            this.Children = new LinkedList<TreeViewKlasse<T>>();
        }
        public TreeViewKlasse<T> AddChild(T child)
        {
            TreeViewKlasse<T> childNode = new TreeViewKlasse<T>(child) { Parent = this };
            this.Children.Add(childNode);
            return childNode;
        }

        public IEnumerator<IIOT_Simulator.TreeViewKlasse<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
