﻿using DesignPatternsSudoku.Models.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesignPatternsSudoku.Models.Puzzles;

namespace DesignPatternsSudoku.Models.Visitor
{
    public class ComponentVisitor : IComponentVisitor
    {
        public void VisitCluster(Cluster cluster)
        {
            if (cluster is Puzzle)
            {
                List<IComponent> clusters = cluster.Children;

                List<Cell> cells = cluster.GetLeafs();
                cells.ForEach(c => c.IsValid = true);

                foreach (IComponent component in clusters)
                {
                    component.Accept(this);
                }
            }
            else
            {
                List<Cell> cells = cluster.GetLeafs();

                HashSet<int> uniqueValues = new HashSet<int>();

                foreach (Cell cell in cells)
                {
                    if (uniqueValues.Contains(cell.EnteredValue))
                    {
                        if(!cell.IsGiven) cell.IsValid = false;

                        Cell duplicateCell = cells.Find(c => c.EnteredValue == cell.EnteredValue && c != cell);

                        if (duplicateCell != null && !duplicateCell.IsGiven) duplicateCell.IsValid = false;
                    }
                    else
                    {
                        if (cell.EnteredValue != 0)
                        {
                            uniqueValues.Add(cell.EnteredValue);
                        }
                    }
                }
            }
        }
    }
}
