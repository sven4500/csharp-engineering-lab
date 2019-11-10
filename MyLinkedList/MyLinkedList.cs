using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Lab1
{
    class MyLinkedList<T>: IList<T>
    {
        public bool IsReadOnly { get { return false; } }
        public int Count { get; protected set; }

        public void Add(T item)
        {
            // У нового Node следующий элемент всегда null.
            var node = new Node<T>(item);
            if (root == null)
                root = last = node;
            else
                last.Next = node;
            Count++;
        }

        public void Clear()
        {
            root = last = null;
            Count = 0;
        }

        public void CopyTo(T[] array, int index)
        {
            var node = root;
            for(int i = 0; node != null; ++i, node = node.Next)
                array[index + i] = node.Item;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void Insert(int index, T item)
        {
            var node = GetNodeByIndex(index);
            var newNode = new Node<T>(item);
            newNode.Next = node.Next;
            node.Next = newNode;
        }

        public void RemoveAt(int index)
        {
            if (index > 0)
            {
                var node = GetNodeByIndex(index - 1);
                if (node.Next == null)
                    throw new ArgumentOutOfRangeException();
                node.Next = node.Next.Next;
                Count--;
            }
            else if (index == 0)
            {
                if (root == null)
                    throw new ArgumentOutOfRangeException();
                root = root.Next;
                Count--;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public int IndexOf(T item)
        {
            int index = -1;
            if (root == null)
                return index;
            var node = root;
            do
            {
                if (node.Item.Equals(item))
                    return index;
                ++index;
            }
            while ((node = node.Next) != null);
            return index;
        }

        public bool Remove(T item)
        {
            Node<T> node = root, previousNode = null;
            while (node != null)
            {
                if (node.Item.Equals(item))
                    break;
                previousNode = node;
                node = node.Next;
            }
            if (node == null)
                return false;
            if (previousNode != null)
            {
                previousNode.Next = node.Next;
            }
            else
            {
                root = node.Next;
            }
            return true;
        }

        public T this[int index]
        { 
            get
            {
                var node = GetNodeByIndex(index);
                return node.Item;
            }

            set
            {
                var node = GetNodeByIndex(index);
                node.Item = value;
            }
        }

        // Возможно это всё нужно добавить в метод this[].
        private Node<T> GetNodeByIndex(int index)
        {
            var node = root;
            while (node != null && index > 0)
            {
                --index;
                node = node.Next;
            }
            if(node == null || index < 0)
                throw new ArgumentOutOfRangeException();
            return node;
        }

        /*private Node<T> GetNodeByItem(T item)
        {
            var node = root;
            while (node != null)
            {
                if (node.Item.Equals(item))
                    break;
                node = node.Next;
            }
            return node;
        }*/

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException("IEnumerable.GetEnumerator not implemented.");
            //return null;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            throw new NotImplementedException("IEnumerable<T>.GetEnumerator not implemented.");
            //return null;
        }

        protected Node<T> root = null;
        protected Node<T> last = null;
    }
}
