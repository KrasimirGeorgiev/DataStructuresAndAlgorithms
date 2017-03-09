namespace Color_Bunnies
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Assignment: http://bgcoder.com/Contests/Practice/Index/132#1
    /// </summary>
    class Program
    {
        static void Main()
        {
            var bunnyDict = new Dictionary<int, int>();
            int totalBunnies = 0;

            int numberOfLines = int.Parse(Console.ReadLine());

            for (int i = 0; i < numberOfLines; i++)
            {
                int current = int.Parse(Console.ReadLine());

                if (bunnyDict.ContainsKey(current))
                {
                    bunnyDict[current]--;
                    totalBunnies++;

                    if (bunnyDict[current] <= 0)
                    {
                        bunnyDict.Remove(current);
                    }
                }
                else
                {
                    totalBunnies++;
                    bunnyDict[current] = current;
                }
            }

            foreach (int count in bunnyDict.Values)
            {
                totalBunnies += count;
            }

            Console.WriteLine(totalBunnies);
        }
    }
}
