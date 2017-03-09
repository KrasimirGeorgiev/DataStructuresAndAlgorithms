namespace PermutationsGeneration
{
    using ConsoleMio.ConsoleEnhancements;
    using ProblemSolvers;

    /// <summary>
    /// Write a recursive program for generating and printing all permutations of the numbers 1, 2, ..., 
    /// n for given integer number n.
    /// </summary>
    public class Program
    {
        private static readonly ConsoleMio Console = new ConsoleMio();

        private static void Main()
        {
            Console.Setup();

            Console.PrintHeading("Task 4 Permutations of numbers");

            ResultPrinter printer = new ResultPrinter(Console);

            int[] collection = { 1, 2, 3, 4 };

            int subsetSize = 4;

            CombinatoricsGen<int> combo = new PermutationsGenerator<int>(subsetSize, collection);

            printer.DispalyResults(collection, collection.Length, combo);
        }
    }
}
