using System;
using System.Collections.Generic;

namespace Shun_Grid_System
{
    public interface IPathfindingAlgorithm
    {
        public LinkedList<IGridCell> FirstTimeFindPath(IGridCell startCell, IGridCell endCell, double maxCost = Double.PositiveInfinity);
        public LinkedList<IGridCell> UpdatePathWithDynamicObstacle(IGridCell currentStartNode, List<IGridCell> foundDynamicObstacles, double maxCost = Double.PositiveInfinity);
        public Dictionary<IGridCell, double> FindAllCellsSmallerThanCost(IGridCell currentStartNode, double maxCost = Double.PositiveInfinity);
    }
}