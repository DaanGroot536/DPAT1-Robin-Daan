using System;
using System.IO;

namespace DesignPatternsSudoku.Models.Strategy
{
    internal class NineByNineFileReader : IFileReader
    {
        public SudokuFileInfo ReadFile(string path)
        {
            string fileContent = File.ReadAllText(path);

            SudokuFileInfo fileInfo = new SudokuFileInfo(fileContent)
            {
                ClusterWidth = 3,
                ClusterHeight = 3,
                Size = 9,
                FileExtension = Path.GetExtension(path)
            };
            return fileInfo;
        }
    }
}
