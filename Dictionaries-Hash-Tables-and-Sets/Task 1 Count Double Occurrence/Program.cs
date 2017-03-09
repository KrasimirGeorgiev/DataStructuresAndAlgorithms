namespace HashTables.CountDoubleOccurrence
{
    using System;
    using System.Collections.Generic;

    using Common;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that counts in a given array of double values the number of occurrences of 
    /// each value. Use Dictionary<TKey,TValue>.
    /// </summary>
    public class Program
    {
        private static HomeworkHelper helper = new HomeworkHelper();

        public static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("Task 1 Count Double Occurrence");

            ICollection<double> collection = helper.ReadCollection(template: 0.1, splitChars: null);

            Dictionary<double, int> doubleOccurenceMap =
                CommonMethods.CreateOccurrenceMap(collection);

            Console.WriteLine("{0,-10} -> value", "key");
            foreach (double key in doubleOccurenceMap.Keys)
            {
                helper.ConsoleMio.Write("{0,-10:F4} -> ", ConsoleColor.DarkGreen, key);
                helper.ConsoleMio.WriteLine(
                    "{0} {1}",
                    ConsoleColor.DarkCyan,
                    doubleOccurenceMap[key],
                    doubleOccurenceMap[key] == 1 ? "time" : "times");
            }
        }
    }
}
