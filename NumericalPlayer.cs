using System;

namespace BoardGameFramework
{
    /// <summary>
    /// Numerical-specific player with odd/even number constraints
    /// </summary>
    public class NumericalPlayer : HumanPlayer, INumberedPlayer
    {
        public bool UsesOddNumbers { get; private set; }
        
        public NumericalPlayer(string name, bool usesOddNumbers) : base(name)
        {
            UsesOddNumbers = usesOddNumbers;
            commandParser = new CommandParser();
        }
        
        public override Move ParseMove(string input, Board board)
        {
            return commandParser.ParseNumericalMove(input, this);
        }
    }
}