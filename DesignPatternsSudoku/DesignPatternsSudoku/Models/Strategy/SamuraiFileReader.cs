using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Models.Strategy
{
    public class SamuraiFileReader : IFileReader
    {
        public SudokuFileInfo ReadFile(string path)
        {
            string fileContent = File.ReadAllText(path);

            string cleanedFileContent = fileContent.Replace("\n", "").Replace("\r", "");

            SudokuFileInfo fileInfo = new SudokuFileInfo(cleanedFileContent);
            fileInfo.ClusterWidth = 3;
            fileInfo.ClusterHeight = 3;
            fileInfo.Size = 21;
            return fileInfo;
        }
    }
}
