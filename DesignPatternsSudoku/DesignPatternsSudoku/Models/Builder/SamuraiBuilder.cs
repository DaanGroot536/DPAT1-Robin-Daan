using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku.Models.Strategy;

namespace DesignPatternsSudoku.Models.Builder
{
    public class SamuraiBuilder : Builder
    {
        private int SubSudokuSize;
        private int SubSudokuWidth;
        private int SubSudokuHeight;

        private readonly Coord[] StartPositions = new Coord[]
        {
            new Coord(0, 0),   // top left
            new Coord(0, 12),  // top right
            new Coord(6, 6),   // middle
            new Coord(12, 0),  // bottom left
            new Coord(12, 12)  // bottom right
        };

        public SamuraiBuilder(SudokuFileInfo fileInfo) : base(fileInfo)
        {
            SubSudokuSize = 81;
            SubSudokuWidth = 9;
            SubSudokuHeight = 9;
            InitializeGrid(fileInfo.Content);
            InitializeClusters();
            DeterminePossibleNumbers(PuzzleInstance);
        }

        public override void InitializeGrid(string input)
        {
            string[] subsudokus = new string[StartPositions.Length];
            for (int i = 0; i < StartPositions.Length; i++)
            {
                subsudokus[i] = input.Substring(i * SubSudokuSize, SubSudokuSize);
            }

            for (int i = 0; i < StartPositions.Length; i++)
            {
                for (int j = 0; j < SubSudokuSize; j++)
                {
                    int row = StartPositions[i].X + j / SubSudokuWidth;
                    int col = StartPositions[i].Y + j % SubSudokuHeight;
                    int value = int.Parse(subsudokus[i][j].ToString());
                    Grid[row, col] = new Cell(new Coord(row, col), value, Enumerable.Range(1, FileInfo.Size).ToList());
                }
            }
        }

        public override void InitializeClusters()
        {
            for (int i = 0; i < StartPositions.Length; i++)
            {
                int startX = StartPositions[i].X;
                int startY = StartPositions[i].Y;

                for (int x = startX; x < startX + SubSudokuWidth; x++)
                {
                    Cluster rowCluster = new Cluster();
                    Cluster colCluster = new Cluster();
                    Cluster blockCluster = new Cluster();

                    for (int y = startY; y < startY + SubSudokuHeight; y++)
                    {
                        int blockX = startX + y / 3;
                        int blockY = startY + y % 3;

                        Cell rowCell = Grid[x, y];
                        Cell colCell = Grid[y, x];
                        Cell blockCell = Grid[blockX, blockY];

                        rowCell.AddCluster(rowCluster);
                        colCell.AddCluster(colCluster);
                        blockCell.AddCluster(blockCluster);

                        rowCluster.Add(rowCell);
                        colCluster.Add(colCell);
                        blockCluster.Add(blockCell);
                    }

                    PuzzleInstance.Children.Add(rowCluster);
                    PuzzleInstance.Children.Add(colCluster);
                    PuzzleInstance.Children.Add(blockCluster);
                }
            }
        }
    }
}
