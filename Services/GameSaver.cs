using System;
using System.IO;
using System.Text.Json;
using BoardGameFramework.Core;
using BoardGameFramework.Players;
using BoardGameFramework.Games.NumericalTicTacToe;
using BoardGameFramework.Interfaces;

namespace BoardGameFramework.Services
{
    public class GameSaver
    {
        private readonly JsonSerializerOptions _jsonOptions;
        
        public GameSaver()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        
        public void Save(Game game, Board board, MoveHistory history, string filename)
        {
            try
            {
                // Ensure filename has .json extension
                if (!filename.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                {
                    filename += ".json";
                }
                
                Console.WriteLine($"[GameSaver] Saving game to {filename}...");

                // Create game state object
                var gameState = CreateGameState(game, board, history);
                
                // Serialize to JSON
                string jsonString = JsonSerializer.Serialize(gameState, _jsonOptions);
                
                // Write to file
                File.WriteAllText(filename, jsonString);

                Console.WriteLine("Save successful!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Save failed: {ex.Message}");
            }
        }

        public void Load(Game game, Board board, MoveHistory history, string filename)
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
                    throw new FileNotFoundException($"Save file '{filename}' not found.");
                }

                Console.WriteLine($"[GameSaver] Loading game from {filename}...");

                // Read and deserialize JSON
                string jsonString = File.ReadAllText(filename);
                var gameState = JsonSerializer.Deserialize<GameState>(jsonString, _jsonOptions);
                
                if (gameState == null)
                {
                    throw new Exception("Failed to deserialize game state.");
                }
                
                // Restore game state
                RestoreGameState(game, board, history, gameState);

                Console.WriteLine("Load successful!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Load failed: {ex.Message}");
            }
        }
        
        private GameState CreateGameState(Game game, Board board, MoveHistory history)
        {
            var grid = board.GetGrid();
            var gameState = new GameState
            {
                GameType = game.GetGameName(),
                SaveDate = DateTime.Now,
                BoardRows = grid.GetLength(0),
                BoardCols = grid.GetLength(1),
                CurrentPlayerIndex = game.GetCurrentPlayerIndex(),
                GameOver = game.IsGameOver(),
                WinnerName = game.GetWinner()?.Name
            };
            
            // Convert 2D array to jagged array for JSON serialization
            gameState.BoardGrid = new int[gameState.BoardRows][];
            for (int i = 0; i < gameState.BoardRows; i++)
            {
                gameState.BoardGrid[i] = new int[gameState.BoardCols];
                for (int j = 0; j < gameState.BoardCols; j++)
                {
                    gameState.BoardGrid[i][j] = grid[i, j];
                }
            }
            
            // Save player states
            var players = game.GetPlayers();
            foreach (var player in players)
            {
                var playerState = new PlayerState
                {
                    Name = player.Name,
                    PlayerType = player.GetType().Name
                };
                
                // Handle numbered players
                if (player is INumberedPlayer numberedPlayer)
                {
                    playerState.UsesOddNumbers = numberedPlayer.UsesOddNumbers;
                }
                
                gameState.Players.Add(playerState);
            }
            
            // Save move history
            gameState.MoveHistory = SerializeMoveHistory(history.GetHistory());
            gameState.RedoHistory = SerializeMoveHistory(history.GetRedoHistory());
            
            // Add game-specific data for Numerical Tic-Tac-Toe
            if (game is NumericalTicTacToeGame numGame)
            {
                gameState.GameSpecificData["UsedNumbers"] = numGame.GetUsedNumbers().ToList();
            }
            
            return gameState;
        }
        
        private List<MoveState> SerializeMoveHistory(IEnumerable<Move> moves)
        {
            var moveStates = new List<MoveState>();
            
            foreach (var move in moves)
            {
                var moveState = new MoveState
                {
                    Row = move.Row,
                    Col = move.Col,
                    PlayerName = move.Player.Name,
                    MoveType = move.GetType().Name
                };
                
                // Handle numerical moves
                if (move is NumericalMove numMove)
                {
                    moveState.Number = numMove.Number;
                }
                
                moveStates.Add(moveState);
            }
            
            return moveStates;
        }
        
        private void RestoreGameState(Game game, Board board, MoveHistory history, GameState gameState)
        {
            // Validate game type
            if (gameState.GameType != game.GetGameName())
            {
                throw new Exception($"Save file is for {gameState.GameType}, but current game is {game.GetGameName()}");
            }
            
            // Restore board state
            var savedGrid = gameState.BoardGrid;
            for (int row = 0; row < gameState.BoardRows; row++)
            {
                for (int col = 0; col < gameState.BoardCols; col++)
                {
                    if (savedGrid[row][col] != 0)
                    {
                        // Find the player who made this move
                        var moveState = gameState.MoveHistory.FirstOrDefault(m => 
                            m.Row == row && m.Col == col && m.Number == savedGrid[row][col]);
                        
                        if (moveState != null)
                        {
                            var player = game.GetPlayers().FirstOrDefault(p => p.Name == moveState.PlayerName);
                            if (player != null)
                            {
                                var move = new NumericalMove(row, col, moveState.Number, player);
                                board.ApplyMove(move);
                            }
                        }
                    }
                }
            }
            
            // Restore move history
            history.Clear();
            foreach (var moveState in gameState.MoveHistory)
            {
                var player = game.GetPlayers().FirstOrDefault(p => p.Name == moveState.PlayerName);
                if (player != null)
                {
                    Move move = moveState.MoveType switch
                    {
                        "NumericalMove" => new NumericalMove(moveState.Row, moveState.Col, moveState.Number, player),
                        _ => throw new Exception($"Unknown move type: {moveState.MoveType}")
                    };
                    history.AddMove(move);
                }
            }
            
            // Restore game state
            game.SetCurrentPlayerIndex(gameState.CurrentPlayerIndex);
            game.SetGameOver(gameState.GameOver);
            
            if (!string.IsNullOrEmpty(gameState.WinnerName))
            {
                var winner = game.GetPlayers().FirstOrDefault(p => p.Name == gameState.WinnerName);
                game.SetWinner(winner);
            }
            
            // Restore game-specific data
            if (game is NumericalTicTacToeGame numGame && gameState.GameSpecificData.ContainsKey("UsedNumbers"))
            {
                if (gameState.GameSpecificData["UsedNumbers"] is JsonElement jsonElement)
                {
                    var usedNumbers = jsonElement.Deserialize<List<int>>();
                    if (usedNumbers != null)
                    {
                        numGame.RestoreUsedNumbers(usedNumbers);
                    }
                }
            }
        }
    }
}