using System;
using System.Collections.Generic;

namespace Shun_Grid_System
{
    public abstract class BasePathfinding
    {
        protected readonly IPathFindingDistanceCost DistanceCost;
        protected readonly IPathFindingAdjacentCellSelection AdjacentCellSelectionFunction;
        protected BasePathfinding(IPathFindingDistanceCost distanceCost, IPathFindingAdjacentCellSelection adjacentCellSelectionFunction = null)
        {
            this.DistanceCost = distanceCost;
            this.AdjacentCellSelectionFunction = adjacentCellSelectionFunction ??
                                                 new PathFindingAllAdjacentCellAccept();
        }

        
        public abstract LinkedList<IGridCell> FirstTimeFindPath(IGridCell startCell, IGridCell endCell, double maxCost = Double.PositiveInfinity);

        public abstract LinkedList<IGridCell> UpdatePathWithDynamicObstacle(IGridCell currentStartNode, List<IGridCell> foundDynamicObstacles, double maxCost = Double.PositiveInfinity);
        public abstract Dictionary<IGridCell, double> FindAllCellsSmallerThanCost(IGridCell currentStartNode, double maxCost = Double.PositiveInfinity);
        
        
        
        protected virtual double GetDistanceCost(IGridCell start, IGridCell end)
        {
            return DistanceCost.GetDistanceCost(start.GetIndex(), end.GetIndex());
        }
    }
}