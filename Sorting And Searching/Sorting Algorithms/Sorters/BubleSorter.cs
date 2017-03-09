namespace SortingHomework.Sorters
{
    using System;
    using System.Collections.Generic;

    public class BubbleSorter<T> : SortingBase<T> where T : IComparable<T>
    {
        public override void Sort(IList<T> collection)
        {
            for (int i = 0; i < collection.Count - 1; i++)
            {
                bool hasSwapped = false;

                for (int j = 0; j < collection.Count - 1; j++)
                {
                    if (IsGreater(collection[j], collection[j + 1]))
                    {
                        collection.SwapIndexes(j, j + 1);
                        hasSwapped = true;
                    }
                }

                if (!hasSwapped)
                {
                    break;
                }
            }
        }
    }
}