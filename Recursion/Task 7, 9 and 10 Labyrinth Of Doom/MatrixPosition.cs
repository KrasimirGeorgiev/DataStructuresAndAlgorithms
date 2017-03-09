namespace LabyrinthOfDoom
{
    using System.Collections.Generic;

    public class MatrixPosition
    {
        private const string FREE_SPACE = "0";

        public MatrixPosition(int row, int col)
        {
            this.Row = row;
            this.Col = col;
            this.DistanceFromStartPosition = -1;
            this.Unexplored = new Stack<MatrixPosition>();
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public int DistanceFromStartPosition { get; set; }

        public Stack<MatrixPosition> Unexplored { get; set; }

        public MatrixPosition Next
        {
            get
            {
                if (this.Unexplored.Count > 0)
                {
                    return this.Unexplored.Pop();
                }
                else
                {
                    return null;
                }
            }
        }

        public bool IsWithinRange(int rowsLength, int colsLength)
        {
            bool result =
                0 <= this.Row && this.Row < rowsLength &&
                0 <= this.Col && this.Col < colsLength;

            return result;
        }
    }
}
