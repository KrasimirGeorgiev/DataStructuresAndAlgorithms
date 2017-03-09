namespace ArticlesDataStructureImplementation.Company
{
    using System;

    public class Article : IEquatable<Article>
    {
        public int Barcode { get; set; }

        public string Vendor { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public bool Equals(Article other)
        {
            return this.Barcode == other.Barcode;
        }
    }
}