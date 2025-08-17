namespace BoardGameFramework
{
    // Concrete implementation using Template Method pattern
    public class NumericalTicTacToeGame : Game
    {
        private NumericalGameRules gameRules;

        public NumericalTicTacToeGame()
        {
            gameRules = new NumericalGameRules();
        }

        protected override HelpSystem CreateHelpSystem()
        {
            return new NumericalTicTacToeHelpSystem();
        }

        protected override bool IsValidMove(Move move)
        {
            return gameRules.IsValidMove(move, board, currentPlayerIndex);
        }

        protected override bool CheckWinCondition()
        {
            return gameRules.CheckWinCondition(board);
        }

        protected override void DisplayGameSpecificInfo()
        {
            var currentPlayer = GetCurrentPlayer();
            if (currentPlayerIndex == 0)
                Console.WriteLine("Available odd numbers: " + gameRules.GetAvailableNumbers(board, true));
            else
                Console.WriteLine("Available even numbers: " + gameRules.GetAvailableNumbers(board, false));
        }
    }
}
