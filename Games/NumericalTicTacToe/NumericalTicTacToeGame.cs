using System.Collections.Generic;
using BoardGameFramework.Core;
using BoardGameFramework.Players;
using BoardGameFramework.Services;
using BoardGameFramework.Interfaces;

namespace BoardGameFramework.Games.NumericalTicTacToe
{
    /// <summary>
    /// Concrete implementation of Template Method pattern
    /// Implements Numerical Tic-Tac-Toe specific rules
    /// </summary>
    public class NumericalTicTacToeGame : Game
    {
        private HashSet<int> usedNumbers = [];
        private NumericalGameRules gameRules = null!;

        // Factory Method implementations - create game-specific objects
        protected override Player CreatePlayer(string name, bool isFirstPlayer)
        {
            return new NumericalPlayer(name, isFirstPlayer);
        }

        protected override Board CreateBoard()
        {
            return new Board(3, 3);
        }

        protected override HelpSystem CreateHelpSystem()
        {
            return new NumericalTicTacToeHelpSystem();
        }

        protected override void InitializeGame()
        {
            board = CreateBoard();
            helpSystem = CreateHelpSystem();
            gameRules = new NumericalGameRules();
            moveHistory = new MoveHistory();
            gameSaver = new GameSaver();
            usedNumbers = [];
            gameOver = false;
            winner = null;

            // Ask for game mode
            Console.WriteLine("\nSelect game mode:");
            Console.WriteLine("1. Human vs Human");
            Console.WriteLine("2. Human vs Computer");
            Console.Write("Choice: ");
            string? modeChoice = Console.ReadLine();

            if (modeChoice == "2")
            {
                // Human vs Computer
                Console.Write("Enter your name: ");
                string? humanName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(humanName)) humanName = "Player";

                Console.Write("Do you want to go first? (y/n): ");
                string? firstChoice = Console.ReadLine();
                bool humanFirst = string.Equals(firstChoice, "y", StringComparison.OrdinalIgnoreCase);

                if (humanFirst)
                {
                    players = [
                        new NumericalPlayer(humanName, true), // Human uses odd
                        new NumericalComputerPlayer("Computer", false) // Computer uses even
                    ];
                }
                else
                {
                    players = [
                        new NumericalComputerPlayer("Computer", true), // Computer uses odd
                        new NumericalPlayer(humanName, false) // Human uses even
                    ];
                }
            }
            else
            {
                // Human vs Human (default)
                Console.Write("Enter Player 1 name (odd numbers): ");
                string? p1Name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(p1Name)) p1Name = "Player 1";

                Console.Write("Enter Player 2 name (even numbers): ");
                string? p2Name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(p2Name)) p2Name = "Player 2";

