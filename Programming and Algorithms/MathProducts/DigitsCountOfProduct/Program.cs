namespace DigitsCountOfProduct
{
    using System;

    class Program
    {
        static void Main()
        {
            Console.Write("Enter the desired factorial to calculate digits count up to: ");
            int n = int.Parse(Console.ReadLine());

            double digits = 0;
            for (int i = 1; i <= n; i++)
            {
                digits += Math.Log10(i);
            }

            decimal result = Math.Floor((decimal)digits) + 1;

            Console.WriteLine("Factorial of {0} has {1} digits", n, result);
        }
    }
}
