using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Node<T>
    {
        public Node()
        {
            Next = null;
        }

        public Node(T item)
        {
            Item = item;
            Next = null;
        }

        public T Item { get; set; }
        public Node<T> Next { get; set; }
    }
}
