using System;
using System.Collections.Generic;

namespace Shun_Grid_System
{
    public interface ICellIndex
    {
        public double Accept(IPathFindingDistanceCost pathFindingDistanceCost, ICellIndex toCellIndex);
    }
}