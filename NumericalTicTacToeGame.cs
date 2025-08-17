using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGameFramework
{
    /// <summary>
    /// Concrete implementation of Template Method pattern
    /// Implements Numerical Tic-Tac-Toe specific rules
    /// </summary>
    public class NumericalTicTacToeGame : Game
    {
        private HashSet<int> usedNumbers;
        private NumericalGameRules gameRules;
        
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
        
        // Template Method implementations
        protected override void InitializeGame()
        {
            board = CreateBoard();
            helpSystem = CreateHelpSystem();
            gameRules = new NumericalGameRules();
            moveHistory = new MoveHistory();
            gameSaver = new GameSaver();
            usedNumbers = new HashSet<int>();
            gameOver = false;
            winner = null;
            
            // Create players
            Console.Write("Enter Player 1 name (odd numbers 1,3,5,7,9): ");
            string p1Name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(p1Name)) p1Name = "Player 1";
            
            Console.Write("Enter Player 2 name (even numbers 2,4,6,8): ");
            string p2Name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(p2Name)) p2Name = "Player 2";
            
            players = new Player[]
            {
                CreatePlayer(p1Name, true),
                CreatePlayer(p2Name, false)
            };
            
            currentPlayerIndex = 0;
        }
        
        protected override bool ValidateMove(Move move)
        {
            var numMove = move as NumericalMove;
            if (numMove == null) return false;
            
            // Check if position is valid and empty
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
            
            // Check if number is valid for player
            var numPlayer = players[currentPlayerIndex] as NumericalPlayer;
            if (!gameRules.IsValidNumberForPlayer(numMove.Number, numPlayer.UsesOddNumbers))
            {
                string expected = numPlayer.UsesOddNumbers ? "odd (1,3,5,7,9)" : "even (2,4,6,8)";
                Console.WriteLine($"Invalid number! You must use {expected} numbers.");
                return false;
            }
            
            return true;
        }
        
        protected override void ExecuteMove(Move move)
        {
            base.ExecuteMove(move);
            var numMove = move as NumericalMove;
            usedNumbers.Add(numMove.Number);
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
        
        protected override bool IsGameOver()
        {
            return gameOver;
        }
        
        protected override bool HasWinner()
        {
            return winner != null;
        }
        
        protected override Player GetWinner()
        {
            return winner;
        }
        
        protected override string GetGameName()
        {
            return "Numerical Tic-Tac-Toe";
        }
        
        public override void UndoMove()
        {
            if (moveHistory.CanUndo())
            {
                Move move = moveHistory.Undo();
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
    }
}