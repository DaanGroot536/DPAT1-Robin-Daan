using DesignPatternsSudoku.Views;

namespace DesignPatternsSudoku.Models.State
{
    public abstract class ModeState
    {
        protected PuzzleView puzzleView;

        public ModeState(PuzzleView puzzleView)
        {
            this.puzzleView = puzzleView;
        }

        public abstract void Print();
        public abstract void ChangeState();
    }
}
