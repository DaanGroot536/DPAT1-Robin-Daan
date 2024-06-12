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
            SetCorrectValues();
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

        private void SetCorrectValues()
        {
            int[,] solutionGrid = new int[FileInfo.Size, FileInfo.Size];
            for (int row = 0; row < FileInfo.Size; row++)
            {
                for (int col = 0; col < FileInfo.Size; col++)
                {
                    solutionGrid[row, col] = Grid[row, col].EnteredValue;
                }
            }

            if (Solve(solutionGrid, 0, 0))
            {
                for (int row = 0; row < FileInfo.Size; row++)
                {
                    for (int col = 0; col < FileInfo.Size; col++)
                    {
                        Grid[row, col].CorrectValue = solutionGrid[row, col];
                    }
                }
            }
            else
            {
                throw new Exception("No solution found for the given Sudoku puzzle.");
            }
        }

        private bool Solve(int[,] grid, int row, int col)
        {
            if (row == FileInfo.Size) return true;
            if (col == FileInfo.Size) return Solve(grid, row + 1, 0);

            if (grid[row, col] != 0)
            {
                return Solve(grid, row, col + 1);
            }

            foreach (var num in Enumerable.Range(1, FileInfo.Size))
            {
                if (IsValid(grid, row, col, num))
                {
                    grid[row, col] = num;
                    if (Solve(grid, row, col + 1))
                    {
                        return true;
                    }
                    grid[row, col] = 0;
                }
            }
            return false;
        }

        private bool IsValid(int[,] grid, int row, int col, int num)
        {
            for (int x = 0; x < FileInfo.Size; x++)
            {
                if (grid[row, x] == num || grid[x, col] == num)
                {
                    return false;
                }
            }

            int startRow = row / FileInfo.ClusterHeight * FileInfo.ClusterHeight;
            int startCol = col / FileInfo.ClusterWidth * FileInfo.ClusterWidth;

            for (int i = 0; i < FileInfo.ClusterHeight; i++)
            {
                for (int j = 0; j < FileInfo.ClusterWidth; j++)
                {
                    if (grid[startRow + i, startCol + j] == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
