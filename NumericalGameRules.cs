namespace BoardGameFramework
{
    public class NumericalGameRules
    {
        public bool IsValidNumberForPlayer(int number, bool isOddPlayer)
        {
            if (number < 1 || number > 9) return false;
            
            bool isOddNumber = (number % 2) == 1;
            return isOddPlayer == isOddNumber;
        }
        
        public bool CheckWinCondition(Board board)
        {
            int[,] grid = board.GetGrid();
            
            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (grid[i, 0] != 0 && grid[i, 1] != 0 && grid[i, 2] != 0)
                {
                    if (grid[i, 0] + grid[i, 1] + grid[i, 2] == 15)
                        return true;
                }
            }
            
            // Check columns
            for (int i = 0; i < 3; i++)
            {
                if (grid[0, i] != 0 && grid[1, i] != 0 && grid[2, i] != 0)
                {
                    if (grid[0, i] + grid[1, i] + grid[2, i] == 15)
                        return true;
                }
            }
            
            // Check main diagonal
            if (grid[0, 0] != 0 && grid[1, 1] != 0 && grid[2, 2] != 0)
            {
                if (grid[0, 0] + grid[1, 1] + grid[2, 2] == 15)
                    return true;
            }
            
            // Check anti-diagonal
            if (grid[0, 2] != 0 && grid[1, 1] != 0 && grid[2, 0] != 0)
            {
                if (grid[0, 2] + grid[1, 1] + grid[2, 0] == 15)
                    return true;
            }
            
            return false;
        }
    }
}