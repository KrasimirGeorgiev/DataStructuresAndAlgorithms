namespace OnlineMarket___Combined
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    public class Program
    {
        private static void Main()
        {
            var shop = new Shop();
            CommandParser.Shop = shop;

            string command;
            while ((command = Console.ReadLine()) != "end")
            {
                CommandParser.ParseCommand(command);
            }

            Console.WriteLine();
        }
    }

    public class CommandParser
    {
        private static char[] splitChars = { ' ' };

        public static Shop Shop { get; set; }

        public static void ParseCommand(string command)
        {
            if (command.StartsWith("add"))
            {
                string[] tokens = command.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine(
                    Shop.AddUnit(tokens[1], tokens[2], tokens[3])
                        ? "SUCCESS: {0} added!"
                        : "FAIL: {0} already exists!", tokens[1]);
            }
            else if (command.StartsWith("remove"))
            {
                string unitName = command.Replace("remove", string.Empty).Trim();
                if (Shop.RemoveUnit(unitName))
                {
                    Console.WriteLine("SUCCESS: {0} removed!", unitName);
                }
                else
                {
                    Console.WriteLine("FAIL: {0} could not be found!", unitName);
                }
            }
            else if (command.StartsWith("find"))
            {
                string type = command.Replace("find", string.Empty).Trim();
                var products = Shop.FilterByType(type);

                Console.WriteLine(
                    "RESULT: {0}", products != null
                    ? string.Join(", ", products)
                    : string.Empty);
            }
            else if (command.StartsWith("power"))
            {
                int count = int.Parse(command.Replace("power", string.Empty).Trim());

                var units = Shop.GetTop(count);

                Console.WriteLine("RESULT: {0}", string.Join(", ", units));
            }
        }
    }

    public class Unit
    {
        //public const int NameMinLengh = 1;

        //public const int NameMaxLenght = 30;

        //public const int TypeMinLenght = 1;

        //public const int TypeMaxLenght = 40;

        //public const int MinimumAttack = 100;

        //public const int MaximumAttack = 1000;

        private string name;

        private string type;

        private int attack;

        public string Name
        {
            get
            {
                return this.name;
            }

            set { this.name = value; }
        }

        public string Type
        {
            get
            {
                return this.type;
            }

            set { this.type = value; }
        }

        public int Attack
        {
            get
            {
                return this.attack;
            }

            set { this.attack = value; }
        }

        public override string ToString()
        {
            return String.Format("{0}[{1}]({2})", this.Name, this.Type, this.Attack);
        }

        public static int CompareByAttack(Unit a, Unit b)
        {
            if (a.Attack > b.Attack)
            {
                return -1;
            }
            else if (a.Attack < b.Attack)
            {
                return 1;
            }
            else
            {
                return a.Name.CompareTo(b.Name);
            }
        }
    }

    public class Shop
    {
        private readonly Dictionary<string, Unit> productsMapByName;
        private readonly OrderedBag<Unit> productsByAttack;
        private readonly Dictionary<string, IList<Unit>> mapByType;

        public Shop()
        {
            this.productsMapByName = new Dictionary<string, Unit>();
            this.productsByAttack = new OrderedBag<Unit>(Unit.CompareByAttack);
            this.mapByType = new Dictionary<string, IList<Unit>>();
        }

        private int Comparison(Unit a, Unit b)
        {
            return b.Attack - a.Attack;
        }

        public bool AddUnit(string unitName, string type, string attack)
        {
            if (this.productsMapByName.ContainsKey(unitName))
            {
                return false;
            }

            var unit = new Unit
            {
                Name = unitName,
                Attack = int.Parse(attack),
                Type = type
            };

            if (!this.mapByType.ContainsKey(unit.Type))
            {
                this.mapByType[type] = new List<Unit>();
            }

            this.productsMapByName.Add(unit.Name, unit);
            this.mapByType[unit.Type].Add(unit);
            this.productsByAttack.Add(unit);

            return true;
        }

        public bool RemoveUnit(string name)
        {
            if (this.productsMapByName.ContainsKey(name))
            {
                var product = this.productsMapByName[name];

                this.productsMapByName.Remove(name);
                this.mapByType[product.Type].Remove(product);
                this.productsByAttack.Remove(product);

                return true;
            }

            return false;
        }

        public IEnumerable<Unit> FilterByType(string type, int count = 10)
        {
            if (!this.mapByType.ContainsKey(type))
            {
                return new Unit[0];
            }

            return this.ApplyOrdering(this.mapByType[type], count);
        }

        public IEnumerable<Unit> GetTop(int count)
        {
            var bottom = new Unit
            {
                Attack = 1000,
                Type = "nqma",
                Name = "test"
            };

            var range = this.productsByAttack.RangeFrom(bottom, fromInclusive: true);

            return this.ApplyOrdering(range.Take(count), count);
        }

        public IEnumerable<Unit> ApplyOrdering(IEnumerable<Unit> products, int takeCount)
        {
            return products
                .OrderByDescending(p => p.Attack)
                .ThenBy(p => p.Name)
                .Take(takeCount);
        }
    }

    public class Validator
    {
        public static string ValidateString(string str, int minLength, int maxLength)
        {
            if (minLength <= str.Length && str.Length <= maxLength)
            {
                return str;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}