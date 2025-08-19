namespace BoardGameFramework
{
    public class GameConsole
    {
        private readonly Dictionary<string, GameFactory> gameFactories = [];
        private Game? currentGame;

        public GameConsole()
        {
            RegisterGames();
        }

        private void RegisterGames()
        {
            // Register available games
            gameFactories.Add("1", new NumericalTicTacToeFactory());
            // Future games would be added here
            // gameFactories.Add("2", new WildTicTacToeFactory());
        }

        public void Run()
        {
            DisplayTitle();

            while (true)
            {
                DisplayMainMenu();
                string? choice = Console.ReadLine();

                if (string.Equals(choice, "q", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(choice, "quit", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Thanks for playing! Goodbye!");
                    break;
                }

                if (choice != null && gameFactories.TryGetValue(choice, out GameFactory? factory))
                {
                    currentGame = factory.InitializeNewGame();
                    currentGame.PlayGame();

                    // After game ends, ask if they want to play again
                    Console.Write("\nPlay again? (y/n): ");
                    string? playAgain = Console.ReadLine();
                    if (!string.Equals(playAgain, "y", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Thanks for playing! Goodbye!");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
            }
        }

        private static void DisplayTitle()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║     BOARD GAME FRAMEWORK v1.0      ║");
            Console.WriteLine("║         Assignment 2 - IFQ563      ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.WriteLine();
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("\n===== MAIN MENU =====");
            Console.WriteLine("Select a game:");

            foreach (var kvp in gameFactories)
            {
                Console.WriteLine($"  {kvp.Key}. {kvp.Value.GetGameName()}");
            }

            Console.WriteLine("  Q. Quit");
            Console.Write("\nYour choice: ");
        }
    }
}