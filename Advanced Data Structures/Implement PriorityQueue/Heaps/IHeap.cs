namespace AdvancedDataStructures.Heaps
{
    using System;

    public interface IHeap<T> where T : IComparable<T>
    {
        void Insert(T element);

        T RemoveTop();

        T Peek();

        void Clear();
    }
}