using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku.Models.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternsSudoku.Models.Builder
{
    public class NormalBuilder : IBuilder
    {
        protected Puzzle PuzzleInstance { get; set; }
        protected SudokuFileInfo FileInfo;
        protected Cell[,] Grid;

        public NormalBuilder(SudokuFileInfo fileInfo)
        {
            PuzzleInstance = new Puzzle(fileInfo);
            FileInfo = fileInfo;
            Grid = new Cell[fileInfo.Size, fileInfo.Size];
            InitializeGrid(fileInfo.Content);
            InitializeClusters();
            DeterminePossibleNumbers(PuzzleInstance);
        }

        public virtual void InitializeGrid(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                int row = i / FileInfo.Size;
                int col = i % FileInfo.Size;
                Cell cell = new Cell(
                    new Coord(row, col),
                    int.Parse(input[i].ToString()),
                    Enumerable.Range(1, FileInfo.Size).ToList()
                );
                Grid[row, col] = cell;
            }
        }

        public Puzzle GetPuzzle()
        {
            return this.PuzzleInstance;
        }

        public void DeterminePossibleNumbers(Cluster mainCluster)
        {
            foreach (Cluster cluster in mainCluster.Children)
            {
                if (cluster is Puzzle)
                {
                    List<IComponent> clusters = cluster.Children;

                    foreach (Cluster composite in clusters)
                    {
                        DeterminePossibleNumbers(composite);
                    }
                }
                else
                {
                    List<Cell> cells = cluster.GetLeafs();
                    List<int> existingNumbers = new List<int>();

                    foreach (Cell cell in cells)
                    {
                        if (cell.EnteredValue != 0 && !existingNumbers.Contains(cell.EnteredValue))
                        {
                            existingNumbers.Add(cell.EnteredValue);
                        }
                    }
                    foreach (Cell cell in cells)
                    {
                        cell.PossibleNumbers = cell.PossibleNumbers.Except(existingNumbers).ToList();
                    }
                }
            }
        }

        public virtual void InitializeClusters()
        {
            for (int x = 0; x < FileInfo.Size; x++)
            {
                Cluster rowCluster = new Cluster();
                Cluster colCluster = new Cluster();
                Cluster blockCluster = new Cluster();
                for (int y = 0; y < FileInfo.Size; y++)
                {
                    int blockX = x / FileInfo.ClusterHeight * FileInfo.ClusterHeight + y / FileInfo.ClusterWidth;
                    int blockY = x % FileInfo.ClusterHeight * FileInfo.ClusterWidth + y % FileInfo.ClusterWidth;

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
                PuzzleInstance.Add(rowCluster);
                PuzzleInstance.Add(colCluster);
                PuzzleInstance.Add(blockCluster);
            }
        }
    }
}
