namespace AdvancedDataStructures
{
    using System;
    using Heaps;
    using Heaps.TreeHeaps;

    /// <summary>
    /// Task: Implement a class PriorityQueue<T> based on the data structure "binary heap".
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable<T>
    {
        private readonly IHeap<T> backbone;

        /// <summary>
        /// Creates a new priority queue, default elements priority is -- greater elements first
        /// </summary>
        /// <param name="reversePriority">
        /// Default: flase, Set to true to have priority -- lesser elements first
        /// </param>
        public PriorityQueue(bool reversePriority = false)
        {
            if (reversePriority)
            {
                this.backbone = new MinTreeHeap<T>();
            }
            else
            {
                this.backbone = new MaxTreeHeap<T>();
            }
        }

        /// <summary>
        /// Creates a new priority queue, depending on the heap priority is in 
        /// Ascending or Descending Order
        /// </summary>
        /// <param name="workheap">
        /// A <see cref="MaxTreeHeap{T}"/> would set the priority to higher value first, and a 
        /// <see cref="MinTreeHeap{T}"/> woud set it to lower value first queue
        /// </param>
        public PriorityQueue(IHeap<T> workheap)
        {
            this.backbone = workheap;
        }

        public int Count { get; private set; }

        public void Enqueue(T element)
        {
            this.backbone.Insert(element);
            this.Count++;
        }

        public T Dequeue()
        {
            if (this.Count > 0)
            {
                this.Count--;
                return this.backbone.RemoveTop();
            }
            else
            {
                throw new InvalidOperationException("Invalid operation - The Queue is Empty");
            }
        }

        public T Peek()
        {
            return this.backbone.Peek();
        }

        public void Clear()
        {
            this.backbone.Clear();
            this.Count = 0;
        }
    }
}
