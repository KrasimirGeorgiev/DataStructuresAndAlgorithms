namespace T14TheLabyrinthOfDoom
{
    using System;
    using ConsoleMio.ConsoleEnhancements;
    using T13AdtDynamicLinkedListQueue;
    using static System.ConsoleColor;

    /// <summary>
    /// We are given a labyrinth of size N x N.
    /// 
    /// Some of its cells are empty (0) and some are full (x).
    /// We can move from an empty cell to another empty cell if they share common wall.
    /// Given a starting position (*) calculate and fill in the array the minimal distance from this position 
    /// to any other cell in the array. Use "u" for all unreachable cells.
    /// </summary>
    public class Program
    {
        private static LinkedQueue<MatrixPosition> unexploredPositions;

        private static readonly ConsoleMio ConsoleMio = new ConsoleMio();

        private static string[,] maze;

        private static void Main(string[] args)
        {
            ConsoleMio.Setup();

            while ((maze = TestMazeFactory.GetNext()) != null)
            {
                ConsoleMio.PrintHeading("Task 14 Minimal Distances In The Labyrinth Of Doom");

                ConsoleMio.WriteLine("Labyrinth Before: ", DarkCyan);
                PrintLabyrinth(maze);
                ConsoleMio.WriteLine();

                unexploredPositions = new LinkedQueue<MatrixPosition>();
                DoNextLabyrinth();

                ConsoleMio.WriteLine("Labyrinth After: ", DarkGreen);
                PrintLabyrinth(maze);
                ConsoleMio.WriteLine();

                ConsoleMio.WriteLine("Press a key to test the next labyrinth...", DarkRed);
                Console.ReadKey(true);
                Console.Clear();
            }

            ConsoleMio.WriteLine(
                "Completed!!! Thank you very much for looking all the tasks!",
                DarkGreen);
        }

        private static void DoNextLabyrinth()
        {
            MatrixPosition start = GetStartingPosition(maze, "*");
            start.DistanceFromStartPosition = 0;
            MarkPossibleDirections(start);

            while (start.Unexplored.Size > 0)
            {
                var next = start.Next;
                next.DistanceFromStartPosition = 1;

                unexploredPositions.Enqueue(next);
            }

            ExploreMaze(unexploredPositions.GetNextInLine());

            MarkUnreachedPlaces(maze, "0", "u");
        }

        private static void MarkUnreachedPlaces(string[,] maze, string unreachedPlaceToken, string markToken)
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j].Equals(unreachedPlaceToken, StringComparison.Ordinal))
                    {
                        maze[i, j] = markToken;
                    }
                }
            }
        }

        private static void PrintLabyrinth(string[,] maze)
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    string current = maze[i, j];

                    if (current.Equals("x", StringComparison.OrdinalIgnoreCase))
                    {
                        ConsoleMio.Write("x ", Red);
                    }
                    else
                    {
                        int parseResult;
                        if (!current.Equals("0") &&
                            int.TryParse(current, out parseResult))
                        {
                            ConsoleMio.Write($"{current} ", Green);
                        }
                        else
                        {
                            ConsoleMio.Write($"{current} ", Black);
                        }
                    }
                }

                ConsoleMio.WriteLine();
            }
        }

        private static void ExploreMaze(MatrixPosition position)
        {
            maze[position.Row, position.Col] = position.DistanceFromStartPosition.ToString();

            MarkPossibleDirections(position);

            while (position.Unexplored.Size > 0)
            {
                var next = position.Next;
                next.DistanceFromStartPosition = position.DistanceFromStartPosition + 1;

                unexploredPositions.Enqueue(next);
            }

            if (unexploredPositions.Size > 0)
            {
                ExploreMaze(unexploredPositions.GetNextInLine());
            }
        }

        private static MatrixPosition GetStartingPosition(string[,] maze, string startToken)
        {
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j].Equals(startToken, StringComparison.Ordinal))
                    {
                        return new MatrixPosition(i, j);
                    }
                }
            }

            throw new ArgumentException("Expected start position to exist in the given maze");
        }

        private static int MarkPossibleDirections(MatrixPosition matrixPosition)
        {
            int rowsLength = maze.GetLength(0),
                colsLength = maze.GetLength(1);

            MatrixPosition[] allPositions =
            {
                new MatrixPosition(matrixPosition.Row - 1, matrixPosition.Col),
                new MatrixPosition(matrixPosition.Row, matrixPosition.Col + 1),
                new MatrixPosition(matrixPosition.Row + 1, matrixPosition.Col),
                new MatrixPosition(matrixPosition.Row, matrixPosition.Col - 1)
            };

            int unexploredCount = 0;
            foreach (var position in allPositions)
            {
                if (position.IsWithinRange(rowsLength, colsLength))
                {
                    if (maze[position.Row, position.Col].Equals("0", StringComparison.Ordinal))
                    {
                        matrixPosition.Unexplored.StackUp(position);
                        unexploredCount++;
                    }
                }
            }

            return unexploredCount;
        }
    }
}
