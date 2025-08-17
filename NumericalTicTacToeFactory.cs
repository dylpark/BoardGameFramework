namespace BoardGameFramework
{
    /// <summary>
    /// Concrete creator in Factory Method pattern
    /// Creates Numerical Tic-Tac-Toe specific objects
    /// </summary>
    public class NumericalTicTacToeFactory : GameFactory
    {
        public override Game CreateGame()
        {
            return new NumericalTicTacToeGame();
        }
        
        public override string GetGameName()
        {
            return "Numerical Tic-Tac-Toe";
        }
        
        public override string GetGameDescription()
        {
            return "Players take turns placing numbers 1-9 on a 3x3 grid.\n" +
                   "Player 1 uses odd numbers (1,3,5,7,9).\n" +
                   "Player 2 uses even numbers (2,4,6,8).\n" +
                   "First to make any line sum to 15 wins!";
        }
    }
}