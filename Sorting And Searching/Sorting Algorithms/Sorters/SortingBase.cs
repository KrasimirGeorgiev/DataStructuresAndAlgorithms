namespace SortingHomework.Sorters
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    public abstract class SortingBase<T> : ISorter<T> where T : IComparable<T>
    {
        public static bool IsGreater(T a, T b)
        {
            return a.CompareTo(b) > 0;
        }

        public abstract void Sort(IList<T> collection);

    }
}