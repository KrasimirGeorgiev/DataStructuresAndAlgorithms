namespace AdvancedDataStructures.Heaps.TreeHeaps
{
    using System;

    public class MinTreeHeap<T> : TreeHeap<T> where T : IComparable<T>
    {
        protected override bool ShouldSiftUp(INode<T> node)
        {
            return node.Parent != null &&
                     node.Parent.Value.CompareTo(node.Value) > 0;
        }

        protected override bool ShouldSiftDown(INode<T> node, out INode<T> child)
        {
            child = null;

            // Children are asigned from left to right so if left child is null right will also be null
            var leftChild = node.LeftChild;
            if (leftChild == null)
            {
                return false;
            }

            var rightChild = node.RightChild;

            if (rightChild == null || leftChild.CompareTo(rightChild) < 0)
            {
                if (node.CompareTo(leftChild) > 0)
                {
                    child = leftChild;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (node.CompareTo(rightChild) > 0)
                {
                    child = rightChild;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}