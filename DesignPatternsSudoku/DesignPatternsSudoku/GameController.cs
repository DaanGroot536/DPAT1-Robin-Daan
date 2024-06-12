using DesignPatternsSudoku.Models.Builder;
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

            string filePath = "./../../../sudoku_files/puzzle.4x4";
            SudokuFileInfo fileInfo = factory.CreateFileInfo(filePath);

            if (filePath.EndsWith(".samurai", StringComparison.OrdinalIgnoreCase))
            {
                SamuraiBuilder builder = new SamuraiBuilder(fileInfo);
                builder.InitializeGrid(fileInfo.Content);
                builder.InitializeClusters();
                builder.DeterminePossibleNumbers(builder.GetPuzzle());
                //builder.SetCorrectValues();
                _puzzle = builder.GetPuzzle();
            }
            else if (filePath.EndsWith(".jigsaw", StringComparison.OrdinalIgnoreCase))
            {
                JigsawBuilder builder = new JigsawBuilder(fileInfo);
                builder.InitializeGrid(fileInfo.Content);
                builder.InitializeClusters();
                builder.DeterminePossibleNumbers(builder.GetPuzzle());
                _puzzle = builder.GetPuzzle();
            }
            else
            {
                NormalBuilder builder = new NormalBuilder(fileInfo);
                builder.InitializeGrid(fileInfo.Content);
                builder.InitializeClusters();
                builder.DeterminePossibleNumbers(builder.GetPuzzle());
                builder.SetCorrectValues();
                _puzzle = builder.GetPuzzle();
            }

            PuzzleView = new PuzzleView(_puzzle, Player);
            PuzzleView.Print();

            Start();
        }

        private void Start()
        {
            while (_playing)
            {
                if (inputHandler.GetInput())
                    _playing = false;
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
