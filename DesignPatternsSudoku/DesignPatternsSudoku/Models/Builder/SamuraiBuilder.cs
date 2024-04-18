using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku.Models.Strategy;

namespace DesignPatternsSudoku.Models.Builder
{
    public class SamuraiBuilder : Builder
    {
        private int _subSudokuSize;
        private int _subSudokuWidth;
        private int _subSudokuHeight;

        Coord[] _startPositions = new Coord[]
        {
            new Coord(0, 0),   // top left
            new Coord(0, 12),  // top right
            new Coord(6, 6),   // middle
            new Coord(12, 0),  // bottom left
            new Coord(12, 12)  // bottom right
        };

        public SamuraiBuilder(SudokuFileInfo fileInfo) : base(fileInfo)
        {
            _subSudokuSize = 81;
            _subSudokuWidth = 9;
            _subSudokuHeight = 9;
            CreateGrid(fileInfo.Content);
            CreateClusters();
            SetPossibleNumbers(_puzzle);
        }
        override
        public void CreateGrid(string input)
        {
            string[] subsudokus = new string[_startPositions.Length];
            for (int i = 0; i < _startPositions.Length; i++)
            {
                subsudokus[i] = input.Substring(i * _subSudokuSize, _subSudokuSize);
            }

            for (int i = 0; i < _startPositions.Length; i++)
            {
                for (int j = 0; j < _subSudokuSize; j++)
                {
                    int row = _startPositions[i].X + j / _subSudokuWidth;
                    int col = _startPositions[i].Y + j % _subSudokuHeight;
                    int value = int.Parse(subsudokus[i][j].ToString());
                    _grid[row, col] = new Cell(new Coord(row, col), value, Enumerable.Range(1, _fileInfo.Size).ToList());
                }
            }
        }
        override
        public void CreateClusters()
        {
            for (int i = 0; i < _startPositions.Length; i++)
            {
                int startX = _startPositions[i].X;
                int startY = _startPositions[i].Y;

                for (int x = startX; x < startX + _subSudokuWidth; x++)
                {
                    Cluster row = new Cluster();
                    Cluster col = new Cluster();
                    Cluster block = new Cluster();

                    for (int y = startY; y < startY + _subSudokuHeight; y++)
                    {
                        int blockX = startX + y / 3;
                        int blockY = startY + y % 3;

                        Cell rowCell = _grid[x, y];
                        Cell colCell = _grid[y, x];
                        Cell blockCell = _grid[blockX, blockY];

                        rowCell.AddCluster(row);
                        colCell.AddCluster(col);
                        blockCell.AddCluster(block);

                        row.Add(rowCell);
                        col.Add(colCell);
                        block.Add(blockCell);
                    }

                    _puzzle.Children.Add(row);
                    _puzzle.Children.Add(col);
                    _puzzle.Children.Add(block);
                }
            }
        }
    }
}
