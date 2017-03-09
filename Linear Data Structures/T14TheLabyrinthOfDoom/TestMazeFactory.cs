namespace T14TheLabyrinthOfDoom
{
    using System.Collections.Generic;

    public static class TestMazeFactory
    {
        private static readonly List<string[,]> Labyrinths = new List<string[,]>()
        {
            new[,]
            {
                { "0", "0", "0" },
                { "0", "*", "0" },
                { "0", "0", "0" }
            },

            new[,]
            {
                { "*", "0", "0" },
                { "0", "0", "0" },
                { "0", "0", "0" }
            },

            new[,]
            {
                { "0", "0", "0" },
                { "0", "0", "0" },
                { "0", "*", "0" }
            },

            new[,]
            {
                { "0", "0", "0" },
                { "*", "x", "0" },
                { "0", "0", "0" }
            },

            new[,]
            {
                { "0", "0", "0", "0", "x", "0" },
                { "0", "*", "0", "0", "x", "x" },
                { "0", "0", "0", "0", "0", "0" }
            },

            new[,]
            {
                { "0", "0", "x", "0", "x", "0" },
                { "0", "*", "x", "0", "x", "x" },
                { "0", "0", "x", "0", "0", "0" }
            },

            new[,]
            {
                { "0", "0", "0", "0", "x", "0" },
                { "0", "*", "x", "0", "x", "x" },
                { "0", "0", "x", "0", "0", "0" }
            },

            new[,]
            {
                { "0", "0", "0", "x", "0", "x" },
                { "0", "x", "0", "x", "0", "x" },
                { "0", "*", "x", "0", "x", "0" },
                { "0", "x", "0", "0", "0", "0" },
                { "0", "0", "0", "x", "x", "0" },
                { "0", "0", "0", "x", "0", "x" }
            }
        };

        private static int next = 0;

        public static string[,] GetNext()
        {
            string[,] result;

            if (next < Labyrinths.Count)
            {
                result = (string[,])Labyrinths[next].Clone();
                next++;
            }
            else
            {
                Reset();
                result = null;
            }

            return result;
        }

        public static void Reset()
        {
            next = 0;
        }
    }
}
