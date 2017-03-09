namespace T2_Rabbits
{
    using System;
    using System.Linq;

    public class Program
    {
        private static void Main()
        {
            var rabbitAnwsers = Console.ReadLine()
                .Split(' ')
                .Where(str => str != "-1")
                .GroupBy(long.Parse);

            long totalRabbits = 0;
            foreach (var grouping in rabbitAnwsers)
            {
                totalRabbits += grouping.Count();
                long remainder = grouping.Count() % (grouping.Key + 1);
                if (remainder != 0)
                {
                    totalRabbits += grouping.Key - remainder + 1;
                }
            }

            Console.WriteLine(totalRabbits);
        }
    }
}