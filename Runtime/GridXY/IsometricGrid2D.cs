using UnityEngine;

namespace Shun_Grid_System
{
    /// <summary>
    ///   /\    /\    /\  
    ///  /  \  /  \  /  \ 
    /// /    \/    \/    \
    /// \ 1,2/\ 2,1/\ 3,0/
    ///  \  /  \  /  \  / 
    ///   \/    \/    \/  
    ///   /\ 1,1/\ 2,0/\  
    ///  /  \  /  \  /  \ 
    /// /    \/    \/    \
    /// \ 0,1/\ 1,0/\2,-1/^
    ///  \  /  \  /  \  / |
    ///   \/ 0,0\/1,-1\/  |
    ///   /\    /\    /   y
    ///  /  \  /  \  /    |
    /// /    \/    \/     V
    ///    <-x-->
    /// </summary>
    public class IsometricGrid2D : SquareGrid2D
    {
        
        public IsometricGrid2D(ICellItem defaultItem = default, float cellWidthSize = 1f, float cellHeightSize = 1f,
            Vector3 worldOriginPosition = new Vector3(), int initWidth = 100, int initHeight = 100, bool isFixedSize = false) : base(defaultItem, cellWidthSize, cellHeightSize, worldOriginPosition, initWidth, initHeight, isFixedSize)
        {
        }
        
        public override CellIndex2D GetIndex(Vector3 worldPosition)
        {
            // Convert world position to isometric coordinates
            float squareX = (worldPosition - WorldOriginPosition).x / CellWidthSize;
            float squareY = (worldPosition - WorldOriginPosition).y / CellHeightSize;
            
            // Convert isometric coordinates to grid coordinates
            int isoX = Mathf.RoundToInt(squareX + squareY);
            int isoY = Mathf.RoundToInt((squareY - squareX));
            
            
            return new CellIndex2D(isoX,isoY);

        }

        public override Vector3 GetCenterWorldPositionOfNearestCell(CellIndex2D cellIndex)
        {
            // Convert grid coordinates to isometric coordinates
            float x = ((float)cellIndex.X - cellIndex.Y) * 0.5f;
            float y = ((float)cellIndex.X + cellIndex.Y) * 0.5f;
            
            return new Vector3(x * CellWidthSize, y * CellHeightSize, 0) + WorldOriginPosition;
         
        }

        public override void OnDrawGizmos()
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
                    
                    
                    Gizmos.DrawLineStrip(new Vector3[]
                    {
                        cellPosition + new Vector3(-CellWidthSize * 0.5f, 0 , 0),
                        cellPosition + new Vector3(0, -CellHeightSize * 0.5f, 0),
                        cellPosition + new Vector3(CellWidthSize * 0.5f, 0, 0),
                        cellPosition + new Vector3(0, CellHeightSize * 0.5f, 0),
                    }, true);

                }
            }
            
        }
    }
}