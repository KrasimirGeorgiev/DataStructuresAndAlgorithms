namespace SpiralMatrix
{
    using System;
    using InitializingMatrix;

    class Program
    {
        private static int counter = 1;
        private static int[,] matrix;

        static void Main()
        {
            int side = 10;

            matrix = new int[side, side];
            int rowStart = 0,
                colStart = 0,
                rowLength = side,
                colLength = side - 1;

            while (rowLength > 0 || colLength > 0)
            {
                MoveDown(rowStart, rowStart + rowLength - 1, colStart);
                colStart++;
                rowStart += rowLength - 1;

                MoveRight(colStart, colStart + colLength - 1, rowStart);
                rowStart--;
                colStart += colLength - 1;

                rowLength--;
                MoveUp(rowStart, rowStart - rowLength + 1, colStart);
                colStart--;
                rowStart -= rowLength - 1;

                colLength--;
                MoveLeft(colStart, colStart - colLength + 1, rowStart);
                rowStart++;
                colStart -= colLength - 1;

                rowLength--;
                colLength--;
            }

            Console.WriteLine(matrix.Stringify(5));
        }

        private static void MoveDown(int start, int end, int col)
        {
            for (int i = start; i <= end; i++)
            {
                matrix[i, col] = counter++;
            }
        }

        private static void MoveUp(int start, int end, int col)
        {
            for (int i = start; i >= end; i--)
            {
                matrix[i, col] = counter++;
            }
        }

        private static void MoveRight(int start, int end, int row)
        {
            for (int i = start; i <= end; i++)
            {
                matrix[row, i] = counter++;
            }
        }

        private static void MoveLeft(int start, int end, int row)
        {
            for (int i = start; i >= end; i--)
            {
                matrix[row, i] = counter++;
            }
        }
    }
}
