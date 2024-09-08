using Codice.CM.Common.Tree;

namespace Shun_Grid_System
{
    public interface ICellItem
    {
        ICellItem Clone();
        void SetCell(IGridCell cell);
        IGridCell GetCell();
    }
}