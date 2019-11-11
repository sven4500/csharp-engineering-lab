using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework; // TestFixture, Test, Assert

namespace Lab1
{
    // Этот класс помогает нам увидеть защищённые поля класса.
    class TestList<T> : MyLinkedList<T>
    {
        public Node<T> Root { get { return root; } }
        public Node<T> Last { get { return last; } }
    }

    [TestFixture]
    public class MyLinkedListTests
    {
        //public void Ctor_

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void Add_AddItems_CountEqual(int count)
        {
            var list = new TestList<int>();
            for (int i = 0; i < count; ++i)
                list.Add(i);
            Assert.AreEqual(count, list.Count);
        }

        [Test]
        public void Add_AddOneItem_RootNotNull()
        {
            var list = new TestList<int>();
            list.Add(0);
            Assert.False(list.Root == null);
        }
        
        [Test]
        public void Add_AddOneItem_LastEqualRoot()
        {
            var list = new TestList<int>();
            list.Add(0);
            Assert.True(list.Last == list.Root);
        }

        [Test]
        public void Add_AddManyItems_LastNotEqualRoot()
        {
            var list = new TestList<int>();
            list.Add(0);
            list.Add(1);
            Assert.False(list.Last == list.Root);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void Add_AddManyItems_LastNextAlwaysNull(int count)
        {
            var list = new TestList<int>();
            for (int i = 0; i < count; ++i)
                list.Add(i);
            Assert.True(list.Last.Next == null);
        }
    }
}
