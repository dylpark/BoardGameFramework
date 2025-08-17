namespace BoardGameFramework
{
    // Abstract base class - addresses feedback about HelpSystem requiring extending
    public abstract class HelpSystem
    {
        public virtual void ShowWelcome()
        {
            Console.WriteLine("Welcome to the Board Game Framework!");
            Console.WriteLine("Type 'help' for commands, 'quit' to exit");
        }

        public virtual void ShowHelp()
        {
            Console.WriteLine("\nGeneral Commands:");
            Console.WriteLine("help - Show this help");
            Console.WriteLine("quit - Exit the game");
            Console.WriteLine("save [filename] - Save current game");
            Console.WriteLine("load [filename] - Load saved game");
            Console.WriteLine("undo - Undo last move");

            ShowGameSpecificHelp();
        }

        protected abstract void ShowGameSpecificHelp();
    }

    // Concrete implementation for Numerical Tic-Tac-Toe
    public class NumericalTicTacToeHelpSystem : HelpSystem
    {
        protected override void ShowGameSpecificHelp()
        {
            Console.WriteLine("\nNumerical Tic-Tac-Toe Rules:");
            Console.WriteLine("move [row] [col] [number] - Place number at position");
            Console.WriteLine("- Player 1 uses odd numbers (1,3,5,7,9)");
            Console.WriteLine("- Player 2 uses even numbers (2,4,6,8)");
            Console.WriteLine("- Goal: Get three numbers in a row that sum to 15");
            Console.WriteLine("- Each number can only be used once");
            Console.WriteLine("- Positions are 1-3 for both row and column");
        }

        public override void ShowWelcome()
        {
            base.ShowWelcome();
            Console.WriteLine("Playing: Numerical Tic-Tac-Toe");
        }
    }
}