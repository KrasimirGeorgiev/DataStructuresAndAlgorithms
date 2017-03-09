namespace TopProductsInPriceRange
{
    using System;

    public class Product : IComparable<Product>
    {
        public string Company { get; set; }

        public string Drug { get; set; }

        public decimal Price { get; set; }

        public int CompareTo(Product other)
        {
            int comparison = this.Price.CompareTo(other.Price);
            return comparison;
        }
    }
}