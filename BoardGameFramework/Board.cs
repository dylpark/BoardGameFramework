namespace BoardGameFramework
{
    public class Board
    {
        private int[,] grid = new int[3, 3];

        public void MakeMove(Move move)
        {
            grid[move.Row, move.Col] = move.Value;
        }

        public void UndoMove(Move move)
        {
            grid[move.Row, move.Col] = 0;
        }

        public int GetCell(int row, int col)
        {
            return grid[row, col];
        }

        public bool IsFull()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (grid[i, j] == 0) return false;
            return true;
        }

        public void Display()
        {
            Console.WriteLine("Current board:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(grid[i, j] == 0 ? "   " : $" {grid[i, j]} ");
                    if (j < 2) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("-----------");
            }
        }

        public int[,] GetState() => (int[,])grid.Clone();
        public void SetState(int[,] state) => grid = (int[,])state.Clone();
    }

    public class Move
    {
        public int Row { get; }
        public int Col { get; }
        public int Value { get; }

        public Move(int row, int col, int value)
        {
            Row = row;
            Col = col;
            Value = value;
        }
    }
}
