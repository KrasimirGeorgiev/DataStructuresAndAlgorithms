namespace T2ReverseCollectionThroughStack
{
    using System.Collections.Generic;
    using System.Linq;
    using ConsoleMio;
    using static System.ConsoleColor;

    /// <summary>
    /// Write a program that reads N integers from the console and reverses them using a stack.
    ///     Use the Stack{int} class.
    /// </summary>
    public class Program
    {
        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        private static void Main()
        {
            Helper.ConsoleMio.Setup();

            Helper.ConsoleMio.PrintHeading("Task 2 Reverse a Sequence Using a Stack");

            int[] collection = Helper.ReadCollection(0, new char[] { ' ' }).ToArray();

            Stack<int> stack = new Stack<int>(collection);

            int[] reversed = new int[collection.Length];

            for (int i = 0; i < reversed.Length; i++)
            {
                reversed[i] = stack.Pop();
            }

            Helper.ConsoleMio.WriteLine(
                $"Result: { string.Join(" ", reversed)}",
                DarkGreen);
        }
    }
}
