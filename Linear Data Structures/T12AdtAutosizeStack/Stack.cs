namespace T12AdtAutosizeStack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Implement the ADT stack as auto-resizable array.
    /// Resize the capacity on demand (when no space is available to add / insert a new element).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Stack<T> : IEnumerable<T>
    {
        private const int DEFAULT_CAPACITY = 8;

        private T[] stackStorage;

        private int size;

        public Stack() : this(DEFAULT_CAPACITY)
        {
        }

        public Stack(int initialCapacity)
        {
            this.stackStorage = new T[initialCapacity];
            this.size = 0;
        }

        public int Size
        {
            get
            {
                return this.size;
            }

            private set
            {
                if (value > this.Capacity)
                {
                    var newStorage = new T[this.Capacity * 2];
                    Array.Copy(this.stackStorage, newStorage, this.size);

                    this.stackStorage = newStorage;
                }

                this.size = value;
            }
        }

        public int Capacity => this.stackStorage.Length;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Size - 1; i >= 0; i--)
            {
                yield return this.stackStorage[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void StackUp(T element)
        {
            this.Size++;
            this.stackStorage[this.size - 1] = element;
        }

        public T PopOut()
        {
            if (this.Size > 0)
            {
                var element = this.stackStorage[this.size - 1];
                this.stackStorage[this.size - 1] = default(T);
                this.Size--;

                return element;
            }
            else
            {
                throw new InvalidOperationException("Stack is empty");
            }
        }
    }
}
