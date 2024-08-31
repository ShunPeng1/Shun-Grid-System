using UnityEngine;

namespace Shun_Grid_System
{
    public class ManhattanDistanceCost : IPathFindingDistanceCost
    {
        public double GetDistanceCost(int xDifference, int yDifference)
        {
            return xDifference + yDifference;
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