using BoardGameFramework;

namespace BoardGameFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameConsole = new GameConsole();
            gameConsole.StartGameSession();
        }
    }
}