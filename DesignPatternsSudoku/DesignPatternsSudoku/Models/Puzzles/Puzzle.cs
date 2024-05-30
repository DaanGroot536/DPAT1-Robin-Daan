using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Strategy;

namespace DesignPatternsSudoku.Models.Puzzles
{
    public class Puzzle : Cluster
    {
        public SudokuFileInfo FileInfo { get; }

        public Puzzle(SudokuFileInfo fileInfo)
        {
            FileInfo = fileInfo;
        }
    }
}
