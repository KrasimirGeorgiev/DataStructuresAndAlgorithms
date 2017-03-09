namespace SortingHomework.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SortableCollectionTests
    {
        private SortableCollection<DateTime> testDatesCollection;

        [TestInitialize]
        public void Initialize()
        {
            this.testDatesCollection = new SortableCollection<DateTime>();
            var nextDate = new DateTime(2000, 1, 1);

            for (int i = 0; i < 120; i++)
            {
                this.testDatesCollection.Add(nextDate);

                nextDate = nextDate.AddMonths(1);
            }
        }

        [TestMethod]
        public void LinearSearchShouldWorkCorrectly()
        {
            Assert.AreEqual(12, this.testDatesCollection.LinearSearch(new DateTime(2001, 1, 1)));

            Assert.AreEqual((9 * 12), this.testDatesCollection.LinearSearch(new DateTime(2009, 1, 1)));

            Assert.AreEqual((5 * 12) + 2, this.testDatesCollection.BinarySearch(new DateTime(2005, 3, 1)));

            Assert.AreEqual(-1, this.testDatesCollection.LinearSearch(new DateTime(1999, 1, 1)));
            Assert.AreEqual(-1, this.testDatesCollection.BinarySearch(new DateTime(2013, 1, 1)));
        }

        [TestMethod]
        public void BinarySearchShouldWorkCorrectly()
        {
            Assert.AreEqual(12, this.testDatesCollection.BinarySearch(new DateTime(2001, 1, 1)));
            Assert.AreEqual((9 * 12), this.testDatesCollection.BinarySearch(new DateTime(2009, 1, 1)));

            Assert.AreEqual((5 * 12) + 2, this.testDatesCollection.BinarySearch(new DateTime(2005, 3, 1)));
            Assert.AreEqual((4 * 12) + 4, this.testDatesCollection.BinarySearch(new DateTime(2004, 5, 1)));

            Assert.AreEqual(-1, this.testDatesCollection.BinarySearch(new DateTime(1999, 1, 1)));
            Assert.AreEqual(-1, this.testDatesCollection.BinarySearch(new DateTime(2013, 1, 1)));
        }
    }
}