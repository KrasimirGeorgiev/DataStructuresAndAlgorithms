namespace AdvancedDataStructures.Heaps.ArrayHeaps
{
    using System;

    public class MinArrayHeap<T> : ArrayHeap<T>, IHeap<T> where T : IComparable<T>
    {
        public MinArrayHeap()
        {
        }

        public MinArrayHeap(int initialCapacity) : base(initialCapacity)
        {
        }

        protected override bool ShouldSiftUp(int index)
        {
            int parentIndex = index / 2;
            if (parentIndex < 1)
            {
                return false;
            }
            else
            {
                return this.data[parentIndex].CompareTo(this.data[index]) > 0;
            }
        }

        protected override bool ShouldSiftDown(int index, out int swapWith)
        {
            int leftChildIndex = index * 2,
                rightChildIndex = (index * 2) + 1;

            swapWith = -1;

            if (leftChildIndex > this.count)
            {
                return false;
            }
            else
            {
                var leftValue = this.data[leftChildIndex];

                if (rightChildIndex > this.count ||
                    leftValue.CompareTo(this.data[rightChildIndex]) < 0)
                {
                    swapWith = leftChildIndex;
                    return leftValue.CompareTo(this.data[index]) < 0;
                }
                else
                {
                    swapWith = rightChildIndex;
                    return this.data[rightChildIndex].CompareTo(this.data[index]) < 0;
                }
            }
        }
    }
}