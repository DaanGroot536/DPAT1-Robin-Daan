using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Views;
using System;
using System.Collections.Generic;

namespace DesignPatternsSudoku.Models.State
{
    public class CorrectionModeState : ModeState
    {
        public CorrectionModeState(PuzzleView puzzleView) : base(puzzleView)
        {
        }

        public override void ChangeState()
        {
            puzzleView.ChangeMode(new FinalModeState(puzzleView));
        }

        public override void Print()
        {
            Console.Clear();
            var leafs = puzzleView.Puzzle.GetLeafs();

            for (int x = 0; x < puzzleView.Puzzle.FileInfo.Size; x++)
            {
                if (x % puzzleView.Puzzle.FileInfo.ClusterHeight == 0)
                {
                    Console.WriteLine(new string('-', puzzleView.Puzzle.FileInfo.Size * 3));
                }

                for (int y = 0; y < puzzleView.Puzzle.FileInfo.Size; y++)
                {
                    var cell = leafs.Find(leaf => leaf.Coord.X == x && leaf.Coord.Y == y);
                    if (y % puzzleView.Puzzle.FileInfo.ClusterWidth == 0)
                    {
                        Console.Write("|");
                    }

                    SetConsoleColors(cell, x, y);

                    Console.Write(cell != null && cell.EnteredValue > 0
                        ? $" {cell.EnteredValue} "
                        : "   ");

                    Console.ResetColor();
                }

                Console.WriteLine("|");
            }

            Console.WriteLine(new string('-', puzzleView.Puzzle.FileInfo.Size * 3));
            Console.WriteLine("\nCorrectie stand - gebruik de spatiebalk om te wisselen");
            Console.WriteLine("Gebruik 'C' om de cellen te checken");
        }

        private void SetConsoleColors(Cell cell, int x, int y)
        {
            if (puzzleView.Player.getXCoord() == x && puzzleView.Player.getYCoord() == y)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (cell != null && !cell.IsValid)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ResetColor();
            }
        }
    }
}
