using System;
using System.Collections.Generic;
using System.Linq;
using Shun_Grid_System;
using UnityEngine;

namespace Shun_Grid_System
{
    [Serializable]
    public class GridCell2D : IGridCell
    {
        public Dictionary<GridCell2D, ICellTransition> AdjacentCells { get; } = new();
        public CellIndex2D Index { get; }
        public ICellItem Item { get; set; }

        public GridCell2D(CellIndex2D cellIndex, ICellItem item = default)
        {
            Index = cellIndex;
            Item = item;
        }

        public void AddAdjacentCell(IGridCell cell, ICellTransition transition = null) 
        {
            if (cell is not GridCell2D baseGridCell2D || AdjacentCells.ContainsKey(baseGridCell2D))
            {
                return;
            }
            AdjacentCells?.TryAdd(baseGridCell2D, transition);

        }

        public void RemoveAdjacentCell(IGridCell cell) 
        {
            if (cell is not GridCell2D baseGridCell2D || !AdjacentCells.ContainsKey(baseGridCell2D))
            {
                return;
            }
            AdjacentCells?.Remove(baseGridCell2D);
        }

        public IGridCell[] GetAdjacentCells() 
        {
            return AdjacentCells.Keys.ToArray() as IGridCell[];
        }
        

        public (IGridCell, ICellTransition)[] GetAdjacentCellsWithTransition() 
        {
            (IGridCell, ICellTransition)[] adjacentCellsWithTransition = new (IGridCell, ICellTransition)[AdjacentCells.Count];

            int i = 0;
            foreach (var (key, value) in AdjacentCells)
            {
                if (key is IGridCell gridCell)
                {
                    adjacentCellsWithTransition[i] = (gridCell, value);
                    i++;
                }
            }
            return adjacentCellsWithTransition;
        }

        public void ClearAdjacentCell()
        {
            AdjacentCells?.Clear();
        }

        public void SetTransition(IGridCell cell, ICellTransition transition) 
        {
            if (cell is not GridCell2D baseGridCell2D || !AdjacentCells.ContainsKey(baseGridCell2D))
            {
                return;
            }
            AdjacentCells[baseGridCell2D] = transition;
        }

        public void RemoveTransition(IGridCell cell) 
        {
            if (cell is not GridCell2D baseGridCell2D || !AdjacentCells.ContainsKey(baseGridCell2D))
            {
                return;
            }
            AdjacentCells.Remove(baseGridCell2D);
        }

        public void SetIndex(ICellIndex index)
        {
            throw new NotImplementedException();
        }

        public ICellIndex GetIndex()
        {
            throw new NotImplementedException();
        }

        public void SetItem(ICellItem item)
        {
            Item = item;
        }

        public void RemoveItem()
        {
            Item = null;
        }


        public override bool Equals(object obj)
        {
            return obj is GridCell2D cell && this == cell;
        }

        public override int GetHashCode()
        {
            return Index.GetHashCode();
        }
    }
}
