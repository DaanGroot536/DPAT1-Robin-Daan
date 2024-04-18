using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using DesignPatternsSudoku.Models.State;
using DesignPatternsSudoku.Views;
using Xunit;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku;
using DesignPatternsSudoku.Models.Strategy;

namespace TestProject
{
    public class StateTests
    {
        [Fact]
        public void PuzzleView_Initialization()
        {
            // Arrange
            SudokuFileInfo fileInfo = new SudokuFileInfo("123456789");
            Puzzle puzzle = new Puzzle(fileInfo);
            Player player = new Player();

            // Act
            PuzzleView puzzleView = new PuzzleView(puzzle, player);

            // Assert
            Assert.NotNull(puzzleView);
            Assert.Equal(puzzle, puzzleView.Puzzle);
            Assert.Equal(player, puzzleView.Player);
        }

        [Fact]
        public void PuzzleView_ChangeMode()
        {
            // Arrange
            SudokuFileInfo fileInfo = new SudokuFileInfo("123456789");
            Puzzle puzzle = new Puzzle(fileInfo);
            Player player = new Player();
            PuzzleView puzzleView = new PuzzleView(puzzle, player);
            ModeState nextState = new HelpModeState(puzzleView);

            // Act
            puzzleView.changeMode(nextState);

            // Assert
            Assert.Equal(nextState, puzzleView.ModeState);
        }
    }
}
