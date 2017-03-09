namespace HeapStructures.Tests
{
    using System;
    using AdvancedDataStructures;
    using AdvancedDataStructures.Heaps;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class PriorityQueueTestBase
    {
        protected IHeap<int> workHeap;
        protected PriorityQueue<int> testQueue;

        [TestInitialize]
        public abstract void Initialize();

        [TestMethod]
        public void AddingAnElementShouldWorkAndIncreaseCount()
        {
            var queue = this.testQueue;
            Assert.AreEqual(0, queue.Count);

            queue.Enqueue(5);
            queue.Enqueue(1);
            queue.Enqueue(18);
            queue.Enqueue(-9);

            Assert.AreEqual(4, queue.Count);
        }

        [TestMethod]
        public void DequeueingElementShouldGetTheExpectedElelment()
        {
            var queue = this.testQueue;
            queue.Enqueue(5);
            queue.Enqueue(1);
            queue.Enqueue(18);
            queue.Enqueue(-9);

            Assert.AreEqual(18, queue.Dequeue());
            Assert.AreEqual(5, queue.Dequeue());
            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(-9, queue.Dequeue());
        }

        [TestMethod]
        public void PeekShouldReturnTheTopElement()
        {
            var queue = this.testQueue;
            queue.Enqueue(5);

            Assert.AreEqual(5, queue.Peek());

            queue.Enqueue(1);
            Assert.AreEqual(5, queue.Peek());

            queue.Enqueue(18);
            Assert.AreEqual(18, queue.Peek());

            queue.Enqueue(-9);
            Assert.AreEqual(18, queue.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetingTheTopElementOfEmptyQueueShouldThrow()
        {
            var queue = this.testQueue;
            queue.Dequeue();
        }

        [TestMethod]
        public void ClearShouldResetCountToZero()
        {
            var queue = this.testQueue;
            queue.Enqueue(5);
            queue.Enqueue(1);
            queue.Enqueue(18);
            queue.Enqueue(-9);

            queue.Clear();

            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void ShouldWorkWithLargerAmountsOfElements()
        {
            var queue = this.testQueue;

            for (int i = 0; i < 10001; i += 10)
            {
                queue.Enqueue(i);
            }

            for (int i = 10000; i >= 0; i -= 10)
            {
                Assert.AreEqual(i, queue.Dequeue());
            }
        }
    }
}
