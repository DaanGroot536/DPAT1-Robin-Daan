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
            int cellWidth = 3;
            int cellHeight = 2;

            for (int y = 0; y < puzzleView.Puzzle.FileInfo.Size; y++)
            {
                for (int z = 0; z < puzzleView.Puzzle.FileInfo.ClusterHeight; z++)
                {
                    for (int x = 0; x < puzzleView.Puzzle.FileInfo.Size; x++)
                    {
                        Cell cell = leafs.Find(leaf => leaf.Coord.X == y && leaf.Coord.Y == x);
                        for (int i = 1; i <= puzzleView.Puzzle.FileInfo.ClusterWidth; i++)
                        {
                            if (puzzleView.Player.getXCoord() == y && puzzleView.Player.getYCoord() == x)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            if (cell != null)
                            {
                                if (cell.EnteredValue > 0)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkGray;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                                if (cell.PossibleNumbers.FirstOrDefault(num => num == (i + (puzzleView.Puzzle.FileInfo.ClusterWidth * z))) == 0)
                                {
                                    if (cell.EnteredValue == (i + (puzzleView.Puzzle.FileInfo.ClusterWidth * z)))
                                    {
                                        Console.Write($"{cell.EnteredValue}".PadLeft(cellWidth));
                                    }
                                    else
                                    {
                                        Console.Write(" ".PadLeft(cellWidth));
                                    }
                                }
                                else
                                {
                                    if (cell.EnteredValue == 0)
                                    {
                                        Console.Write((i + (puzzleView.Puzzle.FileInfo.ClusterWidth * z)).ToString().PadLeft(cellWidth));
                                    }
                                    else
                                    {
                                        Console.Write(" ".PadLeft(cellWidth));
                                    }
                                }
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write(" ".PadLeft(cellWidth));
                            }
                        }
                        Console.Write("|");
                    }
                    Console.WriteLine();
                }

                // Print horizontal dividers
                for (int i = 0; i < puzzleView.Puzzle.FileInfo.Size * (cellWidth * puzzleView.Puzzle.FileInfo.ClusterWidth + 1) - 1; i++)
                {
                    Console.Write("-");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Hulpgetallen stand - gebruik de spatiebalk om te wisselen");
        }
    }
}
