using System;
using System.IO;

namespace DesignPatternsSudoku.Models.Strategy
{
    internal class SixBySixFileReader : IFileReader
    {
        public SudokuFileInfo ReadFile(string path)
        {
            string fileContent = File.ReadAllText(path);

            SudokuFileInfo fileInfo = new SudokuFileInfo(fileContent)
            {
                ClusterWidth = 3,
                ClusterHeight = 2,
                Size = 6,
                FileExtension = Path.GetExtension(path)
            };
            return fileInfo;
        }
    }
}
