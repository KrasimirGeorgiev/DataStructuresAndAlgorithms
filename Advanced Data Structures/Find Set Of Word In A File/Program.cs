namespace Find_Set_Of_Word_In_A_File
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using HomeworkHelpers;

    /// <summary>
    /// Write a program that finds a set of words (e.g. 1000 words) in a large text (e.g. 100 MB text file).
    ///     Print how many times each word occurs in the text.
    ///     Hint: you may find a C# trie in Internet.
    /// </summary>
    public class Program
    {
        private static readonly StreamHomeworkHelper helper = new StreamHomeworkHelper();

        [STAThread]
        private static void Main()
        {
            helper.ConsoleMio.Setup();

            helper.ConsoleMio.PrintHeading("Task 3 Find Set Of Word In A File ");

            helper.ConsoleMio.WriteLine("Select a file with a set of ~1000 words", ConsoleColor.DarkMagenta);
            string pathToSetOfWords = helper.SelectFileToOpen();

            var wordsDict = CreateInitialDictionary(pathToSetOfWords);

            helper.ConsoleMio.WriteLine("Select a large file to search for strings", ConsoleColor.DarkMagenta);
            string pathToLargeFile = helper.SelectFileToOpen();

            var timer = new Stopwatch();
            timer.Start();
            ProccessLargeFile(pathToLargeFile, wordsDict);
            timer.Stop();

            helper.ConsoleMio.WriteLine("Time taken: {0}", ConsoleColor.DarkGreen, timer.Elapsed);
            PrintFindings(wordsDict);
        }

        private static void PrintFindings(Dictionary<string, int> wordsDict)
        {
            helper.ConsoleMio.Write("Searched words count: ", ConsoleColor.DarkMagenta)
                .WriteLine(wordsDict.Count.ToString(), ConsoleColor.DarkGray);

            int counter = 0;
            foreach (var word in wordsDict.Keys)
            {
                helper.ConsoleMio.Write("{0}: ", ConsoleColor.DarkBlue, word)
                    .WriteLine(wordsDict[word].ToString(), ConsoleColor.Blue);

                counter++;
                if (counter % 100 == 0)
                {
                    helper.ConsoleMio.WriteLine("...Print Pause, press a key to continue...", ConsoleColor.DarkMagenta);
                    Console.ReadKey(true);
                }
            }
        }

        private static void ProccessLargeFile(string path, Dictionary<string, int> searchedWords)
        {
            using (var textReader = File.OpenText(path))
            {
                while (!textReader.EndOfStream)
                {
                    string[] linesForProccessing = new string[128];

                    int i = 0;

                    while (!textReader.EndOfStream && i < 128)
                    {
                        linesForProccessing[i] = textReader.ReadLine();
                        i++;
                    }

                    Parallel.ForEach(
                        linesForProccessing,
                        current =>
                        {
                            if (current != null)
                            {
                                // A disadvantage is that it only counts exact matches if a user
                                // was looking for a 'car' and in the text exist the word 'nascar' it wont be matched
                                string[] words = Regex.Split(current, "\\W+");
                                foreach (var word in words)
                                {
                                    if (searchedWords.ContainsKey(word))
                                    {
                                        searchedWords[word]++;
                                    }
                                }
                            }
                        });
                }
            }
        }

        private static Dictionary<string, int> CreateInitialDictionary(string path)
        {
            string text = File.ReadAllText(path);
            Dictionary<string, int> words = Regex.Split(text, "\\W+")
                .Distinct()
                .ToDictionary(k => k, p => 0);

            return words;
        }
    }
}
