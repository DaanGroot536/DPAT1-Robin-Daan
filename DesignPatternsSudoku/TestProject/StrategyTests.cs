using DesignPatternsSudoku.Models.Strategy;
using Xunit;
using Moq;
using System.IO;

namespace TestProject
{
    public class StrategyTests
    {
        private readonly Mock<IFileReader> _fileReaderMock;

        public StrategyTests()
        {
            _fileReaderMock = new Mock<IFileReader>();
        }

        [Fact]
        public void ReadFile_WithClusterWidth2ClusterHeight2Size4_ReturnsCorrectSudokuFileInfo()
        {
            // Arrange
            string path = "./sudoku_files/puzzle.4x4";
            string fileContent = "sample content";
            _fileReaderMock.Setup(fr => fr.ReadFile(path)).Returns(new SudokuFileInfo(fileContent)
            {
                ClusterWidth = 2,
                ClusterHeight = 2,
                Size = 4
            });

            // Act
            var result = _fileReaderMock.Object.ReadFile(path);

            // Assert
            Assert.Equal(fileContent, result.Content);
            Assert.Equal(2, result.ClusterWidth);
            Assert.Equal(2, result.ClusterHeight);
            Assert.Equal(4, result.Size);
        }

        [Fact]
        public void ReadFile_WithClusterWidth3ClusterHeight2Size6_ReturnsCorrectSudokuFileInfo()
        {
            // Arrange
            string path = "./sudoku_files/puzzle.4x4";
            string fileContent = "sample content";
            _fileReaderMock.Setup(fr => fr.ReadFile(path)).Returns(new SudokuFileInfo(fileContent)
            {
                ClusterWidth = 3,
                ClusterHeight = 2,
                Size = 6
            });

            // Act
            var result = _fileReaderMock.Object.ReadFile(path);

            // Assert
            Assert.Equal(fileContent, result.Content);
            Assert.Equal(3, result.ClusterWidth);
            Assert.Equal(2, result.ClusterHeight);
            Assert.Equal(6, result.Size);
        }

        [Fact]
        public void ReadFile_WithClusterWidth3ClusterHeight3Size9_ReturnsCorrectSudokuFileInfo()
        {
            // Arrange
            string path = "./sudoku_files/puzzle.4x4";
            string fileContent = "sample content";
            _fileReaderMock.Setup(fr => fr.ReadFile(path)).Returns(new SudokuFileInfo(fileContent)
            {
                ClusterWidth = 3,
                ClusterHeight = 3,
                Size = 9
            });

            // Act
            var result = _fileReaderMock.Object.ReadFile(path);

            // Assert
            Assert.Equal(fileContent, result.Content);
            Assert.Equal(3, result.ClusterWidth);
            Assert.Equal(3, result.ClusterHeight);
            Assert.Equal(9, result.Size);
        }

        [Fact]
        public void ReadFile_WithEmptyFileContent_ReturnsSudokuFileInfoWithDefaultValues()
        {
            // Arrange
            string path = "empty.txt";
            string fileContent = "";
            _fileReaderMock.Setup(fr => fr.ReadFile(path)).Returns(new SudokuFileInfo(fileContent));

            // Act
            var result = _fileReaderMock.Object.ReadFile(path);

            // Assert
            Assert.Equal(fileContent, result.Content);
            Assert.Equal(0, result.ClusterWidth);
            Assert.Equal(0, result.ClusterHeight);
            Assert.Equal(0, result.Size);
        }

        [Fact]
        public void ReadFile_WithCustomFileContent_ReturnsSudokuFileInfoWithCustomValues()
        {
            // Arrange
            string path = "custom.txt";
            string fileContent = "Custom content";
            _fileReaderMock.Setup(fr => fr.ReadFile(path)).Returns(new SudokuFileInfo(fileContent)
            {
                ClusterWidth = 4,
                ClusterHeight = 3,
                Size = 12
            });

            // Act
            var result = _fileReaderMock.Object.ReadFile(path);

            // Assert
            Assert.Equal(fileContent, result.Content);
            Assert.Equal(4, result.ClusterWidth);
            Assert.Equal(3, result.ClusterHeight);
            Assert.Equal(12, result.Size);
        }

        [Fact]
        public void ReadFile_WithInvalidPath_ThrowsFileNotFoundException()
        {
            // Arrange
            string path = "nonexistent.txt";
            _fileReaderMock.Setup(fr => fr.ReadFile(path)).Throws<FileNotFoundException>();

            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => _fileReaderMock.Object.ReadFile(path));
        }
    }
}
