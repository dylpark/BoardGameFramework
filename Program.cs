using System;
using BoardGameFramework.UI;

namespace BoardGameFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                GameConsole console = new GameConsole();
                console.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}