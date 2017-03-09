namespace SortingHomework.Tests.Sorting
{
    using System;
    using Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class SortingsTestsBase<T> where T : IComparable<T>
    {
        protected T[] Collection;
        protected ISorter<T> sorty;

        [TestInitialize]
        public abstract void TestInitialization();

        [TestMethod]
        public void SortingShouldProduceSortedCollectionInAscendingOrder()
        {
            this.sorty.Sort(this.Collection);

            Assert.IsTrue(this.IsSortedAscending(this.Collection), "The Collection is not sorted in ascending order");
        }

        private bool IsSortedAscending(T[] collection)
        {
            T prev = collection[0];

            for (int i = 1; i < collection.Length; i++)
            {
                if (prev.CompareTo(collection[i]) > 0)
                {
                    return false;
                }

                prev = collection[i];
            }

            return true;
        }
    }
}
