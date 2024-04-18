using DesignPatternsSudoku.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
