using DesignPatternsSudoku.Models.Builder;
using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Factory;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku.Models.Strategy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class BuilderTests
    {
        private SudokuFileInfo _fileInfo;
        private SamuraiBuilder _samuraiBuilder;

        public BuilderTests()
        {
            FileReaderFactory factory = new FileReaderFactory();
            _fileInfo = factory.CreateFileInfo("./sudoku_files/puzzle.samurai");
            _samuraiBuilder = new SamuraiBuilder(_fileInfo);
        }

        [Fact]
        public void CreateGrid_ValidInput_CreatesGridWithCorrectValues()
        {
            // Arrange

            // Act
            _samuraiBuilder.CreateGrid(_fileInfo.Content);
            Puzzle puzzle = _samuraiBuilder.GetPuzzle();

            // Assert
            Assert.NotNull(puzzle);
            Assert.NotNull(puzzle.Children);
            Assert.Equal(8, puzzle.Children[0].GetLeafs()[0].EnteredValue); // Top left cell value
            Assert.Equal(7, puzzle.Children[1].GetLeafs()[2].EnteredValue); // Top right cell value
            Assert.Equal(0, puzzle.Children[2].GetLeafs()[4].EnteredValue); // Middle cell value
        }

        [Fact]
        public void CreateClusters_ValidGrid_CreatesClustersWithCorrectStructure()
        {
            // Arrange

            // Act
            _samuraiBuilder.CreateGrid(_fileInfo.Content);
            _samuraiBuilder.CreateClusters();
            Puzzle puzzle = _samuraiBuilder.GetPuzzle();

            // Assert
            Assert.NotNull(puzzle);
            Assert.NotNull(puzzle.Children);
            Assert.Equal(270, puzzle.Children.Count);
        }

        //[Fact]
        //public void SetPossibleNumbers_ValidPuzzle_SetsPossibleNumbersCorrectly()
        //{
        //    // Arrange

        //    // Act
        //    _samuraiBuilder.CreateGrid(_fileInfo.Content);
        //    _samuraiBuilder.CreateClusters();
        //    _samuraiBuilder.SetPossibleNumbers(_samuraiBuilder.GetPuzzle());
        //    Puzzle puzzle = _samuraiBuilder.GetPuzzle();

        //    // Assert
        //    Assert.NotNull(puzzle);
        //    Assert.NotNull(puzzle.Children);
        //    Assert.Equal(8, puzzle.Children[0].GetLeafs()[0].EnteredValue);
        //    Assert.Equal(new List<int> { 1, 2, 4, 5, 9 }, puzzle.Children[0].GetLeafs()[0].PossibleNumbers);

        //    // Add more assertions for other cells in the grid
        //    // ...

        //    // Verify the clusters' leaf cells have the correct possible numbers
        //    Assert.Equal(new List<int> { 1, 2, 4, 5, 6, 7, 9 }, puzzle.Children[0].GetLeafs()[0].PossibleNumbers);
        //    Assert.Equal(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 }, puzzle.Children[0].GetLeafs()[1].PossibleNumbers);

        //}

    }
}
