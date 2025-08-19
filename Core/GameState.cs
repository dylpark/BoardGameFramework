using System.Text.Json.Serialization;

namespace BoardGameFramework.Core
{
    /// <summary>
    /// Represents the complete state of a game for serialization
    /// </summary>
    public class GameState
    {
        public string GameType { get; set; } = string.Empty;
        public DateTime SaveDate { get; set; }
        public string Version { get; set; } = "1.0";
        
        // Board state (using jagged array for JSON serialization)
        public int[][] BoardGrid { get; set; } = [];
        public int BoardRows { get; set; }
        public int BoardCols { get; set; }
        
        // Game state
        public int CurrentPlayerIndex { get; set; }
        public bool GameOver { get; set; }
        public string? WinnerName { get; set; }
        
        // Players
        public List<PlayerState> Players { get; set; } = new();
        
        // Move history for undo/redo
        public List<MoveState> MoveHistory { get; set; } = new();
        public List<MoveState> RedoHistory { get; set; } = new();
        
        // Game-specific data (for extensibility)
        public Dictionary<string, object> GameSpecificData { get; set; } = new();
    }
    
    /// <summary>
    /// Represents a player's state for serialization
    /// </summary>
    public class PlayerState
    {
        public string Name { get; set; } = string.Empty;
        public string PlayerType { get; set; } = string.Empty; // "Human", "Computer", etc.
        public bool UsesOddNumbers { get; set; } // For numerical games
        public Dictionary<string, object> AdditionalData { get; set; } = new();
    }
    
    /// <summary>
    /// Represents a move's state for serialization
    /// </summary>
    public class MoveState
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public int Number { get; set; } // For numerical moves
        public string MoveType { get; set; } = string.Empty; // "Numerical", etc.
        public Dictionary<string, object> AdditionalData { get; set; } = new();
    }
}