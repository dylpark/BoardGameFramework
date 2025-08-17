using System;
using System.Collections.Generic;
using System.Linq;

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
            // Computer doesn't parse input - it generates moves
            System.Threading.Thread.Sleep(1000); // Thinking delay
            return null; // Will be overridden by NumericalComputerPlayer
        }
    }
    
    /// <summary>
    /// Computer player specifically for Numerical Tic-Tac-Toe
    /// </summary>
    public class NumericalComputerPlayer : ComputerPlayer
    {
        public bool UsesOddNumbers { get; private set; }
        
        public NumericalComputerPlayer(string name, bool usesOddNumbers) : base(name)
        {
            UsesOddNumbers = usesOddNumbers;
            this.Name = name + " (Computer)";
        }
        
        public override Move ParseMove(string input, Board board)
        {
            // This won't be called - logic is in NumericalTicTacToeGame.ProcessPlayerTurn
            return null;
        }
    }
}