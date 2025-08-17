using System;

namespace BoardGameFramework
{
    /// <summary>
    /// FACTORY METHOD PATTERN - Abstract creator
    /// Addresses feedback: "Extensibility/reusability could be improved"
    /// </summary>
    public abstract class GameFactory
    {
        // Factory Method - deferred to subclasses
        public abstract Game CreateGame();
        public abstract string GetGameName();
        public abstract string GetGameDescription();
        
        // Template method using factory method
        public Game InitializeNewGame()
        {
            Console.WriteLine($"\nInitializing {GetGameName()}...");
            Console.WriteLine(GetGameDescription());
            Console.WriteLine();
            
            Game game = CreateGame();
            return game;
        }
    }
}