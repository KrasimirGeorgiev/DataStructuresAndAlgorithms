namespace HashTables.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    using HomeworkHelpers;

    public class CommonMethods
    {
        private static readonly ConsoleHelper ConsoleMio = new ConsoleHelper();

        /// <summary>
        /// Internals: 
        /// The .NET Framework's base class library implements the ContainsKey method on the
        /// Dictionary type.The Dictionary has a private FindEntry method which loops over the entries
        /// in the Dictionary that are pointed to by the buckets array.
        /// The ContainsKey method discards some of the values it finds and simply returns a Boolean
        /// Therefore:
        /// Using TryGetValue can be used to perform these operations at one time, improving 
        /// runtime speed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static Dictionary<T, int> CreateOccurrenceMap<T>(IEnumerable<T> collection)
            where T : IEquatable<T>
        {
            var map = new Dictionary<T, int>();
            foreach (T element in collection)
            {
                int count;
                if (map.TryGetValue(element, out count))
                {
                    map[element]++;
                }
                else
                {
                    map[element] = 1;
                }

                // Another way (slightly slower)
                // if (!map.ContainsKey(element))
                // {
                //     map[element] = 0;
                // }

                // map[element]++;
            }

            return map;
        }

        public static string[] GetInput(
            string regexSplitPattern = "[^a-zA-Z]",
            string optionalFilePath = "test.txt")
        {
            bool canTest = false;
            string message;

            if (File.Exists(optionalFilePath))
            {
                canTest = true;
                message = "Type 'test' to load the test file or any other text - to be" +
                    " splitted to strings: ";
            }
            else
            {
                message = "Enter some text that will be splitted to strings";
            }

            ConsoleMio.Write(message, ConsoleColor.DarkRed);

            string userInput = Console.ReadLine();

            string[] stringSequence;
            if (canTest && userInput.Equals("test", StringComparison.OrdinalIgnoreCase))
            {
                stringSequence = File.ReadAllLines(optionalFilePath)
                    .SelectMany(line =>
                        Regex.Split(line, regexSplitPattern)
                            .Where(split => split != String.Empty))
                    .ToArray();
            }
            else
            {
                stringSequence = Regex
                    .Split(userInput, regexSplitPattern)
                    .Where(split => split != String.Empty)
                    .ToArray();
            }

            return stringSequence;
        }
    }
}
