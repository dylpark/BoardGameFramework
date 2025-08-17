using System;
using System.Collections.Generic;

namespace BoardGameFramework
{
    public class GameConsole
    {
        private Dictionary<string, GameFactory> gameFactories;
        private Game currentGame;
        
        public GameConsole()
        {
            gameFactories = new Dictionary<string, GameFactory>();
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
                string choice = Console.ReadLine();
                
                if (choice.ToLower() == "q" || choice.ToLower() == "quit")
                {
                    Console.WriteLine("Thanks for playing! Goodbye!");
                    break;
                }
                
                if (gameFactories.ContainsKey(choice))
                {
                    GameFactory factory = gameFactories[choice];
                    currentGame = factory.InitializeNewGame();
                    currentGame.PlayGame();
                    
                    // After game ends, ask if they want to play again
                    Console.Write("\nPlay again? (y/n): ");
                    string playAgain = Console.ReadLine();
                    if (playAgain.ToLower() != "y")
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
        
        private void DisplayTitle()
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