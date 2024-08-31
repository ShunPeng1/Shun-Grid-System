namespace Shun_Grid_System
{
    
    public enum PathFindingCostFunction
    {
        Manhattan,
        Euclidean, // Note that when grid is too large, it degrades into Greedy Best-First-Search
        Octile,
        Chebyshev
    }
}