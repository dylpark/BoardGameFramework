using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using BoardGameFramework.Core;
using BoardGameFramework.Games.NumericalTicTacToe;
using BoardGameFramework.Services;

namespace BoardGameFramework.UI
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

                if (string.Equals(choice, "l", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(choice, "load", StringComparison.OrdinalIgnoreCase))
                {
                    if (HandleLoadGame())
                    {
                        Console.Write("\nPlay again? (y/n): ");
                        string? playAgain = Console.ReadLine();
                        if (!string.Equals(playAgain, "y", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Thanks for playing! Goodbye!");
                            break;
                        }
                    }
                }
                else if (choice != null && gameFactories.TryGetValue(choice, out GameFactory? factory))
                {
                    currentGame = factory.InitializeNewGame();
                    currentGame.PlayGame();

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
            Console.WriteLine("Select an option:");

            foreach (var kvp in gameFactories)
            {
                Console.WriteLine($"  {kvp.Key}. New {kvp.Value.GetGameName()} Game");
            }

            Console.WriteLine("  L. Load Saved Game");
            Console.WriteLine("  Q. Quit");
            Console.Write("\nYour choice: ");
        }

        private bool HandleLoadGame()
        {
            try
            {
                Console.WriteLine("\n===== LOAD SAVED GAME =====");

                var saveFiles = Directory.GetFiles(".", "*.json");
                if (saveFiles.Length == 0)
                {
                    Console.WriteLine("No save files found in the current directory.");
                    Console.WriteLine("Save files should have a .json extension.");
                    Console.WriteLine("\nPress any key to return to main menu...");
                    Console.ReadKey();
                    return false;
                }

                Console.WriteLine("Available save files:");
                for (int i = 0; i < saveFiles.Length; i++)
                {
                    var fileName = Path.GetFileName(saveFiles[i]);
                    var fileInfo = new FileInfo(saveFiles[i]);
                    Console.WriteLine($"  {i + 1}. {fileName} ({fileInfo.LastWriteTime:yyyy-MM-dd HH:mm})");
                }

                Console.WriteLine("  0. Enter filename manually");
                Console.WriteLine("  C. Cancel");
                Console.Write("\nYour choice: ");

                string? choice = Console.ReadLine();

                if (string.Equals(choice, "c", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(choice, "cancel", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }

                string filename;
                if (choice == "0")
                {
                    Console.Write("Enter filename: ");
                    filename = Console.ReadLine() ?? "";
                    if (string.IsNullOrWhiteSpace(filename))
                    {
                        Console.WriteLine("Invalid filename.");
                        return false;
                    }
                }
                else if (int.TryParse(choice, out int fileIndex) && fileIndex > 0 && fileIndex <= saveFiles.Length)
                {
                    filename = saveFiles[fileIndex - 1];
                }
                else
                {
                    Console.WriteLine("Invalid choice.");
                    return false;
                }

                // Load and validate the save file
                if (!LoadGameFromFile(filename))
                {
                    Console.WriteLine("\nPress any key to return to main menu...");
                    Console.ReadKey();
                    return false;
                }

                // Start the loaded game
                if (currentGame != null)
                {
                    currentGame.PlayGame();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
                Console.WriteLine("\nPress any key to return to main menu...");
                Console.ReadKey();
                return false;
            }
        }

        private bool LoadGameFromFile(string filename)
        {
            try
            {
                // Try with .json extension if not provided
                if (!File.Exists(filename) && !filename.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                {
                    filename += ".json";
                }

                if (!File.Exists(filename))
                {
                    Console.WriteLine($"Save file '{filename}' not found.");
                    return false;
                }

                Console.WriteLine($"Loading game from {filename}...");

                // Read and parse the save file to determine game type
                string jsonString = File.ReadAllText(filename);
                var gameState = JsonSerializer.Deserialize<GameState>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                if (gameState == null)
                {
                    Console.WriteLine("Failed to read save file.");
                    return false;
                }

                // Find the appropriate factory for this game type
                GameFactory? factory = null;
                foreach (var kvp in gameFactories)
                {
                    if (kvp.Value.GetGameName() == gameState.GameType)
                    {
                        factory = kvp.Value;
                        break;
                    }
                }

                if (factory == null)
                {
                    Console.WriteLine($"No factory found for game type: {gameState.GameType}");
                    Console.WriteLine("This save file may be from an unsupported game version.");
                    return false;
                }

                // Create a new game instance and load the state
                currentGame = factory.InitializeNewGame();

                // Use the game's load functionality
                currentGame.LoadGame(filename);

                Console.WriteLine($"Successfully loaded {gameState.GameType} game!");
                Console.WriteLine($"Saved on: {gameState.SaveDate:yyyy-MM-dd HH:mm:ss}");

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load game: {ex.Message}");
                return false;
            }
        }
    }
}