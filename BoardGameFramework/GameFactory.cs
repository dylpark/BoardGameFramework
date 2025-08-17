namespace BoardGameFramework
{
    // Factory Method Pattern
    public abstract class GameFactory
    {
        public abstract Game CreateGame();
        public abstract Player CreatePlayer(PlayerType type);
    }

    public class NumericalTicTacToeFactory : GameFactory
    {
        public override Game CreateGame()
        {
            return new NumericalTicTacToeGame();
        }

        public override Player CreatePlayer(PlayerType type)
        {
            return type switch
            {
                PlayerType.Human => new HumanPlayer(),
                PlayerType.Computer => new ComputerPlayer(),
                _ => throw new ArgumentException("Invalid player type")
            };
        }
    }
}