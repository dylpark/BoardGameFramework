using System;

namespace BoardGameFramework.Services
{
    /// <summary>
    /// Abstract HelpSystem - Addresses feedback:
    /// "Game and HelpSystem likely require extending for a specific game"
    /// </summary>
    public abstract class HelpSystem
    {
        // Template method for help display
        public void DisplayHelp()
        {
            Console.WriteLine("\n===== HELP MENU =====");
            DisplayCommands();
            DisplayRules();
            DisplayExamples();
            Console.WriteLine("=====================\n");
        }
        
        // Common commands for all games
        protected virtual void DisplayCommands()
        {
            Console.WriteLine("\nCommon Commands:");
            Console.WriteLine("  help         - Show this help menu");
            Console.WriteLine("  undo         - Undo last move");
            Console.WriteLine("  redo         - Redo undone move");
            Console.WriteLine("  save <file>  - Save game to file");
            Console.WriteLine("  load <file>  - Load game from file");
            Console.WriteLine("  quit/exit    - Exit the game");
        }
        
        // Game-specific rules - must be implemented
        protected abstract void DisplayRules();
        protected abstract void DisplayExamples();
    }
}