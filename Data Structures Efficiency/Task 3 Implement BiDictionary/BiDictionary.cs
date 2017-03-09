namespace ImplementBiDictionary
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class BiDictionary<K1, K2, V> : IEnumerable<V>
        where K1 : IEquatable<K1> where K2 : IEquatable<K2>
    {
        private const string CANNOT_FIND_KEYS =
            "Cannot find one or both of the given keys, test with the contains methods first";

        private readonly Dictionary<K1, Dictionary<K2, V>> firstDictionary;

        private readonly Dictionary<K2, Dictionary<K1, V>> secondDictionary;

        private EqualityComparer<K1> firstKeyEqualityComparer;

        private EqualityComparer<K2> secondKeyEqualityComparer;

        public BiDictionary(
            EqualityComparer<K1> firstKeyEqualityComparer = default(EqualityComparer<K1>),
            EqualityComparer<K2> secondKEqualityComparer = default(EqualityComparer<K2>))
        {
            this.firstDictionary = new Dictionary<K1, Dictionary<K2, V>>(firstKeyEqualityComparer);
            this.secondDictionary = new Dictionary<K2, Dictionary<K1, V>>(secondKEqualityComparer);
            this.firstKeyEqualityComparer = firstKeyEqualityComparer;
            this.secondKeyEqualityComparer = secondKEqualityComparer;
        }

        public int DistinctCount
        {
            get { return this.firstDictionary.Count; }
        }

        /// <summary>
        /// Iterates only distinct elements
        /// </summary>
        /// <returns></returns>
        public IEnumerator<V> GetEnumerator()
        {
            foreach (var innerDictionary in this.firstDictionary.Values)
            {
                foreach (var value in innerDictionary.Values)
                {
                    yield return value;
                }
            }

            // Stana...
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public BiDictionary<K1, K2, V> Add(K1 firstKey, K2 secondKey, V item)
        {
            if (!this.firstDictionary.ContainsKey(firstKey))
            {
                this.firstDictionary[firstKey] = new Dictionary<K2, V>(this.secondKeyEqualityComparer);
            }

            if (!this.secondDictionary.ContainsKey(secondKey))
            {
                this.secondDictionary[secondKey] = new Dictionary<K1, V>(this.firstKeyEqualityComparer);
            }

            this.firstDictionary[firstKey].Add(secondKey, item);
            this.secondDictionary[secondKey].Add(firstKey, item);

            return this;
        }

        public bool RemoveAt(K1 firstKey, K2 secondKey)
        {
            if (!this.ContainsFirstKey(firstKey) || !this.ContainsSecondKey(secondKey))
            {
                throw new KeyNotFoundException(CANNOT_FIND_KEYS);
            }

            bool result = this.firstDictionary[firstKey].Remove(secondKey) &&
                          this.secondDictionary[secondKey].Remove(firstKey);

            if (this.firstDictionary[firstKey].Count == 0)
            {
                this.firstDictionary.Remove(firstKey);
            }

            if (this.secondDictionary[secondKey].Count == 0)
            {
                this.secondDictionary.Remove(secondKey);
            }

            return result;
        }

        public bool RemoveKeys(K1 firstKey, K2 secondKey)
        {
            if (!this.ContainsFirstKey(firstKey) || !this.ContainsSecondKey(secondKey))
            {
                throw new KeyNotFoundException(CANNOT_FIND_KEYS);
            }

            return this.firstDictionary.Remove(firstKey) &&
            this.secondDictionary.Remove(secondKey);
        }

        public void Clear()
        {
            this.firstDictionary.Clear();
            this.secondDictionary.Clear();
        }

        // used distinct method names because if k1 and k2 are of the same type 'ContainsKey' becomes ambiguous
        public bool ContainsFirstKey(K1 key)
        {
            return this.firstDictionary.ContainsKey(key);
        }

        public bool ContainsSecondKey(K2 key)
        {
            return this.secondDictionary.ContainsKey(key);
        }

        public bool ContainsCombination(K1 firstKey, K2 secondKey)
        {
            bool result = this.firstDictionary.ContainsKey(firstKey) &&
                          this.firstDictionary[firstKey].ContainsKey(secondKey);

            return result;
        }

        public V this[K1 firstKey, K2 secondKey]
        {
            get { return this.GetValueAt(firstKey, secondKey); }

            set { this.Add(firstKey, secondKey, value); }
        }

        public V GetValueAt(K1 firstKey, K2 secondKey)
        {
            if (!this.firstDictionary.ContainsKey(firstKey) ||
                    !this.secondDictionary.ContainsKey(secondKey))
            {
                throw new KeyNotFoundException(CANNOT_FIND_KEYS);
            }

            var value = this.firstDictionary[firstKey][secondKey];

            return value;
        }

        public ICollection<V> GetValuesByFirstKey(K1 key)
        {
            if (!this.firstDictionary.ContainsKey(key))
            {
                throw new KeyNotFoundException(CANNOT_FIND_KEYS);
            }

            var result = new List<V>(this.firstDictionary[key].Values);

            return result;
        }

        public ICollection<V> GetValuesBySecondKey(K2 key)
        {
            if (!this.secondDictionary.ContainsKey(key))
            {
                throw new KeyNotFoundException(CANNOT_FIND_KEYS);
            }

            var result = new List<V>(this.secondDictionary[key].Values);

            return result;
        }
    }
}
