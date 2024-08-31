using System;
using System.Collections.Generic;


namespace Shun_Grid_System
{
    public class AStarPathFinding : BasePathfinding
    {
        private IGridCell _startCell, _endCell;
        private Dictionary<IGridCell, double> _gValues = new (); // gValues[x] = the cost of the cheapest path from the start to x
        private Dictionary<IGridCell, double> _hValues = new (); // hValues[x] = the cost of the cheapest path from x to the end
        private Dictionary<IGridCell, IGridCell> _predecessors = new (); // predecessors[x] = the cell that comes before x on the best path from the start to x
        private Dictionary<IGridCell, float> _dynamicObstacles = new(); // dynamicObstacle[x] = the cell that is found obstacle after find path and its found time
        
        
        public AStarPathFinding(IPathFindingDistanceCost distanceCostFunction, 
            IPathFindingAdjacentCellSelection adjacentCellSelectionFunction = null) : base(distanceCostFunction, adjacentCellSelectionFunction)
        {
        }

        public override LinkedList<IGridCell> FirstTimeFindPath(IGridCell startCell, IGridCell endCell, double maxCost = Double.PositiveInfinity)
        {
            _startCell = startCell;
            _endCell = endCell;
            _gValues = new (); 
            _predecessors = new (); 
            _dynamicObstacles = new();

            _gValues[startCell] = 0;

            return FindPath(maxCost);
        }

        public override LinkedList<IGridCell> UpdatePathWithDynamicObstacle(IGridCell currentStartCell, List<IGridCell> foundDynamicObstacles, double maxCost = Double.PositiveInfinity)
        {
            return FindPath(maxCost);
        }

        public override Dictionary<IGridCell, double> FindAllCellsSmallerThanCost(IGridCell currentStartCell, double maxCost = Double.PositiveInfinity)
        {
            _startCell = currentStartCell;
            _gValues = new (); 
            _predecessors = new (); 
            _dynamicObstacles = new();
            _gValues[_startCell] = 0;
            
            Priority_Queue.SimplePriorityQueue<IGridCell, double> openSet = new Priority_Queue.SimplePriorityQueue<IGridCell, double>();
            HashSet<IGridCell> visitedSet = new HashSet<IGridCell>();

            openSet.Enqueue(currentStartCell, GetFValue(currentStartCell));

            Dictionary<IGridCell, double> reachableCells = new();

            reachableCells[currentStartCell] = 0;
            
            while (openSet.Count > 0)
            {
                IGridCell currentCell = openSet.Dequeue();
                
                if ( GetGValue(currentCell) > maxCost)
                    continue;
                
                visitedSet.Add(currentCell);
                reachableCells[currentCell] = GetGValue(currentCell);
                
                foreach ((IGridCell adjacentCell, ICellTransition transition) in currentCell.GetAdjacentCellsWithTransition())
                {
                    //if (visitedSet.Contains(adjacentCell))
                    //    continue;

                    if (!AdjacentCellSelectionFunction.CheckMovableCell(currentCell, adjacentCell, transition))
                        continue;

                    double newGCost = GetGValue(currentCell) + GetDistanceCost(currentCell, adjacentCell);

                    if (newGCost < GetGValue(adjacentCell) || 
                        (!openSet.Contains(adjacentCell) && !visitedSet.Contains(adjacentCell)))
                    {
                        _gValues[adjacentCell] = newGCost;
                        _hValues[adjacentCell] = 0;
                        _predecessors[adjacentCell] = currentCell;
                        
                        
                        if (!openSet.Contains(adjacentCell))
                        {
                            openSet.Enqueue(adjacentCell, newGCost);
                        }
                    }
                }
            }

            return reachableCells;
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns> the path between start and end</returns>
        private LinkedList<IGridCell> FindPath(double maxCost = Double.PositiveInfinity)
        {
            Priority_Queue.SimplePriorityQueue<IGridCell, double> openSet = new (); // to be travelled set
            HashSet<IGridCell> visitedSet = new(); // travelled set 
            
            openSet.Enqueue(_startCell, GetFValue(_startCell));
        
            while (openSet.Count > 0)
            {
                IGridCell currentMinFCostCell = openSet.Dequeue();
                
                if ( GetGValue(currentMinFCostCell) > maxCost)
                    continue;
                
                visitedSet.Add(currentMinFCostCell);
                
                if (currentMinFCostCell.Equals(_endCell))
                {
                    return RetracePath(_startCell, _endCell);
                }

                foreach ((IGridCell adjacentCell, ICellTransition transition) in currentMinFCostCell.GetAdjacentCellsWithTransition())
                {
                    //if (visitedSet.Contains(adjacentCell)) 
                    //    continue;  // skip for travelled cell
                    
                    if (!AdjacentCellSelectionFunction.CheckMovableCell(currentMinFCostCell, adjacentCell, transition)) 
                        continue;
                    
                    
                    double newGCostToNeighbour = GetGValue(currentMinFCostCell) + GetDistanceCost(currentMinFCostCell, adjacentCell);
                    
                    if (newGCostToNeighbour < GetGValue(adjacentCell) || 
                        (!openSet.Contains(adjacentCell) && !visitedSet.Contains(adjacentCell)))
                    {
                        double hCost = GetDistanceCost(adjacentCell, _endCell);
                        double fCost = newGCostToNeighbour + hCost;
                        
                        _gValues[adjacentCell] = newGCostToNeighbour;
                        _hValues[adjacentCell] = hCost;
                        _predecessors[adjacentCell] = currentMinFCostCell;
                        
                        

                        if (!openSet.Contains(adjacentCell)) // Not in open set
                        {
                            openSet.Enqueue(adjacentCell, fCost);
                        }
                    }

                }
            }
            //Not found a path to the end
            return null;
        }

        /// <summary>
        /// Get a list of Cell that the pathfinding was found
        /// </summary>
        protected LinkedList<IGridCell> RetracePath(IGridCell start, IGridCell end)
        {
            LinkedList<IGridCell> path = new();
            IGridCell currentCell = end;
            while (!currentCell.Equals(start)) 
            {
                //Debug.Log("Path "+ currentCell.xIndex +" "+ currentCell.zIndex );
                path.AddFirst(currentCell);
                currentCell = _predecessors[currentCell];
            }
            path.AddFirst(start);
            return path;
        }
        
        private double GetFValue(IGridCell cell)
        {
            return GetGValue(cell) + GetHValue(cell);
        }

        private double GetHValue(IGridCell cell)
        {
            return _hValues.TryGetValue(cell, out double value) ? value : 0;
        }
        
        private double GetGValue(IGridCell cell)
        {
            return _gValues.TryGetValue(cell, out double value) ? value : 0;
        }
    
    }
}

