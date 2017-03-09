namespace SortingHomework.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface ISortableCollection<T> where T : IComparable<T>
    {
        IList<T> Items { get; }

        void Add(T item);

        /// <summary>
        /// Searches for a matching item in the collection
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Returns the index of the matching item or -1 if no match is found</returns>
        int BinarySearch(T item);

        /// <summary>
        /// Searches for a matching item in the collection
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Returns the index of the matching item or -1 if no match is found</returns>
        int LinearSearch(T item);

        void PrintAllItemsOnConsole();

        bool Remove(T item);

        void Shuffle();

        void Sort(ISorter<T> sorter);
    }
}