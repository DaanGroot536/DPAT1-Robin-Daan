using System;
using DesignPatternsSudoku.Models.Factory;
using DesignPatternsSudoku.Models.Strategy;
using Xunit;

namespace TestProject
{
    public class FactoryTests
    {
        [Fact]
        public void CreateFileInfo_ValidFilePath_ReturnsSudokuFileInfo()
        {
            // Arrange
            var factory = new FileReaderFactory();
            var filePath = "./sudoku_files/puzzle.4x4";

            // Act
            var fileInfo = factory.CreateFileInfo(filePath);

            // Assert
            Assert.NotNull(fileInfo);
            Assert.IsType<SudokuFileInfo>(fileInfo);
        }

        [Fact]
        public void CreateFileInfo_WithUnsupportedExtension_ThrowsArgumentException()
        {
            // Arrange
            var factory = new FileReaderFactory();
            var filePath = "./sudoku_files/puzzle.3x3";

            // Act
            var exception = Assert.Throws<ArgumentException>(() => factory.CreateFileInfo(filePath));

            // Assert
            Assert.Equal($"FileReader 3x3 is not supported", exception.Message);
        }

        [Fact]
        public void CreateFileInfo_WithUnknownExtension_ThrowsArgumentException()
        {
            // Arrange
            var factory = new FileReaderFactory();
            var filePath = "puzzle.unknown";

            // Act
            var exception = Assert.Throws<ArgumentException>(() => factory.CreateFileInfo(filePath));

            // Assert
            Assert.Equal($"FileReader unknown is not supported", exception.Message);
        }
    }
}
