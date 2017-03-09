namespace SortingHomework.Sorters
{
    using System;
    using System.Collections.Generic;

    public class InsertionSorter<T> : SortingBase<T> where T : IComparable<T>
    {
        public override void Sort(IList<T> collection)
        {
            for (int i = 1; i < collection.Count; i++)
            {
                int j = i;

                while (j > 0 && IsGreater(collection[j - 1], collection[j]))
                {
                    collection.SwapIndexes(j, j - 1);
                    j--;
                }
            }
        }
    }
}