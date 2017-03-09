namespace OnlineMarket
{
    using System;

    public class Product : IEquatable<Product>, IComparable<Product>
    {
        public const int StringMinLenght = 3;

        public const int StringMaxLenght = 20;

        public const decimal MinimmPrice = 0;

        public const decimal MaximumPrice = 5000;

        private string name;

        private string type;

        private decimal price;

        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = Validator.ValidateString(
                value, StringMinLenght, StringMaxLenght);
            }
        }

        public string Type
        {
            get
            {
                return this.type;
            }

            set
            {
                this.type = Validator.ValidateString(
                    value, StringMinLenght, StringMaxLenght);
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }

            set
            {
                if (MinimmPrice <= value && value <= MaximumPrice)
                {
                    this.price = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException(nameof(this.Price));
                }
            }
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public int CompareTo(Product other)
        {
            return (int)(this.Price - other.Price);
        }

        public override string ToString()
        {
            return String.Format("{0}({1:G29})", this.Name, this.Price);
        }

        public bool Equals(Product other)
        {
            return this.Name == other.Name;
        }

        public static int CompareByPrice(Product a, Product b)
        {
            if (a.Price > b.Price)
            {
                return 1;
            }
            else if (a.Price < b.Price)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}