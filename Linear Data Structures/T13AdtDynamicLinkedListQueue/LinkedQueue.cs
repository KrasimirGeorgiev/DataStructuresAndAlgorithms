namespace T13AdtDynamicLinkedListQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using T11ImplementLinkedListStructure;

    /// <summary>
    /// Implement the ADT queue as dynamic linked list.
    /// Use generics (LinkedQueue{T}) to allow storing different data types in the queue.
    /// </summary>
    public class LinkedQueue<T> : IEnumerable<T>
    {
        private ListItem<T> next;

        private int size;

        public LinkedQueue()
        {
            this.size = 0;
        }

        public int Size
        {
            get { return this.size; }
        }

        public void Enqueue(T element)
        {
            if (this.next == null)
            {
                // I'm sorry
                // - no factory
                // - no abstraction/interface
                // - nodejs me likes also
                this.next = new ListItem<T>(element);
            }
            else
            {
                ListItem<T> last = this.next,
                    current = this.next;
                while (current != null)
                {
                    last = current;
                    current = current.NextItem;
                }

                last.SetNext(element);
            }

            this.size++;
        }

        public T GetNextInLine()
        {
            var nextItem = this.next;
            if (nextItem != null)
            {
                this.next = this.next.NextItem;
                this.size--;

                return nextItem.Value;
            }
            else
            {
                throw new InvalidOperationException("The queue is empty");
            }
        }

        public T Peek()
        {
            if (this.next != null)
            {
                return this.next.Value;
            }
            else
            {
                throw new InvalidOperationException("The queue is empty");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            var nextItem = this.next;
            while (this.next != null)
            {
                yield return nextItem.Value;
                nextItem = nextItem.NextItem;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
