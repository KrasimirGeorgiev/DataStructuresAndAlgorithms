namespace AdvancedDataStructures.Heaps.TreeHeaps
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The tree heap was a very tough exersise to build while keeping track of the last 
    /// asigned node and the last incomplete node so accessing them would be O(1) like in the
    /// array implementation.
    /// I implemented the logic of accessing the last free node and last set node through stack 
    /// and queue which should be close to O(1) for the most part and the rearanging/sifting 
    /// algorithm is O(log n) as with usual heaps
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class TreeHeap<T> : IHeap<T> where T : IComparable<T>
    {
        private readonly Queue<INode<T>> incompleteNodesQueue;

        private readonly Stack<INode<T>> lastSetNode;

        private INode<T> rootNode;

        protected TreeHeap()
        {
            this.incompleteNodesQueue = new Queue<INode<T>>();
            this.lastSetNode = new Stack<INode<T>>();
        }

        public void Insert(T element)
        {
            if (this.rootNode == null)
            {
                this.SetRoot(element);
            }
            else
            {
                // Between 1 and the number of cosecutive heapRemoves
                INode<T> incompleteNode;
                do
                {
                    incompleteNode = this.incompleteNodesQueue.Dequeue();
                }
                while (incompleteNode.IsRemoved);

                INode<T> freeChild = this.InitChild(incompleteNode);
                freeChild.Value = element;

                // O(log n)
                while (this.ShouldSiftUp(freeChild))
                {
                    this.SwapNodesValues(freeChild, freeChild.Parent);
                    freeChild = freeChild.Parent;
                }
            }
        }

        public T RemoveTop()
        {
            if (this.lastSetNode.Count == 0)
            {
                if (this.rootNode == null)
                {
                    throw new InvalidOperationException("Cannot RemoveTop - no elements in the heap");
                }
                else
                {
                    var result = this.rootNode.Value;
                    this.Clear();

                    return result;
                }
            }
            else
            {
                var result = this.rootNode.Value;

                var lastNode = this.lastSetNode.Pop();
                this.rootNode.Value = lastNode.Value;

                // lastNode's parent is becoming incomplete so put it in the incompleteNodesQueue
                this.incompleteNodesQueue.Enqueue(lastNode.Parent);

                // And set parent's removed child to null (reference equals ==)
                if (lastNode.Parent.RightChild == lastNode)
                {
                    lastNode.Parent.RightChild = null;
                }
                else
                {
                    lastNode.Parent.LeftChild = null;
                }

                // Since the node can be also in the incompleteNodesQueue
                // we need to flag it to be ignored
                lastNode.IsRemoved = true;

                // Finnaly maintain correct order if root is out of place
                this.SiftDown(this.rootNode);

                return result;
            }
        }

        public T Peek()
        {
            if (this.rootNode != null)
            {
                return this.rootNode.Value;
            }
            else
            {
                throw new InvalidOperationException("Cannot Peek - no elements in the heap");
            }
        }

        public void Clear()
        {
            this.rootNode = null;
            this.lastSetNode.Clear();
            this.incompleteNodesQueue.Clear();
        }

        protected abstract bool ShouldSiftUp(INode<T> node);

        protected abstract bool ShouldSiftDown(INode<T> node, out INode<T> swichWithChild);

        private void SiftDown(INode<T> node)
        {
            INode<T> swichWithChild;
            if (this.ShouldSiftDown(node, out swichWithChild))
            {
                this.SwapNodesValues(node, swichWithChild);
                this.SiftDown(swichWithChild);
            }
        }

        private void SetRoot(T element)
        {
            this.rootNode = new Node<T> { Value = element };
            this.incompleteNodesQueue.Enqueue(this.rootNode);
        }

        private INode<T> InitChild(INode<T> node)
        {
            INode<T> child = new Node<T>(node);

            // If left is null right also is null -> do action an push back to incomplete
            if (node.LeftChild == null)
            {
                node.LeftChild = child;
                this.incompleteNodesQueue.Enqueue(node);
                this.incompleteNodesQueue.Enqueue(child);
            }
            else
            {
                node.RightChild = child;
                this.incompleteNodesQueue.Enqueue(child);
            }

            this.lastSetNode.Push(child);
            return child;
        }

        private void SwapNodesValues(INode<T> a, INode<T> b)
        {
            var aValue = a.Value;
            a.Value = b.Value;
            b.Value = aValue;
        }
    }
}