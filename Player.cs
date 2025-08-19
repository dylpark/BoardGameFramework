namespace BoardGameFramework
{
    /// <summary>
    /// Abstract Player base class - part of original design
    /// </summary>
    public abstract class Player(string name)
    {
        public string Name { get; protected set; } = name;

        public abstract Move? ParseMove(string input, Board board);
    }
}