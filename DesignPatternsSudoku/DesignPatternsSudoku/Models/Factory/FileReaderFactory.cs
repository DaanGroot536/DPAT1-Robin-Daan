using DesignPatternsSudoku.Models.Strategy;
using DesignPatternsSudoku.Models.Puzzles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesignPatternsSudoku.Models.Factory
{
    public class FileReaderFactory
    {
        private readonly Dictionary<string, Func<IFileReader>> PuzzlesMappings = new()
        {
            { "4x4", () => new FourByFourFileReader() },
            { "6x6", () => new SixBySixFileReader() },
            { "9x9", () => new NineByNineFileReader() },
            { "jigsaw", () => new JigsawFileReader() },
            { "samurai", () => new SamuraiFileReader() }
        };

        public SudokuFileInfo CreateFileInfo(string filePath)
        {
            var extension = GetFileExtension(filePath);
            if (PuzzlesMappings.TryGetValue(extension, out var fileReaderCreator))
                return fileReaderCreator.Invoke().ReadFile(filePath);
            throw new ArgumentException($"FileReader for '{extension}' is not supported");
        }

        private string GetFileExtension(string filePath)
        {
            return filePath.Split('.').LastOrDefault() ?? throw new ArgumentException("Invalid file path");
        }
    }
}
