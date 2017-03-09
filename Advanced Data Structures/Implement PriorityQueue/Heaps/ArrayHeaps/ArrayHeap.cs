namespace AdvancedDataStructures.Heaps.ArrayHeaps
{
    using System;

    public abstract class ArrayHeap<T> : IHeap<T>
        where T : IComparable<T>
    {
        protected T[] data;

        protected int count;

        private const int DEFAULT_CAPACITY = 8;

        protected ArrayHeap() : this(DEFAULT_CAPACITY)
        {
            // Objects used for thread-safety
        }

        protected ArrayHeap(int capacity)
        {
            this.data = new T[capacity];
        }

        public int Capacity
        {
            get { return this.data.Length; }
        }

        public void Clear()
        {
            this.data = new T[this.data.Length];
            this.count = 0;
        }

        public void Insert(T element)
        {
            this.count++;
            if (this.count >= this.Capacity)
            {
                this.Expand();
            }

            int index = this.count;
            this.data[index] = element;

            while (this.ShouldSiftUp(index))
            {
                int parentIndex = index / 2;
                this.SwapValues(index, parentIndex);

                index = parentIndex;
            }
        }

        public T Peek()
        {
            return this.data[1];
        }

        public T RemoveTop()
        {
            if (this.count == 0)
            {
                throw new InvalidOperationException("Cannot remove top -- the heap is empty");
            }

            var topValue = this.data[1];
            this.data[1] = this.data[this.count--];

            this.SiftDown(1);

            return topValue;
        }

        protected abstract bool ShouldSiftUp(int index);

        protected abstract bool ShouldSiftDown(int index, out int swapWith);

        private void SiftDown(int index)
        {
            int childIndex;
            if (this.ShouldSiftDown(index, out childIndex))
            {
                this.SwapValues(index, childIndex);
                this.SiftDown(childIndex);
            }
        }

        private void Expand()
        {
            var newData = new T[this.data.Length * 2];
            for (int i = 0; i < this.data.Length; i++)
            {
                newData[i] = this.data[i];
            }

            this.data = newData;
        }

        private void SwapValues(int a, int b)
        {
            T aValue = this.data[a];
            this.data[a] = this.data[b];
            this.data[b] = aValue;
        }
    }
}