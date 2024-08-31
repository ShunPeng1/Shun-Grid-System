namespace Shun_Grid_System
{
    public interface IPathFindingAdjacentCellSelection
    {
        bool CheckMovableCell(IGridCell from, IGridCell to, ICellTransition transition = null);
    }
}