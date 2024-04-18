using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku.Models.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Models.Builder
{
    public class Builder : IBuilder
    {
        protected Puzzle _puzzle { get; set; }
        protected SudokuFileInfo _fileInfo;
        protected Cell[,] _grid;
        public Builder(SudokuFileInfo fileInfo)
        {
            _puzzle = new Puzzle(fileInfo);
            _fileInfo = fileInfo;
            _grid = new Cell[fileInfo.Size, fileInfo.Size];
            CreateGrid(fileInfo.Content);
            CreateClusters();
            SetPossibleNumbers(_puzzle);
        }
        
        public virtual void CreateGrid(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int row = i / _fileInfo.Size;
                int col = i % _fileInfo.Size;
                Cell cell = new Cell(
                    new Coord(row, col),
                    int.Parse(input[i].ToString()),
                    Enumerable.Range(1, _fileInfo.Size).ToList()
                );
                _grid[row, col] = cell;
            }
        }

        public Puzzle GetPuzzle()
        {
            return this._puzzle;
        }

        public void SetPossibleNumbers(Cluster mainCluster)
        {
            foreach (Cluster cluster in mainCluster.Children)
            {
                if (cluster is Puzzle)
                {
                    List<IComponent> clusters = cluster.Children;

                    foreach (Cluster composit in clusters)
                    {
                        SetPossibleNumbers(composit);
                    }
                }
                else
                {
                    List<Cell> cells = cluster.GetLeafs();
                    List<int> givenNumbers = new List<int>();

                    foreach (Cell cell in cells)
                    {
                        if (cell.EnteredValue != 0 && !givenNumbers.Contains(cell.EnteredValue))
                        {
                            givenNumbers.Add(cell.EnteredValue);
                        }
                    }
                    foreach (Cell cell in cells)
                    {
                        cell.PossibleNumbers = cell.PossibleNumbers.Except(givenNumbers).ToList();
                    }
                }
            }
        }

        public virtual void CreateClusters()
        {
            for (int x = 0; x < _fileInfo.Size; x++)
            {
                Cluster row = new Cluster();
                Cluster col = new Cluster();
                Cluster block = new Cluster();
                for (int y = 0; y < _fileInfo.Size; y++)
                {
                    int blockX = x / _fileInfo.ClusterHeight * _fileInfo.ClusterHeight + y / _fileInfo.ClusterWidth;
                    int blockY = x % _fileInfo.ClusterHeight * _fileInfo.ClusterWidth + y % _fileInfo.ClusterWidth;

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
                _puzzle.Add(row);
                _puzzle.Add(col);
                _puzzle.Add(block);
            }
        }
    }
}
