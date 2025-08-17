using System;

namespace BoardGameFramework
{
    public class CommandParser
    {
        public Move ParseMove(string input, Player player)
        {
            // Generic move parsing - can be overridden
            return null;
        }
        
        public NumericalMove ParseNumericalMove(string input, Player player)
        {
            try
            {
                string[] parts = input.Trim().Split(' ');
                
                if (parts.Length != 3)
                {
                    return null;
                }
                
                int row = int.Parse(parts[0]);
                int col = int.Parse(parts[1]);
                int number = int.Parse(parts[2]);
                
                return new NumericalMove(row, col, number, player);
            }
            catch
            {
                return null;
            }
        }
    }
}