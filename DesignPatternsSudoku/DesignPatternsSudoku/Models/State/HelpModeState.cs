using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Models.State
{
    public class HelpModeState : ModeState
    {
        public HelpModeState(PuzzleView puzzleView) : base(puzzleView)
        {
        }

        public override void ChangeState()
        {
            puzzleView.changeMode(new CorrectionModeState(puzzleView));
        }

        public override void Print()
        {
            Console.Clear();
            List<Cell> leafs = puzzleView.Puzzle.GetLeafs();
            for (int y = 0; y < puzzleView.Puzzle.FileInfo.Size; y++)
            {
                for (int z = 0; z < puzzleView.Puzzle.FileInfo.ClusterHeight; z++)
                {
                    for (int x = 0; x < puzzleView.Puzzle.FileInfo.Size; x++)
                    {
                        Cell cell = leafs.Find(leaf => leaf.Coord.X == y && leaf.Coord.Y == x);
                        for (int i = 1; i <= puzzleView.Puzzle.FileInfo.ClusterWidth; i++)
                        {

                            if (cell != null)
                            {
                                if (cell.EnteredValue > 0)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                                if (cell.PossibleNumbers.FirstOrDefault(x => x == (i + (puzzleView.Puzzle.FileInfo.ClusterWidth * z))) == 0)
                                {
                                    if (cell.EnteredValue == (i + (puzzleView.Puzzle.FileInfo.ClusterWidth * z)))
                                    {
                                        Console.Write($"{cell.EnteredValue}");
                                    }
                                    else
                                    {
                                        Console.Write(" ");
                                    }
                                }
                                else
                                {
                                    if (cell.EnteredValue == 0)
                                    {
                                        Console.Write(i + (puzzleView.Puzzle.FileInfo.ClusterWidth * z));
                                    }
                                    else
                                    {
                                        Console.Write(" ");
                                    }
                                }
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write(".");
                            }

                            if (puzzleView.Player.getXCoord() == y && puzzleView.Player.getYCoord() == x)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }

                        }
                        Console.Write("|");
                    }
                    Console.WriteLine("");
                }
                for (int i = 0; i < puzzleView.Puzzle.FileInfo.Size * 4; i++)
                {
                    Console.Write($"-");
                }
                Console.WriteLine();
            }
            Console.WriteLine("");
            Console.WriteLine("Hulpgetallen stand - gebruik de spatiebalk om te wisselen");
        }
    }
}
