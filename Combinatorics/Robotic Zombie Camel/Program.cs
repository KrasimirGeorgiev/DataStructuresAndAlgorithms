namespace Robotic_Zombie_Camel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        private static ulong totalLength = 0;
        private static int subSize;
        private static char[] splitChars = { ' ' };

        private static void Main()
        {
            int totalObelisk = int.Parse(Console.ReadLine());
            List<ulong> distances = new List<ulong>(totalObelisk);

            int counter = 0;
            while (counter < totalObelisk)
            {
                string line = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(line))
                {
                    ulong[] currentOb = line
                        .Split(splitChars, StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => (ulong)Math.Abs(long.Parse(x)))
                        .ToArray();

                    ulong distance = currentOb[0] + currentOb[1];
                    distances.Add(distance);

                    counter++;
                }
            }

            ulong power = ((ulong)1 << (totalObelisk - 1));

            for (int i = 0; i < totalObelisk; i++)
            {
                totalLength += distances[i] * power;
            }

            Console.WriteLine(totalLength);

            return;

            for (int i = 1; i <= totalObelisk; i++)
            {
                subSize = i;
                CalculateSubdistance(distances, 0, 0, 0);
            }

            Console.WriteLine(totalLength);
        }

        private static void CalculateSubdistance(List<ulong> distances, int currentSub, int index, ulong sum)
        {
            if (currentSub >= subSize)
            {
                totalLength += sum;
                return;
            }

            for (int i = index; i < distances.Count; i++)
            {
                sum += distances[i];
                CalculateSubdistance(distances, currentSub + 1, i + 1, sum);
                sum -= distances[i];
            }
        }
    }
}