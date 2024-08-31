using UnityEngine;

namespace Shun_Grid_System
{
    public class PathFindingAllAdjacentCellAccept : IPathFindingAdjacentCellSelection
    {
        public bool CheckMovableCell(IGridCell from, IGridCell to, ICellTransition transition = null)
        {
            return true;
        }
    }
}