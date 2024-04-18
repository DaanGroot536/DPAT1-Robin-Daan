using DesignPatternsSudoku.Models.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsSudoku.Models.Visitor
{
    public interface IComponentVisitor
    {
        void VisitCluster(Cluster cluster);
    }
}
