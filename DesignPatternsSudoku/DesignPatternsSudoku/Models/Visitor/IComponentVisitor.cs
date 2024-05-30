using DesignPatternsSudoku.Models.Composite;

namespace DesignPatternsSudoku.Models.Visitor
{
    public interface IComponentVisitor
    {
        void VisitCluster(Cluster cluster);
    }
}
