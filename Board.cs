using System;

namespace BoardGameFramework
{
    public class Board
    {
        private int[,] grid;
        private int rows;
        private int cols;
        
        public Board(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            grid = new int[rows, cols];
        }
        
        public void Display()
        {
            Console.WriteLine("    0   1   2");
            Console.WriteLine("  +---+---+---+");
            
            for (int i = 0; i < rows; i++)
            {
                Console.Write($"{i} |");
                for (int j = 0; j < cols; j++)
                {
                    string value = grid[i, j] == 0 ? " " : grid[i, j].ToString();
                    Console.Write($" {value} |");
                }
                Console.WriteLine();
                Console.WriteLine("  +---+---+---+");
            }
        }
        
        public bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < rows && col >= 0 && col < cols;
        }
        
        public bool IsEmptyPosition(int row, int col)
        {
            return grid[row, col] == 0;
        }
        
        public void ApplyMove(Move move)
        {
            if (move is NumericalMove numMove)
            {
                grid[numMove.Row, numMove.Col] = numMove.Number;
            }
        }
        
        public void RemoveMove(Move move)
        {
            grid[move.Row, move.Col] = 0;
        }
        
        public bool IsFull()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (grid[i, j] == 0) return false;
                }
            }
            return true;
        }
        
        public int[,] GetGrid()
        {
            return (int[,])grid.Clone();
        }
        
        public int GetValue(int row, int col)
        {
            return grid[row, col];
        }
    }
}