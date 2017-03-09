namespace T11ImplementLinkedListStructure
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Define additionally a class LinkedList<T> with a single field FirstElement (of type ListItem<T>).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedList<T> : ICollection<T>
    {
        private ListItem<T> firstElement;

        public LinkedList(T value)
        {
            this.FirstElement = new ListItem<T>(value);
        }

        public LinkedList(ListItem<T> item)
        {
            this.FirstElement = item;
        }

        public ListItem<T> FirstElement
        {
            get { return this.firstElement; }

            set { this.firstElement = value; }
        }

        public int Count
        {
            get
            {
                int count = 0;

                // Since elements can be linked to the list outside this class - count must be recalculated 
                // each time 
                foreach (T element in this)
                {
                    count++;
                }

                return count;
            }
        }

        public bool IsReadOnly => false;

        public IEnumerator<T> GetEnumerator()
        {
            var current = this.FirstElement;

            while (current != null)
            {
                yield return current.Value;
                current = current.NextItem;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(T item)
        {
            var successor = new ListItem<T>(item);
            successor.SetNext(this.FirstElement);
            this.FirstElement = successor;
        }

        public void Clear()
        {
            this.FirstElement = null;
        }

        public bool Contains(T item)
        {
            foreach (var element in this)
            {
                if (element.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new InvalidOperationException("Method is not supported");
        }

        public bool Remove(T item)
        {
            var current = this.FirstElement;

            while (current != null)
            {
                if (current.Equals(item))
                {
                    var prev = current.PrevItem;
                    var next = current.NextItem;

                    if (prev != null)
                    {
                        prev.SetNext(next);
                        next.SetPrev(prev);
                    }
                    else
                    {
                        this.firstElement = next;
                        next.SetPrev(null);
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
