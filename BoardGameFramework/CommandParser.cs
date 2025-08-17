namespace BoardGameFramework
{
    public enum CommandType { Move, Save, Load, Undo, Help, Quit, Invalid }

    public class Command
    {
        public CommandType Type { get; }
        public List<string> Parameters { get; }

        public Command(CommandType type, List<string> parameters)
        {
            Type = type;
            Parameters = parameters;
        }
    }

    public static class CommandParser
    {
        public static Command ParseCommand(string input)
        {
            var parts = input.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return new Command(CommandType.Invalid, new List<string>());

            var parameters = parts.Skip(1).ToList();

            return parts[0] switch
            {
                "move" => new Command(CommandType.Move, parameters),
                "save" => new Command(CommandType.Save, parameters),
                "load" => new Command(CommandType.Load, parameters),
                "undo" => new Command(CommandType.Undo, parameters),
                "help" => new Command(CommandType.Help, parameters),
                "quit" => new Command(CommandType.Quit, parameters),
                _ => new Command(CommandType.Invalid, parameters)
            };
        }
    }
}