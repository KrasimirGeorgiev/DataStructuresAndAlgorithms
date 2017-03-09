namespace HeapStructures.Tests
{
    using System;
    using AdvancedDataStructures;
    using AdvancedDataStructures.Heaps.TreeHeaps;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PriorityQueueWithTreeHeap : PriorityQueueTestBase
    {
        [TestInitialize]
        public override void Initialize()
        {
            this.workHeap = new MaxTreeHeap<int>();
            this.testQueue = new PriorityQueue<int>(workHeap);
        }

        [TestMethod]
        public void DefaultConstructorWithDefaultParametersShouldCreateMaxPriorityQueue()
        {
            var queue = new PriorityQueue<DateTime>();
            var dateA = new DateTime(2000, 1, 1);
            var dateB = new DateTime(2002, 1, 1);
            var dateC = new DateTime(1999, 1, 1);

            queue.Enqueue(dateA);
            queue.Enqueue(dateB);
            queue.Enqueue(dateC);

            Assert.AreEqual(dateB, queue.Dequeue());
            Assert.AreEqual(dateA, queue.Dequeue());
            Assert.AreEqual(dateC, queue.Dequeue());
        }

        [TestMethod]
        public void MinPriorityQueueShouldWorkCorrectly()
        {
            var queue = new PriorityQueue<int>(reversePriority: true);
            queue.Enqueue(5);
            queue.Enqueue(1);
            queue.Enqueue(18);
            queue.Enqueue(-9);

            Assert.AreEqual(-9, queue.Dequeue());
            Assert.AreEqual(1, queue.Dequeue());
            Assert.AreEqual(5, queue.Dequeue());
            Assert.AreEqual(18, queue.Dequeue());
        }
    }
}