                players = [
                    CreatePlayer(p1Name, true),
                    CreatePlayer(p2Name, false)
                ];
            }

            currentPlayerIndex = 0;
        }

        protected override bool ValidateMove(Move move)
        {
            if (move is not NumericalMove numMove) return false;

            // Check position validity
            if (!board.IsValidPosition(numMove.Row, numMove.Col))
            {
                Console.WriteLine("Invalid position!");
                return false;
            }

            if (!board.IsEmptyPosition(numMove.Row, numMove.Col))
            {
                Console.WriteLine("Position already occupied!");
                return false;
            }

            // Check if number is already used
            if (usedNumbers.Contains(numMove.Number))
            {
                Console.WriteLine($"Number {numMove.Number} has already been used!");
                return false;
            }

            // Use interface to get player's number constraint
            if (players[currentPlayerIndex] is not INumberedPlayer numberedPlayer)
            {
                Console.WriteLine("ERROR: Current player doesn't implement INumberedPlayer!");
                return false;
            }

            // Validate the number for the player
            if (!NumericalGameRules.IsValidNumberForPlayer(numMove.Number, numberedPlayer.UsesOddNumbers))
            {
                string expected = numberedPlayer.UsesOddNumbers ? "odd (1,3,5,7,9)" : "even (2,4,6,8)";
                Console.WriteLine($"Invalid number! Player must use {expected} numbers.");
                return false;
            }

            return true;
        }

        protected override void ProcessPlayerTurn()
        {
            Player currentPlayer = players[currentPlayerIndex];
            bool validMove = false;

            while (!validMove)
            {
                Move? move = null;

                // Check if computer player
                if (currentPlayer is NumericalComputerPlayer computerPlayer)
                {
                    // Computer player logic
                    Console.WriteLine($"{computerPlayer.Name} is thinking...");
                    System.Threading.Thread.Sleep(1000);

                    // Generate a random valid move
                    List<int> availableNumbers = [];
                    for (int i = 1; i <= 9; i++)
                    {
                        if (!usedNumbers.Contains(i))
                        {
                            bool isOdd = (i % 2 == 1);
                            if (isOdd == computerPlayer.UsesOddNumbers)
                            {
                                availableNumbers.Add(i);
                            }
                        }
                    }

                    List<(int row, int col)> emptyPositions = [];
                    for (int row = 0; row < 3; row++)
                    {
                        for (int col = 0; col < 3; col++)
                        {
                            if (board.IsEmptyPosition(row, col))
                            {
                                emptyPositions.Add((row, col));
                            }
                        }
                    }

                    if (availableNumbers.Count > 0 && emptyPositions.Count > 0)
                    {
                        Random random = new();
                        var (row, col) = emptyPositions[random.Next(emptyPositions.Count)];
                        var number = availableNumbers[random.Next(availableNumbers.Count)];

                        Console.WriteLine($"{computerPlayer.Name} plays {number} at ({row}, {col})");

                        move = new NumericalMove(row, col, number, computerPlayer);
                    }
                    else
                    {
                        Console.WriteLine($"{computerPlayer.Name} has no valid moves!");
                        gameOver = true;
                        return;
                    }
                }
                else
                {
                    // Human player
                    Console.Write($"{currentPlayer.Name}'s turn: ");
                    string? input = Console.ReadLine();

                    // Handle special commands
                    if (input != null && HandleCommand(input))
                    {
                        if (!gameOver)
                        {
                            DisplayBoard();
                            continue;
                        }
                        return;
                    }

                    // Parse move for human player
                    if (currentPlayer is NumericalPlayer numPlayer && input != null)
                    {
                        move = numPlayer.ParseMove(input, board);
                    }
                }

                if (move != null)
                {
                    try
                    {
                        if (ValidateMove(move))
                        {
                            ExecuteMove(move);
                            CheckWinCondition();
                            validMove = true;
                        }
                        else if (currentPlayer is not NumericalComputerPlayer)
                        {
                            Console.WriteLine("Invalid move! Try again.");
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        protected override void CheckWinCondition()
        {
            if (gameRules.CheckWinCondition(board))
            {
                winner = players[currentPlayerIndex];
                gameOver = true;
            }
            else if (board.IsFull())
            {
                gameOver = true;
            }
        }

        public HashSet<int> GetUsedNumbers()
        {
            return [.. usedNumbers];
        }

        public override bool IsGameOver()
        {
            return gameOver;
        }

        protected override bool HasWinner()
        {
            return winner != null;
        }

        public override Player GetWinner()
        {
            return winner!;
        }

        public override string GetGameName()
        {
            return "Numerical Tic-Tac-Toe";
        }

        public override void UndoMove()
        {
            if (moveHistory.CanUndo())
            {
                Move move = moveHistory.Undo()!; // Safe because CanUndo() returned true
                board.RemoveMove(move);

                // Remove number from used numbers
                if (move is NumericalMove numMove)
                {
                    usedNumbers.Remove(numMove.Number);
                }

                SwitchPlayer();

                // Reset game state if it was over
                if (gameOver)
                {
                    gameOver = false;
                    winner = null;
                }

                Console.WriteLine("Move undone.");
            }
            else
            {
                Console.WriteLine("No moves to undo.");
            }
        }
        
        // Method for save/load functionality
        public void RestoreUsedNumbers(List<int> numbers)
        {
            usedNumbers.Clear();
            foreach (int number in numbers)
            {
                usedNumbers.Add(number);
            }
        }
    }
}