namespace T7HowManyOccurrencesForEach
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ConsoleMio;
    using static System.ConsoleColor;

    /// <summary>
    /// Write a program that finds in given array of integers (all belonging to the range [0..1000]) 
    /// how many times each of them occurs.
    /// </summary>
    public class Program
    {
        private const int MAGIC_CONSTANT = 10000;

        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        private static void Main()
        {
            Helper.ConsoleMio.Setup();

            Helper.ConsoleMio.PrintHeading("Task 7 Count Number Occurrences");

            int[] collection = Helper.GetCollectionFromUserChoice(0, 10000, 0, 1000).ToArray();

            Helper.ConsoleMio.WriteLine("Results: ", DarkGreen);
            if (collection.Length < MAGIC_CONSTANT)
            {
                SmallCollectionHandler(collection);
            }
            else
            {
                LargeCollectionHandler(collection);
            }

            Console.WriteLine("Completed...");
        }

        private static void LargeCollectionHandler(int[] collection)
        {
            int[] myIndexIsOccurrenceCount = new int[1001];
            foreach (int number in collection)
            {
                myIndexIsOccurrenceCount[number]++;
            }

            for (int i = 1; i < myIndexIsOccurrenceCount.Length; i++)
            {
                if (myIndexIsOccurrenceCount[i] > 0)
                {
                    var pluralForm = myIndexIsOccurrenceCount[i] == 1 ? string.Empty : "s";

                    Helper.ConsoleMio.WriteLine(
                        $"{i} → {myIndexIsOccurrenceCount[i]} time{pluralForm}",
                        DarkBlue);
                }

                if (i % 100 == 0)
                {
                    Console.WriteLine("Press a key to continue printing");
                    Console.ReadKey(true);
                }
            }
        }

        private static void SmallCollectionHandler(int[] collection)
        {
            Dictionary<int, int> occurrencesMap =
                T6RemoveOddOccurrenceElements.Program.MapByElementOccurrence(collection);

            int i = 1;
            foreach (int key in occurrencesMap.Keys)
            {
                var pluralForm = occurrencesMap[key] == 1 ? string.Empty : "s";

                Helper.ConsoleMio.WriteLine(
                    $"{key} → {occurrencesMap[key]} time{pluralForm}",
                    DarkBlue);

                if (i % 10 == 0)
                {
                    Console.WriteLine("Press a key to continue printing");
                    Console.ReadKey(true);
                }

                i++;
            }
        }
    }
}
