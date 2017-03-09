namespace T10ShortestSequenceOfOperations
{
    using ConsoleMio.ConsoleEnhancements;
    using T12AdtAutosizeStack;
    using static System.ConsoleColor;

    /// <summary>
    /// We are given numbers N and M and the following operations:
    ///     N = N+1
    ///     N = N+2
    ///     N = N*2
    /// 
    /// Write a program that finds the shortest sequence of operations from the list above that starts 
    /// from N and finishes in M.
    ///     Example: N = 5, M = 16
    ///     Sequence: 5 → 7 → 8 → 16
    /// 
    /// Hint: use a queue.
    /// </summary>
    public class Program
    {
        private static readonly ConsoleMio Mio = new ConsoleMio();

        private static void Main(string[] args)
        {
            Mio.Setup();

            Mio.PrintHeading("Task 10 Shortest Sequence Of Operations From N to M");

            Mio.Write("Enter number N = ", Black);
            int n = int.Parse(Mio.ReadLine(Red));

            Mio.Write("Enter number M = ", Black);
            int m = int.Parse(Mio.ReadLine(Red));

            // Using the stack from Task 12
            Stack<int> steps = new Stack<int>();
            steps.StackUp(m);

            int current = m;
            while (current > n)
            {
                if (current % 2 == 0 && current / 2 >= n)
                {
                    current /= 2;
                    steps.StackUp(current);
                }
                else if (current - 2 >= n)
                {
                    current -= 2;
                    steps.StackUp(current);
                }
                else if (current - 1 >= n)
                {
                    current -= 1;
                    steps.StackUp(current);
                }
            }

            Mio
                .Write("Result: ", DarkGreen)
                .WriteLine(string.Join(" → ", steps), DarkBlue);
        }
    }
}
