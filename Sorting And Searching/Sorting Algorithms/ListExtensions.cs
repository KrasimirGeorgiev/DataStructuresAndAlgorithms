namespace SortingHomework
{
    using System.Collections.Generic;

    public static class ListExtensions
    {
        public static void SwapIndexes<T>(this IList<T> collection, int i, int j)
        {
            T iValue = collection[i];
            collection[i] = collection[j];
            collection[j] = iValue;
        }
    }
}