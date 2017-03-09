namespace T4LongestSubsequenceOfEqual
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using ConsoleMio;
    using static System.ConsoleColor;

    /// <summary>
    /// Write a method that finds the longest subsequence of equal numbers in given List and returns the 
    /// result as new List{int}.
    ///     Write a program to test whether the method works correctly.
    /// </summary>
    public class Program
    {
        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        private static void Main()
        {
            Helper.ConsoleMio.Setup();

            Helper.ConsoleMio.PrintHeading("Task 4 Longest Subsequence of Equal Elements");

            ConsoleKey choice;
            do
            {
                Console.Write("Run the test or use user input(Y/N): ");
                choice = Console.ReadKey(true).Key;
                Console.WriteLine();
            }
            while (choice != ConsoleKey.Y && choice != ConsoleKey.N);

            if (choice == ConsoleKey.N)
            {
                List<int> sequence = Helper.ReadCollection(0, null).ToList();

                List<int> subSequenceOfEqueal = GetLongestSubsequenceOfEqualElements(sequence);

                int size = subSequenceOfEqueal.Count;

                var collectionAsString = string.Join(" ", subSequenceOfEqueal);
                var pluralForm = size == 1 ? string.Empty : "s";

                Helper.ConsoleMio.WriteLine(
                    $"Result: {size} repeating element{pluralForm} \nCollection: {collectionAsString}",
                    DarkGreen);
            }
            else
            {
                bool result = TestTheThing(GetLongestSubsequenceOfEqualElements);
                if (result)
                {
                    Helper.ConsoleMio.WriteLine(
                        "Test is successfull! What did you expected...", Green);
                }
            }
        }

        /// <summary>
        /// Pops a warnning if some assert fails
        /// </summary>
        /// <param name="methodThatGetsTheThings">The method to be tested for correctness</param>
        /// <returns></returns>
        private static bool TestTheThing(Func<List<int>, List<int>> methodThatGetsTheThings)
        {
            List<int> theList =
                new List<int>() { 1, 1, 2, 3, 4, 4, 4, 1, 1, 1, 2, 2, 4 };

            List<int> result = methodThatGetsTheThings(theList);

            Debug.Assert(result.Count == 3, "Asserting that method extracts correct elements count");
            foreach (int number in result)
            {
                Debug.Assert(number == 4, "Assertion that the method extract the correct element");
            }

            return true;
        }

        private static List<T> GetLongestSubsequenceOfEqualElements<T>(List<T> sequence)
            where T : IEquatable<T>
        {
            int endIndex = 0,
                currentLength = 1,
                maxLength = 1;

            for (int i = 1; i < sequence.Count; i++)
            {
                T previous = sequence[i - 1];

                if (sequence[i].Equals(previous))
                {
                    currentLength++;

                    if (currentLength > maxLength)
                    {
                        maxLength = currentLength;
                        endIndex = i;
                    }
                }
                else
                {
                    currentLength = 1;
                }
            }

            int elementsToSkip = endIndex - maxLength + 1;

            return sequence
                .Skip(elementsToSkip)
                .Take(maxLength)
                .ToList();
        }
    }
}
