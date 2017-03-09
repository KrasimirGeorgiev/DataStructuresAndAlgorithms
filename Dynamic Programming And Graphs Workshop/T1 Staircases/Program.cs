namespace T1_Staircases
{
    using System;

    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            if (n <= 2)
            {
                Console.WriteLine(0);
            }
            else
            {
                int steps = 0;
                int nextStep = 1;

                while (nextStep <= n)
                {
                    n -= nextStep;
                    steps++;
                    nextStep++;
                }
            }
        }
    }
}
