using System;
using System.IO;

namespace DesignPatternsSudoku.Models.Strategy
{
    public class SamuraiFileReader : IFileReader
    {
        public SudokuFileInfo ReadFile(string path)
        {
            string fileContent = File.ReadAllText(path);

            string cleanedFileContent = fileContent.Replace("\n", "").Replace("\r", "");

            SudokuFileInfo fileInfo = new SudokuFileInfo(cleanedFileContent)
            {
                ClusterWidth = 3,
                ClusterHeight = 3,
                Size = 21,
                FileExtension = Path.GetExtension(path)
            };
            return fileInfo;
        }
    }
}
