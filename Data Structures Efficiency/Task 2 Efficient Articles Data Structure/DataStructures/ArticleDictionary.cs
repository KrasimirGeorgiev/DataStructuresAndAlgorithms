namespace ArticlesDataStructureImplementation.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Company;
    using Wintellect.PowerCollections;

    public class ArticleDictionary : ICollection<Article>
    {
        private readonly OrderedMultiDictionary<decimal, Article> data;

        public ArticleDictionary(bool readOnly = false)
            : this(default(Comparison<decimal>), readOnly)
        {
        }

        public ArticleDictionary(Comparison<decimal> priceComparison, bool readOnly)
        {
            this.data = new OrderedMultiDictionary<decimal, Article>(
                allowDuplicateValues: true, keyComparison: priceComparison);

            this.IsReadOnly = readOnly;
        }

        public int Count
        {
            get { return this.data.Count; }
        }

        public bool IsReadOnly { get; }

        public IEnumerator<Article> GetEnumerator()
        {
            foreach (var pair in data)
            {
                foreach (var article in pair.Value)
                {
                    yield return article;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Article item)
        {
            this.data.Add(item.Price, item);
        }

        public void Clear()
        {
            this.data.Clear();
        }

        public bool Contains(Article item)
        {
            if (this.data.ContainsKey(item.Price))
            {
                var collection = this.data[item.Price];
                return collection.Contains(item);
            }
            else
            {
                return false;
            }
        }

        public bool Contains(decimal price)
        {
            return this.data.ContainsKey(price);
        }

        public void CopyTo(Article[] array, int arrayIndex)
        {
            if (this.Count + arrayIndex - 1 > array.Length)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(arrayIndex), "The total length will exceede the bounds of the array");
            }
            else
            {
                foreach (var article in this)
                {
                    array[arrayIndex] = article;
                    arrayIndex++;
                }
            }
        }

        public bool Remove(Article item)
        {
            if (this.data.ContainsKey(item.Price))
            {
                var collection = this.data[item.Price];
                return collection.Remove(item);
            }
            else
            {
                return false;
            }
        }

        public ICollection<Article> this[decimal key]
        {
            get
            {
                if (this.data.ContainsKey(key))
                {
                    return this.data[key];
                }
                else
                {
                    throw new KeyNotFoundException(
                        "The dictionary doesnt contain this key, checck with Contains first");
                }
            }
        }

        public Article[] GetRange(decimal from, decimal to, bool isInclusive = true)
        {
            var range = this.data.Range(from, isInclusive, to, isInclusive);

            return this.CreateArrayFromRange(range);
        }

        public Article[] GetRangeFrom(decimal from, bool isInclusive)
        {
            var range = this.data.RangeFrom(from, isInclusive);

            return this.CreateArrayFromRange(range);
        }

        public Article[] GetRangeTo(decimal to, bool isInclusive)
        {
            var range = this.data.RangeTo(to, isInclusive);

            return this.CreateArrayFromRange(range);
        }

        private Article[] CreateArrayFromRange(OrderedMultiDictionary<decimal, Article>.View range)
        {
            var result = new Article[range.Count];

            int i = 0;
            foreach (var pair in range)
            {
                pair.Value.CopyTo(result, i);
                i = pair.Value.Count;
            }

            return result;
        }
    }
}
