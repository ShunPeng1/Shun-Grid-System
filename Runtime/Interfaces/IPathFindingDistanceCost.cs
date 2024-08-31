namespace Shun_Grid_System
{
    public interface IPathFindingDistanceCost
    {
        double GetDistanceCost(ICellIndex from, ICellIndex to);
        double GetDistanceCost(int xDifference, int yDifference);
        double GetDistanceCost(CellIndex2D from, CellIndex2D to);
    }
}