using DesignPatternsSudoku.Models.Visitor;
using System.Collections.Generic;

namespace DesignPatternsSudoku.Models.Composite
{
    public class Cluster : IComponent
    {
        public List<IComponent> Children { get; set; }

        public Cluster()
        {
            Children = new List<IComponent>();
        }

        public bool Check()
        {
            foreach (IComponent child in Children)
            {
                if (!child.Check())
                {
                    return false;
                }
            }
            return true;
        }

        public void Add(IComponent component)
        {
            Children.Add(component);
        }

        public void Accept(IVisitor visitor)
        {
            visitor.VisitCluster(this);
        }

        public List<Cell> GetLeafs()
        {
            var leafs = new List<Cell>();
            foreach (IComponent item in Children)
            {
                leafs.AddRange(item.GetLeafs());
            }
            return leafs;
        }
    }
}
