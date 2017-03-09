namespace SortingHomework.Sorters
{
    using System;
    using System.Collections.Generic;

    public class SelectionSorter<T> : SortingBase<T> where T : IComparable<T>
    {
        public override void Sort(IList<T> collection)
        {
            for (int i = 0; i < collection.Count - 1; i++)
            {
                int minIndex = i;

                for (int j = i + 1; j < collection.Count; j++)
                {
                    if (IsGreater(collection[minIndex], collection[j]))
                    {
                        minIndex = j;
                    }
                }

                if (i != minIndex)
                {
                    collection.SwapIndexes(i, minIndex);
                }
            }
        }
    }
}
