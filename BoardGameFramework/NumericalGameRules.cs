namespace BoardGameFramework
{
    public class NumericalGameRules
    {
        public bool IsValidMove(Move move, Board board, int currentPlayerIndex)
        {
            // Check position bounds
            if (move.Row < 0 || move.Row >= 3 || move.Col < 0 || move.Col >= 3)
                return false;

            // Check if position is empty
            if (board.GetCell(move.Row, move.Col) != 0)
                return false;

            // Check player number constraints
            bool isPlayer1 = currentPlayerIndex == 0;
            bool isOddNumber = move.Value % 2 == 1;

            if (isPlayer1 && !isOddNumber) return false;
            if (!isPlayer1 && isOddNumber) return false;

            // Check valid number range
            if (move.Value < 1 || move.Value > 9) return false;

            // Check if number is already used
            return !IsNumberUsed(move.Value, board);
        }

        public bool CheckWinCondition(Board board)
        {
            // Check rows, columns, and diagonals for sum of 15
            for (int i = 0; i < 3; i++)
            {
                // Check rows
                if (CheckLineSum(board.GetCell(i, 0), board.GetCell(i, 1), board.GetCell(i, 2)))
                    return true;

                // Check columns
                if (CheckLineSum(board.GetCell(0, i), board.GetCell(1, i), board.GetCell(2, i)))
                    return true;
            }

            // Check diagonals
            if (CheckLineSum(board.GetCell(0, 0), board.GetCell(1, 1), board.GetCell(2, 2)))
                return true;

            if (CheckLineSum(board.GetCell(0, 2), board.GetCell(1, 1), board.GetCell(2, 0)))
                return true;

            return false;
        }

        public string GetAvailableNumbers(Board board, bool oddNumbers)
        {
            var available = new List<int>();
            var range = oddNumbers ? new[] { 1, 3, 5, 7, 9 } : new[] { 2, 4, 6, 8 };

            foreach (int num in range)
            {
                if (!IsNumberUsed(num, board))
                    available.Add(num);
            }

            return string.Join(", ", available);
        }

        private bool CheckLineSum(int a, int b, int c)
        {
            return a != 0 && b != 0 && c != 0 && a + b + c == 15;
        }

        private bool IsNumberUsed(int number, Board board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board.GetCell(i, j) == number)
                        return true;
            return false;
        }
    }
}