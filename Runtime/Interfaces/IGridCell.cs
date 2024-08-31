using System.Collections.Generic;

namespace Shun_Grid_System
{
    public interface IGridCell
    {
        void AddAdjacentCell(IGridCell cell, ICellTransition transition);
        void RemoveAdjacentCell(IGridCell cell);
        IGridCell[] GetAdjacentCells();
        (IGridCell, ICellTransition)[] GetAdjacentCellsWithTransition();
        void ClearAdjacentCell();
        void SetTransition(IGridCell cell, ICellTransition transition);
        void RemoveTransition(IGridCell cell);
        
        void SetIndex(ICellIndex index);
        ICellIndex GetIndex();
        
        void SetItem(ICellItem item);
        void RemoveItem();
        
        
        
        
    }
}