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
        TestList<T> CreateList<T>(int count = 0) where T: new()
        {
            var list = new TestList<T>();
            for (int i = 0; i < count; ++i)
                list.Add(new T());
            return list;
        }

        [Test]
        public void Constructor_WithoutArguments_Succeeds()
        {
            var list = CreateList<object>();
            Assert.AreEqual(null, list.Root);
            Assert.AreEqual(null, list.Last);
            Assert.AreEqual(0, list.Count);
        }

        [Category("Add")]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void Add_AddItems_CountEqual(int count)
        {
            var list = CreateList<int>(count);
            Assert.AreEqual(count, list.Count);
        }

        [Category("Add")]
        [Test]
        public void Add_AddOneItem_RootNotNull()
        {
            var list = CreateList<int>(1);
            Assert.False(list.Root == null);
        }

        [Category("Add")]
        [Test]
        public void Add_AddOneItem_LastEqualRoot()
        {
            var list = CreateList<int>(1);
            Assert.True(list.Last == list.Root);
        }

        [Category("Add")]
        [Test]
        public void Add_AddManyItems_LastNotEqualRoot()
        {
            var list = CreateList<int>(2);
            Assert.False(list.Last == list.Root);
        }

        [Category("Add")]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void Add_AddManyItems_LastNextAlwaysNull(int count)
        {
            var list = CreateList<int>(count);
            Assert.True(list.Last.Next == null);
        }

        [Category("Insert")]
        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(10, false)]
        public void Add_VariableSizeList_LastEqualRoot(int count, bool expected)
        {
            var list = CreateList<object>();
            for (int i = 0; i < count; ++i)
                list.Add(new object());
            Assert.AreEqual(expected, list.Last == list.Root);
        }

        [Category("Insert")]
        [TestCase(10)]
        public void Add_VariableSizeList_ContainsEvery(int count)
        {
            var list = CreateList<int>();
            for (int i = 0; i < count; ++i)
                list.Add(i);
            for (int i = 0; i < count; ++i)
                Assert.True(list.Contains(i));
        }

        [Category("Clear")]
        [TestCase(0)]
        [TestCase(10)]
        public void Clear_VariableListSize_RootEqualNull(int count)
        {
            var list = CreateList<int>(count);
            list.Clear();
            Assert.True(list.Root == null);
        }

        [Category("Clear")]
        [TestCase(0)]
        [TestCase(10)]
        public void Clear_VariableListSize_LastEqualNull(int count)
        {
            var list = CreateList<int>(count);
            list.Clear();
            Assert.True(list.Last == null);
        }

        [Category("Clear")]
        [TestCase(0)]
        [TestCase(10)]
        public void Clear_VariableListSize_CountEqualZero(int count)
        {
            var list = CreateList<int>(count);
            list.Clear();
            Assert.AreEqual(0, list.Count);
        }

        [Category("IndexOf")]
        [Test]
        public void IndexOf_EmptyListReferenceType_Negative()
        {
            var list = CreateList<int>();
            Assert.True(list.IndexOf(new int()) == -1);
        }

        [Category("IndexOf")]
        [Test]
        public void IndexOf_EmptyListValueType_Negative()
        {
            var list = CreateList<int>();
            Assert.True(list.IndexOf(0) == -1);
        }

        [Category("IndexOf")]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        public void IndexOf_ExistingReferenceType_PositiveIndex(int count)
        {
            var list = CreateList<object>();
            object[] obj = new object[count];
            for (int i = 0; i < count; ++i)
                obj[i] = new object();
            for (int i = 0; i < count; ++i)
                list.Add(obj[i]);
            for (int i = 0; i < count; ++i)
                Assert.AreEqual(i, list.IndexOf(obj[i]));
        }

        [Category("IndexOf")]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        public void IndexOf_ExistingValueType_PositiveIndex(int count)
        {
            var list = CreateList<int>();
            for (int i = 0; i < count; ++i)
                list.Add(i);
            for (int i = 0; i < count; ++i)
                Assert.AreEqual(i, list.IndexOf(i));
        }

        [Category("IndexOf")]
        [TestCase(10)]
        public void IndexOf_NotExistingReferenceType_PositiveIndex(int count)
        {
            var list = CreateList<object>(count);
            Assert.AreEqual(-1, list.IndexOf(new object()));
        }

        [Category("IndexOf")]
        [TestCase(10)]
        public void IndexOf_NotExistingValueType_PositiveIndex(int count)
        {
            var list = CreateList<int>(count);
            Assert.AreEqual(-1, list.IndexOf(count));
        }

        [Category("Contains")]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void Contains_ExistingReferenceType_True(int count)
        {
            var list = CreateList<object>();
            object[] obj = new object[count];
            for (int i = 0; i < count; ++i)
                obj[i] = new object();
            for (int i = 0; i < count; ++i)
                list.Add(obj[i]);
            for (int i = 0; i < count; ++i)
                Assert.True(list.Contains(obj[i]));
        }

        [Category("Contains")]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void Contains_ExistingValueType_True(int count)
        {
            var list = CreateList<int>();
            for (int i = 0; i < count; ++i)
                list.Add(i);
            for (int i = 0; i < count; ++i)
                Assert.True(list.Contains(i));
        }

        [Category("Contains")]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void Contains_NotExistingReferenceType_False(int count)
        {
            var list = CreateList<object>(count);
            Assert.False(list.Contains(new object()));
        }

        [Category("Contains")]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void Contains_NotExistingValueType_False(int count)
        {
            var list = CreateList<int>(count);
            Assert.False(list.Contains(count));
        }

        /*[Category("Insert")]
        [TestCase(10)]
        public void Insert_NotEmpty_Count(int count)
        {
            var list = CreateList<object>();
            for (int i = 0; i < count; ++i)
                list.Insert(i, new object());
            Assert.AreEqual(count, list.Count);
        }*/
    }
}
