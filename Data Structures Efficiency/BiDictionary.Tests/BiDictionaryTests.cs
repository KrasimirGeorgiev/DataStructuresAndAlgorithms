namespace BiDictionary.Tests
{
    using System.Collections.Generic;
    using ImplementBiDictionary;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BiDictionaryTests
    {
        private BiDictionary<string, decimal, int> testDict;

        [TestInitialize]
        public void CreateTestDictionary()
        {
            this.testDict = new BiDictionary<string, decimal, int>();

            testDict.Add("pesho", 15.6M, 1)
                .Add("petkoo", -34625M, -5)
                .Add("gosho", 0, 0)
                .Add("boncho", 15.6M, 2)
                .Add("ivnacho", 0, 3)
                .Add("pesho", -34625M, int.MinValue)
                .Add("bay ivan", 50, 50);
        }

        [TestMethod]
        public void AddingKeysShouldWorkWithoutErrors()
        {
            var dict = new BiDictionary<string, decimal, int>();

            dict.Add("canko", 15.6M, 1)
                .Add("vanko", -34625M, -5)
                .Add("damqnko", 0, 0);

            dict["kycko", 5345] = 10;

            Assert.AreEqual(4, dict.DistinctCount);
        }

        [TestMethod]
        public void GettingByFirstKeyShouldReturnCorrectResultCollection()
        {
            string key = "pesho";
            var items = this.testDict.GetValuesByFirstKey(key);

            Assert.AreEqual(2, items.Count);
            Assert.IsTrue(items.Contains(1));
            Assert.IsTrue(items.Contains(int.MinValue));

            string alternative = "boncho";
            items = this.testDict.GetValuesByFirstKey(alternative);

            Assert.AreEqual(1, items.Count);
            Assert.IsTrue(items.Contains(2));
        }

        [TestMethod]
        public void GettingBySecondKeyShouldReturnCorrectResultCollection()
        {
            decimal key = 15.6M;
            var items = this.testDict.GetValuesBySecondKey(key);

            Assert.AreEqual(2, items.Count);
            Assert.IsTrue(items.Contains(1));
            Assert.IsTrue(items.Contains(2));

            decimal alternative = 50;
            items = this.testDict.GetValuesBySecondKey(alternative);

            Assert.AreEqual(1, items.Count);
            Assert.IsTrue(items.Contains(50));
        }

        [TestMethod]
        public void GettingByBothKeysShouldWorkCorrectlyAndReturnSingleItem()
        {
            string firstKey = "gosho";
            decimal secondKey = 0M;

            int actual = this.testDict.GetValueAt(firstKey, secondKey);

            Assert.AreEqual(0, actual);

            string alternativeFirst = "pesho";
            decimal alternativeSecond = -34625M;

            actual = this.testDict[alternativeFirst, alternativeSecond];

            Assert.AreEqual(int.MinValue, actual);
        }

        [TestMethod]
        public void ContainsFirstAndContainsSecondWorksCorrect()
        {
            Assert.IsTrue(this.testDict.ContainsFirstKey("pesho"));
            Assert.IsTrue(this.testDict.ContainsSecondKey(0));
            Assert.IsFalse(this.testDict.ContainsFirstKey("genadi"));
            Assert.IsFalse(this.testDict.ContainsSecondKey(-5));
        }

        [TestMethod]
        public void ContainsCombinationWorksCorrectly()
        {
            bool result = this.testDict.ContainsCombination("pesho", 15.6M);
            Assert.IsTrue(result);

            bool alternative = this.testDict.ContainsCombination("bai ivan", 0);
            Assert.IsFalse(alternative);
        }

        [TestMethod]
        public void EnummerationIsImplementedCorrected()
        {
            var listOItems = new List<int>();

            foreach (var i in this.testDict)
            {
                listOItems.Add(i);
            }

            Assert.AreEqual(7, listOItems.Count);

            listOItems.Sort();

            Assert.AreEqual(int.MinValue, listOItems[0]);
            Assert.AreEqual(50, listOItems[listOItems.Count - 1]);
        }

        [TestMethod]
        public void RemovingAtPositionRemovesOnlyTheSharedValue()
        {
            this.testDict.RemoveAt("pesho", 15.6M);
            Assert.IsFalse(this.testDict.ContainsCombination("pesho", 15.6M));

            this.testDict.RemoveAt("pesho", -34625M);
            Assert.IsFalse(this.testDict.ContainsCombination("pesho", -34625M));
            Assert.IsFalse(this.testDict.ContainsFirstKey("pesho"));

            Assert.IsTrue(this.testDict.ContainsSecondKey(-34625M));
            Assert.IsTrue(this.testDict.ContainsSecondKey(15.6M));
        }
    }
}
