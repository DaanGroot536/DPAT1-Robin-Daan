using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsSudoku.Models.Visitor;

namespace DesignPatternsSudoku.Models.Composite
{
    public class Cell : IComponent
    {
        private List<int> _possibleNumbers;

        public List<int> PossibleNumbers
        {
            get { return _possibleNumbers; }
            set { _possibleNumbers = value; }
        }

        private List<Cluster> clusters;

        public List<Cluster> Clusters
        {
            get { return clusters; }
            set { clusters = value; }
        }

        private int _correctValue;

        public int CorrectValue
        {
            get { return _correctValue; }
            set { _correctValue = value; }
        }

        private int _enteredValue;

        public int EnteredValue
        {
            get { return _enteredValue; }
            set { _enteredValue = value; }
        }

        private bool _isGiven;

        public bool IsGiven
        {
            get { return _isGiven; }
            set { _isGiven = value; }
        }

        private Coord _coord;

        public Coord Coord
        {
            get { return _coord; }
            set { _coord = value; }
        }

        private bool _isValid;

        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }



        public bool Check()
        {
            if (_correctValue == _enteredValue)
            {
                return true;
            }
            return false;
        }

        public List<Cell> GetLeafs()
        {
            List<Cell> leafs = new List<Cell>();
            leafs.Add(this);
            return leafs;
        }

        public void Accept(IComponentVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public void AddCluster(Cluster cluster)
        {
            clusters.Add(cluster);
        }

        public Cell(Coord coord, int startValue, List<int> possibleNumbers)
        {
            if (startValue != 0)
            {
                _correctValue = startValue;
                _isGiven = true;
            }
            _enteredValue = startValue;
            _coord = coord;
            _isValid = true;
            _possibleNumbers = possibleNumbers;
            clusters = new List<Cluster>();
        }
    }
}
