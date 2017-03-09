namespace T1_Knapsack_Problem
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program based on dynamic programming to solve the "Knapsack Problem": 
    /// you are given N products, each has weight Wi and costs Ci and a knapsack of 
    /// capacity M and you want to put inside a subset of the products with highest
    /// cost and weight ≤ M. The numbers N, M, Wi and Ci are integers in the range [1..500].
    /// </summary>
    public class Program
    {
        private const int MaxCapacity = 10;

        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        public static IList<Product> GetTestProducts()
        {
            List<Product> products = new List<Product>()
            {
                new Product
                {
                    Name = "Beer",
                    Weight = 3,
                    Cost = 2
                },
                new Product
                {
                    Name = "Vodka",
                    Weight = 8,
                    Cost = 12
                },
                new Product
                {
                    Name = "Cheese",
                    Weight = 4,
                    Cost = 5
                },
                new Product
                {
                    Name = "Nuts",
                    Weight = 1,
                    Cost = 4
                },
                new Product
                {
                    Name = "Whiskey",
                    Weight = 8,
                    Cost = 13
                },
                new Product
                {
                    Name = "EqualCostItemsShouldBeOrderedByWeight",
                    Weight = 4,
                    Cost = 2
                }
            };

            return products;
        }

        private static void Main()
        {
            Helper.ConsoleMio.Setup();
            Helper.ConsoleMio.PrintHeading("Knapsack Problem");

            // Order Products by Cost/Weight ratio
            // Start from the best ratio and move to the worst adding products to the bag

            IList<Product> products = GetTestProducts();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var productsMappedByCost = products
                .OrderByDescending(p => (double)p.Cost / p.Weight)
                .ThenBy(p => p.Weight)
                .GroupBy(p => (double)p.Cost / p.Weight);

            var myBag = new BagOfProducts(MaxCapacity);

            FillMaBag(myBag, productsMappedByCost);
            stopwatch.Stop();

            Helper.ConsoleMio.Write("Elapsed: ", ConsoleColor.DarkBlue)
                .WriteLine(stopwatch.Elapsed.ToString(), ConsoleColor.DarkCyan);

            PrintBagDetails(myBag);
        }

        private static void PrintBagDetails(BagOfProducts bagOfProducts)
        {
            Helper.ConsoleMio.Write("Bag weight: ", ConsoleColor.DarkBlue)
                .WriteLine(bagOfProducts.BagWeigth.ToString(), ConsoleColor.Blue)
                .Write("Products cost: ", ConsoleColor.DarkBlue)
                .WriteLine(bagOfProducts.BagCost.ToString(), ConsoleColor.Blue);

            while (bagOfProducts.BagWeigth > 0)
            {
                Helper.ConsoleMio.WriteLine(
                    bagOfProducts.RemoveLastProduct().Name, ConsoleColor.Blue);
            }
        }

        private static void FillMaBag(
            BagOfProducts bag, IEnumerable<IGrouping<double, Product>> productsMap)
        {
            foreach (var group in productsMap)
            {
                foreach (var product in group)
                {
                    bag.Add(product);
                    if (bag.BagWeigth == bag.BagWeigthCapacity)
                    {
                        break;
                    }
                }
            }
        }
    }
}