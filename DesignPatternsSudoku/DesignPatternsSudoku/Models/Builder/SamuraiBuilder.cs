using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku.Models.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternsSudoku.Models.Builder
{
    public class SamuraiBuilder : NormalBuilder
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
                        int blockX = startX + (y / 3);
                        int blockY = startY + (y % 3);

                        if (Grid[x, y] != null)
                        {
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
                    }

                    PuzzleInstance.Children.Add(rowCluster);
                    PuzzleInstance.Children.Add(colCluster);
                    PuzzleInstance.Children.Add(blockCluster);
                }
            }
        }

        public override void SetCorrectValues()
        {
            int[,] solutionGrid = new int[FileInfo.Size, FileInfo.Size];
            for (int row = 0; row < FileInfo.Size; row++)
            {
                for (int col = 0; col < FileInfo.Size; col++)
                {
                    if (Grid[row, col] != null)
                    {
                        solutionGrid[row, col] = Grid[row, col].EnteredValue;
                    }
                }
            }

            if (Solve(solutionGrid, 0, 0))
            {
                for (int row = 0; row < FileInfo.Size; row++)
                {
                    for (int col = 0; col < FileInfo.Size; col++)
                    {
                        if (Grid[row, col] != null)
                        {
                            Grid[row, col].CorrectValue = solutionGrid[row, col];
                        }
                    }
                }
            }
            else
            {
                throw new Exception("No solution found for the given Samurai Sudoku puzzle.");
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

            foreach (var num in Grid[row, col].PossibleNumbers)
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
