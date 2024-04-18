using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Models.State
{
    public class FinalModeState : ModeState
    {
        public FinalModeState(PuzzleView puzzleView) : base(puzzleView)
        {
        }

        public override void ChangeState()
        {
            puzzleView.changeMode(new HelpModeState(puzzleView));
        }

        public override void Print()
        {
            Console.Clear();
            List<Cell> leafs = puzzleView.Puzzle.GetLeafs();

            for (int x = 0; x < puzzleView.Puzzle.FileInfo.Size; x++)
            {
                if (x % puzzleView.Puzzle.FileInfo.ClusterHeight == 0)
                {
                    for (int i = 0; i < puzzleView.Puzzle.FileInfo.Size * 3.5; i++)
                    {
                        Console.Write($"-");
                    }
                    Console.WriteLine();
                }
                for (int y = 0; y < puzzleView.Puzzle.FileInfo.Size; y++)
                {
                    Cell cell = leafs.Find(leaf => leaf.Coord.X == x && leaf.Coord.Y == y);
                    if (y % puzzleView.Puzzle.FileInfo.ClusterWidth == 0)
                    {
                        Console.Write($"|");
                    }

                    if (puzzleView.Player.getXCoord() == x && puzzleView.Player.getYCoord() == y)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    if (cell == null)
                    {
                        Console.Write($" . ");
                    }
                    else
                    {
                        if (cell.EnteredValue > 0)
                        {
                            if (puzzleView.Player.getXCoord() == x && puzzleView.Player.getYCoord() == y)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                            if (cell != null)
                            {
                                Console.Write($" {cell.EnteredValue} ");
                            }
                        }
                        else
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
                            Console.Write($"   ");
                        }
                    }

                    Console.ResetColor();
                }
                Console.Write($"|");
                Console.WriteLine();
            }
            for (int i = 0; i < puzzleView.Puzzle.FileInfo.Size * 3.5; i++)
            {
                Console.Write($"-");
            }
            Console.WriteLine();
            Console.WriteLine("Definitieve stand - gebruik de spatiebalk om te wisselen");
        }
    }
}
