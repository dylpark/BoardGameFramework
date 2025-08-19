using System;

namespace BoardGameFramework
{
    /// <summary>
    /// TEMPLATE METHOD PATTERN - Abstract base class defining game algorithm
    /// Addresses feedback: "More commonalities should be extracted into base classes"
    /// </summary>
    public abstract class Game
    {
        protected Player[] players = [];
        protected int currentPlayerIndex;
        protected Board board = null!;
        protected MoveHistory moveHistory = null!;
        protected GameSaver gameSaver = null!;
        protected HelpSystem helpSystem = null!;
        protected bool gameOver;
        protected Player? winner;

        // Template Method - defines the invariant algorithm
        public void PlayGame()
        {
            InitializeGame();
            DisplayWelcome();

            while (!IsGameOver())
            {
                DisplayBoard();
                ProcessPlayerTurn();

                if (!IsGameOver())
                {
                    SwitchPlayer();
                }
            }

            DisplayResult();
        }

        // Factory Methods - subclasses create game-specific objects
        protected abstract Player CreatePlayer(string name, bool isFirstPlayer);
        protected abstract Board CreateBoard();
        protected abstract HelpSystem CreateHelpSystem();

        // Abstract methods - game-specific implementation required
        protected abstract void InitializeGame();
        protected abstract bool ValidateMove(Move move);
        protected abstract void CheckWinCondition();
        protected abstract bool IsGameOver();
        protected abstract string GetGameName();

        // Common concrete methods - shared by all games
        protected virtual void DisplayWelcome()
        {
            Console.Clear();
            Console.WriteLine("=====================================");
            Console.WriteLine($"     Welcome to {GetGameName()}!");
            Console.WriteLine("=====================================");
            Console.WriteLine("Type 'help' for commands\n");
        }

        protected virtual void DisplayBoard()
        {
            Console.WriteLine("\nCurrent Board:");
            board.Display();
            Console.WriteLine();
        }

        protected virtual void ProcessPlayerTurn()
        {
            Player currentPlayer = players[currentPlayerIndex];
            bool validMove = false;

            while (!validMove)
            {
                Console.Write($"{currentPlayer.Name}'s turn: ");
                string? input = Console.ReadLine();

                // Handle null input
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter a valid command or move.");
                    continue;
                }

                // Handle special commands
                if (HandleCommand(input))
                {
                    if (!gameOver) // Command might end game
                    {
                        DisplayBoard();
                        continue;
                    }
                    return;
                }

                // Try to parse as move
                Move? move = currentPlayer.ParseMove(input, board);
                if (move != null && ValidateMove(move))
                {
                    ExecuteMove(move);
                    CheckWinCondition();
                    validMove = true;
                }
                else
                {
                    Console.WriteLine("Invalid move! Try again.");
                }
            }
        }

        protected virtual bool HandleCommand(string input)
        {
            string command = input.ToLower().Trim();

            switch (command)
            {
                case "help":
                    helpSystem.DisplayHelp();
                    return true;

                case "undo":
                    UndoMove();
                    return true;

                case "redo":
                    RedoMove();
                    return true;

                case "quit":
                case "exit":
                    Console.WriteLine("Thanks for playing!");
                    gameOver = true;
                    return true;

                default:
                    if (command.StartsWith("save "))
                    {
                        string filename = command[5..];
                        SaveGame(filename);
                        return true;
                    }
                    else if (command.StartsWith("load "))
                    {
                        string filename = command[5..];
                        LoadGame(filename);
                        return true;
                    }
                    return false;
            }
        }

        protected virtual void ExecuteMove(Move move)
        {
            board.ApplyMove(move);
            moveHistory.AddMove(move);
        }

        protected virtual void SwitchPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        }

        protected virtual void DisplayResult()
        {
            Console.WriteLine("\n=====================================");
            if (HasWinner())
            {
                Console.WriteLine($"   🎉 {GetWinner().Name} WINS! 🎉");
            }
            else
            {
                Console.WriteLine("        It's a DRAW!");
            }
            Console.WriteLine("=====================================\n");
        }

        public virtual void SaveGame(string filename)
        {
            try
            {
                gameSaver.Save(this, board, moveHistory, filename);
                Console.WriteLine($"Game saved to {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save game: {ex.Message}");
            }
        }

        public virtual void LoadGame(string filename)
        {
            try
            {
                gameSaver.Load(this, board, moveHistory, filename);
                Console.WriteLine($"Game loaded from {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load game: {ex.Message}");
            }
        }

        public virtual void UndoMove()
        {
            if (moveHistory.CanUndo())
            {
                Move move = moveHistory.Undo()!; // Safe because CanUndo() returned true
                board.RemoveMove(move);
                SwitchPlayer();
                Console.WriteLine("Move undone.");
            }
            else
            {
                Console.WriteLine("No moves to undo.");
            }
        }

        public virtual void RedoMove()
        {
            if (moveHistory.CanRedo())
            {
                Move move = moveHistory.Redo()!; // Safe because CanRedo() returned true
                board.ApplyMove(move);
                SwitchPlayer();
                Console.WriteLine("Move redone.");
            }
            else
            {
                Console.WriteLine("No moves to redo.");
            }
        }

        protected abstract bool HasWinner();
        protected abstract Player GetWinner();
    }
}