namespace TopProductsInPriceRange
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json.Linq;

    public class ProductsGenerator
    {
        public const decimal MIN_PRICE = 0.99M;

        public const decimal MAX_PRICE = 9999.99M;

        private const string COMPANIES = "./MockData/companies.json";

        private const string DRUGS = "./MockData/drugs.json";

        private static readonly Random Rnd = new Random();

        private static JArray companies;

        private static JArray drugs;

        public static void ReadDataFromJson()
        {
            companies = JArray.Parse(File.ReadAllText(COMPANIES));
            drugs = JArray.Parse(File.ReadAllText(DRUGS));
        }

        public static List<Product> GenerateProducts(int count)
        {
            var products = new List<Product>();

            for (int i = 0; i < count; i++)
            {
                var current = new Product
                {
                    Company = GetCompany(),
                    Drug = GetDrug(),
                    Price = GetPrice(MIN_PRICE, MAX_PRICE)
                };

                products.Add(current);
            }

            return products;
        }

        public static decimal GetPrice(decimal min, decimal max)
        {
            decimal next = ((decimal)Rnd.NextDouble() * (max - min)) + min;
            return Math.Round(next * 100) / 100;
        }

        private static string GetDrug()
        {
            var element = drugs[Rnd.Next(0, drugs.Count)];
            string drug = (string)element["drug"];
            return drug;
        }

        private static string GetCompany()
        {
            var element = companies[Rnd.Next(0, companies.Count)];
            string company = (string)element["company"];
            return company;
        }
    }
}