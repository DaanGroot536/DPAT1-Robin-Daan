using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Models.Builder
{
    public interface IBuilder
    {
        public abstract void CreateClusters();
        public abstract void CreateGrid(string input);
        public void SetPossibleNumbers(Cluster mainCluster);
        public Puzzle GetPuzzle();
    }
}
