using System.Text.Json;

namespace BoardGameFramework
{
    public class GameSaver
    {
        public void SaveGame(Game game, string filename)
        {
            try
            {
                var gameState = new GameState
                {
                    BoardState = game.GetBoard().GetState(),
                    CurrentPlayerIndex = game.GetCurrentPlayerIndex(),
                    MoveHistory = game.GetMoveHistory().GetAllMoves()
                };

                string json = JsonSerializer.Serialize(gameState, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filename, json);
                Console.WriteLine($"Game saved to {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        public void LoadGame(Game game, string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    Console.WriteLine($"File {filename} not found");
                    return;
                }

                string json = File.ReadAllText(filename);
                var gameState = JsonSerializer.Deserialize<GameState>(json);

                if (gameState != null)
                {
                    game.GetBoard().SetState(gameState.BoardState);
                    game.SetCurrentPlayerIndex(gameState.CurrentPlayerIndex);
                    game.GetMoveHistory().SetMoves(gameState.MoveHistory);
                    Console.WriteLine($"Game loaded from {filename}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
            }
        }
    }

    public class GameState
    {
        public int[,] BoardState { get; set; } = new int[3, 3];
        public int CurrentPlayerIndex { get; set; }
        public List<Move> MoveHistory { get; set; } = new List<Move>();
    }
}