namespace BoardGameFramework
{
    /// <summary>
    /// Abstract Player base class - part of original design
    /// </summary>
    public abstract class Player
    {
        public string Name { get; protected set; }
        
        protected Player(string name)
        {
            Name = name;
        }
        
        public abstract Move? ParseMove(string input, Board board);
    }
}