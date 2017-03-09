namespace InitializingMatrix
{
    using System;

    class Program
    {
        static void Main()
        {
            int side = 10;

            int[,] matrix = new int[side, side];
            int counter = 1;

            for (int col = 0; col < side; col++)
            {
                for (int row = col + 1; row < side; row++)
                {
                    matrix[row, col] = counter++;
                }
            }

            for (int col = side - 1; col >= 0; col--)
            {
                for (int row = col - 1; row >= 0; row--)
                {
                    matrix[row, col] = counter++;
                }
            }

            Console.WriteLine(matrix.Stringify(5));
        }
    }
}
