namespace T1_Knapsack_Problem___Dynamic
{
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;
    using T1_Knapsack_Problem;

    public class DynamicProgram
    {
        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        private static void Main()
        {
            Helper.ConsoleMio.Setup();
            Helper.ConsoleMio.PrintHeading("Knapsack Problem");

            IList<Product> products = Program.GetTestProducts()
                .OrderByDescending(p => p.Cost)
                .ToList();

                
        }
    }
}
