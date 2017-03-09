namespace SubsetsofKStrings
{
    using ConsoleMio;
    using ProblemSolvers;

    /// <summary>
    /// Write a program for generating and printing all subsets of k strings from given set of strings.
    /// </summary>
    public class Program
    {
        private static readonly HomeworkHelper Console = new HomeworkHelper();

        private static void Main()
        {
            Console.ConsoleMio.Setup();

            Console.ConsoleMio.PrintHeading("Task 6 All Subsets of K Strings");

            var printr = new ResultPrinter(Console.ConsoleMio);

            string[] collection = { "test", "rock", "fun" };

            int size = 2;

            CombinatoricsGen<string> combo = new CombinatoricsesRegular<string>(size, collection);

            printr.DispalyResults(collection, 2, combo);
        }
    }
}
