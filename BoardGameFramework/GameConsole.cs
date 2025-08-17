namespace BoardGameFramework
{
    public class GameConsole
    {
        public void StartGameSession()
        {
            Console.WriteLine("Welcome to Board Game Framework!");
            Console.WriteLine("1. Numerical Tic-Tac-Toe");
            Console.Write("Select game type: ");

            var gameFactory = new NumericalTicTacToeFactory();
            var game = gameFactory.CreateGame();

            Console.WriteLine("\n1. Human vs Human");
            Console.WriteLine("2. Human vs Computer");
            Console.Write("Choose players: ");
            string? playerChoice = Console.ReadLine();

            if (playerChoice == "2")
            {
                game.AddPlayer(gameFactory.CreatePlayer(PlayerType.Human));
                game.AddPlayer(gameFactory.CreatePlayer(PlayerType.Computer));
            }
            else
            {
                game.AddPlayer(gameFactory.CreatePlayer(PlayerType.Human));
                game.AddPlayer(gameFactory.CreatePlayer(PlayerType.Human));
            }

            game.PlayGame();
        }
    }
}