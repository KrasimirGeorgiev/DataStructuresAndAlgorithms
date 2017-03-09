namespace TopProductsInPriceRange
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program to read a large collection of products (name + price) and efficiently find 
    /// the first 20 products in the price range [a…b].
    ///     Test for 500 000 products and 10 000 price searches.
    ///     Hint: you may use OrderedBag{T} and sub-ranges.
    /// 
    /// Implemented through a Sorted List{T} and a anlgorithm similar to binary search 
    /// Works very well for collections with below average count of repeating elements -> that can be fixed if 
    /// instead of ordinary list a dictionary{decimal, list{Product}} is used and products with the same 
    /// price are stored under the same key, then the binary search is done over the keys of the dictionary...
    /// </summary>
    public class Program
    {
        private static readonly HomeworkHelper helper = new HomeworkHelper();

        /// <summary>
        /// Binary search algorithms require sorted collection.
        /// Searches a collection for a <paramref name="target"/> element and returns the index of the element, 
        /// or the index of the next closest larger value available.
        /// If repeating values exist the value of <paramref name="operation"/> is used to return the 
        /// first or last index of the target element.
        /// </summary>
        /// <typeparam name="T">A type implementing the IComparable interface</typeparam>
        /// <param name="target">The searched element</param>
        /// <param name="source">The source collection - should be sorted</param>
        /// <param name="left">left boundry -> the bottom of the range</param>
        /// <param name="right">right boundry -> the top of the range</param>
        /// <param name="operation">
        /// An integer with value +1 or -1 that will be used to either decrease or increase the index of an exact match
        /// so its first or last index can be detected. 
        /// </param>
        /// <returns>Always returns and index -> of the exact match if exists or the next larger value</returns>
        public static int BinarySearchForValueClosestToTarget<T>(
            T target, IList<T> source, int left, int right, int operation) where T : IComparable<T>
        {
            int pick = left;
            while (left < right)
            {
                pick = (left + right) / 2;

                // If exact match is found continue lineary because there may be equal values
                // the direction to continue is dictated by the operation variable (-1 or +1)
                if (source[pick].CompareTo(target) == 0)
                {
                    while (source[pick].CompareTo(target) == 0)
                    {
                        pick += operation;
                        if (pick < 0 || source.Count <= pick)
                        {
                            break;
                        }
                    }

                    // Undoes the last operation
                    pick -= operation;

                    return pick;
                }

                if (source[pick].CompareTo(target) > 0)
                {
                    right = pick;
                }
                else
                {
                    left = pick + 1;
                }
            }

            // Continue lineary from the last tested position to find the exact index at which
            // neighbours of the first side are smaller and on the other side - larger
            if (source[pick].CompareTo(target) > 0)
            {
                while (source[pick].CompareTo(target) > 0)
                {
                    pick--;
                    if (pick < 0)
                    {
                        break;
                    }
                }

                pick += 1;
            }
            else
            {
                while (source[pick].CompareTo(target) < 0)
                {
                    pick++;
                    if (pick >= source.Count)
                    {
                        pick--;
                        break;
                    }
                }
            }

            return pick;
        }

        private static void Main()
        {
            helper.ConsoleMio.Setup();

            helper.ConsoleMio.PrintHeading("Task 2 Find Top Products In A Price Range ");

            var timer = new Stopwatch();
            helper.ConsoleMio.WriteLine("Generating data...", ConsoleColor.DarkCyan);

            timer.Start();
            ProductsGenerator.ReadDataFromJson();
            List<Product> products = ProductsGenerator.GenerateProducts(500000);
            products.Sort();
            timer.Stop();
            helper.ConsoleMio.WriteLine("Generated in: {0}", ConsoleColor.DarkGreen, timer.Elapsed);

            do
            {
                var menu = helper.ConsoleMio.CreateMenu(new List<string>());
                menu.AddItem("Test")
                    .AddItem("Manual Test");

                string selected = menu.DisplayMenu(ConsoleColor.DarkCyan, ConsoleColor.White);
                if (selected == "Test")
                {
                    timer.Restart();
                    RunTests(products, 20, 10000);
                    timer.Stop();
                    helper.ConsoleMio.Write("Job finished! Elapsed Time: ", ConsoleColor.DarkGreen)
                        .WriteLine(timer.Elapsed.ToString(), ConsoleColor.Gray);
                }
                else
                {
                    RunManualTest(products);
                    helper.ConsoleMio.WriteLine(
                        "Tests ended if no asset message poped out everithing works correct", ConsoleColor.DarkGreen);
                }

                helper.ConsoleMio.WriteLine(
                    "Da izlyaza malko? Press Esc to exit any other to repeat...", ConsoleColor.DarkCyan);
            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static void RunManualTest(List<Product> products)
        {
            helper.ConsoleMio.Write("Max Price: ", ConsoleColor.DarkGray)
                .WriteLine("{0}$", ConsoleColor.Red, products.First().Price)
                .Write("Min Price: ", ConsoleColor.DarkGray)
                .WriteLine("{0}$", ConsoleColor.Green, products.Last().Price);

            decimal top = decimal.Parse(helper.ConsoleMio.Write("Input maximum price: ", ConsoleColor.DarkBlue)
                .ReadInColor(ConsoleColor.Blue));

            decimal bottom = decimal.Parse(helper.ConsoleMio.Write("Input minimum price: ", ConsoleColor.DarkBlue)
                .ReadInColor(ConsoleColor.Blue));

            var productsInRange = ProductsInRange(products, bottom, top, 20);
            var resultsFromLinq = products
                .Where(p => bottom <= p.Price && p.Price <= top)
                .Take(20)
                .ToArray();

            Debug.Assert(
                productsInRange.Count == resultsFromLinq.Length, "The two collections should be of equal length");

            for (int i = 0; i < productsInRange.Count; i++)
            {
                Debug.Assert(
                    productsInRange[i].Price == resultsFromLinq[i].Price, "The two collections should be identical");

                PrintProduct(productsInRange[i]);
            }
        }

        private static void RunTests(List<Product> products, int countToExtract, int repetitions)
        {
            for (int i = 0; i < repetitions; i++)
            {
                decimal min = ProductsGenerator.GetPrice(ProductsGenerator.MIN_PRICE, ProductsGenerator.MAX_PRICE);
                decimal max = ProductsGenerator.GetPrice(ProductsGenerator.MIN_PRICE, ProductsGenerator.MAX_PRICE);

                if (min > max)
                {
                    decimal temp = min;
                    min = max;
                    max = temp;
                }

                ProductsInRange(products, min, max, countToExtract);
            }
        }

        private static void PrintProduct(Product product)
        {
            helper.ConsoleMio.WriteLine("Product: ", ConsoleColor.DarkGreen)
                    .Write("Company: ", ConsoleColor.Blue)
                    .WriteLine(product.Company, ConsoleColor.DarkGray)
                    .Write("Drug: ", ConsoleColor.DarkMagenta)
                    .WriteLine(product.Drug, ConsoleColor.DarkGray)
                    .Write("Price: ", ConsoleColor.DarkRed)
                    .WriteLine(
                        $"{product.Price}$",
                        product.Price < 100 ? ConsoleColor.DarkGreen : ConsoleColor.Red);
        }

        /// <summary>
        /// Creates a list <paramref name="count"/> of <see cref="Product"/> between (inclusive) a given range, 
        /// using a binary search implementation set <paramref name="reverse"/> to true to get the top 
        /// count of products for that range
        /// </summary>
        /// <param name="source">the source collection should be sorted</param>
        /// <param name="bottom">the bottom of the range</param>
        /// <param name="top">the top of the range</param>
        /// <param name="count">number of elements to try to take</param>
        /// <param name="reverse">
        /// by default false -> gets Products from bottom to top, if set to true it will get them from top to bottom
        /// </param>
        /// <returns></returns>
        private static List<Product> ProductsInRange(
           IList<Product> source, decimal bottom, decimal top, int count, bool reverse = false)
        {
            List<Product> products;
            if (reverse)
            {
                int maxProductIndex = BinarySearchForValueClosestToTarget(
                    new Product { Price = top }, source, 0, source.Count, +1);
                if (source[maxProductIndex].Price > top)
                {
                    maxProductIndex--;
                }

                products = GetProductsBelow(source, maxProductIndex, count, bottom);
            }
            else
            {
                int minProductIndex = BinarySearchForValueClosestToTarget(
                    new Product { Price = bottom }, source, 0, source.Count, -1);

                products = GetProductsAbove(source, minProductIndex, count, top);
            }

            return products;
        }

        private static List<Product> GetProductsAbove(IList<Product> products, int index, int count, decimal top)
        {
            List<Product> result = new List<Product>(count);
            for (int i = index; (i < index + count) && i < products.Count; i++)
            {
                if (products[i].Price >= top)
                {
                    break;
                }

                result.Add(products[i]);
            }

            return result;
        }

        private static List<Product> GetProductsBelow(IList<Product> products, int index, int count, decimal bottom)
        {
            List<Product> result = new List<Product>(count);
            for (int i = index; (i > index - count) && i >= 0; i--)
            {
                if (products[i].Price <= bottom)
                {
                    break;
                }

                result.Add(products[i]);
            }

            return result;
        }
    }
}
