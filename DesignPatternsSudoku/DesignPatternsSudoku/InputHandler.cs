using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku
{
    public class InputHandler
    {
        private GameController _gameController;
        public InputHandler(GameController gameController)
        {
            _gameController = gameController;
        }

        public bool GetInput()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            if (char.IsDigit(key.KeyChar))
            {
                _gameController.EnterNumber(int.Parse((key.KeyChar - '0').ToString()));
            }
            else
            {
                HandleNonDigitKey(key);
            }
            if (_gameController.PuzzleView.CheckGameEnd())
            {
                _gameController.PuzzleView.Print();
                return true;
            }
            else
            {
                _gameController.PuzzleView.Print();
                return false;
            }
        }

        private void HandleNonDigitKey(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.W:
                    _gameController.MovePlayer(new Coord(_gameController.Player.Coords.X - 1, _gameController.Player.Coords.Y));
                    break;
                case ConsoleKey.D:
                    _gameController.MovePlayer(new Coord(_gameController.Player.Coords.X, _gameController.Player.Coords.Y + 1));
                    break;
                case ConsoleKey.S:
                    _gameController.MovePlayer(new Coord(_gameController.Player.Coords.X + 1, _gameController.Player.Coords.Y));
                    break;
                case ConsoleKey.A:
                    _gameController.MovePlayer(new Coord(_gameController.Player.Coords.X, _gameController.Player.Coords.Y - 1));
                    break;
                case ConsoleKey.C:
                    Visitor visitor = new Visitor();
                    _gameController.Puzzle.Accept(visitor);
                    break;
                case ConsoleKey.Spacebar:
                    _gameController.PuzzleView.ModeState.ChangeState();
                    break;
                default: break;
            }
        }
    }
}
