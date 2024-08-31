using System.Collections.Generic;
using UnityEngine;

namespace Shun_Grid_System
{
    public class SquareGrid2D : IGrid
    {
        public readonly int Width, Height;
        public readonly float CellWidthSize, CellHeightSize;
        public bool IsFixedSize;
        protected Vector3 WorldOriginPosition;
        protected readonly Dictionary<CellIndex2D, GridCell2D> GridCells;

        public SquareGrid2D(float cellWidthSize = 1f, float cellHeightSize = 1f,
            Vector3 worldOriginPosition = new Vector3(), int initWidth = 100, int initHeight = 100, bool isFixedSize = false)
        {
            IsFixedSize = isFixedSize;
            
            if (IsFixedSize)
            {
                Width = initWidth;
                Height = initHeight;
            }
            else
            {
                Width = int.MaxValue;
                Height = int.MaxValue;
            }
            
            CellHeightSize = cellHeightSize;
            CellWidthSize = cellWidthSize;
            WorldOriginPosition = worldOriginPosition;
            GridCells = new Dictionary<CellIndex2D, GridCell2D>();

            for (int x = 0; x < initWidth; x++)
            {
                for (int y = 0; y < initHeight; y++)
                {
                    GridCells[new CellIndex2D(x, y)] = default;
                }
            }
        }

        public virtual CellIndex2D GetIndex(Vector3 worldPosition)
        {
            int x = Mathf.RoundToInt((worldPosition - WorldOriginPosition).x / CellWidthSize);
            int y = Mathf.RoundToInt((worldPosition - WorldOriginPosition).y / CellHeightSize);
            return new CellIndex2D(x,y);
        }
        
        public virtual Vector3 GetCenterWorldPositionOfNearestCell(CellIndex2D cellIndex)
        {
            return new Vector3(cellIndex.X * CellWidthSize, cellIndex.Y * CellHeightSize, 0) + WorldOriginPosition;
        }
        
        public Vector3 GetCenterWorldPositionOfNearestCell(Vector3 worldPosition)
        {
            var index = GetIndex(worldPosition);
            return GetCenterWorldPositionOfNearestCell(index);
        }

        public Vector3 GetCenterWorldPositionOfNearestCell(GridCell2D cell)
        {
            return GetCenterWorldPositionOfNearestCell(cell.Index);
        }

        public bool CheckValidCell(Vector3 worldPosition)
        {
            var index = GetIndex(worldPosition);
            return CheckValidCell(index);
        }

        public bool CheckValidCell(CellIndex2D cellIndex)
        {
            if (GridCells.ContainsKey(cellIndex))
            {
                return true;
            }
            
            if (IsFixedSize)
            {
                return false;
            }
            
            GridCells[cellIndex] = default(GridCell2D);
            return true;
        }
        
        public void SetCell(GridCell2D cell, CellIndex2D cellIndex)
        {
            if (CheckValidCell(cellIndex))
            {
                GridCells[cellIndex] = cell;
            }
        }

        public void SetCell(GridCell2D cell, Vector3 worldPosition)
        {
            var index = GetIndex(worldPosition);
            if (CheckValidCell(index))
            {
                GridCells[index] = cell;
            }
            
        }

        public GridCell2D GetCell(CellIndex2D cellIndex)
        {
            return CheckValidCell(cellIndex) ? GridCells[cellIndex] : default;
        }
        

        public GridCell2D GetCell(Vector3 worldPosition)
        {
            var index = GetIndex(worldPosition);
            return GetCell(index);
        }


        public Vector2Int GetIndexDifferenceFrom(GridCell2D subtrahendCell2D, GridCell2D minuendCell2D)
        {
            return new(minuendCell2D.Index.X - subtrahendCell2D.Index.X, minuendCell2D.Index.Y - subtrahendCell2D.Index.Y);
        }
        

        public Vector2Int GetMaxIndex()
        {
            return new(Width, Height);
        }

        public Vector2 GetCellWorldSize()
        {
            return new(CellWidthSize, CellHeightSize);
        }
        
        public virtual void OnDrawGizmos()
        {
            // Define the range around the camera within which to draw the grid
            int drawRange = 100;

            // Get the grid index of the camera's position
            var cameraIndex = GetIndex(Camera.main.transform.position);

            // Calculate the minimum and maximum indices to draw
            int minX = cameraIndex.X - drawRange;
            int maxX = cameraIndex.X + drawRange;
            int minY = cameraIndex.Y - drawRange;
            int maxY = cameraIndex.Y + drawRange;
            if (IsFixedSize)
            {
                minX = Mathf.Max(0, cameraIndex.X - drawRange);
                maxX = Mathf.Min(Width, cameraIndex.X + drawRange);
                minY = Mathf.Max(0, cameraIndex.Y - drawRange);
                maxY = Mathf.Min(Height, cameraIndex.Y + drawRange);
            }
            
            // Draw only the cells within the defined range
            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    var index = new CellIndex2D(x, y);
                    Vector3 cellPosition = GetCenterWorldPositionOfNearestCell(index);
                    Gizmos.DrawWireCube(cellPosition, new Vector3(CellWidthSize, CellHeightSize, 0));
                }
            }
        }
    }
}
