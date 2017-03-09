namespace Trees___Exercise
{
    using System;

    /// <summary>
    /// Assingment http://bgcoder.com/Contests/Practice/Index/219#2
    /// </summary>
    public class Program
    {
        private static long[,,,,] memoMap = new long[11, 11, 11, 11, 5];

        private static void Main()
        {
            int lastType = 4;

            int[] candidates = new int[4];
            for (int i = 0; i < candidates.Length; i++)
            {
                memoMap[0, 0, 0, 0, i] = 1;
                candidates[i] = int.Parse(Console.ReadLine());
            }

            long result = PlaceTrees(
                candidates[0], candidates[1], candidates[2], candidates[3], lastType);

            Console.WriteLine(result);
        }

        private static long PlaceTrees(int a, int b, int c, int d, int lastType)
        {
            if (memoMap[a, b, c, d, lastType] != 0)
            {
                return memoMap[a, b, c, d, lastType];
            }

            long count = 0;

            if (a > 0 && lastType != 0)
            {
                count += PlaceTrees(a - 1, b, c, d, 0);
            }

            if (b > 0 && lastType != 1)
            {
                count += PlaceTrees(a, b - 1, c, d, 1);
            }

            if (c > 0 && lastType != 2)
            {
                count += PlaceTrees(a, b, c - 1, d, 2);
            }


            if (d > 0 && lastType != 3)
            {
                count += PlaceTrees(a, b, c, d - 1, 3);
            }

            memoMap[a, b, c, d, lastType] = count;

            return count;
        }
    }
}