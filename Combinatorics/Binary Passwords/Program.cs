namespace Binary_Passwords
{
    using System;
    using System.Linq;

    /// <summary>
    /// Assignment: http://bgcoder.com/Contests/Practice/Index/132#0
    /// </summary>
    class Program
    {
        static void Main()
        {
            string pass = Console.ReadLine();

            long possibilities = pass
                .Where(t => t.Equals('*'))
                .Aggregate<char, long>(1, (current, t) => current * 2);


            Console.WriteLine(possibilities);
        }
    }
}
