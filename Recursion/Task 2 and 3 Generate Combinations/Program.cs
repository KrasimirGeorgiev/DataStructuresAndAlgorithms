namespace CombinationGeneration
{
    using ConsoleMio.ConsoleEnhancements;
    using ProblemSolvers;

    /// <summary>
    /// Task 2: Write a recursive program for generating and printing all the combinations with duplicates
    /// of k elements from n-element
    /// 
    /// Tas 3 Modify the previous program to skip duplicates
    /// </summary>
    public class Program
    {
        private static readonly ConsoleMio Console = new ConsoleMio();

        private static void Main()
        {
            Console.Setup();
            Console.PrintHeading("Task 2 Generate All Combinations With Duplicates");

            int[] collection = { 1, 2, 3, 4, 5 };
            int combinationSetsLength = 2;
            CombinatoricsGen<int> combo =
                new CombinatoricsesWithDuplicates<int>(combinationSetsLength, collection);

            var resultPrinter = new ResultPrinter(Console);

            resultPrinter.DispalyResults(collection, combinationSetsLength, combo);

            Console.PrintHeading("Task 3 Generate All Regular Combinations");

            combo = new CombinatoricsesRegular<int>(combinationSetsLength, collection);

            resultPrinter.DispalyResults(collection, combinationSetsLength, combo);
        }
    }
}
