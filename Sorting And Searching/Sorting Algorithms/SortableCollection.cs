namespace SortingHomework
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public class SortableCollection<T> : ISortableCollection<T> where T : IComparable<T>
    {
        private readonly IList<T> items;

        private readonly Random rnd;

        public SortableCollection()
            : this(new T[0])
        {
        }

        public SortableCollection(IEnumerable<T> items)
        {
            this.rnd = new Random();
            this.items = new List<T>(items);
        }

        public IList<T> Items => this.items;

        public void Add(T item)
        {
            this.Items.Add(item);
        }

        public bool Remove(T item)
        {
            return this.Items.Remove(item);
        }

        public void Sort(ISorter<T> sorter)
        {
            sorter.Sort(this.items);
        }

        public int LinearSearch(T item)
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].CompareTo(item) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        public int BinarySearch(T item)
        {
            int left = 0,
                right = this.Items.Count - 1;

            while (left <= right)
            {
                int index = left + (right - left) / 2;

                if (this.Items[index].CompareTo(item) == 0)
                {
                    return index;
                }
                else if (this.Items[index].CompareTo(item) > 0)
                {
                    right = index - 1;
                }
                else
                {
                    left = index + 1;
                }
            }

            return -1;
        }

        public void Shuffle()
        {
            for (int i = 0; i < this.Items.Count - 1; i++)
            {
                var nextRandom = this.rnd.Next(i + 1, this.Items.Count);
                this.Items.SwapIndexes(i, nextRandom);
            }
        }

        public void PrintAllItemsOnConsole()
        {
            for (int i = 0; i < this.items.Count; i++)
            {
                if (i == 0)
                {
                    Console.Write(this.items[i]);
                }
                else
                {
                    Console.Write(" " + this.items[i]);
                }
            }

            Console.WriteLine();
        }
    }
}
