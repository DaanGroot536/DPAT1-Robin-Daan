using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Models.Puzzles
{
    public class Puzzle : Cluster
    {
        public SudokuFileInfo FileInfo { get; set; }

        public Puzzle(SudokuFileInfo fileInfo) : base()
        {
            FileInfo = fileInfo;
        }
    }
}
