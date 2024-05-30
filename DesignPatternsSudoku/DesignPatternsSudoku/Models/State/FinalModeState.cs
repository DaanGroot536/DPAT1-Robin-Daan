using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Views;
using System;
using System.Collections.Generic;

namespace DesignPatternsSudoku.Models.State
{
    public class FinalModeState : ModeState
    {
        private PuzzleView puzzleView;

        public FinalModeState(PuzzleView puzzleView) : base(puzzleView)
        {
            this.puzzleView = puzzleView;
        }

        public override void ChangeState()
        {
            puzzleView.ChangeMode(new HelpModeState(puzzleView));
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
                    if (y % puzzleView.Puzzle.FileInfo.ClusterWidth == 0)
                    {
                        Console.Write("|");
                    }

                    SetConsoleColors(leafs, x, y);

                    var cell = leafs.Find(leaf => leaf.Coord.X == x && leaf.Coord.Y == y);
                    Console.Write(cell != null && cell.EnteredValue > 0
                        ? $" {cell.EnteredValue} "
                        : "   ");

                    Console.ResetColor();
                }

                Console.WriteLine("|");
            }

            Console.WriteLine(new string('-', puzzleView.Puzzle.FileInfo.Size * 3));
            Console.WriteLine("\nDefinitieve stand - gebruik de spatiebalk om te wisselen");
        }

        private void SetConsoleColors(List<Cell> leafs, int x, int y)
        {
            if (puzzleView.Player.getXCoord() == x && puzzleView.Player.getYCoord() == y)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.ResetColor();
            }
        }
    }
}
