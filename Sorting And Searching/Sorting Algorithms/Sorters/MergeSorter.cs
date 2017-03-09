namespace SortingHomework.Sorters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public class MergeSorter<T> : ISorter<T> where T : IComparable<T>
    {
        public void Sort(IList<T> collection)
        {
            var sorted = this.MergeSort(collection);

            for (int i = 0; i < collection.Count; i++)
            {
                collection[i] = sorted[i];
            }
        }

        private IList<T> MergeSort(IList<T> list)
        {
            if (list.Count <= 1)
            {
                return list;
            }

            int middle = list.Count / 2;
            IList<T> leftList = list.Take(middle).ToList();
            IList<T> rightList = list
                .Skip(middle)
                .Take(middle + 1)
                .ToList();

            leftList = this.MergeSort(leftList);
            rightList = this.MergeSort(rightList);

            list = this.Merge(leftList, rightList);

            return list;
        }

        private IList<T> Merge(IList<T> leftList, IList<T> rightList)
        {
            var resultList = new List<T>(leftList.Count + rightList.Count);
            int l = 0;
            int r = 0;

            while (l < leftList.Count && r < rightList.Count)
            {
                if (leftList[l].CompareTo(rightList[r]) <= 0)
                {
                    resultList.Add(leftList[l]);
                    l++;
                }
                else
                {
                    resultList.Add(rightList[r]);
                    r++;
                }
            }

            resultList.AddRange(leftList.Skip(l).Take(leftList.Count - l));
            resultList.AddRange(rightList.Skip(r).Take(rightList.Count - r));

            return resultList;
        }
    }
}
