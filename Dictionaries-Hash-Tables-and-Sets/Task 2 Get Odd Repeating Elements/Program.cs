namespace HashTables.OddRepeatingElements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Common;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that extracts from a given sequence of strings all elements that present 
    /// in it odd number of times. 
    /// </summary>
    public class Program
    {
        private static HomeworkHelper helper = new HomeworkHelper();

        public static void Main()
        {
            helper.ConsoleMio.Setup();
            helper.ConsoleMio.PrintHeading("Task 2 Get Odd Repeating Elements ");

            string[] stringSequence = CommonMethods.GetInput(regexSplitPattern: "[^a-zA-Z0-9#]+");

            Dictionary<string, int> occurenceMap =
                CommonMethods.CreateOccurrenceMap(stringSequence);

            var filtered = occurenceMap
                .Where(pair => pair.Value % 2 == 1)
                .Select(pair => pair.Key);

            helper.ConsoleMio.Write("{{{0}}}", ConsoleColor.DarkBlue, string.Join(", ", stringSequence));
            helper.ConsoleMio.Write(" -> ", ConsoleColor.DarkCyan);
            helper.ConsoleMio.Write("{{{0}}}",
                ConsoleColor.DarkGreen,
                string.Join(", ", filtered));

            Console.WriteLine();
        }
    }
}
