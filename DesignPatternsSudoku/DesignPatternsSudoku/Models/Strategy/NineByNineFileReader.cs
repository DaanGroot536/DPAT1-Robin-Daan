using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Models.Strategy
{
    internal class NineByNineFileReader : IFileReader
    {
        public SudokuFileInfo ReadFile(string path)
        {
            string fileContent = File.ReadAllText(path);

            SudokuFileInfo fileInfo = new(fileContent)
            {
                ClusterWidth = 3,
                ClusterHeight = 3,
                Size = 9,
                FileExtension = path.Split(".").Reverse().First()
            };
            return fileInfo;
        }
    }
}
