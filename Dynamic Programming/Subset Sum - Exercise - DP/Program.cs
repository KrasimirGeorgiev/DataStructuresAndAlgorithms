namespace Subset_Sum___Exercise___DP
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HomeworkHelpers;

    public class Program
    {
        private const int DefaultValue = -1;

        private static readonly HomeworkHelper Helper = new HomeworkHelper();

        private static void Main()
        {
            Helper.ConsoleMio.Setup();
            Helper.ConsoleMio.PrintHeading("Subset Sum Dynamic Programming");

            int[] set = { -3, 34, 4, 12, 5, 2 };

            int offset = 3;
            int[] sumsMap = CreateSumsMap(set);

            BacktrackSum(-1, sumsMap, set, offset);
        }

        private static void BacktrackSum(int sum, int[] sumsMap, int[] set, int offset)
        {
            if (sumsMap[sum + offset] <= DefaultValue)
            {
                Console.WriteLine("No subset sum of {0}", sum);
            }
            else
            {
                var backtrackList = new List<int>();
                int leftovers = sum;

                while (leftovers > 0 - offset)
                {
                    int keyValue = set[sumsMap[leftovers] - offset];
                    backtrackList.Add(keyValue);

                    leftovers -= keyValue;
                }

                Console.WriteLine("{0} = {1}", sum, string.Join(" + ", backtrackList));
            }
        }

        private static int[] CreateSumsMap(int[] set)
        {
            int[] sums = Enumerable.Repeat(DefaultValue, set.Sum() + 1).ToArray();
            sums[0] = DefaultValue - 1;

            int currentSum = 0;
            for (int i = 0; i < set.Length; i++)
            {
                for (int j = currentSum; j + 1 > 0; j--)
                {
                    if (sums[j] != DefaultValue
                        && sums[j + set[i]] == DefaultValue)
                    {
                        sums[j + set[i]] = i;
                    }
                }

                currentSum += set[i];
            }

            return sums;
        }

        /// <summary>
        /// For Sets containing negative numbers
        /// </summary>
        /// <param name="set"></param>
        /// <param name="offset">the offset with wich to read the results</param>
        /// <returns></returns>
        private static int[] CreateSumMapWithOffset(int[] set, out int offset)
        {
            int negativeWeight = 0,
                positiveWeight = 0,
                notSet = -1;

            for (int i = 0; i < set.Length; i++)
            {
                if (set[i] >= 0)
                {
                    positiveWeight += set[i];
                }
                else
                {
                    negativeWeight -= set[i];
                }
            }

            offset = negativeWeight + 1;

            int[] sumsMap = Enumerable.Repeat(notSet, offset * set.Length + positiveWeight + 1)
                .ToArray();

            sumsMap[0] = -2;

            int currentSum = 0;

            for (int i = 0; i < set.Length; i++)
            {
                set[i] += offset;
                for (int j = currentSum; j + 1 > 0; j--)
                {
                    if (sumsMap[j] != notSet
                        && sumsMap[j + set[i]] == notSet)
                    {
                        sumsMap[j + set[i]] = i;
                    }
                }

                currentSum += set[i];
            }

            return sumsMap;
        }
    }
}
