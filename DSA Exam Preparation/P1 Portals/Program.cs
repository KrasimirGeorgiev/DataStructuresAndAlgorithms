namespace P1_Portals
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// http://bgcoder.com/Contests/Practice/Index/191#0
    /// </summary>
    public class Program
    {
        private static readonly HashSet<Position> visited = new HashSet<Position>();
        private static readonly SortedSet<long> powers = new SortedSet<long>();

        private static int[,] matrix;

        private static void Main()
        {
            var start = new Position(Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray());

            var size = new Position(Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToArray());

            matrix = new int[size.Row, size.Col];
            Position.RowsLenght = size.Row;
            Position.ColsLenght = size.Col;

            visited.Add(start);

            FillMattrix();
            GetTotalTeleportationPower(start, 0);

            if (powers.Any())
            {
                Console.WriteLine(powers.Max());
            }
            else
            {
                Console.WriteLine(0);
            }
        }

        private static bool GetTotalTeleportationPower(Position p, long prevPower)
        {
            if (!p.IsWithinRange() || matrix[p.Row, p.Col] == -1)
            {
                return false;
            }

            int power = matrix[p.Row, p.Col];

            var right = new Position(p.Row, p.Col + power);
            TeleportTo(right, power + prevPower);

            var down = new Position(p.Row + power, p.Col);
            TeleportTo(down, power + prevPower);

            var left = new Position(p.Row, p.Col - power);
            TeleportTo(left, power + prevPower);

            var up = new Position(p.Row - power, p.Col);
            TeleportTo(up, power + prevPower);

            return true;
        }

        private static void TeleportTo(Position pos, long power)
        {
            if (visited.Contains(pos))
            {
                powers.Add(power);
                return;
            }

            visited.Add(pos);

            if (GetTotalTeleportationPower(pos, power))
            {
                powers.Add(power);
            }

            visited.Remove(pos);
        }

        private static void FillMattrix()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string[] line = Console.ReadLine()
                    .Split(' ');

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (line[j] == "#")
                    {
                        matrix[i, j] = -1;
                    }
                    else
                    {
                        matrix[i, j] = int.Parse(line[j]);
                    }
                }
            }
        }
    }

    public class Position : IEquatable<Position>
    {
        public static int RowsLenght { get; set; }

        public static int ColsLenght { get; set; }

        public Position(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public Position(IReadOnlyList<int> info)
        {
            this.Row = info[0];
            this.Col = info[1];
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public override string ToString()
        {
            return string.Format("P: {0}, {1}", this.Row, this.Col);
        }

        public override int GetHashCode()
        {
            return this.Row << 3 + this.Col << 7;
        }

        public bool IsWithinRange()
        {
            bool result = (0 <= this.Row && this.Row < RowsLenght) &&
                          (0 <= this.Col && this.Col < ColsLenght);

            return result;
        }

        public bool Equals(Position other)
        {
            return this.Row == other.Row && this.Col == other.Col;
        }
    }
}
