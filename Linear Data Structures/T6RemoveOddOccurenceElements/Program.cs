namespace T6RemoveOddOccurrenceElements
{
    using System;
    using System.Collections.Generic;
    using ConsoleMio;
    using static System.ConsoleColor;

    /// <summary>
    /// Write a program that removes from given sequence all numbers that occur odd number of times.
    ///     {4, 2, 2, 5, 2, 3, 2, 3, 1, 5, 2} → {5, 3, 3, 5}
    /// </summary>
    public class Program
    {
        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        public static Dictionary<T, int> MapByElementOccurrence<T>(IEnumerable<T> collection)
            where T : IEquatable<T>
        {
            Dictionary<T, int> result = new Dictionary<T, int>();

            foreach (T element in collection)
            {
                if (!result.ContainsKey(element))
                {
                    result[element] = 0;
                }

                result[element]++;
            }

            return result;
        }

        private static void Main()
        {
            Helper.ConsoleMio.Setup();

            Helper.ConsoleMio.PrintHeading("Task 6 Remove Elements That Occur Odd Number Of Times ");

            ICollection<int> collection = Helper.ReadCollection(0, new[] { ' ', ',' });

            Dictionary<int, int> occurrencesMap = MapByElementOccurrence(collection);

            var filteredCollection = new List<int>();

            foreach (int number in collection)
            {
                if (occurrencesMap[number] % 2 == 0)
                {
                    filteredCollection.Add(number);
                }
            }

            Helper.ConsoleMio.WriteLine(
                $"Result: {string.Join(" ", filteredCollection)}", DarkGreen);
        }
    }
}
