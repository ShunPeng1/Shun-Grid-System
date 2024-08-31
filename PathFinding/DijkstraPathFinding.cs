using System;
using System.Collections.Generic;

namespace Shun_Grid_System
{
    public class DijkstraPathFinding : BasePathfinding
    {
        
        private readonly Dictionary<IGridCell, double> _gValues = new();
        private readonly Priority_Queue.SimplePriorityQueue<IGridCell, double> _openCells = new();
        private readonly HashSet<IGridCell> _visitedCells = new();
        

        public DijkstraPathFinding(IPathFindingDistanceCost distanceCostFunction, 
            IPathFindingAdjacentCellSelection adjacentCellSelectionFunction = null) : base(distanceCostFunction, adjacentCellSelectionFunction)
        {
        }

        

        private void ResetPathfinding()
        {
            _gValues.Clear();
            _openCells.Clear();
            _visitedCells.Clear();
        }
        
        public override LinkedList<IGridCell> FirstTimeFindPath(IGridCell startCell, IGridCell endCell,
            double maxCost = Double.PositiveInfinity)
        {
            throw new NotImplementedException();
        }

        public override LinkedList<IGridCell> UpdatePathWithDynamicObstacle(IGridCell currentStartNode,
            List<IGridCell> foundDynamicObstacles,
            double maxCost = Double.PositiveInfinity)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<IGridCell, double> FindAllCellsSmallerThanCost(IGridCell currentStartNode,
            double maxCost = Double.PositiveInfinity)
        {
            throw new NotImplementedException();
        }

        public List<IGridCell> LowestCostCellWithWeightMap(
            IGridCell currentStartCell,
            Dictionary<IGridCell, double> weightCellToCosts, 
            List<IGridCell> obstacles = null)
        {
            ResetPathfinding();
            
            List<IGridCell> lowestCostCells = new List<IGridCell>();
            double lowestWeightCost = GetWeightCost(currentStartCell);

            _gValues[currentStartCell] = 0;
            _openCells.Enqueue(currentStartCell, 0);

            // Dijkstra's algorithm to find the lowest weight cost cell from the start cell
            while (_openCells.Count > 0)
            {
                IGridCell currentCell = _openCells.Dequeue();

                if (_visitedCells.Contains(currentCell)) continue;
                _visitedCells.Add(currentCell);
                
                var gValue = GetDijkstraGValue(currentCell);
                var cellCost = gValue + GetWeightCost(currentCell);
                if (lowestWeightCost > cellCost)
                {
                    lowestWeightCost = cellCost;
                    lowestCostCells.Clear();
                    lowestCostCells.Add(currentCell);
                }
                else if (lowestWeightCost == cellCost)
                {
                    lowestCostCells.Add(currentCell);
                }

                if (gValue > lowestWeightCost) break;

                // Check all adjacent cells
                foreach ((IGridCell neighbor, ICellTransition transition) in currentCell.GetAdjacentCellsWithTransition())
                {
                    if (neighbor == null ||
                        !AdjacentCellSelectionFunction.CheckMovableCell(currentCell, neighbor, transition)
                        || obstacles?.Contains(neighbor) != false) continue;
                    
                    double tentativeCost = GetDijkstraGValue(currentCell) + GetDistanceCost(currentCell, neighbor);

                    if (tentativeCost < GetDijkstraGValue(neighbor))
                    {
                        _gValues[neighbor] = tentativeCost;

                        if (_openCells.Contains(neighbor))
                            _openCells.TryRemove(neighbor);

                        _openCells.Enqueue(neighbor, tentativeCost);

                    }
                }
            }

            return lowestCostCells;


            double GetWeightCost(IGridCell cell)
            {
                return weightCellToCosts.TryGetValue(cell, out double value) ? value : 0;
            }

        }


        double GetDijkstraGValue(IGridCell cell)
        {
            return _gValues.TryGetValue(cell, out double value) ? value : double.PositiveInfinity;
        }

        
    }
}