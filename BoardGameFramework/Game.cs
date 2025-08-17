using System.Data;
using System.Numerics;

namespace BoardGameFramework
{
    // Template Method Pattern - Abstract base class extracting commonalities
    public abstract class Game
    {
        protected List<Player> players = new List<Player>();
        protected Board board;
        protected int currentPlayerIndex = 0;
        protected Player? winner = null;
        protected MoveHistory moveHistory;
        protected GameSaver gameSaver;
        protected HelpSystem helpSystem;

        public Game()
        {
            board = new Board();
            moveHistory = new MoveHistory();
            gameSaver = new GameSaver();
            helpSystem = CreateHelpSystem(); // Factory method for help system
        }

        // Template Method - defines common game algorithm
        public void PlayGame()
        {
            InitializeGame();

            while (!IsGameOver())
            {
                DisplayBoard();
                ProcessPlayerTurn();
                if (CheckWinCondition())
                {
                    winner = GetCurrentPlayer();
                    break;
                }
                SwitchPlayer();
            }

            EndGame();
        }

        // Abstract methods - vary by specific game
        protected abstract bool CheckWinCondition();
        protected abstract bool IsValidMove(Move move);
        protected abstract HelpSystem CreateHelpSystem();
        protected abstract void DisplayGameSpecificInfo();

        // Common methods - extracted commonalities
        protected virtual void InitializeGame()
        {
            Console.WriteLine($"\nGame started! {players.Count} players");
            helpSystem.ShowWelcome();
        }

        protected virtual void DisplayBoard()
        {
            Console.WriteLine();
            board.Display();
            DisplayGameSpecificInfo();
        }

        protected virtual void ProcessPlayerTurn()
        {
            var currentPlayer = GetCurrentPlayer();
            Console.WriteLine($"\n{currentPlayer.Name}: Your turn");

            while (true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                var command = CommandParser.ParseCommand(input);
                if (HandleCommand(command, currentPlayer)) break;
            }
        }

        protected virtual bool HandleCommand(Command command, Player currentPlayer)
        {
            switch (command.Type)
            {
                case CommandType.Quit:
                    Environment.Exit(0);
                    return false;
                case CommandType.Help:
                    helpSystem.ShowHelp();
                    return false;
                case CommandType.Save:
                    gameSaver.SaveGame(this, command.Parameters[0]);
                    return false;
                case CommandType.Load:
                    gameSaver.LoadGame(this, command.Parameters[0]);
                    return false;
                case CommandType.Undo:
                    UndoMove();
                    return false;
                case CommandType.Move:
                    var move = currentPlayer.GetMove(command, board);
                    if (move != null && IsValidMove(move))
                    {
                        board.MakeMove(move);
                        moveHistory.AddMove(move);
                        return true;
                    }
                    Console.WriteLine("Invalid move. Try again.");
                    return false;
                default:
                    Console.WriteLine("Unknown command. Type 'help' for commands.");
                    return false;
            }
        }

        protected virtual void UndoMove()
        {
            var lastMove = moveHistory.GetLastMove();
            if (lastMove != null)
            {
                board.UndoMove(lastMove);
                moveHistory.RemoveLastMove();
                SwitchPlayer();
                Console.WriteLine("Move undone");
            }
            else
            {
                Console.WriteLine("No moves to undo");
            }
        }

        protected virtual void SwitchPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }

        protected virtual bool IsGameOver()
        {
            return winner != null || board.IsFull();
        }

        protected virtual void EndGame()
        {
            DisplayBoard();
            if (winner != null)
                Console.WriteLine($"\n{winner.Name} wins!");
            else
                Console.WriteLine("\nGame is a draw!");
        }

        // Public methods
        public Player GetCurrentPlayer() => players[currentPlayerIndex];
        public void AddPlayer(Player player) => players.Add(player);
        public Board GetBoard() => board;
        public MoveHistory GetMoveHistory() => moveHistory;
        public int GetCurrentPlayerIndex() => currentPlayerIndex;
        public void SetCurrentPlayerIndex(int index) => currentPlayerIndex = index;
    }
}