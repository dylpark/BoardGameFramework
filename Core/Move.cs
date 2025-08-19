using BoardGameFramework.Players;

namespace BoardGameFramework.Core
{
    public abstract class Move
    {
        public Player Player { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        
        protected Move(int row, int col, Player player)
        {
            Row = row;
            Col = col;
            Player = player;
        }
    }
}