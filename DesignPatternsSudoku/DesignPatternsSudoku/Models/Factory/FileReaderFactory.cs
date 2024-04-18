using DesignPatternsSudoku.Models.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsSudoku.Models.Puzzles;

namespace DesignPatternsSudoku.Models.Factory
{
    public class FileReaderFactory
    {
        private readonly Dictionary<string, Func<IFileReader>> _puzzlesMappings = new()
        {
            {"4x4", () => new FourByFourFileReader()},
            {"6x6", () => new SixBySixFileReader()},
            {"9x9", () => new NineByNineFileReader()},
            {"jigsaw", () => new JigsawFileReader()},
            {"samurai", () => new SamuraiFileReader()}
        };

        public SudokuFileInfo CreateFileInfo(string filePath)
        {
            var extension = filePath.Split(".").Reverse().First();
            Func<IFileReader> fileReaderCreator;

            if (_puzzlesMappings.TryGetValue(extension, out fileReaderCreator))
                return fileReaderCreator.Invoke().ReadFile(filePath);
            throw new ArgumentException($"FileReader {extension} is not supported");
        }
    }
}
