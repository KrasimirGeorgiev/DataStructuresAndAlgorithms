namespace HashTables.SetImplementation
{
    using System.Collections;
    using System.Collections.Generic;
    using TableImplementation;

    public class HashedSet<T> : IEnumerable<T>
    {
        private HashTable<int, T> table;

        public HashedSet()
        {
            this.table = new HashTable<int, T>();
        }

        public int Count
        {
            get { return this.table.Count; }
        }

        public void Add(T element)
        {
            this.table.Add(element.GetHashCode(), element);
        }

        public T Find(T element)
        {
            return this.table.Find(element.GetHashCode());
        }

        public void Remove(T element)
        {
            this.table.Remove(element.GetHashCode());
        }

        public void Clear()
        {
            this.table.Clear();
        }

        public HashedSet<T> Intersect(HashedSet<T> target)
        {
            var result = new HashedSet<T>();

            foreach (T element in this)
            {
                if (element.Equals(default(T)))
                {
                    foreach (T other in target)
                    {
                        if (other.Equals(element))
                        {
                            result.Add(element);
                        }
                    }
                }
                else
                {
                    var probe = target.Find(element);
                    if (!probe.Equals(default(T)))
                    {
                        result.Add(probe);
                    }
                }
            }

            return result;
        }

        public HashedSet<T> Union(HashedSet<T> target)
        {
            var result = new HashedSet<T>();

            foreach (T element in this)
            {
                result.Add(element);
            }

            foreach (T element in target)
            {
                result.Add(element);
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var pair in this.table)
            {
                yield return pair.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}