namespace T8GetTheMajorantNumber
{
    using System;
    using System.Linq;
    using ConsoleMio;
    using static System.ConsoleColor;

    /// <summary>
    /// The majorant of an array of size N is a value that occurs in it at least N/2 + 1 times.
    ///     Write a program to find the majorant of given array (if exists).
    ///     {2, 2, 3, 3, 2, 3, 4, 3, 3} → 3
    /// </summary>
    public class Program
    {
        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        private static void Main()
        {
            Helper.ConsoleMio.Setup();

            Helper.ConsoleMio.PrintHeading("Task 8 Find The Majorant In A Collection");

            int[] collection = Helper.GetCollectionFromUserChoice(0, 100, 5, 7).ToArray();

            Helper.ConsoleMio
                .WriteLine("Working with: ", DarkBlue)
                .WriteLine(string.Join(" ", collection), Black);

            int majorant;
            if (GetMajorant(collection, out majorant))
            {
                Helper.ConsoleMio.WriteLine($"The Majorant is: {majorant}", DarkGreen);
            }
            else
            {
                Helper.ConsoleMio.WriteLine("The collection doesn't have a majorant", DarkRed);
            }
        }

        private static bool GetMajorant<T>(T[] collection, out T majorant)
            where T : IEquatable<T>
        {
            var groups = collection
                .GroupBy(s => s);
            try
            {
                majorant = groups
                .First(x => x.Count() >= (collection.Length / 2) + 1)
                .Key;

                return true;
            }
            catch (InvalidOperationException)
            {
                majorant = default(T);
                return false;
            }
        }
    }
}
