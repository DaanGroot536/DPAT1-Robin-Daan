using DesignPatternsSudoku.Models.Visitor;
using System.Collections.Generic;

namespace DesignPatternsSudoku.Models.Composite
{
    public interface IComponent
    {
        bool Check();
        List<Cell> GetLeafs();
        void Accept(IComponentVisitor visitor);
    }
}
