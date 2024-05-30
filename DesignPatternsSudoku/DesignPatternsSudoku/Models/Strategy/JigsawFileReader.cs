using System;
using System.IO;

namespace DesignPatternsSudoku.Models.Strategy
{
    internal class JigsawFileReader : IFileReader
    {
        public SudokuFileInfo ReadFile(string path)
        {
            string fileContent = File.ReadAllText(path);

            SudokuFileInfo fileInfo = new SudokuFileInfo(fileContent)
            {
                Size = 9,
                ClusterHeight = 3,
                ClusterWidth = 3,
                FileExtension = Path.GetExtension(path)
            };
            return fileInfo;
        }
    }
}
