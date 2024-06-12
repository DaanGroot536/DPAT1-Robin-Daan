using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Visitor;

namespace TestProject
{
    public class VisitorTests
    {
        [Fact]
        public void VisitCluster_WhenClusterIsPuzzle_ShouldSetIsValidToTrueForAllCells()
        {
            // Arrange
            Cluster puzzle = new Cluster();
            List<IComponent> clusters = new List<IComponent>();
            List<Cell> cells = new List<Cell>();
            foreach (var cluster in clusters)
            {
                cells.AddRange(cluster.GetLeafs());
            }

            var visitor = new Visitor();

            // Act
            visitor.VisitCluster(puzzle);

            // Assert
            foreach (var cell in cells)
            {
                Assert.True(cell.IsValid);
            }
        }

        [Fact]
        public void VisitCluster_WhenClusterIsNotPuzzle_ShouldValidateUniqueCellValues()
        {
            // Arrange
            Cluster cluster = new Cluster();
            List<Cell> cells = cluster.GetLeafs();
            HashSet<int> uniqueValues = new HashSet<int>();
            foreach (var cell in cells)
            {
                if (uniqueValues.Contains(cell.EnteredValue))
                {
                    cell.IsValid = true;
                    Cell duplicateCell = cells.Find(c => c.EnteredValue == cell.EnteredValue && c != cell);
                    if (duplicateCell != null)
                    {
                        duplicateCell.IsValid = true;
                    }
                }
                else
                {
                    if (cell.EnteredValue != 0)
                    {
                        uniqueValues.Add(cell.EnteredValue);
                    }
                }
            }

            var visitor = new Visitor();

            // Act
            visitor.VisitCluster(cluster);

            // Assert
            foreach (var cell in cells)
            {
                if (uniqueValues.Contains(cell.EnteredValue))
                {
                    Assert.False(cell.IsValid);
                    Cell duplicateCell = cells.Find(c => c.EnteredValue == cell.EnteredValue && c != cell);
                    if (duplicateCell != null)
                    {
                        Assert.False(duplicateCell.IsValid);
                    }
                }
                else
                {
                    Assert.True(cell.IsValid);
                }
            }
        }
    }
    public class VisitorCellTests
    {
        [Fact]
        public void Check_CorrectValueEqualToEnteredValue_ReturnsTrue()
        {
            // Arrange
            Cell cell = new Cell(new Coord(0, 0), 1, new List<int>());

            // Act
            bool result = cell.Check();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Check_CorrectValueNotEqualToEnteredValue_ReturnsFalse()
        {
            // Arrange
            Cell cell = new Cell(new Coord(0, 0), 1, new List<int>());
            cell.EnteredValue = 2;

            // Act
            bool result = cell.Check();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetLeafs_SingleCell_ReturnsItself()
        {
            // Arrange
            Cell cell = new Cell(new Coord(0, 0), 1, new List<int>());

            // Act
            List<Cell> leafs = cell.GetLeafs();

            // Assert
            Assert.Single(leafs);
            Assert.Equal(cell, leafs[0]);
        }
    }
}
