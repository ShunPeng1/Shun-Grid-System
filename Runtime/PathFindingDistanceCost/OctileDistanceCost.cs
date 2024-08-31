using UnityEngine;

namespace Shun_Grid_System
{
    public class OctileDistanceCost : IPathFindingDistanceCost
    {
        public double GetDistanceCost(int xDifference, int yDifference)
        {
            return xDifference > yDifference ? 1.4*yDifference+ 1.0*(xDifference-yDifference) : 1.4*xDifference + 1.0*(yDifference-xDifference);
        }
        public double GetDistanceCost(CellIndex2D from, CellIndex2D to)
        {
            return GetDistanceCost(Mathf.Abs(from.X - to.X), Mathf.Abs(from.Y - to.Y));
        }
        public double GetDistanceCost(ICellIndex from, ICellIndex to)
        {
            return from.Accept(this, to);    
        }
    }
}