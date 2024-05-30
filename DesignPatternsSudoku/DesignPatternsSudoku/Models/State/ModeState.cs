using DesignPatternsSudoku.Views;

namespace DesignPatternsSudoku.Models.State
{
    public abstract class ModeState
    {
        protected PuzzleView PuzzleView;

        public ModeState(PuzzleView puzzleView)
        {
            PuzzleView = puzzleView;
        }

        public abstract void Print();
        public abstract void ChangeState();
    }
}
