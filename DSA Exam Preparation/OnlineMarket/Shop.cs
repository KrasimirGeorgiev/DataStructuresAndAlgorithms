namespace OnlineMarket
{
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Shop
    {
        private readonly HashSet<string> productsMapByName;
        private readonly OrderedBag<Product> productsByPrice;
        private readonly Dictionary<string, IList<Product>> mapByType;

        public Shop()
        {
            this.productsMapByName = new HashSet<string>();
            this.productsByPrice = new OrderedBag<Product>(Product.CompareByPrice);
            this.mapByType = new Dictionary<string, IList<Product>>();
        }

        public bool AddProduct(string productName, string price, string type)
        {
            if (this.productsMapByName.Contains(productName))
            {
                return false;
            }

            var product = new Product
            {
                Name = productName,
                Price = decimal.Parse(price),
                Type = type
            };

            if (!this.mapByType.ContainsKey(type))
            {
                this.mapByType[type] = new List<Product>();
            }

            this.productsMapByName.Add(product.Name);
            this.mapByType[type].Add(product);
            this.productsByPrice.Add(product);

            return true;
        }

        public IEnumerable<Product> FilterByType(string type, int count = 10)
        {
            if (!this.mapByType.ContainsKey(type))
            {
                return null;
            }

            return this.ApplyOrdering(this.mapByType[type], count);
        }

        public IEnumerable<Product> FilterByPrice(
            decimal minPrice = Product.MinimmPrice,
            decimal maxPrice = Product.MaximumPrice,
            int count = 10)
        {
            var minProduct = new Product
            {
                Name = "testProduct",
                Type = "testType",
                Price = minPrice
            };

            var maxProduct = new Product
            {
                Name = "testProduct",
                Type = "testType",
                Price = maxPrice
            };

            var result = this.productsByPrice.Range(
                minProduct, true, maxProduct, true)
                .Take(count);

            return this.ApplyOrdering(result, count);
        }

        public IEnumerable<Product> ApplyOrdering(IEnumerable<Product> products, int takeCount)
        {
            return products
                .OrderBy(p => p.Price)
                .ThenBy(p => p.Name)
                .ThenBy(p => p.Type)
                .Take(takeCount);
        }
    }
}