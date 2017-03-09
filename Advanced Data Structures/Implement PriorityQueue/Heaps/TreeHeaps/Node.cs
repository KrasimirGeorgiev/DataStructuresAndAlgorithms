namespace AdvancedDataStructures.Heaps.TreeHeaps
{
    using System;

    public class Node<T> : INode<T>, IComparable<INode<T>>
        where T : IComparable<T>
    {
        public Node()
        {
        }

        public Node(INode<T> parent)
        {
            this.Parent = parent;
        }

        public INode<T> Parent { get; set; }

        public INode<T> LeftChild { get; set; }

        public INode<T> RightChild { get; set; }

        public T Value { get; set; }

        public bool IsRemoved { get; set; }

        public int CompareTo(INode<T> other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}