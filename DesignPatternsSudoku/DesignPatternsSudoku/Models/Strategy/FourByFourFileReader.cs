using System;
using System.IO;

namespace DesignPatternsSudoku.Models.Strategy
{
    internal class FourByFourFileReader : IFileReader
    {
        public SudokuFileInfo ReadFile(string path)
        {
            string fileContent = File.ReadAllText(path);

            SudokuFileInfo fileInfo = new SudokuFileInfo(fileContent)
            {
                ClusterWidth = 2,
                ClusterHeight = 2,
                Size = 4,
                FileExtension = Path.GetExtension(path)
            };
            return fileInfo;
        }
    }
}
