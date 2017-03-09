namespace T1_Knapsack_Problem
{
    using System.Collections.Generic;

    public class BagOfProducts
    {
        private readonly Stack<Product> products;

        private readonly int bagCapacity;

        private int bagWeigth;

        private int bagCost;

        public BagOfProducts(int bagWeightCapacity)
        {
            this.products = new Stack<Product>();
            this.bagWeigth = 0;
            this.bagCost = 0;
            this.bagCapacity = bagWeightCapacity;
        }

        public int BagWeigth
        {
            get { return this.bagWeigth; }
        }

        public int BagCost
        {
            get { return this.bagCost; }
        }

        public int BagWeigthCapacity
        {
            get { return this.bagCapacity; }
        }

        public bool Add(Product item)
        {
            if (!this.CanAddItem(item))
            {
                return false;
            }

            this.bagWeigth += item.Weight;
            this.bagCost += item.Cost;
            this.products.Push(item);

            return true;
        }

        public Product RemoveLastProduct()
        {
            var item = this.products.Pop();
            this.bagWeigth -= item.Weight;
            this.bagCost -= item.Weight;

            return item;
        }

        private bool CanAddItem(Product item)
        {
            return this.bagWeigth + item.Weight <= this.bagCapacity;
        }
    }
}