namespace HashTable.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HashTables.TableImplementation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HashTableFunctionallity
    {
        [TestMethod]
        public void CreatingAHashTableWorksAndInitialCountIsZero()
        {
            var table = new HashTable<string, int>();

            Assert.AreEqual(0, table.Count);
        }

        [TestMethod]
        public void FillingTheTableAbove75PercentShouldDoubleItsCapacity()
        {
            var table = new HashTable<string, int>(100);

            Assert.AreEqual(100, table.Capacity);

            for (int i = 0; i < 76; i++)
            {
                table["test" + i] = 1;
            }

            Assert.AreEqual(200, table.Capacity);
        }

        [TestMethod]
        public void AddingKeyAndValueShouldWorkSoThatValueCanBeAccessedByIndex()
        {
            var table = new HashTable<DateTime, string>();
            var date = new DateTime(1999, 12, 31);
            string value = "Kraq na svety";

            table.Add(date, value);

            Assert.AreEqual(value, table[new DateTime(1999, 12, 31)]);
        }

        [TestMethod]
        public void GettingAndSettingByIndexShouldWork()
        {
            var table = new HashTable<string, int>();
            for (int i = 0; i < 50; i++)
            {
                table["index" + i] = i;
            }

            Assert.AreEqual(30, table["index30"]);
        }

        [TestMethod]
        public void AccessingAKeyByIndexThatDoNotExistShouldReturnDefaultValueForThatType()
        {
            var strIntTable = new HashTable<string, int>();

            int x = strIntTable["x"];
            Assert.AreEqual(0, x);

            var strObjTable = new HashTable<string, object>();

            object y = strObjTable["y"];

            Assert.IsNull(y);
        }

        [TestMethod]
        public void FindShouldReturnTheValueIfTheKeyIsFound()
        {
            var table = new HashTable<string, double>();

            for (int i = 0; i < 10; i++)
            {
                table["index" + i] = i;
            }

            Assert.AreEqual(7, table.Find("index7"));
        }

        [TestMethod]
        public void FindShouldReturnDefaultValueIfKeyNotFound()
        {
            var table = new HashTable<string, double>();

            double val = table.Find("pesho");

            Assert.AreEqual(default(double), val);

            var nullableValuesTable = new HashTable<int, object>();

            object nullable = nullableValuesTable.Find(100);

            Assert.AreEqual(default(object), nullable);
        }

        [TestMethod]
        public void AddingAnItemShouldAddItemAndIncreaseCount()
        {
            var table = new HashTable<double, int>();
            Assert.AreEqual(0, table.Count);

            for (int i = 1; i < 11; i++)
            {
                table.Add(i, i);
                Assert.AreEqual(i, table.Count);
                Assert.IsTrue(table.ContainsKey(i));
            }
        }

        [TestMethod]
        public void RemovingAnItemShouldRemoveItemAndDecreaseCount()
        {
            var table = new HashTable<string, double>();

            for (int i = 0; i < 10; i++)
            {
                table["index" + i] = i;
            }

            Assert.AreEqual(10, table.Count);

            for (int i = 9; i >= 0; i--)
            {
                table.Remove("index" + i);
                Assert.AreEqual(i, table.Count);
                Assert.IsFalse(table.ContainsKey("index" + i));
            }
        }

        [TestMethod]
        public void ForeachFunctionallityShouldIterateOverAllAddedItems()
        {
            var table = new HashTable<string, string>();

            var keys = new string[100];
            for (int i = 0; i < 100; i++)
            {
                string current = "index" + i;
                keys[i] = current;
                table.Add(current, current);
            }

            int counter = 0;
            foreach (var pair in table)
            {
                Assert.IsTrue(keys.Contains(pair.Key));
                counter++;
            }

            Assert.AreEqual(100, counter);
        }

        [TestMethod]
        public void ClearShouldEmptyTheTableAndKeepTheCurrentCapacity()
        {
            var table = new HashTable<string, List<int>>();
            for (int i = 0; i < 50; i++)
            {
                table[i.ToString()] = new List<int>();
                for (int j = 0; j < 5; j++)
                {
                    table[i.ToString()].Add(j);
                }

                Assert.IsTrue(table[i.ToString()].Count > 0);
            }

            int finalCapacity = table.Capacity;
            table.Clear();

            Assert.IsTrue(finalCapacity >= 50);
            Assert.AreEqual(finalCapacity, table.Capacity);
            Assert.AreEqual(0, table.Count);

            for (int i = 0; i < 50; i++)
            {
                Assert.IsFalse(table.ContainsKey(i.ToString()));
            }
        }

        [TestMethod]
        public void TableKeysShouldReturnCollectionOfAllAddKeys()
        {
            var table = new HashTable<string, int>();
            string[] keysStrings = new string[50];

            for (int i = 0; i < 50; i++)
            {
                keysStrings[i] = "key" + i;
                table["key" + i] = i;
            }

            var keys = table.Keys;

            Assert.AreEqual(50, keys.Count());

            foreach (string s in keys)
            {
                Assert.IsTrue(keysStrings.Contains(s));
            }
        }
    }
}
