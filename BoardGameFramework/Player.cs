using System.Data;

namespace BoardGameFramework
{
    public enum PlayerType { Human, Computer }

    public abstract class Player
    {
        public string Name { get; protected set; }
        public PlayerType Type { get; protected set; }

        protected Player(string name, PlayerType type)
        {
            Name = name;
            Type = type;
        }

        public abstract Move? GetMove(Command command, Board board);
    }

    public class HumanPlayer : Player
    {
        private static int humanPlayerCount = 0;

        public HumanPlayer() : base($"Player {++humanPlayerCount}", PlayerType.Human)
        {
        }

        public override Move? GetMove(Command command, Board board)
        {
            if (command.Type != CommandType.Move || command.Parameters.Count < 3)
                return null;

            if (int.TryParse(command.Parameters[0], out int row) &&
                int.TryParse(command.Parameters[1], out int col) &&
                int.TryParse(command.Parameters[2], out int value))
            {
                return new Move(row - 1, col - 1, value);
            }

            return null;
        }
    }

    public class ComputerPlayer : Player
    {
        private Random random = new Random();
        private static int computerPlayerCount = 0;

        public ComputerPlayer() : base($"Computer {++computerPlayerCount}", PlayerType.Computer)
        {
        }

        public override Move? GetMove(Command command, Board board)
        {
            var validMoves = GetValidMoves(board);
            if (validMoves.Count > 0)
            {
                var move = validMoves[random.Next(validMoves.Count)];
                Console.WriteLine($"Computer plays {move.Value} at ({move.Row + 1}, {move.Col + 1})");
                return move;
            }
            return null;
        }

        private List<Move> GetValidMoves(Board board)
        {
            var moves = new List<Move>();
            var usedNumbers = GetUsedNumbers(board);

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board.GetCell(row, col) == 0)
                    {
                        // Computer uses even numbers
                        for (int num = 2; num <= 8; num += 2)
                        {
                            if (!usedNumbers.Contains(num))
                                moves.Add(new Move(row, col, num));
                        }
                    }
                }
            }
            return moves;
        }

        private HashSet<int> GetUsedNumbers(Board board)
        {
            var used = new HashSet<int>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int value = board.GetCell(i, j);
                    if (value != 0) used.Add(value);
                }
            }
            return used;
        }
    }
}