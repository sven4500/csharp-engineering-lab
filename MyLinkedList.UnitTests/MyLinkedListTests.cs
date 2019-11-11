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

        TestList<T> CreateList<T>(int count = 0) where T: new()
        {
            var list = new TestList<T>();
            for (int i = 0; i < count; ++i)
                list.Add(new T());
            return list;
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void Add_AddItems_CountEqual(int count)
        {
            var list = CreateList<int>(count);
            Assert.AreEqual(count, list.Count);
        }

        [Test]
        public void Add_AddOneItem_RootNotNull()
        {
            var list = CreateList<int>(1);
            Assert.False(list.Root == null);
        }
        
        [Test]
        public void Add_AddOneItem_LastEqualRoot()
        {
            var list = CreateList<int>(1);
            Assert.True(list.Last == list.Root);
        }

        [Test]
        public void Add_AddManyItems_LastNotEqualRoot()
        {
            var list = CreateList<int>(2);
            Assert.False(list.Last == list.Root);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void Add_AddManyItems_LastNextAlwaysNull(int count)
        {
            var list = CreateList<int>(count);
            Assert.True(list.Last.Next == null);
        }

        [TestCase(0)]
        [TestCase(10)]
        public void Clear_VariableListSize_RootEqualNull(int count)
        {
            var list = CreateList<int>(count);
            list.Clear();
            Assert.True(list.Root == null);
        }

        [TestCase(0)]
        [TestCase(10)]
        public void Clear_VariableListSize_CountEqualZero(int count)
        {
            var list = CreateList<int>(count);
            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Contains_ExistingItem_True()
        {
            var list = CreateList<int>();
            list.Add(0);
            list.Add(1);
            Assert.AreEqual(true, list.Contains(1));
        }

        [Test]
        public void Contains_NotExistingItem_False()
        {
            var list = CreateList<int>();
            list.Add(0);
            list.Add(1);
            Assert.AreEqual(false, list.Contains(2));
        }
    }
}
