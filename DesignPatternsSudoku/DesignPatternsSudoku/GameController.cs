﻿using DesignPatternsSudoku.Models.Builder;
using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Factory;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku.Models.Strategy;
using DesignPatternsSudoku.Views;
using System.IO;
using DesignPatternsSudoku.Models.Visitor;

namespace DesignPatternsSudoku
{
    public class GameController
    {
        private Puzzle _puzzle;
        public Puzzle Puzzle
        {
            get { return _puzzle; }
            set { _puzzle = value; }
        }

        private InputHandler inputHandler;

        private bool _playing = true;

        public Player Player = new Player();
        public PuzzleView PuzzleView;
        public GameController()
        {
            inputHandler = new InputHandler(this);
            FileReaderFactory factory = new FileReaderFactory();


            SudokuFileInfo fileInfo = factory.CreateFileInfo("./sudoku_files/puzzle.9x9");

            _puzzle = new Builder(fileInfo).GetPuzzle();

            PuzzleView = new PuzzleView(_puzzle, Player);
            PuzzleView.Print();

            Start();
        }

        private void Start()
        {
            while (_playing)
            {
                inputHandler.GetInput();
            }
        }

        public void MovePlayer(Coord newCoord)
        {
            if (newCoord.X < 0 || newCoord.Y < 0) return;
            if (newCoord.X > _puzzle.FileInfo.Size - 1 || newCoord.Y > _puzzle.FileInfo.Size - 1) return;
            Player.Move(newCoord);
        }

        public void EnterNumber(int number)
        {
            if (number <= _puzzle.FileInfo.Size)
            {
                List<Cell> cells = _puzzle.GetLeafs();
                Cell cell = cells.Find(cell => cell.Coord.X == Player.Coords.X && cell.Coord.Y == Player.Coords.Y);
                if (!cell.IsGiven)
                {
                    cell.EnteredValue = number;
                    RemovePossibleNumberFromCellsInCluster(cell, number);
                }
            }
        }

        private void RemovePossibleNumberFromCellsInCluster(Cell cell, int number)
        {
            foreach (var c in cell.Clusters.SelectMany(parentCluster => parentCluster.Children.Cast<Cell>()))
            {
                c.PossibleNumbers.Remove(number);
            }
        }
    }
}