using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Models.Strategy
{
    internal class JigsawFileReader : IFileReader
    {
        public SudokuFileInfo ReadFile(string path)
        {
            string fileContent = File.ReadAllText(path);

            SudokuFileInfo fileInfo = new(fileContent)
            {
                Size = 9,
                ClusterHeight = 3,
                ClusterWidth = 3,
                FileExtension = path.Split(".").Reverse().First()
        };
            return fileInfo;
        }
    }
}
