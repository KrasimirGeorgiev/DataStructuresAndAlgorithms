namespace InitializingMatrix
{
    using System.Text;

    public static class MatrixExtensions
    {
        public static string Stringify<T>(this T[,] matrix, int padding)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sb.AppendFormat("{0," + padding + "}", matrix[i, j]);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}