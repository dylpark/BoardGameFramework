using BoardGameFramework.Core;
using BoardGameFramework.Interfaces;

namespace BoardGameFramework.Players
{
    public class ComputerPlayer : Player
    {
        protected Random random;

        public ComputerPlayer(string name) : base(name)
        {
            random = new Random();
        }

        public override Move? ParseMove(string input, Board board)
        {
            System.Threading.Thread.Sleep(1000); // Thinking delay
            return null; // Will be overridden by NumericalComputerPlayer
        }
    }

    /// <summary>
    /// Computer player specifically for Numerical Tic-Tac-Toe
    /// </summary>
    public class NumericalComputerPlayer : ComputerPlayer, INumberedPlayer
    {
        public bool UsesOddNumbers { get; private set; }

        public NumericalComputerPlayer(string name, bool usesOddNumbers) : base(name)
        {
            UsesOddNumbers = usesOddNumbers;
            this.Name = name + " (Computer)";
        }

        public override Move? ParseMove(string input, Board board)
        {
            return null; // Logic handled in ProcessPlayerTurn
        }
    }
}