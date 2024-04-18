using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsSudoku.Models.Visitor;

namespace DesignPatternsSudoku.Models.Composite
{
    public interface IComponent
    {
        public bool Check();

        public List<Cell> GetLeafs();
        void Accept(IComponentVisitor visitor);
    }
}
