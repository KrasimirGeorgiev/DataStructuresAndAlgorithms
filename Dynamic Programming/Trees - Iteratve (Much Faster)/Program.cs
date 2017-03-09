namespace Trees___Iteratve__Much_Faster_
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
            memoMap[0, 0, 0, 0, 0] = 1;
            memoMap[0, 0, 0, 0, 1] = 1;
            memoMap[0, 0, 0, 0, 2] = 1;
            memoMap[0, 0, 0, 0, 3] = 1;

            int a = int.Parse(Console.ReadLine()),
                b = int.Parse(Console.ReadLine()),
                c = int.Parse(Console.ReadLine()),
                d = int.Parse(Console.ReadLine());

            long result = 0;

            for (int countA = 0; countA <= a; countA++)
            {
                for (int countB = 0; countB <= b; countB++)
                {
                    for (int countC = 0; countC <= c; countC++)
                    {
                        for (int countD = 0; countD <= d; countD++)
                        {
                            if (countA + countB + countC + countD == 0)
                            {
                                continue;
                            }

                            for (int type = 0; type < 4; type++)
                            {
                                long count = 0;
                                if (type != 0 && countA > 0)
                                {
                                    count += memoMap[countA - 1, countB, countC, countD, 0];
                                }

                                if (type != 1 && countB > 0)
                                {
                                    count += memoMap[countA, countB - 1, countC, countD, 1];
                                }

                                if (type != 2 && countC > 0)
                                {
                                    count += memoMap[countA, countB, countC - 1, countD, 2];
                                }

                                if (type != 3 && countD > 0)
                                {
                                    count += memoMap[countA, countB, countC, countD - 1, 3];
                                }

                                result += count;

                                memoMap[countA, countB, countC, countD, type] = count;
                            }
                        }
                    }
                }
            }

            long answer = 0;
            if (a > 0)
            {
                answer += memoMap[a - 1, b, c, d, 0];
            }

            if (b > 0)
            {
                answer += memoMap[a, b - 1, c, d, 1];
            }

            if (c > 0)
            {
                answer += memoMap[a, b, c - 1, d, 2];
            }
            if (d > 0)
            {
                answer += memoMap[a, b, c, d - 1, 3];
            }

            Console.WriteLine(answer);
        }
    }
}