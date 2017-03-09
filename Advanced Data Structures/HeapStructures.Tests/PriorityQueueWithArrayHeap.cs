namespace HeapStructures.Tests
{
    using AdvancedDataStructures;
    using AdvancedDataStructures.Heaps.ArrayHeaps;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PriorityQueueWithArrayHeap : PriorityQueueTestBase
    {
        [TestInitialize]
        public override void Initialize()
        {
            this.workHeap = new MaxArrayHeap<int>();
            this.testQueue = new PriorityQueue<int>(workHeap);
        }

        [TestMethod]
        public void MinPriorityQueueShouldWorkCorrectly()
        {
            var heap = new MinArrayHeap<int>();
            var queue = new PriorityQueue<int>(heap);
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