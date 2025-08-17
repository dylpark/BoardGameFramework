using System.Collections.Generic;

namespace BoardGameFramework
{
    public class MoveHistory
    {
        private Stack<Move> history;
        private Stack<Move> redoStack;
        
        public MoveHistory()
        {
            history = new Stack<Move>();
            redoStack = new Stack<Move>();
        }
        
        public void AddMove(Move move)
        {
            history.Push(move);
            redoStack.Clear();
        }
        
        public Move Undo()
        {
            if (CanUndo())
            {
                Move move = history.Pop();
                redoStack.Push(move);
                return move;
            }
            return null;
        }
        
        public Move Redo()
        {
            if (CanRedo())
            {
                Move move = redoStack.Pop();
                history.Push(move);
                return move;
            }
            return null;
        }
        
        public bool CanUndo()
        {
            return history.Count > 0;
        }
        
        public bool CanRedo()
        {
            return redoStack.Count > 0;
        }
        
        public void Clear()
        {
            history.Clear();
            redoStack.Clear();
        }
    }
}