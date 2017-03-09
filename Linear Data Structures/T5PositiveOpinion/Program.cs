namespace T5PositiveOpinion
{
    using System.Collections.Generic;
    using System.Linq;
    using ConsoleMio;
    using static System.ConsoleColor;

    /// <summary>
    /// Write a program that removes from given sequence all negative numbers.
    /// </summary>
    public class Program
    {
        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        private static void Main()
        {
            Helper.ConsoleMio.Setup();

            Helper.ConsoleMio.PrintHeading("Task 5 Remove All Negative Numbers From a Sequence");

            List<int> collection = Helper.ReadCollection(0, null).ToList();

            List<int> positiveCollection = new List<int>(collection.Count);

            foreach (int i in collection)
            {
                if (i >= 0)
                {
                    positiveCollection.Add(i);
                }
            }

            Helper.ConsoleMio.WriteLine(
                $"Result: {string.Join(" ", positiveCollection)}", DarkGreen);
        }
    }
}
