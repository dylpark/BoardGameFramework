using BoardGameFramework.Core;

namespace BoardGameFramework.Players
{
    public class ComputerPlayer(string name) : Player(name)
    {
        protected Random random = new();

        public override Move? ParseMove(string input, Board board)
        {
            System.Threading.Thread.Sleep(1000); // Thinking delay
            return null; // Will be overridden by NumericalComputerPlayer
        }
    }
}