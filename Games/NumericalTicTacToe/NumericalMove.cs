using BoardGameFramework.Core;
using BoardGameFramework.Players;

namespace BoardGameFramework.Games.NumericalTicTacToe
{
    public class NumericalMove : Move
    {
        public int Number { get; set; }
        
        public NumericalMove(int row, int col, int number, Player player) 
            : base(row, col, player)
        {
            Number = number;
        }
    }
}