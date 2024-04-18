using System;
using DesignPatternsSudoku.Models.Composite;
using DesignPatternsSudoku.Models.Factory;
using DesignPatternsSudoku.Models.Strategy;
using Xunit;


namespace TestProject
{
    public class CompositeTests
    {
        [Fact]
        public void Cell_Check_ReturnsTrueWhenCorrectValueMatchesEnteredValue()
        {
            // Arrange
            var cell = new Cell(new Coord(0, 0), 5, new List<int> { 1, 2, 3, 4, 5 });

            // Act
            bool result = cell.Check();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Cell_Check_ReturnsFalseWhenCorrectValueDoesNotMatchEnteredValue()
        {
            // Arrange
            var cell = new Cell(new Coord(0, 0), 5, new List<int> { 1, 2, 3, 4, 5 });
            cell.EnteredValue = 3;

            // Act
            bool result = cell.Check();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Cell_GetLeafs_ReturnsItselfInList()
        {
            // Arrange
            var cell = new Cell(new Coord(0, 0), 5, new List<int> { 1, 2, 3, 4, 5 });

            // Act
            List<Cell> leafs = cell.GetLeafs();

            // Assert
            Assert.Single(leafs);
            Assert.Equal(cell, leafs[0]);
        }

        [Fact]
        public void Cluster_Check_ReturnsTrueWhenAllChildrenAreValid()
        {
            // Arrange
            var cell1 = new Cell(new Coord(0, 0), 1, new List<int> { 1 });
            var cell2 = new Cell(new Coord(0, 1), 2, new List<int> { 2 });
            var cluster = new Cluster();
            cluster.Add(cell1);
            cluster.Add(cell2);

            // Act
            bool result = cluster.Check();

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Cluster_Check_ReturnsFalseWhenAnyChildIsInvalid()
        {
            // Arrange
            var cell1 = new Cell(new Coord(0, 0), 1, new List<int> { 1 });
            var cell2 = new Cell(new Coord(0, 1), 2, new List<int> { 2 });
            var cluster = new Cluster();
            cluster.Add(cell1);
            cluster.Add(cell2);
            cell2.EnteredValue = 3;

            // Act
            bool result = cluster.Check();

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Cluster_GetLeafs_ReturnsAllLeafsFromChildren()
        {
            // Arrange
            var cell1 = new Cell(new Coord(0, 0), 1, new List<int> { 1 });
            var cell2 = new Cell(new Coord(0, 1), 2, new List<int> { 2 });
            var cluster = new Cluster();
            cluster.Add(cell1);
            cluster.Add(cell2);

            // Act
            List<Cell> leafs = cluster.GetLeafs();

            // Assert
            Assert.Equal(2, leafs.Count);
            Assert.Contains(cell1, leafs);
            Assert.Contains(cell2, leafs);
        }
    }
}
