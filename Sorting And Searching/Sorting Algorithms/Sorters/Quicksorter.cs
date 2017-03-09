namespace SortingHomework.Sorters
{
    using System;
    using System.Collections.Generic;

    public class Quicksorter<T> : SortingBase<T> where T : IComparable<T>
    {
        private IList<T> collection;

        public override void Sort(IList<T> collectionToSort)
        {
            this.collection = collectionToSort;
            this.QuickSort(0, collectionToSort.Count - 1);
        }

        private void QuickSort(int left, int right)
        {
            if (left >= right)
            {
                return;
            }

            int boundaryIndex = this.Partition(left, right);
            this.QuickSort(left, boundaryIndex - 1);
            this.QuickSort(boundaryIndex + 1, right);
        }

        private int Partition(int left, int right)
        {
            int pivotIndex = this.FindPivot(left, right);
            this.collection.SwapIndexes(left, pivotIndex);

            T pivot = this.collection[left];

            int low = left + 1,
                high = right;
            while (low <= high)
            {
                while (this.collection[high].CompareTo(pivot) > 0)
                {
                    high--;
                }

                while (low <= high && this.collection[low].CompareTo(pivot) <= 0)
                {
                    low++;
                }

                if (low <= high)
                {
                    this.collection.SwapIndexes(low, high);
                    low++;
                    high--;
                }
            }

            this.collection.SwapIndexes(left, high);
            return high;
        }

        private int FindPivot(int left, int right)
        {
            int middle = (left + right) / 2;

            if (IsGreater(this.collection[right], this.collection[middle]))
            {
                if (IsGreater(this.collection[middle], this.collection[left]))
                {
                    return middle;
                }
                else if (IsGreater(this.collection[left], this.collection[right]))
                {
                    return right;
                }
                else
                {
                    return left;
                }
            }
            else
            {
                if (IsGreater(this.collection[right], this.collection[left]))
                {
                    return right;
                }
                else if (IsGreater(this.collection[middle], this.collection[left]))
                {
                    return middle;
                }
                else
                {
                    return left;
                }
            }

        }
    }
}
