using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Models.Strategy
{
    internal class FourByFourFileReader : IFileReader
    {
        public SudokuFileInfo ReadFile(string path)
        {
            string fileContent = File.ReadAllText(path);

            SudokuFileInfo fileInfo = new(fileContent)
            {
                ClusterWidth = 2,
                ClusterHeight = 2,
                Size = 4,
                FileExtension = path.Split(".").Reverse().First()
            };
            return fileInfo;
        }
    }
}
