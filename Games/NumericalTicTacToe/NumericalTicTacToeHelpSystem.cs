using System;
using BoardGameFramework.Services;

namespace BoardGameFramework.Games.NumericalTicTacToe
{
    /// <summary>
    /// Concrete HelpSystem for Numerical Tic-Tac-Toe
    /// Extends HelpSystem as per Assignment 1 feedback
    /// </summary>
    public class NumericalTicTacToeHelpSystem : HelpSystem
    {
        protected override void DisplayRules()
        {
            Console.WriteLine("\nGame Rules:");
            Console.WriteLine("  • Board is a 3x3 grid");
            Console.WriteLine("  • Player 1 uses ODD numbers: 1, 3, 5, 7, 9");
            Console.WriteLine("  • Player 2 uses EVEN numbers: 2, 4, 6, 8");
            Console.WriteLine("  • Each number can only be used ONCE");
            Console.WriteLine("  • WIN: Make any row, column, or diagonal sum to 15");
            Console.WriteLine("  • DRAW: All positions filled with no winner");
        }
        
        protected override void DisplayExamples()
        {
            Console.WriteLine("\nHow to Play:");
            Console.WriteLine("  Enter: row col number");
            Console.WriteLine("  Example: '0 0 5' places 5 at top-left");
            Console.WriteLine("  Example: '1 1 8' places 8 at center");
            Console.WriteLine("  Example: '2 2 3' places 3 at bottom-right");
            Console.WriteLine("\nWinning Example:");
            Console.WriteLine("  Row: 2 + 7 + 6 = 15");
            Console.WriteLine("  Column: 4 + 5 + 6 = 15");
            Console.WriteLine("  Diagonal: 8 + 5 + 2 = 15");
        }
    }
}