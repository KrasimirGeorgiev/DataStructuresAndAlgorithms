namespace T1_Knapsack_Problem
{
    public class Product
    {
        public string Name { get; set; }

        public int Weight { get; set; }

        public int Cost { get; set; }

        public override string ToString()
        {
            return $"{this.Name}, cost: {this.Cost}, weight: {this.Weight}";
        }
    }
}