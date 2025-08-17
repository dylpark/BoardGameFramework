namespace BoardGameFramework
{
    public class MoveHistory
    {
        private List<Move> moves = new List<Move>();

        public void AddMove(Move move)
        {
            moves.Add(move);
        }

        public Move? GetLastMove()
        {
            return moves.Count > 0 ? moves.Last() : null;
        }

        public void RemoveLastMove()
        {
            if (moves.Count > 0)
                moves.RemoveAt(moves.Count - 1);
        }

        public List<Move> GetAllMoves()
        {
            return new List<Move>(moves);
        }

        public void SetMoves(List<Move> newMoves)
        {
            moves = new List<Move>(newMoves);
        }

        public bool CanUndo => moves.Count > 0;
    }
}
