using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Views;

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

                    if (cell != null)
                    {
                        if (cell.EnteredValue > 0)
                        {
                            if (puzzleView.Player.getXCoord() == x && puzzleView.Player.getYCoord() == y)
                            {
                                Console.BackgroundColor = ConsoleColor.Yellow;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            else if (!cell.IsValid)
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            Console.Write($" {cell.EnteredValue} ");
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
                    else
                    {
                        Console.Write($" . ");
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

            Console.WriteLine("");
            Console.WriteLine("Correctie stand - gebruik de spatiebalk om te wisselen");
            Console.WriteLine("Gebruik 'C' om de cellen te checken");
        }
    }
}
