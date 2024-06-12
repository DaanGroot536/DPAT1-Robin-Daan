using DesignPatternsSudoku.Models.Composite;

namespace DesignPatternsSudoku.Models.Visitor
{
    public interface IVisitor
    {
        void VisitCluster(Cluster cluster);
    }
}
