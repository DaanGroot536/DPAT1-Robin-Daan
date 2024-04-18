using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Models.Strategy
{
    public interface IFileReader
    {
        public SudokuFileInfo ReadFile(string path);
    }
}
