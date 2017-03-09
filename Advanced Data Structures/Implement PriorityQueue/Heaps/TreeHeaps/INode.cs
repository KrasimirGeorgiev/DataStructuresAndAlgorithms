namespace AdvancedDataStructures.Heaps.TreeHeaps
{
    using System;

    public interface INode<T> : IComparable<INode<T>>
        where T : IComparable<T>
    {
        INode<T> Parent { get; set; }

        INode<T> LeftChild { get; set; }

        INode<T> RightChild { get; set; }

        T Value { get; set; }

        bool IsRemoved { get; set; }
    }
}