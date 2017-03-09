namespace OnlineMarket
{
    using System;
    using System.Collections.Generic;

    public class CommandParser
    {
        public static Shop Shop { get; set; }

        public static void ParseCommand(string command)
        {
            if (command.StartsWith("add"))
            {
                string[] tokens = command.Split(' ');

                Console.WriteLine(
                    Shop.AddProduct(tokens[1], tokens[2], tokens[3])
                        ? "Ok: Product {0} added successfully"
                        : "Error: Product {0} already exists", tokens[1]);
            }
            else if (command.StartsWith("filter by type"))
            {
                string type = command.Replace("filter by type ", string.Empty);
                var products = Shop.FilterByType(type);
                if (products == null)
                {
                    Console.WriteLine("Error: Type {0} does not exists", type);
                }
                else
                {
                    Console.WriteLine("Ok: {0}", string.Join(", ", products));
                }
            }
            else if (command.StartsWith("filter by price"))
            {
                string[] tokens = command.Replace("filter by price ", string.Empty)
                    .Split(' ');

                IEnumerable<Product> products;

                if (tokens.Length == 4)
                {
                    products = Shop.FilterByPrice(
                        decimal.Parse(tokens[1]), decimal.Parse(tokens[3]));
                }
                else
                {
                    if (tokens[0] == "from")
                    {
                        products = Shop.FilterByPrice(minPrice: decimal.Parse(tokens[1]));
                    }
                    else
                    {
                        products = Shop.FilterByPrice(maxPrice: decimal.Parse(tokens[1]));
                    }
                }

                Console.WriteLine("Ok: {0}", string.Join(", ", products));
            }
        }
    }
}