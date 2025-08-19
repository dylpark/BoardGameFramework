namespace BoardGameFramework
{
    public class HumanPlayer : Player
    {
        protected CommandParser commandParser;
        
        public HumanPlayer(string name) : base(name)
        {
            commandParser = new CommandParser();
        }
        
        public override Move? ParseMove(string input, Board board)
        {
            return CommandParser.ParseMove(input, this);
        }
    }
}