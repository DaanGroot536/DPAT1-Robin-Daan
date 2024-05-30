using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Puzzles;
using DesignPatternsSudoku.Models.State;
using System;

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

        public void ChangeMode(ModeState nextState)
        {
            ModeState = nextState;
        }

        public void Print()
        {
            ModeState.Print();
        }
    }
}
