namespace P3_Shortest_Path
{
    using System;
    using System.Text;

    class Program
    {
        private static StringBuilder sb;

        private static int counter = 0;

        static void Main()
        {
            char[] directions = Console.ReadLine().ToCharArray();

            sb = new StringBuilder();

            GenerateAllDirections(directions, 0);


            if (counter == 0)
            {
                Console.WriteLine(1);
                Console.WriteLine(directions);
            }
            else
            {
                Console.WriteLine(counter);
                Console.WriteLine(sb.ToString());
            }
        }

        private static void GenerateAllDirections(char[] directions, int index)
        {
            if (index == directions.Length)
            {
                counter++;
                sb.Append(directions)
                    .AppendLine();
            }
            else if (directions[index] == '*')
            {
                directions[index] = 'L';
                GenerateAllDirections(directions, index + 1);

                directions[index] = 'R';
                GenerateAllDirections(directions, index + 1);

                directions[index] = 'S';
                GenerateAllDirections(directions, index + 1);

                directions[index] = '*';
            }
            else
            {
                GenerateAllDirections(directions, index + 1);
            }
        }
    }
}