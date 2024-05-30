using DesignPatternsSudoku.Models.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku
{
    public class Player
    {
        private Coord _coords;

        public Coord Coords
        {
            get { return _coords; }
            set { _coords = value; }
        }

        public Player()
        {
            _coords = new Coord(0, 0);
        }

        public void Move(Coord coords)
        {
            Coords = coords;
        }

        public int getXCoord()
        {
            return _coords.X;
        }

        public int getYCoord()
        {
            return _coords.Y;
        }
    }
}
