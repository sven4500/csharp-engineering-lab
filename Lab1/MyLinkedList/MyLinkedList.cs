using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Lab1
{
    public class MyLinkedList<T>: IList<T>
    {
        public bool IsReadOnly { get { return false; } }
        public int Count { get; protected set; }

        public void Add(T item)
        {
            // У нового Node следующий элемент всегда null.
            var node = new Node<T>(item);
            if (root == null)
            {
                root = node;
                last = root;
            }
            else
            {
                last.Next = node;
                last = last.Next;
            }
            Count++;
        }

        public void Clear()
        {
            root = null;
            last = null;
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
            ++Count;
        }

        public void RemoveAt(int index)
        {
            if (index > 0 && index < Count)
            {
                var node = GetNodeByIndex(index - 1);
                if (node.Next == last)
                    last = node;
                node.Next = node.Next.Next;
                Count--;
            }
            else if (index == 0)
            {
                if (root == null)
                    throw new ArgumentOutOfRangeException();
                root = root.Next;
                if (root == null)
                    last = null;
                Count--;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        public int IndexOf(T item)
        {
            var node = root;
            for (int index = 0; node != null; ++index, node = node.Next)
                if (node.Item.Equals(item))
                    return index;
            return -1;
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
            {
                return false;
            }

            if (previousNode != null)
            {
                previousNode.Next = node.Next;
                if (node == last)
                    last = previousNode;
            }
            else
            {
                root = node.Next;
                if (root == null)
                    last = null;
            }

            --Count;
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

        // https://stackoverflow.com/questions/1272673/obtain-generic-enumerator-from-an-array
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator();
        }

        public IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            var node = root;
            while (node != null)
            {
                yield return node.Item;
                node = node.Next;
            }
        }

        protected Node<T> root = null;
        protected Node<T> last = null;
    }
}
