namespace HashTables.TableImplementation
{
    using System.Collections;
    using System.Collections.Generic;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private const float LOAD_FACTOR = 0.75f;

        private LinkedList<TKey> keys;

        private LinkedList<KeyValuePair<TKey, TValue>>[] entries;

        public HashTable(int initialCapacity = 16)
        {
            this.entries = new LinkedList<KeyValuePair<TKey, TValue>>[initialCapacity];
            this.keys = new LinkedList<TKey>();
        }

        public int Count
        {
            get { return this.keys.Count; }
        }

        public int Capacity
        {
            get { return this.entries.Length; }
        }

        public IEnumerable<TKey> Keys
        {
            get { return this.keys; }
        }

        /// <summary>
        /// Gets or sets a value for a given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The value for a given key or default value if not present</returns>
        public TValue this[TKey key]
        {
            get
            {
                int index = this.GetIndex(key);
                var node = this.FindSublistNode(index, key);
                if (node == null)
                {
                    // Maybe stuped or useful you decide...
                    return default(TValue);
                }
                else
                {
                    return node.Value.Value;
                }
            }

            set
            {
                this.Add(key, value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (this.Count >= this.Capacity * LOAD_FACTOR)
            {
                this.Resize();
            }

            var pair = new KeyValuePair<TKey, TValue>(key, value);

            if (this.AddPair(pair))
            {
                this.keys.AddLast(key);
            }
        }

        public bool ContainsKey(TKey key)
        {
            var index = this.GetIndex(key);
            var node = this.FindSublistNode(index, key);

            return node != null;
        }

        /// <summary>
        /// Finds and returns a value from a given key. Returns default value if key is not found.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue Find(TKey key)
        {
            return this[key];
        }

        public KeyValuePair<TKey, TValue>? Remove(TKey key)
        {
            int index = this.GetIndex(key);
            var node = this.FindSublistNode(index, key);

            if (node == null)
            {
                return null;
            }
            else
            {
                if (node.List.Count > 1)
                {
                    node.List.Remove(node);
                }
                else
                {
                    this.entries[index] = null;
                }

                this.keys.Remove(key);

                return node.Value;
            }
        }

        public void Clear()
        {
            this.entries = new LinkedList<KeyValuePair<TKey, TValue>>[this.entries.Length];
            this.keys.Clear();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var list in this.entries)
            {
                if (list != null)
                {
                    foreach (var pair in list)
                    {
                        yield return pair;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private LinkedListNode<KeyValuePair<TKey, TValue>> FindSublistNode(int index, TKey key)
        {
            if (this.entries[index] == null)
            {
                return null;
            }
            else
            {
                var node = this.entries[index].First;

                do
                {
                    if (node.Value.Key.Equals(key))
                    {
                        return node;
                    }

                } while ((node = node.Next) != null);

                return null;
            }
        }

        private int Hash(int h)
        {
            // Using Java collections hash algorithm
            h ^= (h >> 20) ^ (h >> 12);
            return h ^ (h >> 7) ^ (h >> 4);
        }

        private void Resize()
        {
            int nextSize = this.entries.Length * 2;
            var nextEntries = new LinkedList<KeyValuePair<TKey, TValue>>[nextSize];
            var oldEntries = this.entries;

            this.entries = nextEntries;

            foreach (var entry in oldEntries)
            {
                if (entry != null)
                {
                    foreach (var pair in entry)
                    {
                        this.AddPair(pair);
                    }
                }
            }
        }

        /// <summary>
        /// Adds or updates a pair to the hashtable
        /// </summary>
        /// <param name="pair"></param>
        /// <returns>if the pair is added returns true, if the pair is updated returns false</returns>
        private bool AddPair(KeyValuePair<TKey, TValue> pair)
        {
            int index = this.GetIndex(pair.Key);

            if (this.entries[index] == null)
            {
                var entry = new LinkedList<KeyValuePair<TKey, TValue>>();
                entry.AddFirst(pair);

                this.entries[index] = entry;
                return true;
            }
            else
            {
                var node = this.FindSublistNode(index, pair.Key);
                if (node == null)
                {
                    this.entries[index].AddLast(pair);
                    return true;
                }
                else
                {
                    node.Value = pair;
                    return false;
                }
            }
        }

        private int GetIndex(TKey key)
        {
            int hashed = this.Hash(key.GetHashCode());
            hashed = this.IntegerAbsoluteValue(hashed);

            int index = hashed % this.Capacity;
            return index;
        }

        private int IntegerAbsoluteValue(int x)
        {
            int y = (x >> 31);
            return (x ^ y) - y;
        }
    }
}