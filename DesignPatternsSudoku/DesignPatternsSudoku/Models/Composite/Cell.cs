using System;
using System.Collections.Generic;
using DesignPatternsSudoku.Models.Visitor;

namespace DesignPatternsSudoku.Models.Composite
{
    public class Cell : IComponent
    {
        public List<int> PossibleNumbers { get; set; }
        public List<Cluster> Clusters { get; set; }
        public int CorrectValue { get; set; }
        public int EnteredValue { get; set; }
        public bool IsGiven { get; set; }
        public Coord Coord { get; set; }
        public bool IsValid { get; set; }

        public Cell(Coord coord, int startValue, List<int> possibleNumbers)
        {
            Coord = coord;
            EnteredValue = startValue;
            PossibleNumbers = possibleNumbers;
            Clusters = new List<Cluster>();

            if (startValue != 0)
            {
                CorrectValue = startValue;
                IsGiven = true;
            }

            IsValid = true;
        }

        public bool Check()
        {
            return CorrectValue == EnteredValue;
        }

        public List<Cell> GetLeafs()
        {
            return new List<Cell> { this };
        }

        public void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public void AddCluster(Cluster cluster)
        {
            Clusters.Add(cluster);
        }
    }
}
