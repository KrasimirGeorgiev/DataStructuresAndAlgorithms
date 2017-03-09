namespace HashTables.CountWordsInText
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that counts how many times each word from given text file words.txt appears 
    /// in it. The character casing differences should be ignored. The result words should be 
    /// ordered by their number of occurrences in the text. 
    /// </summary>
    public class Program
    {
        private static HomeworkHelper helper = new HomeworkHelper();

        private static void Main(string[] args)
        {
            helper.ConsoleMio.Setup();

            helper.ConsoleMio.PrintHeading("Task 3 Count Words In Text");

            string[] allWords = Common.CommonMethods.GetInput(optionalFilePath: "words.txt");

            Dictionary<string, int> occurenceMap = new Dictionary<string, int>();

            foreach (string word in allWords)
            {
                if (!occurenceMap.ContainsKey(word.ToLower()))
                {
                    occurenceMap[word.ToLower()] = 0;
                }

                occurenceMap[word.ToLower()]++;
            }

            var sortedByOccurence = occurenceMap
                .OrderBy(x => x.Value)
                .ToList();

            int maxFormatPadding = occurenceMap.Keys.Max(k => k.Length);
            string format = "{0,-" + maxFormatPadding + "}";

            Console.WriteLine(format + " -> Count", "Word");
            foreach (var pair in sortedByOccurence)
            {
                helper.ConsoleMio.Write(format, ConsoleColor.DarkGreen, pair.Key);
                helper.ConsoleMio.Write(" -> ", ConsoleColor.DarkCyan);
                helper.ConsoleMio.WriteLine(
                    "{0} {1}",
                    ConsoleColor.DarkBlue,
                    pair.Value,
                    pair.Value == 1 ? "times" : "time");
            }
        }
    }
}
