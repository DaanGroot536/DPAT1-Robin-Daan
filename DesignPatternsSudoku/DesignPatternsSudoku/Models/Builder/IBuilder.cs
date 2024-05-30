using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Puzzles;

namespace DesignPatternsSudoku.Models.Builder
{
    public interface IBuilder
    {
        void InitializeClusters();
        void InitializeGrid(string input);
        void DeterminePossibleNumbers(Cluster mainCluster);
        Puzzle GetPuzzle();
    }
}
