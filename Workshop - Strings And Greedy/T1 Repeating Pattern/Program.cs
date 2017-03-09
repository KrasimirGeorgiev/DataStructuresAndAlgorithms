namespace T1_Repeating_Pattern
{
    using System;

    public class Program
    {
        private static void Main()
        {
            string theString = Console.ReadLine();
            bool containsSubPattern = false;

            for (int i = 1; i < theString.Length / 2 + 1; i++)
            {
                if (theString.Length % i != 0)
                {
                    continue;
                }

                string subStr = theString.Substring(0, i);

                if (CheckPattern(subStr, theString))
                {
                    Console.WriteLine(subStr);
                    containsSubPattern = true;
                    break;
                }
            }

            if (!containsSubPattern)
            {
                Console.WriteLine(theString);
            }
        }

        private static bool CheckPattern(string pattern, string mainString)
        {
            bool result = true;
            int start = 0;

            while (start < mainString.Length && result)
            {
                for (int i = start, j = 0; i < pattern.Length + start; i++, j++)
                {
                    if (!pattern[j].Equals(mainString[i]))
                    {
                        result = false;
                        break;
                    }
                }

                start += pattern.Length;
            }

            return result;
        }
    }
}