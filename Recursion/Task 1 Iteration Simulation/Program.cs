namespace IterationSimulation
{
    using System.Linq;
    using ConsoleMio.ConsoleEnhancements;
    using static System.ConsoleColor;

    /// <summary>
    /// Write a recursive program that simulates the execution of n nested loopsfrom 1 to n.
    /// </summary>
    public class Program
    {
        private static readonly ConsoleMio Console = new ConsoleMio();
        private static int n;
        private static int[] collection;

        private static void Main()
        {
            Console.Setup();

            Console.PrintHeading("Task 1 Iteration Simulation ");

            Console.Write("Enter n: ", DarkCyan);
            n = int.Parse(Console.ReadLine(Gray));
            collection = new int[n];

            var iterator = new RecursionIterator(n);
            iterator.Iterate(IterationAction);
        }

        private static void IterationAction(int i)
        {
            if (RecursionIterator.IteratorsInstances.Count < n)
            {
                var nestedIterator = new RecursionIterator(n);
                nestedIterator.Iterate(IterationAction);
            }
            else
            {
                Console.WriteLine(string.Join(
                    "", RecursionIterator.IteratorsInstances.Select(r => r.CurrentPosition)), DarkMagenta);
            }
        }
    }
}
