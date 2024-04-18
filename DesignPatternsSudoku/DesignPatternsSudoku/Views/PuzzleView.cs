using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku.Models.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Views
{
    public class PuzzleView
    {
        public ModeState ModeState { get; set; }

        public Puzzle Puzzle { get; set; }

        public Player Player { get; set; }

        public PuzzleView(Puzzle puzzle, Player player)
        {
            Puzzle = puzzle;
            Player = player;
            ModeState = new FinalModeState(this);
        }

        public void changeMode(ModeState nextState)
        {
            ModeState = nextState;
        }

        public void Print()
        {
            ModeState.Print();
        }
    }
}
