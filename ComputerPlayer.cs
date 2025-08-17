using System;

namespace BoardGameFramework
{
    public class ComputerPlayer : Player
    {
        protected Random random;
        
        public ComputerPlayer(string name) : base(name)
        {
            random = new Random();
        }
        
        public override Move ParseMove(string input, Board board)
        {
            Console.WriteLine($"{Name} (Computer) is thinking...");
            System.Threading.Thread.Sleep(1000);
            return GenerateRandomMove(board);
        }
        
        protected virtual Move GenerateRandomMove(Board board)
        {
            // Find all empty positions
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board.IsEmptyPosition(row, col))
                    {
                        // For numerical game, pick a valid number
                        int number = random.Next(1, 10);
                        return new NumericalMove(row, col, number, this);
                    }
                }
            }
            return null;
        }

        
    }
}