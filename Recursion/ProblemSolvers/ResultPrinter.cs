namespace ProblemSolvers
{
    using System;
    using System.Collections.Generic;
    using ConsoleMio.ConsoleEnhancements.Contracts;

    public class ResultPrinter
    {
        private readonly IConsoleWriter consoleMio;

        public ResultPrinter(IConsoleWriter writer)
        {
            this.consoleMio = writer;
        }

        public void DispalyResults<T>(T[] collection, int combinationSetsLength, CombinatoricsGen<T> combo)
        {
            this.consoleMio
                .Write("Working with: ", ConsoleColor.DarkMagenta)
                .WriteLine($"{string.Join(" ", collection)};", ConsoleColor.DarkGreen)
                .WriteLine($"And variations between {combinationSetsLength} elements", ConsoleColor.DarkMagenta);

            IList<IList<T>> items = combo.Result;

            foreach (var list in items)
            {
                this.consoleMio.Write($"({string.Join(" ", list)}), ", ConsoleColor.DarkBlue);
            }

            Console.WriteLine("\n");
        }
    }
}