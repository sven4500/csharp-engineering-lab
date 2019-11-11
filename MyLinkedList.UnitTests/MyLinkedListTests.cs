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
        public void Add_MultipleItems_CountEqual(int count)
        {
            var list = CreateList<int>(count);
            Assert.AreEqual(count, list.Count);
        }

        [Category("Add")]
        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(2, true)]
        public void Add_MultipleItems_RootNotNull(int count, bool expected)
        {
            var list = CreateList<int>(count);
            Assert.AreEqual(expected, list.Root != null);
        }
        
        [Category("Add")]
        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(2, false)]
        public void Add_MultipleItems_LastEqualRoot(int count, bool expected)
        {
            var list = CreateList<int>(count);
            Assert.AreEqual(expected, list.Last == list.Root);
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

        [Category("CopyTo")]
        [TestCase(0, 0)]
        [TestCase(0, 5)]
        [TestCase(10, 0)]
        [TestCase(10, 5)]
        public void CopyTo_From_ArrayContainsListElements(int count, int offset)
        {
            var list = CreateList<int>();
            int[] arr = new int[1 + 2 * count];
            for (int i = 0; i < count; ++i)
                list.Add(i);
            list.CopyTo(arr, offset);
            for (int i = 0; i < count; ++i)
                Assert.AreEqual(i, arr[offset + i]);
        }

        [Category("Remove")]
        [TestCase(0, 0, false)]
        [TestCase(10, 9, true)]
        [TestCase(10, 10, false)]
        public void Remove_ExistingItem_True(int count, int item, bool expected)
        {
            var list = CreateList<int>();
            for (int i = 0; i < count; ++i)
                list.Add(i);
            Assert.AreEqual(expected, list.Remove(item));
        }

        [Category("Remove")]
        [TestCase(0, 0)]
        [TestCase(10, 9)]
        [TestCase(10, 10)]
        public void Remove_ExistingItem_CountDecrements(int count, int item)
        {
            var list = CreateList<int>();
            for (int i = 0; i < count; ++i)
                list.Add(i);
            if (list.Remove(item))
                --count;
            Assert.AreEqual(count, list.Count);
        }

        [Category("Remove")]
        [TestCase(10, 8, false)]
        [TestCase(10, 9, true)]
        [TestCase(10, 10, false)]
        public void Remove_LastItem_LastChanges(int count, int item, bool change)
        {
            var list = CreateList<int>();
            for (int i = 0; i < count; ++i)
                list.Add(i);
            var last = list.Last;
            list.Remove(item);
            Assert.AreEqual(change, last != list.Last);
        }

        [Category("Remove")]
        [TestCase(0, 0, false)]
        [TestCase(10, 0, true)]
        [TestCase(10, 1, false)]
        public void Remove_RootItem_RootChanges(int count, int item, bool change)
        {
            var list = CreateList<int>();
            for (int i = 0; i < count; ++i)
                list.Add(i);
            var root = list.Root;
            list.Remove(item);
            Assert.AreEqual(change, root != list.Root);
        }

        [Category("Remove")]
        [Test]
        public void Remove_OnlyRootItem_RootNull()
        {
            var list = CreateList<int>(1);
            list.Remove(0);
            Assert.AreEqual(null, list.Root);
        }

        [Category("Remove")]
        [Test]
        public void Remove_OnlyRootItem_LastNull()
        {
            var list = CreateList<int>(1);
            list.Remove(0);
            Assert.AreEqual(null, list.Last);
        }

        /*[Category("Insert")]
        [TestCase(10)]
        public void Insert_EmptyList_Count(int count)
        {
            var list = CreateList<object>();
            for (int i = 0; i < count; ++i)
                list.Insert(i, new object());
            Assert.AreEqual(count, list.Count);
        }*/
    }
}
